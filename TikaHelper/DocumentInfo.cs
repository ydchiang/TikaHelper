using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MXIC.EPR.TikaHelper
{
    [JsonConverter(typeof(DocumentMedadataConverter))]
    public class DocumentInfo
    {
        // Metadata
        public string Author { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public DateTime LastSavedDate { get; set; }
        public int WordCount { get; set; }

        // Content
        public string Content { get; set; }

        // Tika parsing cost (milliseconds)
        public long ParseCost { get; set; }
    }

    internal sealed class DocumentMedadataConverter : JsonConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jsonObject = JObject.Load(reader);

            string author = "Unknown";
            DateTime creationDate = DateTime.MinValue;
            DateTime lastModifiedDate = DateTime.MinValue;
            DateTime lastSavedDate = DateTime.MinValue;
            int wordCount = -1;

            if (jsonObject.ContainsKey("Author")) author = jsonObject["Author"].Value<string>();
            if (jsonObject.ContainsKey("Creation-Date")) creationDate = jsonObject["Creation-Date"].Value<DateTime>();
            if (jsonObject.ContainsKey("Last-Modified")) lastModifiedDate = jsonObject["Last-Modified"].Value<DateTime>();
            if (jsonObject.ContainsKey("Last-Save-Date")) lastSavedDate = jsonObject["Last-Save-Date"].Value<DateTime>();
            if (jsonObject.ContainsKey("Word-Count")) wordCount = jsonObject["Word-Count"].Value<int>();

            return new DocumentInfo
            {
                Author = author,
                CreationDate = creationDate,
                LastModifiedDate = lastModifiedDate,
                LastSavedDate = lastSavedDate,
                WordCount = wordCount,
                Content = ""
            };
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(DocumentInfo).IsAssignableFrom(objectType);
        }
    }
}
