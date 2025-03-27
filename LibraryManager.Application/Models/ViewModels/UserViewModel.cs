using LibraryManager.Domain.Entities;

namespace LibraryManager.Application.Models.ViewModels;

public class UserViewModel(string name, string email, int id, string role, List<string>? loans)
{
    public string Name { get; private set; } = name;
    public string Email { get; private set; } = email;
    public int Id { get; private set; } = id;
    public string Role { get; set; } = role;
    public List<string>? LoansBookTitle { get; private set; } = loans;

    public static UserViewModel FromEntity(User user)
    {
        var loanedBooks = user.Loans?
            .Where(l => l.Book is not null && l.IsActive)
            .Select(loan => loan.Book.Title)
            .ToList();
        
        return new(
            user.Name,
            user.Email,
            user.Id,
            user.Role.ToString(),
            loanedBooks);
    }
}