namespace BlazorFullStackCrud.Client.Services.UserService
{
    public interface IUserService
    {
        List<User> Users { get; set; }

        Task GetUsers();

        Task<User> GetSingleUser(int id);
        Task CreateUser(User user);
        Task<bool> UpdateUser(User user);
        Task DeleteUser(int id);

    }
}
