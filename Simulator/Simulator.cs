using System;
using BO;
using System.Threading;
using System.Linq;
using static System.Math;
using BlApi;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics;

namespace Simulator;

public static class Simulator
{
    //static readonly BlApi.IBl bl = BlApi.Factory.Get()!;

    //private static readonly Random random = new Random();
    ////Func<bool> report;
    //static volatile bool Active;
    //Func<string> Report(Status curStatus, oldTime, newStatus, newTime);
    //public static void Register(observer, Func<string> report)//for Event registration לרישום אירוע
    //{
    //    report += observer;
    //}
    //public static void UnRegister(observer, Func<string> report)//Event cancellation לביטול אירוע
    //{
    //    report += observer;
    //}


    //public event EventHandler<AccountEventArgs>? AccountClosed;
   // void accountClosedHandler(int result) => AccountClosed?.Invoke(this, new AccountEventArgs(result));

   // public delegate void report();
    private delegate void endSimulation();
    private delegate void update();
    //report rep;
    //endSimulation endSim;
    //update upd;
    //public void ToReport(report rep)
    //{
    //    this.rep += rep;
    //}

    // איזה משתנים יש ??
    public static void Activate()
    {

        new Thread(() =>
        {
        //    Active = true;
        //    while (!Active)
        //    {
        //        int OrderID = bl.Order.OrderForSimulator();
        //        if (OrderID != 0)// not null
        //        {
                    
        //            //BO.Order order = bl.Order.GetByID(OrderID);
        //            //int delay = random.Next(3,11);
        //            //DateTime time = DateTime.Now + new TimeSpan(delay * 1000);
        //            //report(order.Status, DateTime.Now, time);
        //            //Thread.Sleep(delay*1000);
        //            ////report(); צריך לדווח שהסתיים! 

        //            //if (order.ShipDate == null)
        //            //{
        //            //    bl.Order.UpdateShipDate(OrderID);
        //            //}
        //            //else
        //            //    bl.Order.UpdateDelivery(OrderID);

        //        }
        //        else //there are no such orders
        //        {
        //            Thread.Sleep(1000);
        //        }
        //        Thread.Sleep(1000); //stop one second before each iteration
        //    }
        //    //report(finished);
        }).Start();
    }

    public static void stopSimulator()
    {
        //if(Active)
        //{
        //    Simulator.Stop();
        //    Active = false;
        //}
    }

}