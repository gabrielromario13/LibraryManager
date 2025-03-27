using LibraryManager.Domain.Entities;

namespace LibraryManager.Application.Models.ViewModels;

public class PenaltyViewModel(
    int id,
    int userId,
    int loanId,
    decimal amount,
    bool paid)
{
    public int Id { get; private set; } = id;
    public int UserId { get; private set; } = userId;
    public int LoanId { get; private set; } = loanId;
    public decimal Amount { get; private set; } = amount;
    public bool Paid { get; private set; } = paid;
    
    public static PenaltyViewModel FromEntity(Penalty penalty)
        => new(penalty.Id, penalty.UserId, penalty.LoanId, penalty.Amount, penalty.Paid);
}