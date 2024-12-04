namespace OptiX.Application.Transactions.Responses;

public record TransactionDto(Guid Id, Guid AccountId, DateTime Date, decimal Amount);