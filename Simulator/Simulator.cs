using System;
using BO;
using System.Threading;
using System.Linq;
using static System.Math;
using BlApi;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics;
using System.Timers;
//using DO;

namespace Simulation;

public static class Simulator
{
    static readonly BlApi.IBl bl = BlApi.Factory.Get()!;
    private static readonly Random random = new Random();
    static volatile bool Active; // comdition that reprsents that the simulator is active

  
    //An event delegate property for stopping the simulator
    public delegate void SimulatorDone(); 
    public static event SimulatorDone? EndSimulator;
    //An event delegate property for updating the progress of the simulation
    public delegate void Report(int ID, OrderStatus oldStatus, DateTime oldTime, OrderStatus newStatus, DateTime newTime);
    public static event Report?  ReportMyProgress;

    public static void RegisterEndSimulator(SimulatorDone handler) //for event subscription
    {
        EndSimulator += handler; // attaching the event handler to the event
    }
    public static void UnRegisterEndSimulator(SimulatorDone handler)//for event unsubscription
    {
        EndSimulator -= handler;
    }


    public static void RegisterReport(Report handler)//for event subscription
    {
        ReportMyProgress += handler;
    }
    public static void UnRegisterReport(Report handler) //for event unsubscription
    {
        ReportMyProgress -= handler;
    }

    public static void Activate() //function that starts the simulator
    {

        new Thread(() => //creating a thread that performs the simulation
        {
            Active = true;
            while (Active)
            {
                int OrderID = bl.Order.OrderForSimulator(); //receiving the next order to be taken care of
                DateTime estimatedTime = DateTime.Now;
                OrderStatus defaultStatus = OrderStatus.Default;
                if (OrderID != 0)// there is an order to be taken care of
                {

                    BO.Order order = (bl.Order.GetByID(OrderID))?? throw new Exception();
                    int delay = random.Next(3, 11);
                    estimatedTime = DateTime.Now + new TimeSpan(delay * 1000);
                    OrderStatus? oldStatus = order.Status;
                    //Reporting to the PL that an order was received bi initiating the "ReportMyProgress" event
                    ReportMyProgress?.Invoke(order.ID, oldStatus ?? throw new Exception("null"), DateTime.Now,
                    order.Status == OrderStatus.Default ? OrderStatus.shipped : OrderStatus.Deliverded, estimatedTime);
                    Thread.Sleep(delay * 1000);
                    //updating the order status
                    if (order.Status == OrderStatus.Confirmed)
                    {
                        bl.Order.UpdateShipDate(OrderID);
                    }
                    else if (order.Status == OrderStatus.shipped)
                    {
                        bl.Order.UpdateDelivery(OrderID);
                    }
                }
                else //there are no such orders
                {
                    ReportMyProgress?.Invoke(0, defaultStatus, estimatedTime, defaultStatus, estimatedTime);
                //    if(ReportMyProgress != null)
                //    {
                //        ReportMyProgress(0, defaultStatus, estimatedTime, defaultStatus, estimatedTime);
                //    }
                }
                Thread.Sleep(1000); //stop one second before each iteration
            }
            EndSimulator?.Invoke(); //stopping the simulator
        }).Start();
    }

    public static void stopSimulator() //function that stops the simulation
    {
        if (Active)
        {
            Active = false;
           
        }
    }

}