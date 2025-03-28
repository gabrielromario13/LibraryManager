using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Domain.Enums;
using LibraryManager.Domain.Repositories;
using MediatR;

namespace LibraryManager.Application.Commands.LoanCommands;

public class UpdateLoanCommandHandler(
    ILoanRepository repository,
    IBookRepository bookRepository,
    IPenaltyRepository penaltyRepository)
    : IRequestHandler<UpdateLoanCommand, ResultViewModel<int>>
{
    public async Task<ResultViewModel<int>> Handle(
        UpdateLoanCommand request,
        CancellationToken cancellationToken)
    {
        var loan = await repository
            .GetSingle(l => l.Id == request.Id && l.Status != LoanStatus.Returned);
        
        if (loan is null)
            return ResultViewModel<int>.Error("Empréstimo não encontrado.");

        var message = string.Empty;
        if (loan.Status == LoanStatus.Overdue)
        {
            var penalty = (await penaltyRepository.GetSingle(p => p.LoanId == loan.Id && p.Paid == false))!;
            message = $"Usuário possui uma penalidade pendente de R${penalty.Amount:C2}";
        }

        try
        {
            loan.FinishLoan();
            await repository.Update(loan);

            var book = (await bookRepository.GetById(loan.BookId))!;
            book.IncrementAvailableCopies();
            await bookRepository.Update(book);
        }
        catch
        {
            return ResultViewModel<int>.Error("Não foi possível retornar livro no momento.");
        }

        return ResultViewModel<int>.Success(loan.Id, message);
    }
}