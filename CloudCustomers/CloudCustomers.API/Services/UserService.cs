using CloudCustomers.API.Models;

namespace CloudCustomers.API.Services;

public class UserService : IUserService
{
    public Task<List<User>> GetUsersAsync()
    {
        throw new NotImplementedException();
    }
}