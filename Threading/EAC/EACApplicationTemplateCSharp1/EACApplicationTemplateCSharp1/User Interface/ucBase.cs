using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using PacificLife.Life;

namespace EACApplicationTemplateCSharp1.User_Interface
{
    /// <summary>
    /// Base of EAC User Controls.  Initializes ConfigData passed in by EAC Framework.
    /// 
    /// All EAC User Controls should inherit from this class or a derived type like ucScreenBase.
    /// </summary> 
    public partial class ucBase : PacificLife.Life.Enterprise.Foundation.PLBaseForm.PLBaseControl
    {
        const string CONTROL_NAME = "ucBase";

        #region Initialization

        public ucBase()
        {
#if TRACE_BASE
            Trace.WriteLine(string.Format("{0}:{1}()", CONTROL_NAME, System.Reflection.MethodInfo.GetCurrentMethod().Name));
#endif
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void InitialLoad()
        {
#if TRACE_BASE
            Trace.WriteLine(string.Format("{0}:{1}()", CONTROL_NAME, System.Reflection.MethodInfo.GetCurrentMethod().Name));
#endif
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void InitializeUI()
        {
#if TRACE_BASE
            Trace.WriteLine(string.Format("{0}:{1}()", CONTROL_NAME, System.Reflection.MethodInfo.GetCurrentMethod().Name));
#endif
        }

        #endregion

        #region EAC Framework Methods

        /// <summary>
        /// Called by EAC framework when User Control is loaded
        /// </summary>
        /// <param name="args"></param>
        /// <returns>Config data loaded from environment specific EAC Database is passed in args()</returns>
        public override bool ReLoad(object[] args)
        {
#if TRACE_BASE
            Trace.WriteLine(string.Format("{0}:{1}()", CONTROL_NAME, System.Reflection.MethodInfo.GetCurrentMethod().Name));
#endif
            // We are called each time the EAC/TestWindowConsole loads a new app(usercontrol).  Ensure
            // that we start clean.

            Common.Initialize();

            // Intialize the ApplicationLevel event handler

            ApplicationEventHandler.Init();

            try
            {
                // Save the passed in configuration data.
                ConfigData.RawXML = (string)args[0];
                InitialLoad();
            }
            catch(Exception ex)
            {
#if TRACE_BASE
                Trace.WriteLine(string.Format("{0}:{1}()", CONTROL_NAME, System.Reflection.MethodInfo.GetCurrentMethod().Name));
#endif
                MessageBox.Show(ex.Message);
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public override bool Refreshctl()
        {
#if TRACE_BASE
            Trace.WriteLine(string.Format("{0}:{1}()", CONTROL_NAME, System.Reflection.MethodInfo.GetCurrentMethod().Name));
#endif
            InitialLoad();
            return base.Refreshctl();
        }

        #endregion

        #region ApplicationLevel Event Handlers

        protected virtual void InitializeApplicationEventHandlers()
        {
        }

        protected virtual void ToggleDebugMode()
        {
        }

        protected virtual void ToggleDeveloperMode()
        {
        }

        protected virtual void RemoveApplicationEventHandlers()
        {
        }

        #endregion

    }
}
