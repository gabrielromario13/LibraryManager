using LibraryManager.Domain.Enums;

namespace LibraryManager.Domain.Entities;

public class User(string name, string email, string password) : BaseEntity
{
    public string Name { get; private set; } = name;
    public string Email { get; private set; } = email;
    public string Password { get; private set; } = password;
    public UserRoles Role { get; private set; } = UserRoles.Reader;

    public ICollection<Loan> Loans { get; set; } = null!;
    public ICollection<Penalty> Penalties { get; set; } = null!;
    public ICollection<Reservation> Reservations { get; set; } = null!;
    

    public void Update(string name, string email)
    {
        Name = name;
        Email = email;
    }
        
    public void UpdatePassword(string  newPassword)
    {
        Password = newPassword;
    }
}