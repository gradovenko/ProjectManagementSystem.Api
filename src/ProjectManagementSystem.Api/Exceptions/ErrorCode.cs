namespace ProjectManagementSystem.Api.Exceptions
{
    public sealed class ErrorCode
    {
        public const string InternalServerError = "internal_server_error";
        public const string InvalidRequest = "invalid_request";
        public const string InvalidGrant = "invalid_grant";
        public const string UnauthorizedClient = "unauthorized_client";
        public const string UnsupportedGrantType = "unsupported_grant_type";
        public const string Forbidden = "forbidden";
        public const string UserNotFound = "user_not_found";
        public const string UserAlreadyExists = "user_already_exists";
        public const string NameAlreadyExists = "name_already_exists";
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
        public const string AssigneeNotFound = "assignee_not_found";
        public const string TimeEntryActivityNotFound = "time_entry_activity_not_found";
        public const string TimeEntryActivityAlreadyExists = "time_entry_activity_already_exists";
        public const string TimeEntryNotFound = "time_entry_not_found";
        public const string TimeEntryAlreadyExists = "time_entry_already_exists";
        public const string PermissionNotFound = "permission_not_found";
        public const string RoleNotFound = "role_not_found";
        public const string RoleAlreadyExists = "role_activity_already_exists";
    }
}