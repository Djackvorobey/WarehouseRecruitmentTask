using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace TMA.Warehouse.Api.IApi
{
    public interface IApiService
    {
        public Task<HttpResponseMessage> PostAsString<TModel>(string url, TModel model);
        public Task<TModel> Post<TModel>(string url, TModel model);
        public Task<List<TModel>> GetAll<TModel>(string url, string accountId);
        public Task<HttpResponseMessage> GetById(string url, int id);
        public Task<HttpResponseMessage> Put<TModel>(string url, TModel model);
        public Task<HttpResponseMessage> Delete(string url, int id);

    }
}
