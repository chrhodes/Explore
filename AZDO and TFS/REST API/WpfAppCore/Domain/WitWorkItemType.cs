using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace WpfAppCore.Domain
{

    public class WitWorkItemTypesRoot
    {
        public int count { get; set; }
        public WitValue[] value { get; set; }
    }

    public class WitValue
    {
        public string name { get; set; }
        public string referenceName { get; set; }
        public string description { get; set; }
        public string color { get; set; }
        public Icon icon { get; set; }
        public bool isDisabled { get; set; }
        public string xmlForm { get; set; }
        public WitField[] fields { get; set; }
        public Fieldinstance[] fieldInstances { get; set; }
        public Transitions transitions { get; set; }
        public WitState[] states { get; set; }
        public string url { get; set; }
    }

    public class Icon
    {
        public string id { get; set; }
        public string url { get; set; }
    }

    public class Transitions
    {
        public Doing[] Doing { get; set; }
        public Done[] Done { get; set; }
        public ToDo[] ToDo { get; set; }
        public Child[] _ { get; set; }
        public Closed[] Closed { get; set; }
        public Design[] Design { get; set; }
        public Ready[] Ready { get; set; }
        public Active[] Active { get; set; }
        public Inactive[] Inactive { get; set; }
        public Completed[] Completed { get; set; }
        public InPlanning[] InPlanning { get; set; }
        public InProgress[] InProgress { get; set; }
        public Requested[] Requested { get; set; }
        public Accepted[] Accepted { get; set; }
    }

    public class Doing
    {
        public string to { get; set; }
        public string[] actions { get; set; }
    }

    public class Done
    {
        public string to { get; set; }
        public object actions { get; set; }
    }

    public class ToDo
    {
        public string to { get; set; }
        public string[] actions { get; set; }
    }

    public class Child
    {
        public string to { get; set; }
        public object actions { get; set; }
    }

    public class Closed
    {
        public string to { get; set; }
        public string[] actions { get; set; }
    }

    public class Design
    {
        public string to { get; set; }
        public object actions { get; set; }
    }

    public class Ready
    {
        public string to { get; set; }
        public object actions { get; set; }
    }

    public class Active
    {
        public string to { get; set; }
        public object actions { get; set; }
    }

    public class Inactive
    {
        public string to { get; set; }
        public object actions { get; set; }
    }

    public class Completed
    {
        public string to { get; set; }
        public object actions { get; set; }
    }

    public class InPlanning
    {
        public string to { get; set; }
        public object actions { get; set; }
    }

    public class InProgress
    {
        public string to { get; set; }
        public object actions { get; set; }
    }

    public class Requested
    {
        public string to { get; set; }
        public string[] actions { get; set; }
    }

    public class Accepted
    {
        public string to { get; set; }
        public string[] actions { get; set; }
    }

    public class WitField
    {
        public string defaultValue { get; set; }
        public string helpText { get; set; }
        public bool alwaysRequired { get; set; }
        public string referenceName { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Fieldinstance
    {
        public string defaultValue { get; set; }
        public string helpText { get; set; }
        public bool alwaysRequired { get; set; }
        public string referenceName { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class WitState
    {
        public string name { get; set; }
        public string color { get; set; }
        public string category { get; set; }
    }

}
