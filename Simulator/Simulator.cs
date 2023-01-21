﻿using System;
using BO;
using System.Threading;
using System.Linq;
using static System.Math;
using BlApi;
using System.Security.Cryptography.X509Certificates;

namespace Simulator
{
    public static class Simulator
    {
        static readonly BlApi.IBl bl = BlApi.Factory.Get()!;

        private static readonly Random random = new Random();
        //Func<bool> report;
        static volatile bool Active;

        public delegate void report();// איזה משתנים יש ??
        public static void Activate()
        {

            new Thread(() =>
            {
                Active = true;
                while (Active)
                {
                    int OrderID = bl.Order.OrderForSimulator();
                    if (OrderID != 0)// not null
                    {
                        
                        BO.Order order = bl.Order.GetByID(OrderID);
                        int delay = random.Next(3,11);
                        DateTime time = DateTime.Now + new TimeSpan(delay * 1000);
                        //report() לא מובן איזה משתנים צריך להכניס??
                        Thread.Sleep(delay*1000);
                        //report(); צריך לדווח שהסתיים! 

                        if (order.ShipDate == null)
                        {
                            bl.Order.UpdateShipDate(OrderID);
                        }
                        else
                            bl.Order.UpdateDelivery(OrderID);

                    }
                }
                //report(finished)
            }).Start();

            //public static void Register()//for Event registration לרישום אירוע
            //{
            //    //report += observer

            //}
            //public static void UnRegister()//Event cancellation לביטול אירוע
            //{
            //    //report += observer

            //}

        }

    }
}