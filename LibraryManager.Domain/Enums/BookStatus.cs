namespace LibraryManager.Domain.Enums;

public enum BookStatus
{
    Available = 1,
    Reserved = 2,
    Loaned = 3,
    Unavailable = 4,
    Overdue = 5
}