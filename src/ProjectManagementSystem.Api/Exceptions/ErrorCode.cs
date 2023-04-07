namespace ProjectManagementSystem.Api.Exceptions;

public sealed class ErrorCode
{
    public static (string Title, string Detail) InternalServerError = ("internal_server_error", "n");
    public static (string Title, string Detail) InvalidRequest = ("invalid_request", "n");
    public static (string Title, string Detail) InvalidGrant = ("invalid_grant", "n");
    public static (string Title, string Detail) UnauthorizedClient = ("unauthorized_client", "n");
    public static (string Title, string Detail) UnsupportedGrantType = ("unsupported_grant_type", "n");
    public static (string Title, string Detail) Forbidden = ("forbidden", "n");
    public static (string Title, string Detail) UserNotFound = ("user_not_found", "n");
    public static (string Title, string Detail) UserAlreadyExists = ("user_already_exists", "n");
    public static (string Title, string Detail) NameAlreadyExists = ("name_already_exists", "n");
    public static (string Title, string Detail) EmailAlreadyExists = ("email_already_exists", "n");
    public static (string Title, string Detail) InvalidPassword = ("invalid_password", "n");
    public static (string Title, string Detail) OldUserPasswordWrong = ("old_user_password_wrong", "n");
    public static (string Title, string Detail) IssuePriorityNotFound = ("issue_priority_not_found", "n");
    public static (string Title, string Detail) IssuePriorityAlreadyExists = ("issue_priority_already_exists", "n");
    public static (string Title, string Detail) IssueStatusNotFound = ("issue_status_not_found", "n");
    public static (string Title, string Detail) IssueStatusAlreadyExists = ("issue_status_already_exists", "n");
    public static (string Title, string Detail) ProjectNotFound = ("project_not_found", "n");
    public static (string Title, string Detail) ProjectWithSameNameAlreadyExists = ("project_already_exists", "n");
    public static (string Title, string Detail) ProjectWithSamePathAlreadyExists = ("project_already_exists", "n");
    public static (string Title, string Detail) ProjectWithSameIdButOtherParamsAlreadyExists = ("project_already_exists", "n");
    public static (string Title, string Detail) TrackerNotFound = ("tracker_not_found", "n");
    public static (string Title, string Detail) TrackerAlreadyExists = ("tracker_already_exists", "n");
    public static (string Title, string Detail) IssueAlreadyExists = ("issue_already_exists", "n");
    public static (string Title, string Detail) IssueNotFound = ("issue_not_found", "n");
    public static (string Title, string Detail) AssigneeNotFound = ("assignee_not_found", "n");
    public static (string Title, string Detail) TimeEntryActivityNotFound = ("time_entry_activity_not_found", "n");
    public static (string Title, string Detail) TimeEntryActivityAlreadyExists = ("time_entry_activity_already_exists", "n");
    public static (string Title, string Detail) TimeEntryNotFound = ("time_entry_not_found", "n");
    public static (string Title, string Detail) TimeEntryAlreadyExists = ("time_entry_already_exists", "n");
    public static (string Title, string Detail) LabelNotFound = ("label_not_found", "n");
    public static (string Title, string Detail) LabelWithSameTitleAlreadyExists = ("label_already_exists", "n");
    public static (string Title, string Detail) LabelWithSameIdButOtherParamsAlreadyExists = ("label_already_exists", "n");
    public static (string Title, string Detail) ReactionNotFound = ("reaction_not_found", "n");
}