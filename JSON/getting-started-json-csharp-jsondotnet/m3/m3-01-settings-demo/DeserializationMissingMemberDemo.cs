using JsonSamples;
using System;
using Newtonsoft.Json;

namespace m3_01_settings_demo
{
    public static class DeserializationMissingMemberDemo
    {
        /// <summary>
        /// MissingMemberHandling controls how missing members are handled during deserialization
        /// </summary>
        public static void Show()
        {

            Console.Clear();
            Console.WriteLine("*** Deserialization Missing Member ***");

            // Exteded contains a value called dance
            // But there is no dance property in the Author class 
            string xavierJsonExtraNameValue = Generate.ExtendedSingleJson();

            // Deserialize without providing any settings
            Author xavierPocoNoSetting;
            Console.WriteLine("Deserialize with no settings specified");

            xavierPocoNoSetting = JsonConvert.DeserializeObject<Author>(xavierJsonExtraNameValue);

            Console.WriteLine(xavierPocoNoSetting.name);
            //Check value of 'happy' and 'dance'
            // By default, Json.NET ignores a JSON member if there is no field or property for its value to be set to during deserialization.

            // Deserialize with Ignore setting
            Author xavierPocoWithSettingIgnore;
            Console.WriteLine("Deserialize with MissingMemberHandling.Ignore");
            JsonSerializerSettings jsonSettingsIgnore = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            xavierPocoWithSettingIgnore = JsonConvert.DeserializeObject<Author>(
                xavierJsonExtraNameValue, 
                jsonSettingsIgnore
            );
            Console.WriteLine(xavierPocoWithSettingIgnore.name);

            // Deserialize with Error setting
            // Json.NET raises an exception when there is a missing member during deserialization
            try
            {
                Author xavierPocoWithSettingError;
                Console.WriteLine("Deserialize with MissingMemberHandling.Error");
                JsonSerializerSettings jsonSettingsError = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    MissingMemberHandling = MissingMemberHandling.Error
                };
                xavierPocoWithSettingError = JsonConvert.DeserializeObject<Author>(
                    xavierJsonExtraNameValue, 
                    jsonSettingsError
                );
                Console.WriteLine(xavierPocoWithSettingError.name);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
