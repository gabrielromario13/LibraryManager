using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Domain.Repositories;
using LibraryManager.Infrastructure.Auth;
using MediatR;

namespace LibraryManager.Application.Commands.UserCommands;

public class InsertUserCommandHandler(
    IUserRepository userRepository,
    IAuthService authService,
    UserValidationService userValidator)
    : IRequestHandler<InsertUserCommand, ResultViewModel<int>>
{
    public async Task<ResultViewModel<int>> Handle(
        InsertUserCommand request,
        CancellationToken cancellationToken)
    {
        if (request.Password != request.ConfirmPassword)
            return ResultViewModel<int>.Error("As senhas não coincidem.");

        var validationResult = await userValidator.ValidateUserData(request.Email);
        if (!validationResult.IsSuccess)
            return ResultViewModel<int>.Error(validationResult.Message);
        
        var hashedPassword = authService.ComputeHash(request.Password);
        var user = request.ToEntity(hashedPassword);
        await userRepository.Add(user);
            
        return ResultViewModel<int>.Success(user.Id);
    }
}