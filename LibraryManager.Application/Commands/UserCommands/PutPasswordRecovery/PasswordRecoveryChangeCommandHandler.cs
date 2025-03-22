using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Domain.Repositories;
using LibraryManager.Infrastructure.Auth;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace LibraryManager.Application.Commands.UserCommands.PutPasswordRecovery;

public class PasswordRecoveryChangeCommandHandler(
    IUserRepository userRepository,
    IAuthService authService,
    IMemoryCache cache)
    : IRequestHandler<PasswordRecoveryChangeCommand, ResultViewModel>
{
    public async Task<ResultViewModel> Handle(PasswordRecoveryChangeCommand request,
        CancellationToken cancellationToken)
    {
        var cacheKey = $"Código de recuperação: {request.Email}";

        if (!cache.TryGetValue(cacheKey, out string? code) || code != request.Code)
            return ResultViewModel<string>.Error("Código inválido.");

        var user = await userRepository.GetSingle(x => x.Email == request.Email);
        if (user is null)
            return ResultViewModel<string>.Error("Usuário não encontrado.");

        var hash = authService.ComputeHash(request.NewPassword);
        await userRepository.UpdatePassword(user, hash);

        cache.Remove(cacheKey);

        return ResultViewModel.Success();
    }
}