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
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using Microsoft.Practices.RecipeFramework.Library;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;

namespace Microsoft.Practices.Modeling.ExtensionProvider.Serialization
{
	/// <summary>
	/// Helper class for type serialization
	/// </summary>
	/// <remarks>
	/// Uses <see cref="XmlSerializer"/> and fallback to <see cref="BinaryFormatter"/> 
	/// if a type does not support XML serialization.
	/// </remarks>
	public static class GenericSerializer
	{
		private static IDictionary<string, XmlSerializer> serializerCache = new Dictionary<string, XmlSerializer>();

		#region Public Implementation
		/// <summary>
		/// Deserializes the specified data.
		/// </summary>
		/// <param name="data">The string representation of the type.</param>
		/// <returns></returns>
		public static T Deserialize<T>(string data)
		{			
			Guard.ArgumentNotNullOrEmptyString(data, "data");

			Type[] types = { };
			return Deserialize<T>(types, data);
		}

		/// <summary>
		/// Deserializes the specified types.
		/// </summary>
		/// <param name="types">The types.</param>
		/// <param name="data">The string representation of the type.</param>
		/// <returns></returns>
		public static T Deserialize<T>(Type[] types, string data)
		{
			Guard.ArgumentNotNull(types, "types");
			Guard.ArgumentNotNullOrEmptyString(data, "data");

			if (Uri.IsHexDigit(data[0]))
			{
				// we have a binary formatter base64 encoded
				BinaryFormatter formatter = new BinaryFormatter();
				using (MemoryStream stream = new MemoryStream(Convert.FromBase64String(data)))
				{
					return (T)formatter.Deserialize(stream);
				}
			}

			// we have an xml serialized string
			XmlSerializer serializer = GetXmlSerializer(typeof(T), types);
			using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(data)))
			{
				return (T)serializer.Deserialize(stream);
			}
		}

		/// <summary>
		/// Deserializes the specified file info.
		/// </summary>
		/// <param name="fileInfo">The file info.</param>
		/// <returns></returns>
		public static T Deserialize<T>(FileInfo fileInfo)
		{
			Guard.ArgumentNotNull(fileInfo, "fileInfo");

			Type[] types = { };
			return Deserialize<T>(types, fileInfo);
		}

		/// <summary>
		/// Deserializes the specified types.
		/// </summary>
		/// <param name="types">The types.</param>
		/// <param name="fileInfo">The file info.</param>
		/// <returns></returns>
		// FXCOP: FileInfo is more appropriate here as fileInfo refers to a file not a directory.
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public static T Deserialize<T>(Type[] types, FileInfo fileInfo)
		{
			Guard.ArgumentNotNull(types, "types");
			Guard.ArgumentNotNull(fileInfo, "fileInfo");

			XmlSerializer serializer = GetXmlSerializer(typeof(T), types);

			if(File.Exists(fileInfo.FullName))
			{
				using(StreamReader reader = new StreamReader(fileInfo.FullName))
				{
					return (T)serializer.Deserialize(reader);
				}
			}

			return default(T);
		}

		/// <summary>
		/// Serializes the specified object.
		/// </summary>
		/// <param name="obj">The object.</param>
		/// <returns></returns>
        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames")]
		public static string Serialize<T>(object obj)
		{
			Guard.ArgumentNotNull(obj, "obj");

			Type[] types = { };
			return Serialize<T>(obj, types);
		}

		/// <summary>
		/// Serializes the specified object.
		/// </summary>
		/// <param name="obj">The object.</param>
		/// <param name="types">The types.</param>
		/// <returns></returns>
        [SuppressMessage("Microsoft.Naming","CA1720:IdentifiersShouldNotContainTypeNames")]
		public static string Serialize<T>(object obj, Type[] types)
		{
			Guard.ArgumentNotNull(obj, "obj");
			Guard.ArgumentNotNull(types, "types");

			string text;

			try
			{
				XmlSerializer serializer = GetXmlSerializer(typeof(T), types);					
				using (MemoryStream stream = new MemoryStream())
			    {
			        serializer.Serialize(stream, obj);
			        text = Encoding.UTF8.GetString(stream.ToArray());
			    }
			}
			catch (InvalidOperationException ioe)
			{
			    Trace.TraceWarning(ioe.InnerException == null ? ioe.Message : ioe.InnerException.Message);
				// if we could not use XML serialization, fallback to binary serialization
				BinaryFormatter formatter = new BinaryFormatter();
				using (MemoryStream stream = new MemoryStream())
				{
					formatter.Serialize(stream, obj);
					text = Convert.ToBase64String(stream.ToArray());
				}
			}

			return text;
		}

		#endregion

		#region Private Implementation

		private static XmlSerializer GetXmlSerializer(Type type, Type[] extraTypes)
		{
			XmlSerializer serializer;
			string key = CreateKey(type, extraTypes);
			if (!serializerCache.TryGetValue(key, out serializer))
			{
				serializer = new XmlSerializer(type, extraTypes);
				serializerCache.Add(key, serializer);
			}
			return serializer;
		}

		private static string CreateKey(Type type, Type[] extraTypes)
		{
			StringBuilder builder = new StringBuilder(type.FullName);
			foreach (Type t in extraTypes)
			{
				builder.Append(t.FullName);
			}
			return builder.ToString();
		} 

		#endregion
	}
}