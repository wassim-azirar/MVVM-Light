using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MvvmLightWP8.Helpers;
using MvvmLightWP8.Models;
using Newtonsoft.Json;

namespace MvvmLightWP8.Services
{
    public class DataService : IDataService
    {
        private const string URL_BASE = "http://www.galasoft.ch/labs/friends/handle.ashx?code={0}&{1}={2}&seed={3}";

        public async Task<IEnumerable<Friend>> GetFriends()
        {
            var client = new HttpClient();

            var uri = new Uri(string.Format(
                URL_BASE,
                Constants.Code,
                Constants.QueryKeyAction,
                Constants.ActionGet,
                DateTime.Now.Ticks));

            var json = await client.GetStringAsync(uri);

            var result = JsonConvert.DeserializeObject<ListOfFriends>(json);
            return result.Data;
        }

        public async Task<string> Save(Friend updatedFriend)
        {
            var client = new HttpClient();

            var uri = new Uri(string.Format(
                URL_BASE,
                Constants.Code,
                Constants.QueryKeyAction,
                Constants.ActionSave,
                DateTime.Now.Ticks));

            var json = JsonConvert.SerializeObject(updatedFriend);

            try
            {
                var content = new StringContent(json);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await client.PostAsync(uri, content);
                var result = await response.Content.ReadAsStringAsync();

                return result;
            }
            catch (Exception)
            {
                return "0";
            }
        }
    }
}
