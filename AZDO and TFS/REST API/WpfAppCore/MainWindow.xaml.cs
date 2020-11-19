using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
// https://www.nuget.org/packages/Microsoft.TeamFoundationServer.Client/
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

// https://www.nuget.org/packages/Microsoft.VisualStudio.Services.InteractiveClient/
using Microsoft.VisualStudio.Services.Client;

// https://www.nuget.org/packages/Microsoft.VisualStudio.Services.Client/
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.OAuth;
using Microsoft.VisualStudio.Services.WebApi;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using WpfAppCore.Domain;

namespace WpfAppCore
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {


        private object _propertyName;
        private static string _URI_BD_STS_QA2 = @"https://dev.azure.com/BD-STS-QA2";
        private static string _URI_BD_STS_PROD = @"https://dev.azure.com/BD-STS-PROD";
        private static string _URI_VNC_Development = @"https://dev.azure.com/VNC-Development";

        private static string _teamProjectName = "TFS";

        private static string _PAT_BD_STS_PROD = "ktjisgkllgewl4lkazadc3ggj5rouzpkmwrieauz6sss62ajhl4a";

        //private static string _PAT_BD_STS_QA2 = "bjllb2zrs3izlgbvh5q2tqr4neudouk5yidulwn52boc5mkl3cwa";
        private static string _PAT_BD_STS_QA2 = "3mesoqd2xxsgxvx6fqsdc7h2fhswo2ohpa2t6aennimb3x7wr6vq";

        private static string _PAT_VNC_DevelopmentLimited = "ssyqqvap35hunafmt6abskzgcrqroldvlhwrwl3hcjh3oo7mf5yq";
        private static string _PAT_VNC_Development = "nioxcaauiyoxqaovxlcnbyodj6uvbvikm543gvuudxcekbjx5xsa";

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private string _callResult;

        public string CallResult
        {
            get => _callResult;
            set
            {
                if (_callResult == value)
                    return;
                _callResult = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CallResult)));
            }
        }

        //private string _response;
        //public string Response
        //{
        //    get => _response;
        //    set
        //    {
        //        if (_response == value)
        //            return;
        //        _response = value;
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Response)));
        //    }
        //}

        private string _requestUri;

        public string RequestUri
        {
            get => _requestUri;
            set
            {
                if (_requestUri == value)
                    return;
                _requestUri = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RequestUri)));
            }
        }

        private HttpResponseMessage _response;
        public HttpResponseMessage Response
        {
            get => _response;
            set
            {
                if (_response == value)
                    return;
                _response = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Response)));
            }
        }

        //public ObservableCollection<KeyValuePair<string, IEnumerable<string>>> RequestHeaders { get; set; }
        //    = new ObservableCollection<KeyValuePair<string, IEnumerable<string>>>();

        //public ObservableCollection<KeyValuePair<string, IEnumerable<string>>> ResponseHeaders { get; set; }
        //    = new ObservableCollection<KeyValuePair<string, IEnumerable<string>>>();

        private int _requestResponseExchangeCount;

        public int RequestResponseExchangeCount
        {
            get => _requestResponseExchangeCount;
            set
            {
                if (_requestResponseExchangeCount == value)
                    return;
                _requestResponseExchangeCount = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RequestResponseExchangeCount)));
            }
        }

        public ObservableCollection<RequestResponseInfo> RequestResponseExchange { get; set; }
            = new ObservableCollection<RequestResponseInfo>();

        #region Lists

        public RESTResult<Domain.List> Lists { get; set; } = new RESTResult<Domain.List>();

        //private int _listsCount;
        //public int ListsCount
        //{
        //    get => _listsCount;
        //    set
        //    {
        //        if (_listsCount == value)
        //            return;
        //        _listsCount = value;
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ListsCount)));
        //    }
        //}

        //private ObservableCollection<Domain.List> _lists;
        //public ObservableCollection<Domain.List> Lists
        //{
        //    get => _lists;
        //    set
        //    {
        //        if (_lists == value)
        //            return;
        //        _lists = value;
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Lists)));
        //    }
        //}

        //private Domain.Process _selectedListGrid;

        //public Domain.Process SelectedListGrid
        //{
        //    get => _selectedListGrid;
        //    set
        //    {
        //        if (_selectedListGrid == value)
        //            return;
        //        _selectedListGrid = value;
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedListGrid)));
        //    }
        //}

        #endregion

        #region Processes

        public RESTResult<Domain.Process> Processes { get; set; } = new RESTResult<Domain.Process>();

        //private int _ProcessCount;

        //public int ProcessCount
        //{
        //    get => _ProcessCount;
        //    set
        //    {
        //        if (_ProcessCount == value)
        //            return;
        //        _ProcessCount = value;
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProcessCount)));
        //    }
        //}

        //private ObservableCollection<Domain.Process> _processes;
        //public ObservableCollection<Domain.Process> Processes
        //{
        //    get => _processes;
        //    set
        //    {
        //        if (_processes == value)
        //            return;
        //        _processes = value;
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Processes)));
        //    }
        //}

        //private Domain.Process _selectedProcessGrid;

        //public Domain.Process SelectedProcessGrid
        //{
        //    get => _selectedProcessGrid;
        //    set
        //    {
        //        if (_selectedProcessGrid == value)
        //            return;
        //        _selectedProcessGrid = value;
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedProcessGrid)));
        //    }
        //}

        #endregion

        #region Projects

        public RESTResult<Domain.Project> Projects { get; set; } = new RESTResult<Domain.Project>();

        //private int _ProjectsCount;

        //public int ProjectsCount
        //{
        //    get => _ProjectsCount;
        //    set
        //    {
        //        if (_ProjectsCount == value)
        //            return;
        //        _ProjectsCount = value;
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProjectsCount)));
        //    }
        //}

        //private ObservableCollection<Project> _projects;
        //public ObservableCollection<Project> Projects
        //{
        //    get => _projects;
        //    set
        //    {
        //        if (_projects == value)
        //            return;
        //        _projects = value;
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Projects)));
        //    }
        //}

        //private Project _selectedProjectGrid;

        //public Project SelectedProjectGrid
        //{
        //    get => _selectedProjectGrid;
        //    set
        //    {
        //        if (_selectedProjectGrid == value)
        //            return;
        //        _selectedProjectGrid = value;
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedProjectGrid)));
        //    }
        //}

        //private string _selectedProject;

        //public string SelectedProject
        //{
        //    get => _selectedProject;
        //    set
        //    {
        //        if (_selectedProject == value)
        //            return;
        //        _selectedProject = value;
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedProject)));
        //    }
        //}

        #endregion

        #region Teams

        public RESTResult<Domain.Team> Teams { get; set; } = new RESTResult<Domain.Team>();

        //private int _teamsCount;
        //public int TeamsCount
        //{
        //    get => _teamsCount;
        //    set
        //    {
        //        if (_teamsCount == value)
        //            return;
        //        _teamsCount = value;
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TeamsCount)));
        //    }
        //}

        //private ObservableCollection<Domain.Team> _teams;
        //public ObservableCollection<Domain.Team> Teams
        //{
        //    get => _teams;
        //    set
        //    {
        //        if (_teams == value)
        //            return;
        //        _teams = value;
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Teams)));
        //    }
        //}

        //private Domain.Team _selectedTeamGrid;

        //public Domain.Team SelectedTeamGrid
        //{
        //    get => _selectedTeamGrid;
        //    set
        //    {
        //        if (_selectedTeamGrid == value)
        //            return;
        //        _selectedTeamGrid = value;
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedTeamGrid)));
        //    }
        //}

        #endregion

        #region WorkItemTypes

        // If don't initialize here (see GetWorkItemTypes for another place to initialize)
        // Would need to implement PCN so Bindings work.  Guess could also do in code so Controls know about things.

        public RESTResult<Domain.WorkItemType> WorkItemTypes { get; set; } = new RESTResult<Domain.WorkItemType>();
        public RESTResult<Domain.State> States { get; set; } = new RESTResult<Domain.State>();
        public RESTResult<Domain.Field> Fields { get; set; } = new RESTResult<Domain.Field>();
        //public RESTResult<Domain.FieldDef> FieldDef { get; set; } = new RESTResult<Domain.FieldDef>();


        //private RestResult<Domain.WorkItemType> _workItemTypes2;// = new RestResult<Domain.WorkItemType>();

        //public RestResult<Domain.WorkItemType> WorkItemTypes2
        //{
        //    get =>  _workItemTypes2;
        //    set
        //    {
        //        if (_workItemTypes2 == value)
        //            return;
        //        _workItemTypes2 = value;
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(WorkItemTypes2)));
        //    }
        //}

        //private int _workItemTypesCount;
        //public int WorkItemTypesCount
        //{
        //    get => _workItemTypesCount;
        //    set
        //    {
        //        if (_workItemTypesCount == value)
        //            return;
        //        _workItemTypesCount = value;
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(WorkItemTypesCount)));
        //    }
        //}

        //private ObservableCollection<Domain.WorkItemType> _workItemTypes;
        //public ObservableCollection<Domain.WorkItemType> WorkItemTypes
        //{
        //    get => _workItemTypes;
        //    set
        //    {
        //        if (_workItemTypes == value)
        //            return;
        //        _workItemTypes = value;
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(WorkItemTypes)));
        //    }
        //}

        //private Domain.WorkItemType _selectedWorkItemTypeGrid;

        //public Domain.WorkItemType SelectedWorkItemTypeGrid
        //{
        //    get => _selectedWorkItemTypeGrid;
        //    set
        //    {
        //        if (_selectedWorkItemTypeGrid == value)
        //            return;
        //        _selectedWorkItemTypeGrid = value;
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedWorkItemTypeGrid)));
        //    }
        //}

        #endregion

        public ObservableCollection<AvailableCollection> AvailableCollections { get; set; }
            = new ObservableCollection<AvailableCollection>();

        private AvailableCollection _SelectedCollection;

        public AvailableCollection SelectedCollection
        {
            get => _SelectedCollection;
            set
            {
                if (_SelectedCollection == value)
                    return;
                _SelectedCollection = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedCollection)));
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            InitializeView();
            DataContext = this;
        }

        private void InitializeView()
        {
            LoadCollections();
            //SelectedCollection = new CollectionDetails();
            ////var foo = AvailableCollections.First().Key;
            //CB1.SelectedItem = AvailableCollections.First();
            //ResponseHeaders.CollectionChanged += ResponseHeaders_CollectionChanged;
        }

        //private void ResponseHeaders_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        //{
        //    var foo = sender;
        //}

        private void LoadCollections()
        {
            AvailableCollections.Add(
                new AvailableCollection
                {
                    Name = "BD_STS_PROD",
                    Details = new CollectionDetails
                    {
                        Uri = @"https://dev.azure.com/BD-STS-PROD",
                        PAT = _PAT_BD_STS_PROD
                    }
                });

            AvailableCollections.Add(
                new AvailableCollection
                {
                    Name = "BD_STS_QA2",
                    Details = new CollectionDetails
                    {
                        Uri = @"https://dev.azure.com/BD-STS-QA2",
                        PAT = _PAT_BD_STS_QA2
                    }
                });

            AvailableCollections.Add(
                new AvailableCollection
                {
                    Name = "VNC-Development",
                    Details = new CollectionDetails
                    {
                        Uri = @"https://dev.azure.com/VNC-Development",
                        PAT = _PAT_VNC_Development
                    }
                });

            AvailableCollections.Add(
                new AvailableCollection
                {
                    Name = "VNC-Development Limited",
                    Details = new CollectionDetails
                    {
                        Uri = @"https://dev.azure.com/VNC-Development",
                        PAT = _PAT_VNC_DevelopmentLimited
                    }
                });
        }

        private async void GetProjects_Click(object sender, RoutedEventArgs e)
        {
            CallResult = "";
            CallResult = await GetProjects(SelectedCollection.Details);
        }

        private async void GetProject_Click(object sender, RoutedEventArgs e)
        {
            CallResult = "";
            //CallResult = await GetProject(SelectedCollection.Details, SelectedProjectGrid);
            CallResult = await GetProject(SelectedCollection.Details, Projects.SelectedItem);
        }

        private async void GetProcesses_Click(object sender, RoutedEventArgs e)
        {
            CallResult = "";
            CallResult = await GetProcesses(SelectedCollection.Details);
        }

        private async void GetProcess_Click(object sender, RoutedEventArgs e)
        {
            CallResult = "";
            CallResult = await GetProcess(SelectedCollection.Details, Processes.SelectedItem);
        }

        private async void SampleREST_Click(object sender, RoutedEventArgs e)
        {
            CallResult = "";
            CallResult = await SampleREST(SelectedCollection.Details);
        }

        private async void PersonalAccessToken_Click(object sender, RoutedEventArgs e)
        {
            CallResult = "";
            CallResult = await PersonalAccessTokenRestSample(SelectedCollection.Details);
        }

        private async void MicrosoftAccount_Click(object sender, RoutedEventArgs e)
        {
            CallResult = "";
            CallResult = await MicrosoftAccountRestSample(SelectedCollection.Details);
        }

        private async void OAuth_Click(object sender, RoutedEventArgs e)
        {
            CallResult = "";
            CallResult = await OAuthSample(SelectedCollection.Details);
        }

        private async void AzureActiveDirectory_Click(object sender, RoutedEventArgs e)
        {
            CallResult = "";
            CallResult = await AADRestSample(SelectedCollection.Details);
        }

        public async Task<string> GetProcesses(CollectionDetails collection)
        {
            string jsonResult = default;
            StringBuilder sb = new StringBuilder();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    InitializeHttpClient(collection, client);

                    RequestUri = $"{collection.Uri}/_apis/work/processes?api-version=6.0-preview.2";

                    RequestResponseInfo exchange = InitializeExchange(client, RequestUri);

                    using (HttpResponseMessage response = await client.GetAsync(RequestUri))
                    {
                        RecordExchangeResponse(response, exchange);

                        response.EnsureSuccessStatusCode();

                        string outJson = await response.Content.ReadAsStringAsync();
                        //sb.Append(outJson);

                        JObject o = JObject.Parse(outJson);

                        ProcessesRoot resultRoot = JsonConvert.DeserializeObject<ProcessesRoot>(outJson);
                        //Processes = new ObservableCollection<WpfAppCore.Domain.Process>(resultRoot.value);
                        //ProcessCount = Processes.Count();

                        Processes.ResultItems = new ObservableCollection<Domain.Process>(resultRoot.value);
                        jsonResult = outJson;

                        IEnumerable<string> continuationHeaders = default;

                        bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                        Processes.Count = Processes.ResultItems.Count();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            RequestResponseExchangeCount = RequestResponseExchange.Count();
            return jsonResult;
        }

        private async Task<string> GetProcess(CollectionDetails collection, Domain.Process selectedProcess)
        {
            string jsonResult = default;
            StringBuilder sb = new StringBuilder();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    InitializeHttpClient(collection, client);

                    RequestUri = $"{collection.Uri}/_apis/work/processes/{selectedProcess.typeId}?api-version=6.1-preview.1";

                    RequestResponseInfo exchange = InitializeExchange(client, RequestUri);

                    using (HttpResponseMessage response = await client.GetAsync(RequestUri))
                    {
                        RecordExchangeResponse(response, exchange);

                        IEnumerable<string> continuationHeaders = default;

                        bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                        response.EnsureSuccessStatusCode();

                        string outJson = await response.Content.ReadAsStringAsync();
                        //sb.Append(outJson);

                        JObject o = JObject.Parse(outJson);

                        jsonResult = outJson;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            RequestResponseExchangeCount = RequestResponseExchange.Count();

            return jsonResult;
        }

        public async Task<string> GetProjects(CollectionDetails collection)
        {
            string jsonResult = default;
            StringBuilder sb = new StringBuilder();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    InitializeHttpClient(collection, client);

                    RequestUri = $"{collection.Uri}/_apis/projects?api-version=6.1-preview.4";

                    RequestResponseInfo exchange = InitializeExchange(client, RequestUri);

                    using (HttpResponseMessage response = await client.GetAsync(RequestUri))
                    {
                        RecordExchangeResponse(response, exchange);

                        response.EnsureSuccessStatusCode();

                        string outJson = await response.Content.ReadAsStringAsync();
                        sb.Append(outJson);

                        ProjectsRoot resultRoot = JsonConvert.DeserializeObject<ProjectsRoot>(outJson);

                        //Projects = new ObservableCollection<Project>(resultRoot.value);
                        Projects.ResultItems = new ObservableCollection<Project>(resultRoot.value);

                        //JObject o = JObject.Parse(outJson);
                        //foreach (var p in o)
                        //{
                        //    var foo = p.Key;

                        //    if (foo == "count")
                        //    {
                        //        Debug.WriteLine(p.Value);
                        //    }

                        //    if (foo == "value")
                        //    {
                        //        foreach (var t in p.Value)
                        //        {
                        //            Debug.WriteLine(t.ToString());
                        //        }

                        //    }
                        //    var bar = p.Value;
                        //}

                        //dynamic jsonArray = JArray.Parse(outJson);

                        //dynamic json = JValue.Parse(outJson);

                        IEnumerable<string> continuationHeaders = default;

                        bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                        while (hasContinuationToken)
                        {
                            RequestResponseInfo exchange2 = new RequestResponseInfo();

                            string continueToken = continuationHeaders.First();

                            string requestUri2 = $"{collection.Uri}/_apis/projects?api-version=6.1-preview.4&continuationToken={continueToken}";

                            exchange2.Uri = requestUri2;
                            exchange2.RequestHeadersX.AddRange(client.DefaultRequestHeaders);

                            using (HttpResponseMessage response2 = await client.GetAsync(requestUri2))
                            {
                                //ResponseHeaders.AddRange(response2.Headers);

                                exchange2.Response = response2;
                                exchange2.ResponseHeadersX.AddRange(response2.Headers);

                                RequestResponseExchange.Add(exchange2);

                                response2.EnsureSuccessStatusCode();
                                string outJson2 = await response2.Content.ReadAsStringAsync();
                                sb.Append(outJson2);

                                JObject oC = JObject.Parse(outJson2);

                                //Rootobject projectsC = JsonConvert.DeserializeObject<Rootobject>(outJson2);
                                //var projectsarrayC = projectsC.value;

                                ProjectsRoot projects2C = JsonConvert.DeserializeObject<ProjectsRoot>(outJson2);
                                //var projectsarray2C = projects2C.value;

                                //Projects.AddRange(projectsarray2C);
                                Projects.ResultItems.AddRange(projects2C.value);

                                hasContinuationToken = response2.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);
                            }
                        }

                        jsonResult = sb.ToString();

                        //ProjectsCount = Projects.Count();
                        Projects.Count = Projects.ResultItems.Count();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            RequestResponseExchangeCount = RequestResponseExchange.Count();

            return jsonResult;
        }

        private async Task<string> GetProject(CollectionDetails collection, Domain.Project selectedProject)
        {
            string jsonResult = default;
            StringBuilder sb = new StringBuilder();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    InitializeHttpClient(collection, client);

                    RequestUri = $"{collection.Uri}/_apis/work/projects/{selectedProject.id}?api-version=6.1-preview.4";

                    RequestResponseInfo exchange = InitializeExchange(client, RequestUri);

                    using (HttpResponseMessage response = await client.GetAsync(RequestUri))
                    {
                        RecordExchangeResponse(response, exchange);

                        IEnumerable<string> continuationHeaders = default;

                        bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                        response.EnsureSuccessStatusCode();

                        string outJson = await response.Content.ReadAsStringAsync();
                        //sb.Append(outJson);

                        JObject o = JObject.Parse(outJson);

                        //ProcessesRoot processes = JsonConvert.DeserializeObject<ProcessesRoot>(outJson);
                        //var processarray = processes.value;

                        //Processes = new ObservableCollection<WpfAppCore.Domain.Process>(processarray);

                        //ProcessCount = Processes.Count();

                        //jsonResult = outJson;
                    }
                }


            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            RequestResponseExchangeCount = RequestResponseExchange.Count();
            return jsonResult;
        }

        private static void InitializeHttpClient(CollectionDetails collection, HttpClient client)
        {
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            string base64PAT = Convert.ToBase64String(
                    ASCIIEncoding.ASCII.GetBytes(
                        string.Format("{0}:{1}", "", collection.PAT)));

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64PAT);
        }

        #region Misc

        /// <summary>
        /// This sample 
        /// creates a new work item query for New Bugs,
        /// stores it under 'MyQueries',
        /// runs the query, 
        /// and then sends the results to the console.
        /// </summary>
        public async Task<string> SampleREST(CollectionDetails collection)
        {
            string jsonResult = default;
            StringBuilder sb = new StringBuilder();
            var _personalaccesstoken = _PAT_BD_STS_QA2;
            var _collectionUri = _URI_BD_STS_QA2;

            // Connection object could be created once per application and we will use it to get httpclient objects. 
            // Httpclients have been reused between callers and threads.
            // Their lifetime has been managed by connection (we don't have to dispose them).
            // This is more robust then newing up httpclient objects directly.  

            // Be sure to send in the full collection uri, i.e. http://myserver:8080/tfs/defaultcollection
            // We are using default VssCredentials which uses NTLM against a Team Foundation Server.  See additional provided
            // examples for creating credentials for other types of authentication.
            //VssConnection connection = new VssConnection(new Uri(_collectionUri), new VssCredentials());
            VssConnection connection = new VssConnection(new Uri(_collectionUri), new VssBasicCredential(string.Empty, _personalaccesstoken));

            // Create instance of WorkItemTrackingHttpClient using VssConnection
            WorkItemTrackingHttpClient witClient = connection.GetClient<WorkItemTrackingHttpClient>();

            // Get 2 levels of query hierarchy items
            List<QueryHierarchyItem> queryHierarchyItems = witClient.GetQueriesAsync(_teamProjectName, depth: 2).Result;

            // Search for 'My Queries' folder
            QueryHierarchyItem myQueriesFolder = queryHierarchyItems.FirstOrDefault(qhi => qhi.Name.Equals("My Queries"));
            if (myQueriesFolder != null)
            {
                string queryName = "REST Sample";

                // See if our 'REST Sample' query already exists under 'My Queries' folder.
                QueryHierarchyItem newBugsQuery = null;
                if (myQueriesFolder.Children != null)
                {
                    newBugsQuery = myQueriesFolder.Children.FirstOrDefault(qhi => qhi.Name.Equals(queryName));
                }
                if (newBugsQuery == null)
                {
                    // if the 'REST Sample' query does not exist, create it.
                    newBugsQuery = new QueryHierarchyItem()
                    {
                        Name = queryName,
                        Wiql = "SELECT [System.Id],[System.WorkItemType],[System.Title],[System.AssignedTo],[System.State],[System.Tags] FROM WorkItems WHERE [System.TeamProject] = @project AND [System.WorkItemType] = 'Bug' AND [System.State] = 'Resolved'",
                        IsFolder = false
                    };
                    newBugsQuery = witClient.CreateQueryAsync(newBugsQuery, _teamProjectName, myQueriesFolder.Name).Result;
                }

                // run the 'REST Sample' query
                WorkItemQueryResult result = witClient.QueryByIdAsync(newBugsQuery.Id).Result;

                if (result.WorkItems.Any())
                {
                    int skip = 0;
                    const int batchSize = 100;
                    IEnumerable<WorkItemReference> workItemRefs;
                    do
                    {
                        workItemRefs = result.WorkItems.Skip(skip).Take(batchSize);
                        if (workItemRefs.Any())
                        {
                            // get details for each work item in the batch
                            List<WorkItem> workItems = witClient.GetWorkItemsAsync(workItemRefs.Select(wir => wir.Id)).Result;
                            foreach (WorkItem workItem in workItems)
                            {
                                // write work item to console
                                sb.AppendLine($"{workItem.Id}, {workItem.Fields["System.Title"]}");
                                //Console.WriteLine("{0} {1}", workItem.Id, workItem.Fields["System.Title"]);
                            }
                        }
                        skip += batchSize;
                    }
                    while (workItemRefs.Count() == batchSize);
                }
                else
                {
                    Console.WriteLine("No work items were returned from query.");
                }
            }

            //_callResult = sb.ToString();
            jsonResult = sb.ToString();
            return jsonResult;
        }

        public async Task<string> PersonalAccessTokenRestSample(CollectionDetails collection)
        {
            string jsonResult = default;

            // Create instance of VssConnection using Personal Access Token
            VssConnection connection = new VssConnection(
                new Uri(collection.Uri),
                new VssBasicCredential(string.Empty, collection.PAT));
            //_callResult = connection.ServerId.ToString();

            return jsonResult;
        }

        public async Task<string> MicrosoftAccountRestSample(CollectionDetails collection)
        {
            string jsonResult = default;

            // Create instance of VssConnection using Visual Studio sign-in prompt
            VssConnection connection = new VssConnection(
                new Uri(collection.Uri),
                new VssClientCredentials());
            //_callResult = connection.ServerId.ToString();
            return jsonResult;
        }

        public async Task<string> AADRestSample(CollectionDetails collection)
        {
            string jsonResult = default;
            string userName = default;
            string password = default;

            // Create instance of VssConnection using Azure AD Credentials for Azure AD backed account
            VssConnection connection = new VssConnection(
                new Uri(collection.Uri),
                new VssAadCredential(userName, password));

            return jsonResult;
        }

        public async Task<string> OAuthSample(CollectionDetails collection)
        {
            string jsonResult = default;

            // Create instance of VssConnection using OAuth Access token
            VssConnection connection = new VssConnection(
                new Uri(collection.Uri),
                new VssOAuthAccessTokenCredential(collection.PAT));

            return jsonResult;
        }

        #endregion

        private async Task<string> GetAllTeams(CollectionDetails collection)
        {
            string jsonResult = default;
            StringBuilder sb = new StringBuilder();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    InitializeHttpClient(collection, client);

                    RequestUri = $"{collection.Uri}/_apis/teams?api-version=6.1-preview.3";

                    RequestResponseInfo exchange = InitializeExchange(client, RequestUri);

                    using (HttpResponseMessage response = await client.GetAsync(RequestUri))
                    {
                        RecordExchangeResponse(response, exchange);

                        response.EnsureSuccessStatusCode();

                        string outJson = await response.Content.ReadAsStringAsync();
                        //sb.Append(outJson);

                        //JObject o = JObject.Parse(outJson);

                        TeamsRoot resultRoot = JsonConvert.DeserializeObject<TeamsRoot>(outJson);

                        Teams.ResultItems = new ObservableCollection<Domain.Team>(resultRoot.value);

                        IEnumerable<string> continuationHeaders = default;

                        bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                        while (hasContinuationToken)
                        {
                            RequestResponseInfo exchange2 = new RequestResponseInfo();

                            string continueToken = continuationHeaders.First();

                            string requestUri2 = $"{collection.Uri}/_apis/projects?api-version=6.1-preview.4&continuationToken={continueToken}";

                            exchange2.Uri = requestUri2;
                            exchange2.RequestHeadersX.AddRange(client.DefaultRequestHeaders);

                            using (HttpResponseMessage response2 = await client.GetAsync(requestUri2))
                            {
                                //ResponseHeaders.AddRange(response2.Headers);

                                exchange2.Response = response2;
                                exchange2.ResponseHeadersX.AddRange(response2.Headers);

                                RequestResponseExchange.Add(exchange2);

                                response2.EnsureSuccessStatusCode();
                                string outJson2 = await response2.Content.ReadAsStringAsync();
                                sb.Append(outJson2);

                                //JObject oC = JObject.Parse(outJson2);

                                //Rootobject projectsC = JsonConvert.DeserializeObject<Rootobject>(outJson2);
                                //var projectsarrayC = projectsC.value;

                                TeamsRoot resultRootC = JsonConvert.DeserializeObject<TeamsRoot>(outJson2);
                                var resultArray2C = resultRootC.value;

                                Teams.ResultItems.AddRange(resultArray2C);

                                hasContinuationToken = response2.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);
                            }
                        }

                        jsonResult = sb.ToString();

                        Teams.Count = Teams.ResultItems.Count();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            RequestResponseExchangeCount = RequestResponseExchange.Count();
            return jsonResult;

        }

        private async void GetAllTeams_Click(object sender, RoutedEventArgs e)
        {
            CallResult = "";
            CallResult = await GetAllTeams(SelectedCollection.Details);
        }

        private void GetTeams_Click(object sender, RoutedEventArgs e)
        {
            //CallResult = "";
            //CallResult = await GetAllTeams(SelectedCollection.Details);
        }

        private async Task<string> GetProcessLists(CollectionDetails collection)
        {
            string jsonResult = default;
            StringBuilder sb = new StringBuilder();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    InitializeHttpClient(collection, client);

                    RequestUri = $"{collection.Uri}/_apis/work/processes/lists?api-version=6.0-preview.1";

                    RequestResponseInfo exchange = InitializeExchange(client, RequestUri);

                    using (HttpResponseMessage response = await client.GetAsync(RequestUri))
                    {
                        RecordExchangeResponse(response, exchange);


                        response.EnsureSuccessStatusCode();

                        string outJson = await response.Content.ReadAsStringAsync();
                        //sb.Append(outJson);

                        JObject o = JObject.Parse(outJson);

                        ListsRoot resultRoot = JsonConvert.DeserializeObject<ListsRoot>(outJson);
                        Lists.ResultItems = new ObservableCollection<Domain.List>(resultRoot.value);

                        IEnumerable<string> continuationHeaders = default;

                        bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                        Lists.Count = Lists.ResultItems.Count();

                        jsonResult = outJson;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            RequestResponseExchangeCount = RequestResponseExchange.Count();

            return jsonResult;
        }

        private async void GetLists_Click(object sender, RoutedEventArgs e)
        {
            CallResult = "";
            CallResult = await GetProcessLists(SelectedCollection.Details);
        }

        private async Task<string> GetProcessList(CollectionDetails details, Domain.Process selectedProcess)
        {
            return "";
        }

        private void GetList_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Not Implemented Yet :(");
        }





        private void GetRegionMetaData_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Not Implemented Yet :(");
        }

        private void GetHealth_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Not Implemented Yet :(");
        }



        #region Work Item Tracking Process

        private void GetBehaviors_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Not Implemented Yet :(");
        }

        private async void GetFields_Click(object sender, RoutedEventArgs e)
        {
            CallResult = "";
            CallResult = await GetFields(SelectedCollection.Details, Processes.SelectedItem, WorkItemTypes.SelectedItem);
        }

        private async Task<string> GetFields(CollectionDetails collection,
            Domain.Process selectedProcess,
            Domain.WorkItemType selectedWorkItem)
        {
            string jsonResult = default;
            StringBuilder sb = new StringBuilder();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    InitializeHttpClient(collection, client);

                    RequestUri = $"{collection.Uri}/_apis/work/processes/{selectedProcess.typeId}/workitemtypes/{selectedWorkItem.referenceName}/fields?api-version=6.1-preview.2";

                    RequestResponseInfo exchange = InitializeExchange(client, RequestUri);

                    using (HttpResponseMessage response = await client.GetAsync(RequestUri))
                    {
                        RecordExchangeResponse(response, exchange);

                        response.EnsureSuccessStatusCode();

                        string outJson = await response.Content.ReadAsStringAsync();
                        //sb.Append(outJson);

                        JObject o = JObject.Parse(outJson);

                        FieldsRoot resultRoot = JsonConvert.DeserializeObject<FieldsRoot>(outJson);
                        ////WorkItemTypes = new ObservableCollection<WpfAppCore.Domain.WorkItemType>(resultRoot.value);
                        ////WorkItemTypesCount = WorkItemTypes.Count;

                        Fields.ResultItems = new ObservableCollection<Domain.Field>(resultRoot.value);

                        IEnumerable<string> continuationHeaders = default;

                        bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                        Fields.Count = Fields.ResultItems.Count;

                        //jsonResult = outJson;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            RequestResponseExchangeCount = RequestResponseExchange.Count();

            return jsonResult;
        }

        private async void GetField_Click(object sender, RoutedEventArgs e)
        {
            CallResult = "";
            CallResult = await GetField(SelectedCollection.Details,
                Processes.SelectedItem,
                WorkItemTypes.SelectedItem,
                Fields.SelectedItem);
        }

        //FieldDef FieldDef { get; set;  }

        private FieldDef _fieldDef;
        public FieldDef FieldDef
        {
            get => _fieldDef;
            set
            {
                if (_fieldDef == value)
                    return;
                _fieldDef = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FieldDef)));
            }
        }
        

        private async Task<string> GetField(CollectionDetails collection,
            Domain.Process selectedProcess,
            Domain.WorkItemType selectedWorkItem, 
            Domain.Field selectedField)
        {
            string jsonResult = default;
            StringBuilder sb = new StringBuilder();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    InitializeHttpClient(collection, client);

                    RequestUri = $"{collection.Uri}/_apis/work/processes/{selectedProcess.typeId}/workitemtypes/{selectedWorkItem.referenceName}/fields/{selectedField.referenceName}?api-version=6.1-preview.2";

                    RequestResponseInfo exchange = InitializeExchange(client, RequestUri);

                    using (HttpResponseMessage response = await client.GetAsync(RequestUri))
                    {
                        RecordExchangeResponse(response, exchange);

                        response.EnsureSuccessStatusCode();

                        string outJson = await response.Content.ReadAsStringAsync();
                        //sb.Append(outJson);

                        JObject o = JObject.Parse(outJson);

                         FieldDef = JsonConvert.DeserializeObject<Domain.FieldDef>(outJson);
                        ////WorkItemTypes = new ObservableCollection<WpfAppCore.Domain.WorkItemType>(resultRoot.value);
                        ////WorkItemTypesCount = WorkItemTypes.Count;

                        //FieldDef.ResultItems = new ObservableCollection<Domain.FieldDef>(resultRoot.value);

                        //IEnumerable<string> continuationHeaders = default;

                        //bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                        //Fields.Count = Fields.ResultItems.Count;

                        //jsonResult = outJson;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            RequestResponseExchangeCount = RequestResponseExchange.Count();

            return jsonResult;
        }

        private async void GetStates_Click(object sender, RoutedEventArgs e)
        {
            CallResult = "";
            CallResult = await GetStates(SelectedCollection.Details, Processes.SelectedItem, WorkItemTypes.SelectedItem);
        }

        private async Task<string> GetStates(CollectionDetails collection, Domain.Process selectedProcess, Domain.WorkItemType selectedWorkItem)
        {
            string jsonResult = default;
            StringBuilder sb = new StringBuilder();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    InitializeHttpClient(collection, client);

                    RequestUri = $"{collection.Uri}/_apis/work/processes/{selectedProcess.typeId}/workItemTypes/{selectedWorkItem.referenceName}/states?api-version=6.1-preview.1";

                    RequestResponseInfo exchange = InitializeExchange(client, RequestUri);

                    using (HttpResponseMessage response = await client.GetAsync(RequestUri))
                    {
                        RecordExchangeResponse(response, exchange);

                        response.EnsureSuccessStatusCode();

                        string outJson = await response.Content.ReadAsStringAsync();
                        //sb.Append(outJson);

                        JObject o = JObject.Parse(outJson);

                        StatesRoot resultRoot = JsonConvert.DeserializeObject<StatesRoot>(outJson);
                        ////WorkItemTypes = new ObservableCollection<WpfAppCore.Domain.WorkItemType>(resultRoot.value);
                        ////WorkItemTypesCount = WorkItemTypes.Count;

                        States.ResultItems = new ObservableCollection<Domain.State>(resultRoot.value);

                        IEnumerable<string> continuationHeaders = default;

                        bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                        States.Count = States.ResultItems.Count;

                        //jsonResult = outJson;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            RequestResponseExchangeCount = RequestResponseExchange.Count();

            return jsonResult;
        }

        private async void GetWorkItemTypes_Click(object sender, RoutedEventArgs e)
        {
            CallResult = "";
            CallResult = await GetWorkItemTypes(SelectedCollection.Details, Processes.SelectedItem);
        }

        private async Task<string> GetWorkItemTypes(CollectionDetails collection, Domain.Process selectedProcess)
        {
            string jsonResult = default;
            StringBuilder sb = new StringBuilder();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    InitializeHttpClient(collection, client);

                    RequestUri = $"{collection.Uri}/_apis/work/processes/{selectedProcess.typeId}/workitemtypes?api-version=6.1-preview.2";

                    RequestResponseInfo exchange = InitializeExchange(client, RequestUri);

                    //WorkItemTypes2 = new RestResult<Domain.WorkItemType>();

                    using (HttpResponseMessage response = await client.GetAsync(RequestUri))
                    {
                        RecordExchangeResponse(response, exchange);

                        IEnumerable<string> continuationHeaders = default;

                        bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                        response.EnsureSuccessStatusCode();

                        string outJson = await response.Content.ReadAsStringAsync();
                        //sb.Append(outJson);

                        JObject o = JObject.Parse(outJson);

                        WorkItemTypesRoot resultRoot = JsonConvert.DeserializeObject<WorkItemTypesRoot>(outJson);
                        //WorkItemTypes = new ObservableCollection<WpfAppCore.Domain.WorkItemType>(resultRoot.value);
                        //WorkItemTypesCount = WorkItemTypes.Count;

                        WorkItemTypes.ResultItems = new ObservableCollection<Domain.WorkItemType>(resultRoot.value); ;
                        WorkItemTypes.Count = WorkItemTypes.ResultItems.Count;

                        //jsonResult = outJson;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            RequestResponseExchangeCount = RequestResponseExchange.Count();

            return jsonResult;
        }

        #endregion


        private async void GetTeam_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Not Implemented Yet :(");
        }

        #region Private Helpers

        private RequestResponseInfo InitializeExchange(HttpClient client, string requestUri)
        {
            RequestResponseExchange.Clear();
            RequestResponseInfo exchange = new RequestResponseInfo();
            //RequestHeaders.Clear();
            //ResponseHeaders.Clear();

            //RequestHeaders.AddRange(client.DefaultRequestHeaders);

            exchange.Uri = requestUri;
            exchange.RequestHeadersX.AddRange(client.DefaultRequestHeaders);

            return exchange;
        }

        private void RecordExchangeResponse(HttpResponseMessage response, RequestResponseInfo exchange)
        {
            Response = response;

            var statusCode = response.StatusCode;
            //var statusCodeT = response.StatusCode.GetType();
            //var statusCodeTC = response.StatusCode.GetTypeCode();
            var statusCode2 = (int)response.StatusCode;

            //ResponseHeaders.AddRange(response.Headers);

            exchange.Response = response;
            exchange.ResponseStatusCode = statusCode2;

            exchange.ResponseContentHeaders.AddRange(response.Content.Headers);
            exchange.ResponseHeadersX.AddRange(response.Headers);

            RequestResponseExchange.Add(exchange);
        }

        #endregion

        private void GetState_Click(object sender, RoutedEventArgs e)
        {

        }


    }
}
