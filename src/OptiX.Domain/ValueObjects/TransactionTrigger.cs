namespace OptiX.Domain.ValueObjects;

public enum TransactionTrigger
{
    Deposit = 0,
    Withdrawal = 1,
    TradeOpening = 2,
    TradeClosing = 3,
    Commission = 4,
    PromoCodeActivation = 5,
    DemoAccountInitialization = 6
}