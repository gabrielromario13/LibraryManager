using LibraryManager.Application.Commands.BookCommands;
using LibraryManager.Application.Commands.UserCommands;
using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Domain.Repositories;
using LibraryManager.Infrastructure.Auth;
using LibraryManager.Infrastructure.Data.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryManager.Application;

public static class Configure
{
    public static void AddApplication(this IServiceCollection services)
    {
        services
            .AddHandlers()
            .AddValidation();
    }

    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<UserValidationService>();
        
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<ILoanRepository, LoanRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPenaltyRepository, PenaltyRepository>();
        services.AddScoped<IReservationRepository, ReservationRepository>();
        
        services.AddMediatR(config
            => config.RegisterServicesFromAssemblyContaining<InsertBookCommandHandler>());
        
        return services;
    }

    private static void AddValidation(this IServiceCollection services)
    {
        services.AddTransient<IPipelineBehavior<InsertBookCommand, ResultViewModel<int>>,
            ValidateInsertBookCommandBehavior>();
    }
}