using System.Text;
using Mango.Web.Models;
using Mango.Web.Models.Dtos;
using Mango.Web.Services.IServices;
using Newtonsoft.Json;
using static Mango.Web.SD;

namespace Mango.Web.Services;

public class BaseService : IBaseService
{
    public ResponseDto responseDto { get; set; }
    private IHttpClientFactory httpClient { get; set; }

    public BaseService(IHttpClientFactory httpClient)
    {
        this.responseDto = new ResponseDto();
        this.httpClient = httpClient;
    }

    public async Task<T> SendAsync<T>(ApiRequest apiRequest)
    {
        try
        {
            var client = httpClient.CreateClient("MangoAPI");
            HttpRequestMessage message = new HttpRequestMessage();
            message.Headers.Add("Accept-Type", "application/json");
            message.RequestUri = new Uri(apiRequest.Url);
            client.DefaultRequestHeaders.Clear();
            if (apiRequest.Data != null)
            {
                message.Content = new StringContent(
                    JsonConvert.SerializeObject(apiRequest.Data),
                    Encoding.UTF8,
                    "application/json"
                );
            }

            HttpResponseMessage responseMessage = null;
            switch (apiRequest.Type)
            {
                case ApiType.POST:
                    message.Method = HttpMethod.Post;
                    break;
                case ApiType.PUT:
                    message.Method = HttpMethod.Put;
                    break;
                case ApiType.DELETE:
                    message.Method = HttpMethod.Delete;
                    break;
                default:
                    message.Method = HttpMethod.Get;
                    break;
            }
            // Send request with request message
            responseMessage = await client.SendAsync(message);
            
            // Get content as string
            var responseContent = await responseMessage.Content.ReadAsStringAsync();
            
            // Convert - Deserialize string to T type by Newtonsoft.Json
            var apiResponseDto = JsonConvert.DeserializeObject<T>(responseContent);

            return apiResponseDto;
        }
        catch (Exception e)
        {
            var dto = new ResponseDto
            {
                IsSuccess = false,
                DisplayMessage = "Error",
                ErrorMessages = new List<string>(){e.Message}
            };
            
            // For converting ResponseDto type to T
            var res = JsonConvert.SerializeObject(dto);
            var apiResponseDto = JsonConvert.DeserializeObject<T>(res);
            
            return apiResponseDto;
        }
    }
    
    public void Dispose()
    {
        GC.SuppressFinalize(true);
    }
}