using static Mango.Web.SD;
namespace Mango.Web.Models;

public class ApiRequest
{
    public ApiType Type { get; set; } = ApiType.GET;
    public string Url { get; set; } = string.Empty;
    public object Data { get; set; }
    public string AccessToken { get; set; } = string.Empty;
}