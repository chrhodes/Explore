using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsExtensionMethods;

namespace TasksWinForm
{
    public partial class Form1 : Form
    {
        const string CLASS_NAME = "Form1";

        #region Initialization

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeApplicationEventHandlers();
        }

        #endregion

        #region Event Handlers

        private void btnBlink_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < (int)nudLoop.Value; i++)
            {
                txtLoop.Text = i.ToString();
                txtLoop.Update();

                DebugWindow.Common.WriteToDebugWindow("blinkPanel panel1 Red X");
                blinkPanel(panel1, Color.White, Color.Red, (int)nudDelayRed.Value, (int)nudRepeatRed.Value);

                DebugWindow.Common.WriteToDebugWindow("blinkPanel panel1 Green X");
                blinkPanel(panel2, Color.White, Color.Green, (int)nudDelayGreen.Value, (int)nudRepeatGreen.Value);

                DebugWindow.Common.WriteToDebugWindow("blinkPanel panel1 Blue X");
                blinkPanel(panel3, Color.White, Color.Blue, (int)nudDelayBlue.Value, (int)nudRepeatBlue.Value);
            }
        }

        private void btnBlinkAsync_Click(object sender, EventArgs e)
        {
            Task<int> redTask;
            Task<int> greenTask;
            Task<int> blueTask;

            for (int i = 0; i < (int)nudLoop.Value; i++)
            {
                txtLoop.Text = (i+1).ToString();
                txtLoop.Update();

                DebugWindow.Common.WriteToDebugWindow("blinkPanelAsync panel1 Red");
                redTask = blinkPanelAsync(panel1, Color.White,Color.Red, (int)nudDelayRed.Value, (int)nudRepeatRed.Value);

                DebugWindow.Common.WriteToDebugWindow("blinkPanelAsync panel1 Green");
                greenTask = blinkPanelAsync(panel2, Color.White, Color.Green, (int)nudDelayGreen.Value, (int)nudRepeatGreen.Value);

                DebugWindow.Common.WriteToDebugWindow("blinkPanelAsync panel1 Blue");
                blueTask = blinkPanelAsync(panel3, Color.White, Color.Blue, (int)nudDelayBlue.Value, (int)nudRepeatBlue.Value);

                // This doesn't work as IsCompleted doesn't seem to change.
                //while( ! redTask.IsCompleted && ! blueTask.IsCompleted && ! greenTask.IsCompleted)
                //{
                //    DebugWindow.Common.WriteToDebugWindow(string.Format("Red {0}  Green {1}  Blue {2}{3}",
                //        redTask.IsCompleted,
                //        greenTask.IsCompleted,
                //        blueTask.IsCompleted,
                //        Environment.NewLine));
                //}

                // This seems to hang at the first one :(
                //redTask.Wait();
                //greenTask.Wait();
                //blueTask.Wait();

                //System.Threading.Thread.Sleep(2000);

                // This seems to never get beyond waiting to start.
                //while (ckAbort.Checked == false)
                //{
                //    DebugWindow.Common.WriteToDebugWindow(string.Format("Red {0}  Green {1}  Blue {2}{3}",
                //        redTask.Status,
                //        greenTask.Status,
                //        blueTask.Status,
                //        Environment.NewLine));
                //}
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            panel1.BackColor = Color.White;
            panel2.BackColor = Color.Red;
            panel3.BackColor = Color.Green;
            panel4.BackColor = Color.Blue;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.BackColor = Color.Blue;
            panel2.BackColor = Color.White;
            panel3.BackColor = Color.Red;
            panel4.BackColor = Color.Green;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel1.BackColor = Color.Green;
            panel2.BackColor = Color.Blue;
            panel3.BackColor = Color.White;
            panel4.BackColor = Color.Red;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel1.BackColor = Color.Red;
            panel2.BackColor = Color.White;
            panel3.BackColor = Color.Green;
            panel4.BackColor = Color.Blue;
        }

        #endregion

        #region Application Event Handlers

        protected void InitializeApplicationEventHandlers()
        {
#if TRACE
            Trace.WriteLine(string.Format("{0}:{1}()", CLASS_NAME, System.Reflection.MethodInfo.GetCurrentMethod().Name));
#endif
            ApplicationEventHandler.Init();
            ApplicationEventHandler.ToggleF11Event += this.ToggleDebugMode;
            ApplicationEventHandler.ToggleF12Event += this.ToggleDeveloperMode;
        }

        /// <summary>
        /// Perform any operations that are needed when DebugMode switches state.
        /// </summary>
        protected void ToggleDebugMode()
        {
#if TRACE
            Trace.WriteLine(string.Format("{0}:{1}()", CLASS_NAME, System.Reflection.MethodInfo.GetCurrentMethod().Name));
#endif

            Common.DebugMode = !Common.DebugMode;
            DebugWindow.Common.DebugMode = !DebugWindow.Common.DebugMode;

            if(Common.DebugMode)
            {
                // TODO:
            }
            else
            {
                // TODO:
            }
        }

        /// <summary>
        /// Perform any operations that are needed when DeveloperMode switches state.
        /// </summary>
        protected void ToggleDeveloperMode()
        {
#if TRACE
            Trace.WriteLine(string.Format("{0}:{1}()", CLASS_NAME, System.Reflection.MethodInfo.GetCurrentMethod().Name));
#endif

            Common.DeveloperMode = !Common.DeveloperMode;
            DebugWindow.Common.DeveloperMode = !DebugWindow.Common.DeveloperMode;

            if(Common.DeveloperMode)
            {
                this.BackColor = Color.LightCoral;
                DebugWindow.Common.DebugWindow.Show();
            }
            else
            {
                this.BackColor = SystemColors.ControlLight;
                DebugWindow.Common.DebugWindow.Hide();
            }
        }

        #endregion

        #region Main Function Routines

        private void blinkPanel(Panel panel, Color firstColor, Color secondColor, int delay, int repeat)
        {
            for(int i = 0 ; i < repeat ; i++)
            {
                panel.BackColor = firstColor;
                panel.Update();
                Thread.Sleep(delay);
                panel.BackColor = secondColor;
                panel.Update();
                Thread.Sleep(delay);
            }
        }

        private async Task<int> blinkPanelAsync(Panel panel, Color firstColor, Color secondColor, int delay, int repeat)
        {
            for(int i = 0 ; i < repeat ; i++)
            {
                panel.BackColor = firstColor;
                panel.Update();
                await TaskEx.Delay(delay);
                panel.BackColor = secondColor;
                panel.Update();
                await TaskEx.Delay(delay);
            }

            return 0;
        }

        #endregion

    }
}
