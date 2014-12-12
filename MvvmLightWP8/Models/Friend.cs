using System;
using GalaSoft.MvvmLight;
using Newtonsoft.Json;

namespace MvvmLightWP8.Models
{
    public class Friend : ObservableObject
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("first_name")] public string FirstName;

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("picture")]
        public Uri PictureUri { get; set; }

#if DEBUG
        public Friend()
        {
            FirstName = "Wassim";
            LastName = "AZIRAR";
            PictureUri = new Uri("http://www.galasoft.ch/logo/LogoHead128.png");
        }
#endif
    }
}
