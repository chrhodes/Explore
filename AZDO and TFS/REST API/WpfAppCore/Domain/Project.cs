using System;
using System.Collections.Generic;
using System.Text;

namespace WpfAppCore.Domain
{


    //public class Project
    //{
    //    public string id { get; set; }
    //    public string name { get; set; }
    //    public string url { get; set; }
    //    public string state { get; set; }
    //    public int revision { get; set; }
    //    public string visibility { get; set; }
    //    public DateTime lastUpdateTime { get; set; }
    //}


    public class ProjectsRoot
    {
        public int count { get; set; }
        public Project[] value { get; set; }
    }

    public class Project
    {
        public string id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string state { get; set; }
        public int revision { get; set; }
        public string visibility { get; set; }
        public DateTime lastUpdateTime { get; set; }
        public string description { get; set; }
    }


    public class Rootobject
    {
        public int count { get; set; }
        public Value[] value { get; set; }
    }

    public class Value
    {
        public string id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string state { get; set; }
        public int revision { get; set; }
        public string visibility { get; set; }
        public DateTime lastUpdateTime { get; set; }
        public string description { get; set; }
    }


}
