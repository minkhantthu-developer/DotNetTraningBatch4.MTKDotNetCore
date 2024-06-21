using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKTDotNetCore.Shared.HttpServices
{
    public class RestClientService
    {
        private readonly RestClient _client;
        public RestClientService(string url)
        {
            _client = new RestClient(new Uri(url));
        }

        public async Task<T> ExecuteAsync<T>(string endPoint, EnumHttpMethod enumHttpMethod, object? requestModel = null)
        {
            try
            {
                RestRequest? request = null;
                RestResponse? response = null;
                switch (enumHttpMethod)
                {
                    case EnumHttpMethod.Get:
                        request = new RestRequest(endPoint, Method.Get); break;
                    case EnumHttpMethod.Post:
                        request = new RestRequest(endPoint, Method.Post); break;
                    case EnumHttpMethod.Put:
                        request = new RestRequest(endPoint, Method.Put); break;
                    case EnumHttpMethod.Patch:
                        request = new RestRequest(endPoint, Method.Patch); break;
                    case EnumHttpMethod.Delete:
                        request = new RestRequest(endPoint, Method.Delete); break;
                    default:
                        throw new ArgumentException("Url Not Found");
                }
                if (requestModel is not null)
                {
                    request.AddJsonBody(requestModel);
                }
                response = await _client.ExecuteAsync(request);
                string JsonStr = response.Content!;
                var model = JsonConvert.DeserializeObject<T>(JsonStr);
                return model;
            }
            catch(Exception ex)
            {
                return default(T)!;
                throw new Exception(ex.ToString());
            }


        }
    }

    public enum EnumHttpMethod
    {
        Default,
        Get,
        Post,
        Put,
        Patch,
        Delete
    }
}
