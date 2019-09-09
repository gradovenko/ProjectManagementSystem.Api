namespace ProjectManagementSystem.WebApi.Exceptions
{
    public class ErrorCode
    {
        public const string InternalServerError = "internal_server_error";
        public const string BadRequest = "bad_request";
        public const string InvalidCredentials = "invalid_credentials";
        public const string UnsupportedGrantType = "unsupported_grant_type";
        public const string Forbidden = "forbidden";
        public const string UserNotFound = "user_not_found";
        public const string UserAlreadyExists = "user_already_exists";
        public const string UsernameAlreadyExists = "username_already_exists";
        public const string EmailAlreadyExists = "email_already_exists";
        public const string InvalidPassword = "invalid_password";
    }
}