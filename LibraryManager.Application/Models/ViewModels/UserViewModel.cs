using LibraryManager.Domain.Entities;

namespace LibraryManager.Application.Models.ViewModels;

public class UserViewModel(int id, string name, string email, string role)
{
    public int Id { get; private set; } = id;
    public string Name { get; private set; } = name;
    public string Email { get; private set; } = email;
    public string Role { get; set; } = role;

    public static UserViewModel FromEntity(User user) =>
        new(user.Id,
            user.Name,
            user.Email,
            user.Role.ToString());
}