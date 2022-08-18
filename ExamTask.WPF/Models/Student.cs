using Newtonsoft.Json;
using System;

namespace ExamTask.Main.Models
{
    public class Student
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("faculty")]
        public string Faculty { get; set; }

        [JsonProperty("passportId")]
        public string PassportId { get; set; }

        [JsonProperty("passport")]
        public Attachment Passport { get; set; }

        [JsonProperty("imageId")]
        public string ImageId { get; set; }

        [JsonProperty("image")]
        public Attachment Image { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

    }
}
