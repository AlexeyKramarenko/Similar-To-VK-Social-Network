namespace MvcApp.Services
{
    public interface ISessionService
    {
        string CurrentUserId { get; }
    }
}