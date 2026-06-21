namespace BuildingBlocks.Errors;

public static class ErrorTypes
{
    public const string UnknownError = "UNKNOWN_ERROR";
    
    public const string ValidationError = "VALIDATION_ERROR";
    public const string Unauthorized = "UNAUTHORIZED";
    public const string Forbidden = "FORBIDDEN";
    public const string UserNotFound = "USER_NOT_FOUND";
    public const string UsernameTaken = "USERNAME_TAKEN";
    public const string InvalidBet = "INVALID_BET";
    public const string InsufficientFunds = "INSUFFICIENT_FUNDS";
    public const string WalletVersionConflict = "WALLET_VERSION_CONFLICT";
    public const string GameNotFound = "GAME_NOT_FOUND";
    public const string InternalError = "INTERNAL_ERROR";
    
    // User/Auth
    public const string EmailAlreadyUsed = "EMAIL_ALREADY_USED";
    public const string InvalidCredentials = "INVALID_CREDENTIALS";
    public const string AccountBanned = "ACCOUNT_BANNED";

    // Billing
    public const string WalletNotFound = "WALLET_NOT_FOUND";
    public const string TransactionNotFound = "TRANSACTION_NOT_FOUND";
    public const string InvalidCurrency = "INVALID_CURRENCY";
    public const string NegativeAmountNotAllowed = "NEGATIVE_AMOUNT_NOT_ALLOWED";

    // Gameplay
    public const string BetOutOfRange = "BET_OUT_OF_RANGE";
    public const string GameRoundNotFound = "GAME_ROUND_NOT_FOUND";
    public const string GameNotAvailable = "GAME_NOT_AVAILABLE";

    // integration
    public const string GrpcCallFailed = "GRPC_CALL_FAILED";
    public const string ExternalServiceUnavailable = "EXTERNAL_SERVICE_UNAVAILABLE";
    public const string EventProcessingFailed = "EVENT_PROCESSING_FAILED";
    public const string DuplicateEvent = "DUPLICATE_EVENT";

    // infrastructure
    public const string DatabaseError = "DATABASE_ERROR";
    public const string ConfigurationError = "CONFIGURATION_ERROR";
    public const string NotImplemented = "NOT_IMPLEMENTED";
}
