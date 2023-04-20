namespace ProjectManagementSystem.Api.Exceptions;

public sealed class ErrorCode
{
    public static (string Title, string Detail) UserNotFound = ("Not found", "User with this identifier not found");
    public static (string Title, string Detail) UserWithSameNameAlreadyExists = ("Conflict", "User with same name already exists");
    public static (string Title, string Detail) UserWithSameEmailAlreadyExists = ("Conflict", "User with same email already exists");
    public static (string Title, string Detail) UserWithSameIdButDifferentParamsAlreadyExists = ("Conflict", "User with same id but different params already exists");

    public static (string Title, string Detail) InvalidPassword = ("invalid_password", "n");
    public static (string Title, string Detail) OldUserPasswordWrong = ("old_user_password_wrong", "n");

    
    public static (string Title, string Detail) ProjectNotFound = ("Not found", "Project with this identifier not found");
    public static (string Title, string Detail) ProjectWithSameIdAlreadyExists = ("Conflict", "Project with same name already exists");
    public static (string Title, string Detail) ProjectWithSameNameAlreadyExists = ("Conflict", "Project with same name already exists");
    public static (string Title, string Detail) ProjectWithSamePathAlreadyExists = ("Conflict", "Project with same path already exists");

    public static (string Title, string Detail) IssueNotFound = ("issue_not_found", "n");
    public static (string Title, string Detail) AuthorNotFound = ("author_not_found", "n");
    
    public static (string Title, string Detail) TimeEntryNotFound = ("time_entry_not_found", "n");
    public static (string Title, string Detail) TimeEntryAlreadyExists = ("time_entry_already_exists", "n");
    
    public static (string Title, string Detail) LabelNotFound = ("Not found", "Label with this identifier not found");
    public static (string Title, string Detail) LabelWithSameTitleAlreadyExists = ("Conflict", "Label with same title already exists");
    public static (string Title, string Detail) LabelWithSameIdButDifferentParamsAlreadyExists = ("Conflict", "Label with same identifier but different params already exists");
    
    public static (string Title, string Detail) ReactionNotFound = ("Not found", "Reaction with this identifier not found");
    public static (string Title, string Detail) ReactionWithSameEmojiAlreadyExists = ("Conflict", "Reaction with same emoji already exists");
    public static (string Title, string Detail) ReactionWithSameIdButDifferentParamsAlreadyExists = ("Conflict", "Reaction with same identifier but different params already exists");
    
    public static (string Title, string Detail) CommentNotFound = ("comment_not_found", "n");
    public static (string Title, string Detail) ParentCommentNotFound = ("parent_comment_not_found", "n");
    public static (string Title, string Detail) ParentCommentAlreadyChildCommentOfAnotherComment = ("parent_comment_already_child_comment_of_another_comment", "n");
    public static (string Title, string Detail) CommentWithSameIdButOtherParamsAlreadyExists = ("comment_with_same_id_but_other_params_already_exists", "n");
}