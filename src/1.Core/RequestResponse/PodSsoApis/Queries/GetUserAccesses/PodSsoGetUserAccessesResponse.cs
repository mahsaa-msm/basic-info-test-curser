namespace Master.Data.Core.RequestResponse.PodSsoApis.Queries.GetUserAccesses;
public sealed class PodSsoGetUserAccessesResponse
{
    public bool hasError { get; set; }
    public int errorCode { get; set; }
    public string message { get; set; }
    public List<UserAccessAclInfoModel> aclInfo { get; set; }
}
public sealed class UserAccessAclInfoModel
{
    public UserAccessResourceModel resource { get; set; }
    public UserAccessRoleModel role { get; set; }
}
public sealed class UserAccessResourceModel
{
    public long id { get; set; }
    public string name { get; set; }
}
public sealed class UserAccessRoleModel
{
    public long roleId { get; set; }
    public string roleName { get; set; }
    public string description { get; set; }
    public List<UserAccessPermissionModel> rolePermissions { get; set; }
    public bool active { get; set; }
}
public sealed class UserAccessPermissionModel
{
    public long permissionId { get; set; }
    public string name { get; set; }
    public string description { get; set; }
}