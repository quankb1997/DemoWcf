using Newtonsoft.Json;
using System.Text;
using WcfClient.Models;

namespace WcfClient.Service
{
    public class AccountService : IAccountService
    {
        HttpClient client;
        public const string BASE_ADDR = "http://localhost:59189/AccountService.svc/api/account";
        public AccountService(HttpClient client)
        {
            this.client = client;
        }
        public async Task<List<AccountModels>?> GetAccounts()
        {
            string url = BASE_ADDR;
            string json = await client.GetStringAsync(url);
            return JsonConvert.DeserializeObject<List<AccountModels>>(json);    
        }
        public async Task<AccountModels?> GetAccount(int id)
        {
            string url = $"{BASE_ADDR}/{id}";
            string json = await client.GetStringAsync(url);
            return JsonConvert.DeserializeObject<AccountModels>(json);
        }
        public async Task<AccountModels?> Login(string email, string password)
        {
            string url = $"{BASE_ADDR}/{email}/{password}";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);  
            var result = await client.SendAsync(request);
            if(result.IsSuccessStatusCode)
            {
                var json = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<AccountModels>(json);
            }
            return null;
        }
        public async Task<int?> Create(AccountModels acc)
        {
            string url = BASE_ADDR;
            string accjson = JsonConvert.SerializeObject(acc);
            var content = new StringContent(accjson, Encoding.UTF8,"application/json");
            var result = await client.PostAsync(url, content);
            if(result.IsSuccessStatusCode)
            {
                var json = await result.Content.ReadAsStringAsync();
                return Convert.ToInt32(json);
            }
            return null;
        }
        public async Task<int?> Edit(AccountModels acc, int id)
        {
            string url = $"{BASE_ADDR}/{id}";
            string accjson = JsonConvert.SerializeObject(acc);
            var content = new StringContent(accjson, Encoding.UTF8, "application/json");
            var result = await client.PutAsync(url, content);
            if (result.IsSuccessStatusCode)
            {
                 await result.Content.ReadAsStringAsync();
            }
            return null;
        }
        public async Task<int?> Delete( int id)
        {
            string url = $"{BASE_ADDR}/{id}";
            var result = await client.DeleteAsync(url);
            if (result.IsSuccessStatusCode)
            {
                await result.Content.ReadAsStringAsync();
            }
            return null;
        }
    }
}
