using KickTime.Core.DTOs.Auth;
using KickTime.Core.Entities;

namespace KickTime.Application.Authentication.Mappings;

public static class UserMappings
{
    public static UserProfileResponse ToProfileResponse(
        this User user,
        string roleName)
    {
        return new UserProfileResponse
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Phone = user.Phone,
            Role = roleName
        };
    }

    //public static RegisterResponse ToRegisterResponse(
    //    this User user)
    //{
    //    return new RegisterResponse
    //    {
    //        UserId = user.Id,
    //        Message = "User registered successfully."
    //    };
    //}
}