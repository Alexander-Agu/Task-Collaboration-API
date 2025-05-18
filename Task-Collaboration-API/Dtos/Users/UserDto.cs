using System.ComponentModel.DataAnnotations;

namespace Task_Collaboration_API.Dtos.Users;

public record class UserDto(
    string Firstname,
    string Lastname,
    DateOnly CreatedDate
);