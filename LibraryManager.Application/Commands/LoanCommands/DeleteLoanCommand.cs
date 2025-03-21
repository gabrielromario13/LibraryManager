using LibraryManager.Application.Models.ViewModels;
using MediatR;

namespace LibraryManager.Application.Commands.LoanCommands;

public class DeleteLoanCommand(int id) : IRequest<ResultViewModel>
{
    public int Id { get; private set; } = id;
}