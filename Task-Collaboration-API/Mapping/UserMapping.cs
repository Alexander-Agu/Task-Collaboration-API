using System;
using Task_Collaboration_API.Dtos.Users;
using Task_Collaboration_API.Entities;

namespace Task_Collaboration_API.Mapping;

public static class UserMapping
{
    public static User ToEntity(this CreateUser createUser)
    {
        return new()
        {
            Firstname = createUser.Firstname,
            Lastname = createUser.Lastname,
            Email = createUser.Email,
            Password = createUser.Password,
            CreatedDate = createUser.CreatedDate
        };
    }


    public static User ToEntity(this UpdateUser updateUser)
    {
        return new()
        {
            Firstname = updateUser.Firstname,
            Lastname = updateUser.Lastname,
            Email = updateUser.Email,
            Password = updateUser.Password,
            CreatedDate = updateUser.CreatedDate
        };
    }


    public static UserDto ToDto(this User user)
    {
        return new(
            Firstname: user.Firstname,
            Lastname: user.Firstname,
            CreatedDate: user.CreatedDate
        );
    }
}
