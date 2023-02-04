namespace Mango.Services.ProductAPI.Models.Dtos;

public class ResponseDto
{
    public bool IsSuccess { get; set; } = true;
    public object Result { get; set; }
    public string DisplayMessage { get; set; } = string.Empty;
    public IList<string> ErrorMessages { get; set; } = new List<string>();
}