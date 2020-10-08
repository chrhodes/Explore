using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using PacificLife.Life;
using EACApplicationTemplateCSharp1;

namespace EACApplicationTemplateCSharp1.User_Interface
{
    /// <summary>
    /// Base of EAC user controls that swap screens.
    /// </summary>
    public partial class ucScreenBase : ucBase
    {
        const string CONTROL_NAME = "ucScreenBase";

        public ucScreenBase FromScreen
        {
            get;
            set;
        }

        #region Initialize

        public ucScreenBase()
        {
#if TRACE_BASE
            Trace.WriteLine(string.Format("{0}:{1}()", CONTROL_NAME, System.Reflection.MethodInfo.GetCurrentMethod().Name));
#endif
            InitializeComponent();
        }

        public override void InitialLoad()
        {
#if TRACE_BASE
            Trace.WriteLine(string.Format("{0}:{1}()", CONTROL_NAME, System.Reflection.MethodInfo.GetCurrentMethod().Name));
#endif
            InitializeUI();
        }

        public override void InitializeUI()
        {
#if TRACE_BASE
            Trace.WriteLine(string.Format("{0}:{1}()", CONTROL_NAME, System.Reflection.MethodInfo.GetCurrentMethod().Name));
#endif

            InitializeApplicationEventHandlers();
        }

        #endregion

        #region Display Swapable User Controls

        /// <summary>
        /// 
        /// </summary>
        protected void ReturnToSender()
        {
#if TRACE_BASE
            Trace.WriteLine(string.Format("{0}:{1}()", CONTROL_NAME, System.Reflection.MethodInfo.GetCurrentMethod().Name));
#endif
            // TODO: this may need some error checking

            SwapUserControl(this, FromScreen.GetType());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uc"></param>
        private void ShowControl(ucScreenBase uc)
        {
#if TRACE_BASE
            Trace.WriteLine(string.Format("{0}:{1}()", CONTROL_NAME, System.Reflection.MethodInfo.GetCurrentMethod().Name));
#endif
            // Clear the existing Controls from the base control
            Common.SwapScreenBaseControl.Controls.Clear();
            // and then add the new UserControl to display to the controls collection.
            Common.SwapScreenBaseControl.Controls.Add(uc);

            uc.Dock = DockStyle.Fill;

            // Wire up the Event Handlers
            uc.InitializeApplicationEventHandlers();
            // And indicate we have just swapped back in.
            uc.AfterSwappingIn();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ucType"></param>
        protected void ShowControl(Type ucType)
        {
#if TRACE_BASE
            Trace.WriteLine(string.Format("{0}:{1}()", CONTROL_NAME, System.Reflection.MethodInfo.GetCurrentMethod().Name));
#endif
            try
            {
                ucScreenBase uc;

                if(Common.SwapScreenBaseControl == null)
                {
                    // Save whomever got loaded first as the SwapScreenBaseControl
                    // Doesn't really matter which control is used as the Controls are cleared before
                    // loading the control we want to see.
                    Common.SwapScreenBaseControl = this;
                }

                // Look to see if we have already loaded this type of control.
                if(Common.InitializedUserControls.ContainsKey(ucType.ToString()))
                {
                    uc = Common.InitializedUserControls[ucType.ToString()];
                }
                else
                {
                    // If not, create one and store it.
                    uc = (ucScreenBase)Activator.CreateInstance(ucType);
                    Common.InitializedUserControls.Add(uc.GetType().ToString(), uc);
                    uc.InitializeUI();
                }

                uc.FromScreen = this;
                ShowControl(uc);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Could not display screen." + ex.ToString());
            }
        }

        protected void SwapUserControl(ucScreenBase fromUserControl, Type toUserControl)
        {
#if TRACE_BASE
            Trace.WriteLine(string.Format("{0}:{1}()", CONTROL_NAME, System.Reflection.MethodInfo.GetCurrentMethod().Name));
#endif
            fromUserControl.RemoveApplicationEventHandlers();
            fromUserControl.BeforeSwappingOut();

            ShowControl(toUserControl);
        }

        /// <summary>
        /// Called before a user control is swapped out.  May be overridden in derived class.
        /// </summary>
        protected virtual void BeforeSwappingOut()
        {
        }

        /// <summary>
        /// Called after a user control is swapped in.  May be overridden in derived class.
        /// </summary>
        protected virtual void AfterSwappingIn()
        {
        }

        #endregion

    }
}
