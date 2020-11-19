using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
// https://www.nuget.org/packages/Microsoft.TeamFoundationServer.Client/

// https://www.nuget.org/packages/Microsoft.VisualStudio.Services.InteractiveClient/

// https://www.nuget.org/packages/Microsoft.VisualStudio.Services.Client/

namespace WpfAppCore
{
    public class RequestResponseInfo
    {
        private string _uri;

        public string Uri
        {
            get => _uri;
            set => _uri = value;
        }

        public ObservableCollection<KeyValuePair<string, IEnumerable<string>>> RequestHeadersX { get; set; }
            = new ObservableCollection<KeyValuePair<string, IEnumerable<string>>>();

        private HttpResponseMessage _Response;

        public HttpResponseMessage Response
        {
            get => _Response;
            set => _Response = value;
        }

        private Int32 _ResponseStatusCode;

        public Int32 ResponseStatusCode
        {
            get => _ResponseStatusCode;
            set => _ResponseStatusCode = value;
        }

        public ObservableCollection<KeyValuePair<string, IEnumerable<string>>> ResponseHeadersX { get; set; }
            = new ObservableCollection<KeyValuePair<string, IEnumerable<string>>>();

        public ObservableCollection<KeyValuePair<string, IEnumerable<string>>> ResponseContentHeaders { get; set; }
            = new ObservableCollection<KeyValuePair<string, IEnumerable<string>>>();
    }

}
