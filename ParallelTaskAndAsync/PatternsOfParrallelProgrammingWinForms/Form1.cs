using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PatternsOfParallelProgrammingWinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            Go();
        }

        private void btnGoParallel_Click(object sender, EventArgs e)
        {
            GoParallel();
        }


        #region Main Function Routines

        private void DisplayItem(int i)
        {
            string newText = string.Format("{0,3} - {1}{2}", i, Thread.CurrentThread.ManagedThreadId, Environment.NewLine); // running on worker thread 

            this.Invoke((MethodInvoker)delegate
            {
                txtOutput.AppendText(newText); // runs on UI thread
                txtOutput.Update();
            }); 

            //txtOutput.AppendText(newText);
        }

        private void Go()
        {
            for (int i = Int16.Parse(txtInclusiveLowerBound.Text); i < Int16.Parse(txtExclusiveUpperBound.Text); i++)
            {
                DisplayItem(i);
            }
        }

        private void MyParallelFor(int inclusiveLowerBound, int exclusiveUpperBound, Action<int> body)
        {
            // Determine the number of interations to be processed, the number of cores to use,
            // and the approximate number of interations to process in each thread.

            int size = exclusiveUpperBound - inclusiveLowerBound;
            int numProcs = Environment.ProcessorCount;
            int range = size / numProcs;

            // Use a thread for each partion.  Create them all, start them all, wait on them all.

            var threads = new List<Thread>(numProcs);

            for (int p = 0; p < numProcs; p++)
            {
                int start = p * range + inclusiveLowerBound;
                int end = (p == numProcs - 1) ? exclusiveUpperBound : start + range;

                threads.Add(new Thread(() => { for (int i = start; i < end; i++) body(i); }));
            }

            foreach (var thread in threads)
            {
                thread.Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }

        }
        #endregion

        private void GoParallel()
        {
            Action<int> body = DisplayItem;

            MyParallelFor(Int16.Parse(txtInclusiveLowerBound.Text), Int16.Parse(txtExclusiveUpperBound.Text), body);
        }

    }
}
