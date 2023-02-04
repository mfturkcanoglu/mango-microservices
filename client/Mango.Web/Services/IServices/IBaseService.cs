using Mango.Web.Models;
using Mango.Web.Models.Dtos;

namespace Mango.Web.Services.IServices;

public interface IBaseService : IDisposable
{
    ResponseDto responseDto { get; set; }
    Task<T> SendAsync<T>(ApiRequest apiRequest);
}