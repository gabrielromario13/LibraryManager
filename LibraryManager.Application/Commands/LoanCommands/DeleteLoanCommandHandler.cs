using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Domain.Repositories;
using MediatR;

namespace LibraryManager.Application.Commands.LoanCommands;

public class DeleteLoanCommandHandler(ILoanRepository repository, IBookRepository bookRepository)
    : IRequestHandler<DeleteLoanCommand, ResultViewModel>
{
    public async Task<ResultViewModel> Handle(DeleteLoanCommand request, CancellationToken cancellationToken)
    {
        var loan = await repository.GetById(request.Id);
        if (loan is null)
            return ResultViewModel.Error("Empréstimo não encontrado.");

        var book = await bookRepository.GetById(loan.BookId);
        if (book is not null)
        {
            book.Available();
            await bookRepository.Update(book);
        }

        loan.Deactivate();
        await repository.Update(loan);

        return ResultViewModel.Success();
    }
}