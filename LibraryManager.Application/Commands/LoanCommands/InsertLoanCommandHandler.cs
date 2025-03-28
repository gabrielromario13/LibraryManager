using System.Linq.Expressions;
using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Domain.Entities;
using LibraryManager.Domain.Enums;
using LibraryManager.Domain.Repositories;
using MediatR;

namespace LibraryManager.Application.Commands.LoanCommands;

public class InsertLoanCommandHandler(
    ILoanRepository repository,
    IBookRepository bookRepository,
    IPenaltyRepository penaltyRepository)
    : IRequestHandler<InsertLoanCommand, ResultViewModel<int>>
{
    public async Task<ResultViewModel<int>> Handle(
        InsertLoanCommand request,
        CancellationToken cancellationToken)
    {
        var penalties = await penaltyRepository.GetAllByUser(request.UserId);
        if (penalties.Any())
            return ResultViewModel<int>.Error(
                $"Empréstimo indisponível. Usuário possui penalidades " +
                $"que somam o valor de R${penalties.Sum(p => p.Amount):C2}");
        
        var book = await bookRepository
            .GetSingle(b => b.Id == request.BookId && b.AvailableCopies > 0);

        if (book is null)
            return ResultViewModel<int>.Error("Livro não encontrado.");

        if (request.DueDate < DateTime.UtcNow.AddDays(1))
            return ResultViewModel<int>.Error("Data de devolução inválida.");
        
        var loan = request.ToEntity();
        
        try
        {
            await repository.Add(loan);
            
            book.DecrementAvailableCopies();
            await bookRepository.Update(book);
        }
        catch
        {
            return ResultViewModel<int>.Error("Não foi possível cadastrtar empréstimo.");
        }

        return ResultViewModel<int>.Success(loan.Id);
    }
}