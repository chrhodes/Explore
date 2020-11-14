using System;
using System.Collections.Generic;
using System.Linq;

namespace WpfAppCore
{
    public class AvailableCollection
    {
        public string Name { get; set; }
        public CollectionDetails Details { get; set; }
    }

    public class CollectionDetails
    {
        public string Uri { get; set; }
        public string PAT { get; set; }
    }
}
