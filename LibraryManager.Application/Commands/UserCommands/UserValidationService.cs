using System.Net.Mail;
using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Domain.Repositories;

namespace LibraryManager.Application.Commands.UserCommands;

public class UserValidationService(IUserRepository userRepository)
{
    public async Task<ResultViewModel> ValidateUserData(string email)
    {
        if (!IsValidEmail(email))
            return ResultViewModel.Error("E-mail inválido.");

        var emailExists = await userRepository.EmailExists(email);
        if (emailExists)
            return ResultViewModel.Error("E-mail já cadastrado.");

        return ResultViewModel.Success();
    }
    
    private bool IsValidEmail(string email)
    {
        var trimmedEmail = email.Trim();

        if (trimmedEmail.EndsWith("."))
            return false;
        
        try
        {
            var addr = new MailAddress(email);
            return addr.Address == trimmedEmail;
        }
        catch
        {
            return false;
        }
    }
}