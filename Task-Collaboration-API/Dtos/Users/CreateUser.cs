using System.ComponentModel.DataAnnotations;

namespace Task_Collaboration_API.Dtos.Users;

public record class CreateUser(
    [Required][StringLength(20)] string Firstname,
    [Required][StringLength(20)] string Lastname,
    [Required][StringLength(20)] string Email,
    [Required][StringLength(30)] string Password
);
