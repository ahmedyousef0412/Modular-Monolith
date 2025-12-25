namespace Identity.Api.Controllers.Roles.Requests;

public record UpdateRoleRequest(
    string Name,
    string Description
);
