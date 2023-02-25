using System.Net.Http.Json;
using Lazy.Model;

namespace Lazy.Client.Services
{
    public abstract class DataService<T>
    {
        protected readonly HttpClient HttpClient;
        protected abstract string ApiUrl { get; }

        protected DataService(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        public async Task<T?> GetById(int id)
        {
            try
            {
                var result = await HttpClient.GetFromJsonAsync<T>($"{ApiUrl}/{id}");
                return result;
            }
            catch (Exception e)
            {
                //todo show toast message and stop navigation
                return default(T);
            }
        }

        public async Task<T?> CreateOrUpdate(T model)
        {
            // HttpClient.PutAsync("/api/EmailTemplate", new StringContent())
            var response = await HttpClient.PutAsJsonAsync(ApiUrl, model);
            T? result = default;
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadFromJsonAsync<T>();
                if (result == null) throw (new Exception("Could not deserialize response"));
                Console.WriteLine($"EmailTemplate PUT with id {((dynamic)result).Id}, [{response.StatusCode} - {response.ReasonPhrase}]");
            }
            else
            {
                var contentstring = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"EmailTemplate PUT error [{response.StatusCode} - {response.ReasonPhrase}] : {contentstring}");
            }

            return result;
        }

        public async Task DeleteById(int id)
        {
            try
            {
                await HttpClient.DeleteAsync($"{ApiUrl}/{id}");
            }
            catch (Exception e)
            {
                //todo show toast message and stop navigation
            }
        }
    }
}