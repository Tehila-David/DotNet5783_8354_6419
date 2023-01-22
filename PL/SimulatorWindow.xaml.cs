using Simulator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
namespace PL
{
    /// <summary>
    /// Interaction logic for Simulator.xaml
    /// </summary>
    /// 
    public partial class SimulatorWindow : Window
    {
        BlApi.IBl bl = BlApi.Factory.Get()!;
        private Simulator.Simulator.report();
        private bool isTimerRun;
        BackgroundWorker worker;

        public SimulatorWindow()
        {
            InitializeComponent();
             worker = new BackgroundWorker();
            worker.RunWorkerAsync();
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
        }


        public endSimulatorObserver(MyValue t)
        {
            t.ValueChanged += this.ValueChangeFunc;
        }
        public updateSimulatorObserver(MyValue t)
        {
            t.ValueChanged += this.ValueChangeFunc;
        }
        private void Observer(Object sender, NTAccountEventArgs args)
        {
            Update(args.cancelAsync == false);
        }



        private void Worker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void Worker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            switch (e.ProgressPercentage)
            {
                case 0:
                    {

                        break;
                    }
                case 1:
                    {

                        break;
                    }
                case 2:
                    {

                        break;
                    }
                case 3:
                    {

                        break;
                    }
            }
        }

        private void Worker_DoWork(object? sender, DoWorkEventArgs e)
        {
            Simulator.RegisterToUpdateEvent();
            Simulator.Simulatorr sim;
            Simulator.Activate(); //starting the simulator

            while (worker.CancellationPending == true)
            {
                System.Threading.Thread.Sleep(1000);
                worker.ReportProgress();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            MessageBox.Show("You can't close this window!");
        }
    }
}
