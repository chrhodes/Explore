//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.312
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Microsoft.Practices.ServiceFactory.HostDesigner {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Microsoft.Practices.ServiceFactory.HostDesigner.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The Client Application represents the application that consumes the service. The Client Application contains one or more proxies. 
        ///
        ///To add a proxy, using the Host Explorer, right-click on your client application and choose Add New Proxy..
        /// </summary>
        internal static string ClientApplicationGuidance {
            get {
                return ResourceManager.GetString("ClientApplicationGuidance", resourceCulture);
            }
        }
        
        internal static System.Drawing.Bitmap DefaultHostModelViewIcon {
            get {
                object obj = ResourceManager.GetObject("DefaultHostModelViewIcon", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Describes in a standard-based way where messages should be sent, how they should be sent, and what the messages should look like..
        /// </summary>
        internal static string EndpointGuidance {
            get {
                return ResourceManager.GetString("EndpointGuidance", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Failed to load ‘{0}’ guidance package..
        /// </summary>
        internal static string GuidancePackageLoedFailure {
            get {
                return ResourceManager.GetString("GuidancePackageLoedFailure", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The Host Application represents the application that will accept request messages and send response messages. The Host Application contains one or more Service References. 
        ///
        ///To add a Service Reference, using the Host Explorer, right-click on your Host Application and choose Add New Service Reference..
        /// </summary>
        internal static string HostApplicationGuidance {
            get {
                return ResourceManager.GetString("HostApplicationGuidance", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The Host Model defines service endpoints and consumers. The Host Model supports the Visual Studio .NET Web project and the IIS Web application. It can be extended with other types of bindings and WCF self-hosting scenarios. It also provides the features required to generate the code and configuration for the host application, service endpoints, and client proxies. 
        ///
        ///To get started, use the Host Explorer to add a Host Application and a Client Application. To do this, right click on the top-level Host Model [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string HostModelGuidandace {
            get {
                return ResourceManager.GetString("HostModelGuidandace", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The Proxy represents the part of the client application that consumes the service..
        /// </summary>
        internal static string ProxyGuidance {
            get {
                return ResourceManager.GetString("ProxyGuidance", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An unexpected error occurred while executing &apos;{0}&apos; recipe..
        /// </summary>
        internal static string RecipeExecutionError {
            get {
                return ResourceManager.GetString("RecipeExecutionError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The Service Reference associates the service implementation shape from the service contract designer with the application that will host the service. A Service Reference contains one or more Endpoints. 
        ///
        ///To add an Endpoint, using the Host Explorer, right-click on your service reference and choose Add New Endpoint..
        /// </summary>
        internal static string ServiceReferenceGuidance {
            get {
                return ResourceManager.GetString("ServiceReferenceGuidance", resourceCulture);
            }
        }
    }
}
