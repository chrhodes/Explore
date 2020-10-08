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

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.Modeling.Validation;

namespace Microsoft.Practices.ServiceFactory.Validation
{
	/// <summary>
	/// Support for continuing validation after crossing a model boundary with the ModelReferenceValidator.
	/// </summary>
	/// <remarks> 
	/// ModelElements that are linked to from other models need to implement this interface if they are
	/// to support cross-model validation. 
	/// </remarks>
	public interface IValidatableElement
	{
		void ContinueValidation(ValidationContext context);
	}
}
