namespace Application.Interfaces;

public interface IJwtProvider
{
    string Generate(Guid userId, string email);
}