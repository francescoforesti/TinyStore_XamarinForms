using System;
using TinyStore_XamarinForms.Model;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TinyStore_XamarinForms.Service
{
    public interface ICatalogService
    {
        Task<List<Item>> FindItems(string category);
        Task<List<string>> FindCategories();
        Task<Item> FindItem(long id);
    }

    public class CatalogService : ICatalogService
    {
        private static string BaseUrl { get; set; } = "http://tinystore.getsandbox.com/";

        private static Dictionary<string, string> endpoints { get; set; } = new Dictionary<string, string>()
        {
            {"categories", BaseUrl+"categories"},
            {"catalog", BaseUrl+"catalog"},
            {"catalogByCategory", BaseUrl+"catalog?category={0}"},
            {"item", BaseUrl+"catalog/{0}"}

        };

        HttpClient client;

        public CatalogService()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }

        // TODO : handle failures and errors (404 and so on)..

        public async Task<List<Item>> FindItems(string category)
        {
            string uriString = String.IsNullOrEmpty(category) ?
                                                 endpoints["catalog"] : string.Format(endpoints["catalogByCategory"], category);
            var uri = new Uri(uriString);
            var response = await client.GetAsync(uri);
            var result = new List<Item>();
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                result = Deserialize<List<Item>>(content);
            }
            return result;
        }

        public async Task<List<string>> FindCategories()
        {
            var uri = new Uri(string.Format(endpoints["categories"], string.Empty));
            var response = await client.GetAsync(uri);
            var result = new List<string>();
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                result = Deserialize<List<string>>(content);
            }
            return result;
        }

        public async Task<Item> FindItem(long id)
        {
            string filter = "" + id;
            var uri = new Uri(string.Format(endpoints["item"], filter));
            var response = await client.GetAsync(uri);
            Item result = null;
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                result = Deserialize<Item>(content);
            }
            return result;
        }

        private T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
