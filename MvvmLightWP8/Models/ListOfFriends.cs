using System.Collections.Generic;
using Newtonsoft.Json;

namespace MvvmLightWP8.Models
{
    public class ListOfFriends
    {
        [JsonProperty("data")]
        public List<Friend> Data { get; set; }
    }
}
