﻿Example 8 - TfsUserControl

#region Header

// This source is subject to Microsoft Public License (Ms-PL).
// Please see http://taskcardcreator.codeplex.com for details.
// All other rights reserved.

#endregion Header

namespace TFSIterationPathServices
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Navigation;

    using Microsoft.TeamFoundation.ProcessConfiguration.Client;
    using Microsoft.TeamFoundation.WorkItemTracking.Client;

    using ReportInterface;

    /// <summary>
    /// Interaction logic for Tfs.xaml
    /// </summary>
    public partial class TfsUserControl : UserControl, INotifyPropertyChanged
    {
        #region Fields

        private IEnumerable<IReport> allReports;
        private string pendingRequest = string.Empty;
        private string projectName;
        private string selectedIterationPath;
        private IReport selectedReport;
        private TeamConfiguration selectedTeam;
        private bool showAll = false;
        private IEnumerable<IReport> supportedReports;
        private BackgroundWorker worker;

        #endregion Fields

        #region Constructors

        public TfsUserControl(IEnumerable<IReport> supportedReports, IEnumerable<IReport> allReports)
        {
            DataContext = this;

            this.supportedReports = supportedReports;
            this.allReports = allReports;

            WorkItems = new ObservableCollection<WorkItem>();
            Reports = new ObservableCollection<IReport>(supportedReports);
            Teams = new ObservableCollection<TeamConfiguration>();

            SelectedReport = Reports.First();

            InitializeComponent();
        }

        #endregion Constructors

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Properties

        public ObservableCollection<IReport> Reports
        {
            get; set;
        }

        public string SelectedIterationPath
        {
            get { return selectedIterationPath; }
            set
            {
                if (selectedIterationPath != value)
                {
                    selectedIterationPath = value;
                    OnPropertyChanged("SelectedIterationPath");
                    QueriesSelectionChanged(value);
                }
            }
        }

        public IReport SelectedReport
        {
            get { return selectedReport; }
            set
            {
                if (selectedReport != value)
                {
                    selectedReport = value;
                    OnPropertyChanged("SelectedReport");
                }
            }
        }

        public TeamConfiguration SelectedTeam
        {
            get { return selectedTeam; }
            set
            {
                if (selectedTeam != value)
                {
                    selectedTeam = value;
                    OnPropertyChanged("SelectedTeam");
                    SelectedIterationPath = value.TeamSettings.CurrentIterationPath;
                }
            }
        }

        public IEnumerable<WorkItem> SelectedWorkItems
        {
            get
            {
                return listView.SelectedItems.Cast<WorkItem>();
            }
        }

        public bool ShowAll
        {
            get { return showAll; }
            set
            {
                showAll = value;

                Reports.Clear();
                if (showAll)
                {
                    foreach (var report in allReports)
                    {
                        Reports.Add(report);
                    }
                }
                else
                {
                    foreach (var report in supportedReports)
                    {
                        Reports.Add(report);
                    }
                }
                SelectedReport = Reports.FirstOrDefault();

                OnPropertyChanged("ShowAll");
            }
        }

        public ObservableCollection<TeamConfiguration> Teams
        {
            get; set;
        }

        public ObservableCollection<WorkItem> WorkItems
        {
            get; set;
        }

        public WorkItemStore workItemStoreService
        {
            get; set;
        }

        #endregion Properties

        #region Methods

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void BwDoWork(object sender, DoWorkEventArgs e)
        {
            //      Logger.Write("Entering BwDoWork method");

            var newWorkItems = new ObservableCollection<WorkItem>();

            if (!string.IsNullOrEmpty((string)e.Argument))
            {
                var queryString = string.Format("SELECT * FROM WorkItems WHERE [System.IterationPath] = '{0}'", e.Argument);

                var q = new Query(workItemStoreService, queryString);
                if (q.IsLinkQuery)
                {
                    var queryResults = q.RunLinkQuery();
                    foreach (WorkItemLinkInfo i in queryResults)
                    {
                        var wi = workItemStoreService.GetWorkItem(i.TargetId);
                        newWorkItems.Add(wi);
                    }
                }
                else
                {
                    var queryResults = q.RunQuery();
                    foreach (WorkItem workitem in queryResults)
                    {
                        newWorkItems.Add(workitem);
                    }
                }
            }
            e.Result = newWorkItems;
        }

        private void BwRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //      Logger.Write("Entering BwRunWorkerCompleted method");

            if (e.Cancelled == true)
            {
            }
            else if (e.Error != null)
            {
                //       Logger.Write(string.Format("Exception: {0}", e.Error.Message));
                //        MessageBox.Show(string.Format("Error: {0}", e.Error.Message));
            }
            else
            {
                var newWorkItems = (ObservableCollection<WorkItem>)e.Result;
                //       Logger.Write(string.Format("Work items loaded: {0}", newWorkItems.Count));
                foreach (var item in newWorkItems)
                {
                    WorkItems.Add(item);
                }

                listView.SelectAll();
            }
            progress.Visibility = Visibility.Collapsed;

            // Any pending requests?
            if (!string.IsNullOrEmpty(pendingRequest))
            {
                QueriesSelectionChanged((string)pendingRequest.Clone());
                pendingRequest = string.Empty;
            }
        }

        private void HyperlinkRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void QueriesSelectionChanged(string queryString)
        {
            //      Logger.Write("Entering QueriesSelectionChanged method");

            progress.Visibility = Visibility.Visible;

            WorkItems.Clear();

            if (worker == null)
            {
                worker = new BackgroundWorker();
                worker.WorkerSupportsCancellation = false;
                worker.WorkerReportsProgress = false;

                worker.DoWork += BwDoWork;
                worker.RunWorkerCompleted += BwRunWorkerCompleted;
            }

            if (worker.IsBusy)
            {
                pendingRequest = queryString;
            }
            else
            {
                worker.RunWorkerAsync(queryString);
            }
        }

        #endregion Methods
    }
}