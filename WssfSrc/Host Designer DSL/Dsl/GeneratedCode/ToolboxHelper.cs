//===============================================================================
// Microsoft patterns & practices
// Web Service Software Factory
//===============================================================================
// Copyright � Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//===============================================================================

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using DslModeling = global::Microsoft.VisualStudio.Modeling;
using DslDesign = global::Microsoft.VisualStudio.Modeling.Design;

namespace Microsoft.Practices.ServiceFactory.HostDesigner
{
	/// <summary>
	/// Helper class used to create and initialize toolbox items for this DSL.
	/// </summary>
	/// <remarks>
	/// Double-derived class to allow easier code customization.
	/// </remarks>
	public partial class HostDesignerToolboxHelper : HostDesignerToolboxHelperBase 
	{
		/// <summary>
		/// Constructs a new HostDesignerToolboxHelper.
		/// </summary>
		public HostDesignerToolboxHelper(global::System.IServiceProvider serviceProvider)
			: base(serviceProvider) { }
	}
	
	/// <summary>
	/// Helper class used to create and initialize toolbox items for this DSL.
	/// </summary>
	public abstract class HostDesignerToolboxHelperBase
	{
		/// <summary>
		/// Toolbox item filter string used to identify HostDesigner toolbox items.  
		/// </summary>
		/// <remarks>
		/// See the MSDN documentation for the ToolboxItemFilterAttribute class for more information on toolbox
		/// item filters.
		/// </remarks>
		public const string ToolboxFilterString = "HostDesigner.1.0";

		private global::System.IServiceProvider sp;
		
		/// <summary>
		/// Constructs a new HostDesignerToolboxHelperBase.
		/// </summary>
		protected HostDesignerToolboxHelperBase(global::System.IServiceProvider serviceProvider)
		{
			if(serviceProvider == null) throw new global::System.ArgumentNullException("serviceProvider");
			
			this.sp = serviceProvider;
		}
		
		/// <summary>
		/// Serivce provider used to access services from the hosting environment.
		/// </summary>
		protected global::System.IServiceProvider ServiceProvider
		{
			get
			{
				return this.sp;
			}
		}

		/// <summary>
		/// Returns the display name of the tab that should be opened by default when the editor is opened.
		/// </summary>
		public static string DefaultToolboxTabName
		{
			get
			{
				return string.Empty;
			}
		}
		
		
		/// <summary>
		/// Returns the toolbox items count in the default tool box tab.
		/// </summary>
		public static int DefaultToolboxTabToolboxItemsCount
		{
			get
			{
				return 0;
			}
		}
		

		/// <summary>
		/// Returns a list of toolbox items for use with this DSL.
		/// </summary>
		public virtual global::System.Collections.Generic.IList<DslDesign::ModelingToolboxItem> CreateToolboxItems()
		{
			global::System.Collections.Generic.List<DslDesign::ModelingToolboxItem> toolboxItems = new global::System.Collections.Generic.List<DslDesign::ModelingToolboxItem>();
			

			return toolboxItems;
		}
		
		/// <summary>
		/// Creates an ElementGroupPrototype for the element tool corresponding to the given domain class id.
		/// Default behavior is to create a prototype containing an instance of the domain class.
		/// Derived classes may override this to add additional information to the prototype.
		/// </summary>
		protected virtual DslModeling::ElementGroupPrototype CreateElementToolPrototype(DslModeling::Store store, global::System.Guid domainClassId)
		{
			DslModeling::ModelElement element = store.ElementFactory.CreateElement(domainClassId);
			DslModeling::ElementGroup elementGroup = new DslModeling::ElementGroup(store.DefaultPartition);
			elementGroup.AddGraph(element, true);
			return elementGroup.CreatePrototype();
		}
	}
}
