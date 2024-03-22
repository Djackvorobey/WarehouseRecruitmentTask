using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using TMA.Warehouse.Api.IApi;

namespace TMA.Warehouse.Api
{
    public class ApiService : IApiService
    {
        public async Task<HttpResponseMessage> PostAsString<TModel>(string url, TModel model)
        {
            try
            {
                string json = JsonSerializer.Serialize(model);

                StringContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage responseMessage = await client.PostAsync(url, content);

                    if (!responseMessage.IsSuccessStatusCode)
                    {
                        MessageBox.Show($"HTTP Error: {responseMessage.StatusCode}");

                        return default;
                    }

                    return responseMessage;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return default;
            }
        }

        public async Task<TModel> Post<TModel>(string url, TModel model)
        {
            try
            {
                string json = JsonSerializer.Serialize(model);

                StringContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage responseMessage = await client.PostAsync(url, content);

                    if (!responseMessage.IsSuccessStatusCode)
                    {
                        string errorContent = await responseMessage.Content.ReadAsStringAsync();

                        MessageBox.Show($"HTTP Error: {responseMessage.StatusCode}, {errorContent}");

                        return default;
                    }

                    string responseJson = await responseMessage.Content.ReadAsStringAsync();

                    return JsonSerializer.Deserialize<TModel>(responseJson);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return default;
            }
        }

        public async Task<HttpResponseMessage> GetById(string url, int id)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage responseMessage = await client.GetAsync($"{url}/{id}");

                    if (!responseMessage.IsSuccessStatusCode)
                    {
                        MessageBox.Show($"HTTP Error: {responseMessage.StatusCode}");
                        return default;
                    }

                    return responseMessage;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return default;
            }
        }

        public async Task<List<TModel>> GetAll<TModel>(string url, string accountId)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage responseMessage = await client.GetAsync($"{url}/{int.Parse(accountId)}");

                    if (!responseMessage.IsSuccessStatusCode)
                    {
                        MessageBox.Show($"HTTP Error: {responseMessage.StatusCode}");

                        return default;
                    }

                    string responseJson = await responseMessage.Content.ReadAsStringAsync();

                    return JsonSerializer.Deserialize<List<TModel>>(responseJson);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return default;
            }
        }

        public async Task<HttpResponseMessage> Put<TModel>(string url, TModel model)
        {
            try
            {
                string json = JsonSerializer.Serialize(model);

                StringContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage responseMessage = await client.PutAsync(url, content);

                    if (!responseMessage.IsSuccessStatusCode)
                    {
                        MessageBox.Show($"HTTP Error: {responseMessage.StatusCode}");

                        return default;
                    }

                    return responseMessage;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return default;
            }
        }

        public async Task<HttpResponseMessage> Delete(string url, int id)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage responseMessage = await client.DeleteAsync($"{url}/{id}");

                    if (!responseMessage.IsSuccessStatusCode)
                    {
                        MessageBox.Show($"HTTP Error: {responseMessage.StatusCode}");
                        return default;
                    }

                    return responseMessage;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return default;
            }
        }
    }
}
