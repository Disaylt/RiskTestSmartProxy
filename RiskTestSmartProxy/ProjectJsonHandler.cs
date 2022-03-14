using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RiskTestSmartProxy
{
    public static class ProjectJsonHandler
    {
        public static JsonObject GetJsonObject<JsonObject>(string jsonContent)
        {
            try
            {
                var jsonObject = JToken.Parse(jsonContent).ToObject<JsonObject>();
                return jsonObject;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return default(JsonObject);
            }
        }


        public static JsonObject GetJsonObject<JsonObject>(string jsonContent, string[] jsonAtributes)
        {
            string jsonContentWithoutAtributes = jsonContent;
            foreach(string atribute in jsonAtributes)
            {
                jsonContentWithoutAtributes = JToken.Parse(jsonContentWithoutAtributes)[atribute].ToString();
            }
            JsonObject json = GetJsonObject<JsonObject>(jsonContentWithoutAtributes);
            return json;
        }
    }
}
