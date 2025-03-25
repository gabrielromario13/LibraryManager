using LibraryManager.Application.Models.ViewModels;
using MediatR;

namespace LibraryManager.Application.Commands.LoanCommands;

public class UpdateLoanCommand(int id) : IRequest<ResultViewModel<int>>
{
    public int Id { get; private set; } = id;
}