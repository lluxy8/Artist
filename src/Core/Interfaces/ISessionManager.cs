namespace Core.Interfaces
{
    public interface ISessionManager
    {
        void SetUserSession(Guid userId);
        void ClearSession();
        Guid? GetUserId();
    }
}
