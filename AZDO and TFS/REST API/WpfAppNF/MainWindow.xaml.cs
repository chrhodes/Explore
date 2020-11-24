using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
// https://www.nuget.org/packages/Microsoft.TeamFoundationServer.Client/
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

// https://www.nuget.org/packages/Microsoft.VisualStudio.Services.InteractiveClient/
using Microsoft.VisualStudio.Services.Client;

// https://www.nuget.org/packages/Microsoft.VisualStudio.Services.Client/
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.OAuth;
using Microsoft.VisualStudio.Services.WebApi;

namespace WpfAppNF
{

    public partial class MainWindow : Window
    {
		static string callResult;

        private string[] _fruitsP = { "AppleP", "OrangeP", "PearP" };
        static string _callResult;
        private static string _URI_BD_STS_QA2 = @"https://dev.azure.com/BD-STS-QA2";
        private static string _URI_BD_STS_PROD = @"https://dev.azure.com/BD-STS-PROD";
        private static string _URI_VNC_Development = @"https://dev.azure.com/VNC-Development";

        private static string _teamProjectName = "TFS";

        private static string _PAT_BD_STS_PROD = "";

        private static string _PAT_BD_STS_QA2 = "";

        private static string _PAT_VNC_Development = "";

        //public Dictionary<string, CollectionDetails> AvailableCollections;

        private Dictionary<string, CollectionDetails> _availableCollections;
        public Dictionary<string, CollectionDetails> AvailableCollections
        {
            get => _availableCollections;
            set => _availableCollections = value;
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private CollectionDetails _SelectedCollection;

        public CollectionDetails SelectedCollection
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
        }

        private void LoadCollections()
        {
            AvailableCollections = new Dictionary<string, CollectionDetails>
            {
                { "BD_STS_PROD",
                    new CollectionDetails { Uri=@"https://dev.azure.com/BD-STS-PROD", PAT=_PAT_BD_STS_PROD } },
                { "BD_STS_QA2",
                    new CollectionDetails { Uri=@"https://dev.azure.com/BD-STS-QA2", PAT=_PAT_BD_STS_QA2 } },
                { "VNC-Development",
                    new CollectionDetails { Uri=@"https://dev.azure.com/VNC-Development", PAT=_PAT_VNC_Development } }
            };
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GetProjects();
			result.Text = callResult;
			
        }

        private void OnSelectedCollectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedCollection = CB1.SelectedValue as CollectionDetails;
        }

        private void GetProjects_Click(object sender, RoutedEventArgs e)
        {
            result.Text = "";
            GetProjects();
            result.Text = _callResult;
        }

        private void GetProcessList_Click(object sender, RoutedEventArgs e)
        {
            result.Text = "";
            //GetProcessList(_VNC_Development, _PAT_VNC_Development);
            result.Text = _callResult;
        }

        private void SampleREST_Click(object sender, RoutedEventArgs e)
        {
            result.Text = "";
            SampleREST();
            result.Text = _callResult;
        }

        private void PersonalAccessToken_Click(object sender, RoutedEventArgs e)
        {
            result.Text = "";
            PersonalAccessTokenRestSample();
            result.Text = _callResult;
        }

        private void MicrosoftAccount_Click(object sender, RoutedEventArgs e)
        {
            result.Text = "";
            MicrosoftAccountRestSample();
            result.Text = _callResult;
        }

        private void OAuth_Click(object sender, RoutedEventArgs e)
        {
            result.Text = "";
            OAuthSample();
            result.Text = _callResult;
        }

        private void AzureActiveDirectory_Click(object sender, RoutedEventArgs e)
        {
            result.Text = "";
            AADRestSample();
            result.Text = _callResult;
        }

        public static async void GetProcessList(string collectionUri)
        {
            var _personalaccesstoken = _PAT_BD_STS_QA2;
            var _collectionUri = _URI_BD_STS_QA2;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(
                        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    string base64PAT = Convert.ToBase64String(
                            System.Text.ASCIIEncoding.ASCII.GetBytes(
                                string.Format("{0}:{1}", "", _personalaccesstoken)));

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64PAT);

                    using (HttpResponseMessage response = await client.GetAsync($"{_collectionUri}/_apis/work/processes?api-version=6.0-preview.2"))
                    {
                        response.EnsureSuccessStatusCode();
                        string responseBody = await response.Content.ReadAsStringAsync();
                        Debug.WriteLine(responseBody);
                        _callResult = responseBody;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        public static async void GetProjects()
        {
            var _personalaccesstoken = _PAT_BD_STS_QA2;
            var _collectionUri = _URI_BD_STS_QA2;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(
                        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    string base64PAT = Convert.ToBase64String(
                            System.Text.ASCIIEncoding.ASCII.GetBytes(
                                string.Format("{0}:{1}", "", _personalaccesstoken)));

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64PAT);

                    using (HttpResponseMessage response = await client.GetAsync($"{_collectionUri}/_apis/projects"))
                    {
                        response.EnsureSuccessStatusCode();
                        string responseBody = await response.Content.ReadAsStringAsync();
                        Debug.WriteLine(responseBody);
                        _callResult = responseBody;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// This sample creates a new work item query for New Bugs, stores it under 'MyQueries', runs the query, and then sends the results to the console.
        /// </summary>
        public static void SampleREST()
        {
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

            _callResult = sb.ToString();

        }

        public static void PersonalAccessTokenRestSample()
        {
            var _personalaccesstoken = _PAT_BD_STS_QA2;
            var _collectionUri = _URI_BD_STS_QA2;

            // Create instance of VssConnection using Personal Access Token
            VssConnection connection = new VssConnection(new Uri(_collectionUri), new VssBasicCredential(string.Empty, _personalaccesstoken));
            _callResult = connection.ServerId.ToString();
        }

        public static void MicrosoftAccountRestSample()
        {
            var _collectionUri = _URI_BD_STS_QA2;

            // Create instance of VssConnection using Visual Studio sign-in prompt
            VssConnection connection = new VssConnection(new Uri(_collectionUri), new VssClientCredentials());
            _callResult = connection.ServerId.ToString();
        }

        public static void AADRestSample()
        {
            // Create instance of VssConnection using Azure AD Credentials for Azure AD backed account
            //VssConnection connection = new VssConnection(new Uri(_collectionUri), new VssAadCredential(userName, password));
        }

        public static void OAuthSample()
        {
            // Create instance of VssConnection using OAuth Access token
            //VssConnection connection = new VssConnection(new Uri(_collectionUri), new VssOAuthAccessTokenCredential(accessToken));
        }
    }
}
