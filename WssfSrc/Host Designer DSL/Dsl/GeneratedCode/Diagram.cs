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
using DslDiagrams = global::Microsoft.VisualStudio.Modeling.Diagrams;

[module: global::System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Scope = "type", Target = "Microsoft.Practices.ServiceFactory.HostDesigner.HostDesignerDiagram")]

namespace Microsoft.Practices.ServiceFactory.HostDesigner
{
	/// <summary>
	/// DomainClass HostDesignerDiagram
	/// Description for
	/// Microsoft.Practices.ServiceFactory.HostDesigner.HostDesignerDiagram
	/// </summary>
	[DslDesign::DisplayNameResource("Microsoft.Practices.ServiceFactory.HostDesigner.HostDesignerDiagram.DisplayName", typeof(global::Microsoft.Practices.ServiceFactory.HostDesigner.HostDesignerDomainModel), "Microsoft.Practices.ServiceFactory.HostDesigner.GeneratedCode.DomainModelResx")]
	[DslDesign::DescriptionResource("Microsoft.Practices.ServiceFactory.HostDesigner.HostDesignerDiagram.Description", typeof(global::Microsoft.Practices.ServiceFactory.HostDesigner.HostDesignerDomainModel), "Microsoft.Practices.ServiceFactory.HostDesigner.GeneratedCode.DomainModelResx")]
	[global::System.CLSCompliant(true)]
	[DslModeling::DomainObjectId("e6b021f0-526f-4dfd-a19d-703fc1f22af6")]
	public partial class HostDesignerDiagram : DslDiagrams::Diagram
	{
		#region Diagram boilerplate
		private static DslDiagrams::StyleSet classStyleSet;
		private static global::System.Collections.Generic.IList<DslDiagrams::ShapeField> shapeFields;
		/// <summary>
		/// Per-class style set for this shape.
		/// </summary>
		protected override DslDiagrams::StyleSet ClassStyleSet
		{
			get
			{
				if (classStyleSet == null)
				{
					classStyleSet = CreateClassStyleSet();
				}
				return classStyleSet;
			}
		}
		
		/// <summary>
		/// Per-class ShapeFields for this shape
		/// </summary>
		public override global::System.Collections.Generic.IList<DslDiagrams::ShapeField> ShapeFields
		{
			get
			{
				if (shapeFields == null)
				{
					shapeFields = CreateShapeFields();
				}
				return shapeFields;
			}
		}
		#endregion
		#region Toolbox filters
		private static global::System.ComponentModel.ToolboxItemFilterAttribute[] toolboxFilters = new global::System.ComponentModel.ToolboxItemFilterAttribute[] {
					new global::System.ComponentModel.ToolboxItemFilterAttribute(global::Microsoft.Practices.ServiceFactory.HostDesigner.HostDesignerToolboxHelperBase.ToolboxFilterString, global::System.ComponentModel.ToolboxItemFilterType.Require) };
		
		/// <summary>
		/// Toolbox item filter attributes for this diagram.
		/// </summary>
		public override global::System.Collections.ICollection TargetToolboxItemFilterAttributes
		{
			get
			{
				return toolboxFilters;
			}
		}
		#endregion
		#region Auto-placement
		/// <summary>
		/// Indicate that child shapes should added through view fixup should be placed automatically.
		/// </summary>
		public override bool ShouldAutoPlaceChildShapes
		{
			get
			{
				return true;
			}
		}
		#endregion
		#region Constructors, domain class Id
	
		/// <summary>
		/// HostDesignerDiagram domain class Id.
		/// </summary>
		public static readonly new global::System.Guid DomainClassId = new global::System.Guid(0xe6b021f0, 0x526f, 0x4dfd, 0xa1, 0x9d, 0x70, 0x3f, 0xc1, 0xf2, 0x2a, 0xf6);
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="store">Store where new element is to be created.</param>
		/// <param name="propertyAssignments">List of domain property id/value pairs to set once the element is created.</param>
		public HostDesignerDiagram(DslModeling::Store store, params DslModeling::PropertyAssignment[] propertyAssignments)
			: this(store != null ? store.DefaultPartition : null, propertyAssignments)
		{
		}
		
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="partition">Partition where new element is to be created.</param>
		/// <param name="propertyAssignments">List of domain property id/value pairs to set once the element is created.</param>
		public HostDesignerDiagram(DslModeling::Partition partition, params DslModeling::PropertyAssignment[] propertyAssignments)
			: base(partition, propertyAssignments)
		{
		}
		#endregion
	}
}
namespace Microsoft.Practices.ServiceFactory.HostDesigner
{
	
	}
