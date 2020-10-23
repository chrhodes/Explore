using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
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

namespace WpfAppNF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
		static string callResult;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GetProjects();
			result.Text = callResult;
			
        }

		public static async void GetProjects()
		{
			try
			{
				var personalaccesstoken = "ktjisgkllgewl4lkazadc3ggj5rouzpkmwrieauz6sss62ajhl4a";

				using (HttpClient client = new HttpClient())
				{
					client.DefaultRequestHeaders.Accept.Add(
						new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

					client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
						Convert.ToBase64String(
							System.Text.ASCIIEncoding.ASCII.GetBytes(
								string.Format("{0}:{1}", "", personalaccesstoken))));

					using (HttpResponseMessage response = await client.GetAsync(
								"https://dev.azure.com/BD-STS-PROD/_apis/projects"))
					{
						response.EnsureSuccessStatusCode();
						string responseBody = await response.Content.ReadAsStringAsync();
						Debug.WriteLine(responseBody);
						callResult = responseBody;
					}
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
			}
		}
	}
}
