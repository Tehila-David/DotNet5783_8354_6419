using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PL.Cart;
using Simulation;
using static System.Net.Mime.MediaTypeNames;

namespace PL;

/// <summary>
/// Interaction logic for Simulator.xaml
/// </summary>
/// 
public partial class SimulatorWindow : Window
{
    static readonly BlApi.IBl bl = BlApi.Factory.Get()!;
    BackgroundWorker worker;
    Stopwatch stopwatch;
    bool isTimerRun;



    public SimulatorWindow()
    {
        InitializeComponent();
        worker = new BackgroundWorker();
        stopwatch = new Stopwatch();
        worker.DoWork += Worker_DoWork!;
        worker.ProgressChanged += Worker_ProgressChanged;
        worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
        worker.WorkerReportsProgress = true;
        worker.WorkerSupportsCancellation = true;
        worker.RunWorkerAsync();
        stopwatch.Restart();
    }



    public static readonly DependencyProperty IdDependency =
                DependencyProperty.Register(nameof(ID), typeof(int), typeof(SimulatorWindow));
    public int ID
    { get => (int)GetValue(IdDependency);
        private set => SetValue(IdDependency, value); }



    public static readonly DependencyProperty curStatusDependency =
                DependencyProperty.Register(nameof(curStatus), typeof(BO.OrderStatus), typeof(SimulatorWindow));
    public BO.OrderStatus curStatus
    {
        get => (BO.OrderStatus)GetValue(curStatusDependency);
        private set => SetValue(curStatusDependency, value);
    }


    public static readonly DependencyProperty nextStatusDependency =
                DependencyProperty.Register(nameof(finalStatus), typeof(BO.OrderStatus), typeof(SimulatorWindow));
    public BO.OrderStatus finalStatus
    {
        get => (BO.OrderStatus)GetValue(nextStatusDependency);
        private set => SetValue(nextStatusDependency, value);
    }





    public static readonly DependencyProperty oldTimeDependency =
                DependencyProperty.Register(nameof(oldTime), typeof(string), typeof(SimulatorWindow));
    public string oldTime
    {
        get => (string)GetValue(oldTimeDependency);
        private set => SetValue(oldTimeDependency, value);
    }



    public static readonly DependencyProperty newTimeDependency =
                DependencyProperty.Register(nameof(newTime), typeof(string), typeof(SimulatorWindow));
    public string newTime
    {
        get => (string)GetValue(newTimeDependency);
        private set => SetValue(newTimeDependency, value);
    }





    private void OrderUpdated(int ID, BO.OrderStatus curStatus, DateTime now, BO.OrderStatus nextStatus, DateTime future)
    {
        ArrayList arrayList = new ArrayList();
        arrayList.Add(ID);
        arrayList.Add(curStatus);
        arrayList.Add(now);
        arrayList.Add(nextStatus);
        arrayList.Add(future);
        worker.ReportProgress(ID, arrayList);
    }

    private void OrderCompleted()
    {
        worker.ReportProgress(1);
    }


    private void Worker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
    {
        if (worker.CancellationPending == false)
        {
            Simulator.UnRegisterEndSimulator(OrderCompleted);
            Simulator.UnRegisterReport(OrderUpdated);
            stopwatch.Stop();
            isTimerRun = false;
            Close();
        }
    }

    private void Worker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
    {

        //    //int progress = e.ProgressPercentage;
        //    //resultLabel.Content = (progress + "%");
        //    //resultProgressBar.Value = progress;4
        //   if (e.ProgressPercentage == 3)
        //    {
        //        string timerText = stopwatch.Elapsed.ToString();
        //        timerText = timerText.Substring(0, 8);
        //        this.txtClock.Text = timerText;
        //    }
        //    else if (e.ProgressPercentage >= 100000)
        //    {
        //        ArrayList arrayList = (ArrayList)e.UserState!;
        //        ID = e.ProgressPercentage;
        //        curStatus = (BO.OrderStatus)arrayList[1];
        //        oldTime = ((DateTime)arrayList[2]!).ToString();
        //        finalStatus = (BO.OrderStatus)arrayList[3];
        //        newTime = ((DateTime)arrayList[4]!).ToString();
        //    }
        //    else
        //    {
        //        Completed = true;
        //        if (finished)
        //        {
        //            worker.CancelAsync();

        //        }
        //    }
        //}
    }




        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Simulator.RegisterReport(OrderUpdated);
            Simulator.RegisterEndSimulator(OrderCompleted);
            Simulator.Activate(); //starting the simulator

            while (worker.CancellationPending == false)
            {
                Thread.Sleep(1000);
                worker.ReportProgress(3);
            }
        }


        private void SimulatorStop_Click(object sender, EventArgs e)
        {
            Simulator.stopSimulator();
            worker.CancelAsync();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            MessageBox.Show("You can't close this window!");
        }
    
}
