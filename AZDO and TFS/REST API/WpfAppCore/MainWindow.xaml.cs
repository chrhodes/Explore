﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

using Newtonsoft.Json.Linq;

namespace WpfAppCore
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        private static string _URI_BD_STS_QA2 = @"https://dev.azure.com/BD-STS-QA2";
        private static string _URI_BD_STS_PROD = @"https://dev.azure.com/BD-STS-PROD";
        private static string _URI_VNC_Development = @"https://dev.azure.com/VNC-Development";

        private static string _teamProjectName = "TFS";

        private static string _PAT_BD_STS_PROD = "ktjisgkllgewl4lkazadc3ggj5rouzpkmwrieauz6sss62ajhl4a";

        //private static string _PAT_BD_STS_QA2 = "bjllb2zrs3izlgbvh5q2tqr4neudouk5yidulwn52boc5mkl3cwa";
        private static string _PAT_BD_STS_QA2 = "3mesoqd2xxsgxvx6fqsdc7h2fhswo2ohpa2t6aennimb3x7wr6vq";

        private static string _PAT_VNC_Development = "ssyqqvap35hunafmt6abskzgcrqroldvlhwrwl3hcjh3oo7mf5yq";

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

        public ObservableCollection<KeyValuePair<string, IEnumerable<string>>> ResponseHeaders { get; set; }
            = new ObservableCollection<KeyValuePair<string, IEnumerable<string>>>();


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
                new AvailableCollection { 
                        Name="BD_STS_PROD", 
                        Details= new CollectionDetails { 
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
        }

        private async void GetProjects_Click(object sender, RoutedEventArgs e)
        {
            CallResult = "";
            CallResult = await GetProjects(SelectedCollection.Details);
		}

        private async void GetProcessList_Click(object sender, RoutedEventArgs e)
        {
            CallResult = "";
            CallResult = await GetProcessList(SelectedCollection.Details);
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

        public async Task<string> GetProcessList(CollectionDetails collection)
        {
            string jsonResult = default;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    string base64PAT = Convert.ToBase64String(
                            ASCIIEncoding.ASCII.GetBytes(
                                string.Format("{0}:{1}", "", collection.PAT)));

                    client.DefaultRequestHeaders.Authorization 
                        = new AuthenticationHeaderValue("Basic", base64PAT);

                    ResponseHeaders.Clear();

                    using (HttpResponseMessage response 
                        = await client.GetAsync($"{collection.Uri}/_apis/work/processes?api-version=6.0-preview.2"))
                    {
                        var headers = response.Headers;

                        IEnumerable<string> continuationHeaders = default;

                        bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);



                        //foreach (var item in headersList)
                        //{
                        //    ResponseHeaders.Add(item);
                        //}

                        ResponseHeaders.AddRange(headers);

                        // This does not fire any event.
                        //ResponseHeaders = new ObservableCollection<KeyValuePair<string, IEnumerable<string>>>(headersList);

                        response.EnsureSuccessStatusCode();
                        string responseBody = await response.Content.ReadAsStringAsync();

                        jsonResult = responseBody;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

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
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    string base64PAT = Convert.ToBase64String(
                            ASCIIEncoding.ASCII.GetBytes(
                                string.Format("{0}:{1}", "", collection.PAT)));

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64PAT);

                    ResponseHeaders.Clear();

                    using (HttpResponseMessage response = await client.GetAsync($"{collection.Uri}/_apis/projects?api-version=6.1-preview.4"))
                    {
                        ResponseHeaders.AddRange(response.Headers);
                        //var headers = response.Headers;

                        //var headersList = response.Headers.ToList();

                        //foreach (var item in headersList)
                        //{
                        //    ResponseHeaders.Add(item);
                        //}

                        response.EnsureSuccessStatusCode();
                        string outJson = await response.Content.ReadAsStringAsync();
                        sb.Append(outJson);

                        JObject o = JObject.Parse(outJson);

                        foreach (var p in o)
                        {
                            var foo = p.Key;

                            if (foo == "count")
                            {
                                Debug.WriteLine(p.Value);
                            }

                            if (foo == "value")
                            {
                                foreach (var t in p.Value)
                                {
                                    Debug.WriteLine(t.ToString());
                                }

                            }
                            var bar = p.Value;
                        }

                        //dynamic jsonArray = JArray.Parse(outJson);

                        //dynamic json = JValue.Parse(outJson);

                        IEnumerable<string> continuationHeaders = default;

                        bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                        while (hasContinuationToken)
                        {
                            string continueToken = continuationHeaders.First();

                            using (HttpResponseMessage response2 = await client.GetAsync($"{collection.Uri}/_apis/projects?api-version=6.1-preview.4&continuationToken={continueToken}"))
                            {
                                ResponseHeaders.AddRange(response2.Headers);

                                response2.EnsureSuccessStatusCode();
                                string outJson2 = await response2.Content.ReadAsStringAsync();
                                sb.Append(outJson2);



                                hasContinuationToken = response2.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);
                            }
                        }

                        //response.EnsureSuccessStatusCode();
                        //string responseBody = await response.Content.ReadAsStringAsync();

                        //if (hasContinuationToken)
                        //{
                        //    string continueToken1 = continuationHeaders.First();

                        //    HttpResponseMessage response2 = await client.GetAsync($"{collection.Uri}/_apis/projects?api-version=6.1-preview.4&continuationToken={continueToken1}");

                        //    response2.EnsureSuccessStatusCode();
                        //    string responseBody2 = await response2.Content.ReadAsStringAsync();

                        //    var headers2 = response2.Headers;

                        //    IEnumerable<string> continuationHeaders2 = default;

                        //    bool hasContinuationToken2 = response2.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders2);

                        //    if (hasContinuationToken2)
                        //    {
                        //        string continueToken2 = continuationHeaders2.First();

                        //        HttpResponseMessage response3 = await client.GetAsync($"{collection.Uri}/_apis/projects?api-version=6.1-preview.4&continuationToken={continueToken2}");

                        //        response3.EnsureSuccessStatusCode();
                        //        string responseBody3 = await response3.Content.ReadAsStringAsync();

                        //        var headers3 = response3.Headers;

                        //        IEnumerable<string> continuationHeaders3 = default;

                        //        bool hasContinuationToken3 = response3.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders3);

                        //        if (hasContinuationToken3)
                        //        {
                        //            string continueToken3 = continuationHeaders2.First();

                        //            HttpResponseMessage response4 = await client.GetAsync($"{collection.Uri}/_apis/projects?api-version=6.1-preview.4&continuationToken={continueToken3}");

                        //            var headers4 = response4.Headers;

                        //            IEnumerable<string> continuationHeaders4 = default;

                        //            bool hasContinuationToken4 = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders4);
                        //        }
                        //    }
                        //}

                        //jsonResult = responseBody;
                        jsonResult = sb.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            return jsonResult;
        }

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

    }
}
