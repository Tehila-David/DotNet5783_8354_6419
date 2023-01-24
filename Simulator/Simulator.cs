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
    static volatile bool Active;

    //public static event Action Stop;
    //public static event Func<OrderStatus, DateTime, OrderStatus, DateTime, bool> report;
    //public static void RegisterEndSimulator(Func<OrderStatus, DateTime, OrderStatus, DateTime, bool> observer)//for Event registration לרישום אירוע

    public delegate void SimulatorDone();
    public static event SimulatorDone EndSimulator;

    public delegate void Report(int ID, OrderStatus oldStatus, DateTime oldTime, OrderStatus newStatus, DateTime newTime);
    public static event Report ReportMyProgress;

    public static void RegisterEndSimulator(SimulatorDone handler)//for Event registration לרישום אירוע
    {
        EndSimulator += handler;
    }
    public static void UnRegisterEndSimulator(SimulatorDone handler)
    {
        EndSimulator -= handler;
    }


    public static void RegisterReport(Report handler)//for Event registration לרישום אירוע
    {
        ReportMyProgress += handler;
    }
    public static void UnRegisterReport(Report handler)
    {
        ReportMyProgress -= handler;
    }

    public static void Activate()
    {

        new Thread(() =>
        {
            Active = true;
            while (Active)
            {
                int OrderID = bl.Order.OrderForSimulator();
                DateTime estimatedTime = DateTime.Now;
                OrderStatus defaultStatus = OrderStatus.Default;
                if (OrderID != 0)// not null
                {

                    BO.Order order = (bl.Order.GetByID(OrderID))?? throw new Exception();
                    int delay = random.Next(3, 11);
                    estimatedTime = DateTime.Now + new TimeSpan(delay * 10000000);
                    //estimatedTime = DateTime.Now + new TimeSpan(0, 0, delay);
                    OrderStatus? oldStatus = order.Status;
                    ReportMyProgress?.Invoke(order.ID, oldStatus ?? throw new Exception("null"), DateTime.Now,
                    order.Status == OrderStatus.Default ? OrderStatus.shipped : OrderStatus.Deliverded, estimatedTime);
                    Thread.Sleep(delay * 1000);

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
            EndSimulator?.Invoke();
        }).Start();
    }

    public static void stopSimulator()
    {
        if (Active)
        {
            Active = false;
           
        }
    }

}