using System;
using Newtonsoft.Json;

namespace MvvmLightWP8.Models
{
    public class Friend
    {
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        
        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("picture")]
        public Uri PictureUri { get; set; }
    }
}
