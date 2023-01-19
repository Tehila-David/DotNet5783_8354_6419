using System;
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
        //Func<bool> report;
        static volatile bool Active;

        public delegate void Report();//
        public static void Activate()
        {

            new Thread(() =>
            {
                Active = true;
                while (Active)
                {
                    int OrderID = bl.Order.
                }






            }).Start();

            public static void Register()//for Event registration לרישום אירוע
            {
                //report += observer

            }
            public static void UnRegister()//Event cancellation לביטול אירוע
            {
                //report += observer

            }

        }

    }
}