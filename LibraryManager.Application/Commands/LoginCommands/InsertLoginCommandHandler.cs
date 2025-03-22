using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Domain.Repositories;
using LibraryManager.Infrastructure.Auth;
using MediatR;

namespace LibraryManager.Application.Commands.LoginCommands;

public class InsertLoginCommandHandler(IUserRepository repository, IAuthService auth)
    : IRequestHandler<InsertLoginCommand, ResultViewModel<string>>
{
    public async Task<ResultViewModel<string>> Handle(InsertLoginCommand request, CancellationToken cancellationToken)
    {
        var hashedPassword = auth.ComputeHash(request.Password);
        var user = await repository.GetSingle(x => x.Email == request.Email);

        if (user is null || user.Password != hashedPassword)
            return ResultViewModel<string>.Error("Credenciais inválidas.");

        var token = auth.GenerateToken(user.Id.ToString(), user.Role.ToString()); 
        
        return ResultViewModel<string>.Success(token);
    }
}