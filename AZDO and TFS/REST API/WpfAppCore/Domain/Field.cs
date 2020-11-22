using System;
using System.Collections.Generic;
using System.Text;

namespace WpfAppCore.Domain
{
    public class FieldsRoot
    {
        public int count { get; set; }
        public Field[] value { get; set; }
    }

    public class Field
    {
        public string referenceName { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string url { get; set; }
        public string customization { get; set; }
        public string description { get; set; }
        public bool required { get; set; }
        public string defaultValue { get; set; }
    }

    public class FieldDef
    {
        public string referenceName { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public bool required { get; set; }
        public string defaultValue { get; set; }
        public string url { get; set; }
        public string customization { get; set; }
    }


    //public class Rootobject
    //{
    //    public string referenceName { get; set; }
    //    public string name { get; set; }
    //    public string type { get; set; }
    //    public bool required { get; set; }
    //    public string defaultValue { get; set; }
    //    public string url { get; set; }
    //    public string customization { get; set; }
    //}


    //public class Rootobject
    //{
    //    public int count { get; set; }
    //    public Value[] value { get; set; }
    //}

    //public class Value
    //{
    //    public string referenceName { get; set; }
    //    public string name { get; set; }
    //    public string type { get; set; }
    //    public string url { get; set; }
    //    public string customization { get; set; }
    //    public string description { get; set; }
    //    public bool required { get; set; }
    //    public string defaultValue { get; set; }
    //}

}
