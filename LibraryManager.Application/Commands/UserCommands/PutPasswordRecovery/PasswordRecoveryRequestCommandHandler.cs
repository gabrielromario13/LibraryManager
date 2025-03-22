using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace LibraryManager.Application.Commands.UserCommands.PutPasswordRecovery;

public class PasswordRecoveryRequestCommandHandler(
    IUserRepository userRepository,
    /*IEmailServices _emailService,*/
    IMemoryCache cache)
    : IRequestHandler<PasswordRecoveryRequestCommand, ResultViewModel>
{
    // private readonly IEmailServices _emailService;

    public async Task<ResultViewModel> Handle(PasswordRecoveryRequestCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetSingle(x => x.Email == request.Email);
        
        // var code = new Random().Next(100000, 999999).ToString();
        // var cachkey = $"Recovery Code :{request.Email}";
        // cache.Set(cachkey,code, TimeSpan.FromMinutes(10));   
        // await emailService.SendAsync(user.Email, "Recuperação de senha", $"Seu código de recuperação é: {code}");
        
        return user is null
            ? ResultViewModel<string>.Error("Usuário não encontrado") :
            ResultViewModel.Success();
    }
}