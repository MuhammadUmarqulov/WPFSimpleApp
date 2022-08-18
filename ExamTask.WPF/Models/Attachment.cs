using Newtonsoft.Json;

namespace ExamTask.Main.Models
{
    public class Attachment
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

    }
}
