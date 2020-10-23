using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace m4_02_custom_jsonconverter_demo
{
    public class RemoveNullsJsonConverter : JsonConverter
    {
        private readonly Type[] _types;

        public RemoveNullsJsonConverter(params Type[] types)
        {
            _types = types;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            JToken token = JToken.FromObject(value);

            if (token.Type != JTokenType.Object)
            {
                token.WriteTo(writer);
            }
            else
            {
                JObject jObject = (JObject)token;
                JObject newJ = new JObject();

                foreach (KeyValuePair<string, JToken> j in jObject)
                {
                    // Ignore the null values
                    if (j.Value.Type == JTokenType.Null)
                    {
                        continue;
                    }

                    newJ.Add(j.Key, j.Value);
                }

                newJ.WriteTo(writer);
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException("This converter only writes, it is indicated in the CanRead false");
        }

        public override bool CanRead
        {
            get
            {
                return true;
            }
        }

        public override bool CanConvert(Type objectType)
        {
            return _types.Any(t => t == objectType);
        }
    }
}
