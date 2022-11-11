using CloudCustomers.API.Models;

namespace CloudCustomers.API.Services;

public interface IUserService
{
    Task<List<User>> GetUsersAsync();
}