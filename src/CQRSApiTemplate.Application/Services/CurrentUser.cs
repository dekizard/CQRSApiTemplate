using CQRSApiTemplate.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace CQRSApiTemplate.Application.Services;

public class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetUserId()
    {
        return _httpContextAccessor.HttpContext?.User?.Claims
          .FirstOrDefault(t => t.Type == "uuid")?.Value ?? "Application";
    }
}
