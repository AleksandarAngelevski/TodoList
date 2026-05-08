namespace Service.Interfaces;

public interface ICurrentUser
{
    string? GetUserId();
    string? GetUserName();
}