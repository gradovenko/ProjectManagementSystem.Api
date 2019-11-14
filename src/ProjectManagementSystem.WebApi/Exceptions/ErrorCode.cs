namespace ProjectManagementSystem.WebApi.Exceptions
{
    public class ErrorCode
    {
        public const string InternalServerError = "internal_server_error";
        public const string BadRequest = "bad_request";
        public const string InvalidCredentials = "invalid_credentials";
        public const string GrantTypeIsEmpty = "grant_type_is_empty";
        public const string LoginIsEmpty = "login_is_empty";
        public const string PasswordIsEmpty = "password_is_empty";
        public const string RefreshTokenIsEmpty = "refresh_token_is_empty";
        public const string InvalidRefreshToken = "invalid_refresh_token";
        public const string UnsupportedGrantType = "unsupported_grant_type";
        public const string Forbidden = "forbidden";
        public const string UserNotFound = "user_not_found";
        public const string UserAlreadyExists = "user_already_exists";
        public const string UsernameAlreadyExists = "username_already_exists";
        public const string EmailAlreadyExists = "email_already_exists";
        public const string InvalidPassword = "invalid_password";
        public const string IssuePriorityNotFound = "issue_priority_not_found";
        public const string IssuePriorityAlreadyExists = "issue_priority_already_exists";
        public const string IssueStatusNotFound = "issue_status_not_found";
        public const string IssueStatusAlreadyExists = "issue_status_already_exists";
        public const string ProjectNotFound = "project_not_found";
        public const string ProjectAlreadyExists = "project_already_exists";
        public const string TrackerNotFound = "tracker_not_found";
        public const string TrackerAlreadyExists = "tracker_already_exists";
        public const string IssueAlreadyExists = "issue_already_exists";
        public const string IssueNotFound = "issue_not_found";
        public const string PerformerNotFound = "performer_not_found";
    }
}