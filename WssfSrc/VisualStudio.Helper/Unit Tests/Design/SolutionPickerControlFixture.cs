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

// The following code was generated by Microsoft Visual Studio 2005.
// The test owner should check each test for validity.
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.Practices.VisualStudio.Helper.Design;
using System.Windows.Forms;
using Microsoft.Practices.VisualStudio.Helper;
using Microsoft.Practices.UnitTestLibrary;

namespace Microsoft.Practices.VisualStudio.Helper.Tests
{
	[TestClass]
	public class SolutionPickerControlFixture
	{
		internal static TControl GetControl<TControl>(Control.ControlCollection collection)
			where TControl : Control
		{
			foreach (Control c in collection)
			{
				if (c is TreeView)
				{
					return (TControl)c;
				}
			}
			return default(TControl);
		}

		[TestMethod]
		public void TestDefaultConstructor()
		{
			SolutionPickerControl target = new SolutionPickerControl();
			Assert.IsNotNull(target);
		}

	}
}
