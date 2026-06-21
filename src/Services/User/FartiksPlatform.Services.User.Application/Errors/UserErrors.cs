using FartiksPlatform.BuildingBlocks.Errors;

namespace FartiksPlatform.Services.User.Application.Errors;

public static class UserErrors
{
    public static readonly Error UserNotFound = new("User.NotFound", "The user with the specified ID was not found.");
    public static readonly Error UserAlreadyExists = new("User.AlreadyExists", "A user with this email already exists.");
    public static readonly Error InvalidCredentials = new("User.InvalidCredentials", "Invalid email or password.");
    public static readonly Error EmailNotVerified = new("User.EmailNotVerified", "The user's email has not been verified.");
    public static readonly Error UserDeactivated = new("User.Deactivated", "The user account is deactivated.");
    public static readonly Error InvalidEmail = new("User.InvalidEmail", "The provided email format is invalid.");
    public static readonly Error WeakPassword = new("User.WeakPassword", "The password does not meet security requirements.");
    public static readonly Error UnableToCreateUser = new("User.UnableToCreate", "Unable to create a new user account.");
    public static readonly Error UnableToUpdateUser = new("User.UnableToUpdate", "Unable to update the user account.");
    public static readonly Error UnableToDeleteUser = new("User.UnableToDelete", "Unable to delete the user account.");
}
