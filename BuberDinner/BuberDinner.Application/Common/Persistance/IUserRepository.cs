using BuberDinner.Domain.Entities;

namespace BuberDinner.Application.Common.Persistance;
public interface IUserRepository
{
    User? GetByEmail(string email);
    void Add(User user);
}
