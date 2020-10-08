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
using System.CodeDom.Compiler;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.Practices.UnitTestLibrary.Utilities
{
	public static class DynamicCompilation
	{
		/// <summary>
		/// Compiles the assembly from source.
		/// </summary>
		/// <param name="source">The source.</param>
		/// <param name="referencedAssemblies">The referenced assemblies.</param>
		/// <returns></returns>
		public static CompilerResults CompileAssemblyFromSource(
			string source,
			string[] referencedAssemblies)
		{
			return CompileAssemblyFromSource(new string[] { source }, null, new CSharp.CSharpCodeProvider(), referencedAssemblies);
		}

		public static CompilerResults CompileAssemblyFromSource(
			string[] sources,
			string[] referencedAssemblies)
		{
			return CompileAssemblyFromSource(sources, null, new CSharp.CSharpCodeProvider(), referencedAssemblies);
		}

		public static CompilerResults CompileAssemblyFromSource(
			string source,
			string outputAssemblyName,
			CodeDomProvider provider,
			string[] referencedAssemblies)
		{
			return CompileAssemblyFromSource(new string[] { source }, outputAssemblyName, provider, referencedAssemblies);
		}

		/// <summary>
		/// Compiles the assembly from source.
		/// </summary>
		/// <param name="source">The source.</param>
		/// <param name="outputAssemblyName">Name of the output assembly.</param>
		/// <param name="provider">The provider.</param>
		/// <param name="referencedAssemblies">The referenced assemblies.</param>
		/// <returns></returns>
		public static CompilerResults CompileAssemblyFromSource(
			string[] sources,
			string outputAssemblyName,
			CodeDomProvider provider, 
			string[] referencedAssemblies)
		{
			CompilerParameters parameters = new CompilerParameters();
			parameters.GenerateExecutable = false;
			parameters.GenerateInMemory = string.IsNullOrEmpty(outputAssemblyName);
			parameters.IncludeDebugInformation = false;
			parameters.TreatWarningsAsErrors = true;
			parameters.CompilerOptions = "/optimize";
			parameters.OutputAssembly = outputAssemblyName;
			if (referencedAssemblies != null &&
				referencedAssemblies.Length > 0)
			{
				parameters.ReferencedAssemblies.AddRange(referencedAssemblies);
			}		
	
			CompilerResults results = provider.CompileAssemblyFromSource(parameters, sources);
			Assert.IsFalse(results.Errors.HasErrors, "Errors in CompileAssemblyFromSource: " + (results.Errors.HasErrors ? results.Errors[0].ErrorText : ""));

			return results;
		}
	}
}
