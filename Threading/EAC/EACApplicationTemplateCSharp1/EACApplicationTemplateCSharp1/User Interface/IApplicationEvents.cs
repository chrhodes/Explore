using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EACApplicationTemplateCSharp1.User_Interface
{
    interface IApplicationEvents
    {
        //void InitializeApplicationEventHandlers;
        event EventHandler  ToggleDebugMode;
        event EventHandler  ToggleDeveloperMode;
        //void RemoveApplicationEventHandlers;
    }
}
