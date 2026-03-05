using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TravelPortal.web.Helpers;

namespace TravelPortal.web.Models.Common
{
    public class ApiClient : IDisposable
    {
        private static readonly HttpClientHandler _handler = new HttpClientHandler
        {
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
        };

        private static HttpClient _client = new HttpClient(_handler);
        public ApiClient(string baseUrl = null, string token = null)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.Expect100Continue = false;
            _client = new HttpClient
            {
                BaseAddress = new Uri(baseUrl ?? ConfigHelper.ApiUrl),
                Timeout = TimeSpan.FromMinutes(5)
            };

            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            // Shared hosting WAF safe headers
            _client.DefaultRequestHeaders.UserAgent.ParseAdd(
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0 Safari/537.36");

            _client.DefaultRequestHeaders.AcceptEncoding.ParseAdd("gzip, deflate");
            _client.DefaultRequestHeaders.ConnectionClose = false;

            if (!string.IsNullOrEmpty(token))
                SetBearerToken(token);
        }

        #region Headers

        public void SetBearerToken(string token)
        {
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }

        public void AddHeader(string key, string value)
        {
            if (!_client.DefaultRequestHeaders.Contains(key))
                _client.DefaultRequestHeaders.Add(key, value);
        }

        #endregion

        #region GET

        public async Task<T> GetAsync<T>(string url)
        {
            try
            {
                var response = await _client.GetAsync(url);
                return await HandleResponse<T>(response);
            }
            catch (TaskCanceledException)
            {
                throw new Exception("Request Timeout");
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Network Error: " + ex.Message);
            }
            //var response = await _client.GetAsync(url);
            //return await HandleResponse<T>(response);
        }

        #endregion

        #region POST

        public async Task<T> PostAsync<T>(string url, object data)
        {
            var content = CreateJsonContent(data);
            var response = await _client.PostAsync(url, content);
            return await HandleResponse<T>(response);
        }
        public T Post<T>(string url, object data)
        {
            var content = CreateJsonContent(data);

            var response = _client
                .PostAsync(url, content)
                .GetAwaiter()
                .GetResult();

            return HandleResponse<T>(response)
                .GetAwaiter()
                .GetResult();
        }


        #endregion

        #region PUT

        public async Task<T> PutAsync<T>(string url, object data)
        {
            var content = CreateJsonContent(data);
            var response = await _client.PutAsync(url, content);
            return await HandleResponse<T>(response);
        }

        #endregion

        #region DELETE

        public async Task<T> DeleteAsync<T>(string url)
        {
            var response = await _client.DeleteAsync(url);
            return await HandleResponse<T>(response);
        }

        #endregion

        #region PATCH

        public async Task<T> PatchAsync<T>(string url, object data)
        {
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), url)
            {
                Content = CreateJsonContent(data)
            };

            var response = await _client.SendAsync(request);
            return await HandleResponse<T>(response);
        }

        #endregion

        #region Form Data

        public async Task<T> PostFormAsync<T>(string url, Dictionary<string, string> formData)
        {
            var content = new FormUrlEncodedContent(formData);
            var response = await _client.PostAsync(url, content);
            return await HandleResponse<T>(response);
        }

        #endregion

        #region File Upload

        public async Task<T> UploadFileAsync<T>(string url, byte[] fileBytes, string fileName)
        {
            using (var content = new MultipartFormDataContent())
            {
                var fileContent = new ByteArrayContent(fileBytes);
                fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");

                content.Add(fileContent, "file", fileName);

                var response = await _client.PostAsync(url, content);
                return await HandleResponse<T>(response);
            }
        }

        #endregion

        #region Helpers

        private StringContent CreateJsonContent(object data)
        {
            var json = JsonConvert.SerializeObject(data);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        private async Task<T> HandleResponse<T>(HttpResponseMessage response)
        {
            var result = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"API Error: {response.StatusCode} - {result}");

            return JsonConvert.DeserializeObject<T>(result);
        }

        public void Dispose()
        {
            _client?.Dispose();
        }

        #endregion
    }
}