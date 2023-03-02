using System.Net.Http.Json;

namespace Lazy.Client.Services;

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
            Console.WriteLine($"DataService<{typeof(T).Name}> GET id {((dynamic)result).Id}");
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine($"DataService<{typeof(T).Name}> GET exception [{e.Message}");
            return default(T);
        }
    }

    public async Task<T?> CreateOrUpdate(T model)
    {
        // HttpClient.PutAsync("/api/EmailTemplate", new StringContent())
        T? result = default;
        try
        {
            var response = await HttpClient.PutAsJsonAsync(ApiUrl, model);

            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadFromJsonAsync<T>();
                if (result == null) throw (new Exception("Could not deserialize response"));
                Console.WriteLine($"DataService<{typeof(T).Name}> PUT with id {((dynamic)result).Id}, [{response.StatusCode} - {response.ReasonPhrase}]");
            }
            else
            {
                var contentstring = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"DataService<{typeof(T).Name}> PUT error [{response.StatusCode} - {response.ReasonPhrase}] : {contentstring}");
            }

        }
        catch (Exception e)
        {
            Console.WriteLine($"DataService<{typeof(T).Name}> PUT exception [{e.Message}");
        }
        return result;
    }

    public async Task<bool> DeleteById(int id)
    {
        try
        {
            var response = await HttpClient.DeleteAsync($"{ApiUrl}/{id}");
            Console.WriteLine($"DataService<{typeof(T).Name}> DELETE [{response.StatusCode} - {response.ReasonPhrase}]");
            return response.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            Console.WriteLine($"DataService<{typeof(T).Name}> DELETE exception [{e.Message}");
            return false;
        }
    }
}