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
using Microsoft.Practices.Modeling.CodeGeneration.Artifacts;
using System.IO;

namespace Microsoft.Practices.UnitTestLibrary.Utilities
{
	public class AssemblyResourceResolver : IResourceResolver
	{

		#region IResourceResolver Members

		public string GetResourcePath(string resourceItem)
		{
			string path = Path.GetDirectoryName(GetType().Assembly.Location);
			return Path.Combine(path, resourceItem);
		}

		public string GetResource(string resourceItem)
		{
			return File.ReadAllText(GetResourcePath(resourceItem));
		}

		#endregion
	}
}
