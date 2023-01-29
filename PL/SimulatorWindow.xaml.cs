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
    Stopwatch stopwatch; //to represent the timer of the simulator

    bool isTimerRun = true;
    bool isCompleted = false;
    bool isFinished = false;



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
        worker.RunWorkerAsync(); //starting the background worker
        stopwatch.Restart();
    }


    //Dependecy property for the order ID of the order being taken care of currently
    public static readonly DependencyProperty IdDependency =
                DependencyProperty.Register(nameof(ID), typeof(int), typeof(SimulatorWindow));
    public int ID
    {
        get => (int)GetValue(IdDependency);
        private set => SetValue(IdDependency, value);
    }


    //Dependecy property for the current status of the order being taken care of currently
    public static readonly DependencyProperty curStatusDependency =
                DependencyProperty.Register(nameof(curStatus), typeof(BO.OrderStatus), typeof(SimulatorWindow));
    public BO.OrderStatus curStatus
    {
        get => (BO.OrderStatus)GetValue(curStatusDependency);
        private set => SetValue(curStatusDependency, value);
    }

    //Dependecy property for the next status of the order being taken care of currently
    public static readonly DependencyProperty nextStatusDependency =
                DependencyProperty.Register(nameof(finalStatus), typeof(BO.OrderStatus), typeof(SimulatorWindow));
    public BO.OrderStatus finalStatus
    {
        get => (BO.OrderStatus)GetValue(nextStatusDependency);
        private set => SetValue(nextStatusDependency, value);
    }


    //Dependecy property for the start time of the current order simulation
    public static readonly DependencyProperty oldTimeDependency =
                DependencyProperty.Register(nameof(oldTime), typeof(string), typeof(SimulatorWindow));
    public string oldTime
    {
        get => (string)GetValue(oldTimeDependency);
        private set => SetValue(oldTimeDependency, value);
    }


    //Dependecy property for the end time of the current order simulation
    public static readonly DependencyProperty newTimeDependency =
                DependencyProperty.Register(nameof(newTime), typeof(string), typeof(SimulatorWindow));
    public string newTime
    {
        get => (string)GetValue(newTimeDependency);
        private set => SetValue(newTimeDependency, value);
    }




    // "OrderUpdated" and "OrderCompleted" are observer functions for the simulator events
    //An observer for updating the simulation
    private void ReportMyProgress(int ID, BO.OrderStatus curStatus, DateTime now, BO.OrderStatus nextStatus, DateTime future)
    {
        ArrayList myList = new ArrayList();
        myList.Add(ID);
        myList.Add(curStatus);
        myList.Add(now);
        myList.Add(nextStatus);
        myList.Add(future);
        worker.ReportProgress(0, myList); //Passing the correct information to be displayed through the "percentProgress" and "userState" properties
    }
    //An observaer for the end of the simulation
    private void EndOfSimulator()
    {
        worker.ReportProgress(1); //Activating the "Worker_ProgressChanged" to stop the simulator
    }


    private void Worker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
    {
        if (!worker.CancellationPending)
        {
            //Unsubscribing the observer methods to the simulator events
            Simulator.UnRegisterEndSimulator(EndOfSimulator);
            Simulator.UnRegisterReport(ReportMyProgress);
            stopwatch.Stop(); //Stopping the stopwatch
            isTimerRun = false;
            this.Close(); //Closing the window                                                                                                                                                                                                                                                 
        }
    }

    private void Worker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        //ProgressPercentage - type of update, updating order or ending simulator
        //userState = Id, status, time of order, for updating order
    {
  
        if (e.ProgressPercentage == 2) //DoWork wants to continue the simulator
        {
            string timerText = stopwatch.Elapsed.ToString();
            timerText = timerText.Substring(0, 8);
            this.txtClock.Text = timerText;
            //ID = 0;
        }
        else if (e.ProgressPercentage == 0) //Updating the status of an event
        {
            if (isCompleted)
            {
                isCompleted = false;
            }
            ArrayList myList = (ArrayList)e.UserState!;
            ID = (int)myList[0]!;
            curStatus = (BO.OrderStatus)myList[1]!;
            oldTime = ((DateTime)myList[2]!).ToString();
            finalStatus = (BO.OrderStatus)myList[3]!;
            newTime = ((DateTime)myList[4]!).ToString();
        }
        else if(e.ProgressPercentage == 1) //  a request to end the simulator is received
        {
            isCompleted = true;
            isTimerRun = false;
            if (isFinished == true)
            {
                worker.CancelAsync();
            }
        }
    }

    private void Worker_DoWork(object sender, DoWorkEventArgs e)
    {
        //subscribing the oberver methods to the simulator events
        Simulator.RegisterReport(ReportMyProgress);
        Simulator.RegisterEndSimulator(EndOfSimulator);
        Simulator.Activate(); //starting the simulator

        while (!worker.CancellationPending) //While there is no request to stop the simulation
        {
            worker.ReportProgress(2); //Reporting the correct stopwatch progress - by
                                      //Calling the "Worker_ProgressChanged" method
            Thread.Sleep(1000); //suspending the threשd for one second

        }
    }


    private void End_of_simulation_Click(object sender, RoutedEventArgs e) //button to end the simulation
    {
        Simulator.stopSimulator();
         if (worker.WorkerSupportsCancellation == true)
         {
             worker.CancelAsync();
         }
        else
        {
            isFinished = true;
        }
    }

    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        //this function diables the closing of the simulator window by pressing on the "x" sign
    {
        e.Cancel = isTimerRun;
        if (isTimerRun == true) //if the stopwatch is still running
            MessageBox.Show("This window does not close by pressing the 'X' button.");
    }

}
