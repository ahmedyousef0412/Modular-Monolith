namespace SharedKernel.Domain;

public static class UserErrors
{
   
    public static readonly Error InvalidToken = new(
        "User.InvalidToken",
        "The password reset token is invalid or has expired.");

    
    public static readonly Error PasswordResetFailed = new(
        "User.PasswordResetFailed",
        "Password reset failed.");


    public static readonly Error InvalidCurrentPassword = new(
        "User.InvalidCurrentPassword",
        "The current password is incorrect.");

    public static readonly Error NewPasswordSameAsOld = new(
        "User.NewPasswordSameAsOld",
        "The new password cannot be the same as the old password.");

    public static readonly Error PasswordsDoNotMatch = new("User.PasswordsDoNotMatch"
        ,"The new password not match the confirm passwrod.");
}