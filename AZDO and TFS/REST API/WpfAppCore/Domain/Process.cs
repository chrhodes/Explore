using System;
using System.Collections.Generic;
using System.Text;

namespace WpfAppCore.Domain
{
    public class ProcessesRoot
    {
        public int count { get; set; }
        public Process[] value { get; set; }
    }

    public class Process
    {
        public string typeId { get; set; }
        public string name { get; set; }
        public string referenceName { get; set; }
        public string description { get; set; }
        public string parentProcessTypeId { get; set; }
        public bool isEnabled { get; set; }
        public bool isDefault { get; set; }
        public string customizationType { get; set; }
    }

    //public class Rootobject
    //{
    //    public int count { get; set; }
    //    public Value[] value { get; set; }
    //}

    //public class Value
    //{
    //    public string typeId { get; set; }
    //    public string name { get; set; }
    //    public string referenceName { get; set; }
    //    public string description { get; set; }
    //    public string parentProcessTypeId { get; set; }
    //    public bool isEnabled { get; set; }
    //    public bool isDefault { get; set; }
    //    public string customizationType { get; set; }
    //}


    //public class Rootobject
    //{
    //    public string typeId { get; set; }
    //    public object referenceName { get; set; }
    //    public string name { get; set; }
    //    public string description { get; set; }
    //    public Properties properties { get; set; }
    //}

    //public class Properties
    //{
    //    public string _class { get; set; }
    //    public string parentProcessTypeId { get; set; }
    //    public bool isEnabled { get; set; }
    //    public string version { get; set; }
    //    public bool isDefault { get; set; }
    //}


}
