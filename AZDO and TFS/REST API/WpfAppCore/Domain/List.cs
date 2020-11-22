using System;
using System.Collections.Generic;
using System.Text;

namespace WpfAppCore.Domain
{

    public class ListsRoot
    {
        public int count { get; set; }
        public List[] value { get; set; }
    }

    public class List
    {
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public bool isSuggested { get; set; }
        public string url { get; set; }
    }


    //public class Rootobject
    //{
    //    public int count { get; set; }
    //    public Value[] value { get; set; }
    //}

    //public class Value
    //{
    //    public string id { get; set; }
    //    public string name { get; set; }
    //    public string type { get; set; }
    //    public bool isSuggested { get; set; }
    //    public string url { get; set; }
    //}

}
