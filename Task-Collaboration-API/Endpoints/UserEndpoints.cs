using System;
using Microsoft.EntityFrameworkCore;
using Task_Collaboration_API.Data;
using Task_Collaboration_API.Dtos.Users;
using Task_Collaboration_API.Entities;
using Task_Collaboration_API.Mapping;

namespace Task_Collaboration_API.Endpoints;

public static class UserEndpoints
{
    public static RouteGroupBuilder MapUserEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("project/user").WithParameterValidation();
        string userGetEndpoint = "GetUser";


        // GET - gets user my Id
        group.MapGet("/{id}", async (int id, TaskCollaborationContext dbContext) =>
        {
            User? user = await dbContext.Users.FindAsync(id);
            if (user is null) return Results.NotFound();

            return Results.Ok(user.ToDto());
        }).WithName(userGetEndpoint);


        // POST - creates an account
        group.MapPost("/", async (CreateUser newUser, TaskCollaborationContext dbContext) =>
        {
            User user = newUser.ToEntity();

            // Checking if email already exists
            bool emailExist = await dbContext.Users.AnyAsync(x => x.Email == user.Email);
            if (emailExist) return Results.Conflict(new { message = "Email already exists" });

            // Checking if password is valid
            if (!ValidatePassword(user.Password)) return Results.Conflict(new { message = "Password not strong enough" });

            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();

            return Results.CreatedAtRoute(userGetEndpoint, new { id = user.Id }, user.ToDto());
        })
        .WithParameterValidation();


        // PUT - updates user information
        group.MapPut("/{id}", async (int id, UpdateUser updateUser, TaskCollaborationContext dbContext) =>
        {
            User? user = await dbContext.Users.FindAsync(id);
            if (user is null) return Results.NotFound();

            // Checking Names
            if (!string.IsNullOrEmpty(updateUser.Firstname) && updateUser.Firstname != user.Firstname) user.Firstname = updateUser.Firstname;
            if (!string.IsNullOrEmpty(updateUser.Lastname) && updateUser.Lastname != user.Lastname) user.Lastname = updateUser.Lastname;

            // Checking Email
            if (!string.IsNullOrEmpty(updateUser.Email) && updateUser.Email != user.Email)
            {
                // Checking if email already exists
                bool emailExist = await dbContext.Users.AnyAsync(x => x.Email == user.Email);
                if (emailExist) return Results.Conflict(new { message = "Email already exists" });
                else
                {
                    user.Email = updateUser.Email;
                }
            }

            // Checking Password
            if (!string.IsNullOrEmpty(updateUser.Password) && updateUser.Password != user.Password)
            {
                // Checking if password is valid
                if (!ValidatePassword(user.Password)) return Results.Conflict(new { message = "Password not strong enough" });
                else
                {
                    user.Password = updateUser.Password;
                }
            }

            await dbContext.SaveChangesAsync();

            return Results.CreatedAtRoute(userGetEndpoint, new { id = user.Id }, user.ToDto());
        });


        // DELETE - delete an account
        group.MapDelete("/{id}", async (int id, TaskCollaborationContext dbContext) =>
        {
            User? user = await dbContext.Users.FindAsync(id);
            if (user is null) return Results.NotFound();

            await dbContext.Users.Where(x => x.Id == id).ExecuteDeleteAsync();
            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });


        return group;
    }

    private static bool ValidatePassword(string password)
    {
        if (password.Length < 8) return false; // Checks if password is over 8 characters


        bool number = false, special = false, lower = false, upper = false;

        for (int x = 0; x <= password.Length - 1; x++)
        {
            if (char.IsNumber(password, x)) number = true; // Checks if password has a number

            if (char.IsUpper(password, x)) upper = true; // Checks if password has an uppercase letter

            if (char.IsLower(password, x)) lower = true; // Checks if password has a lowercase letter

            if ("!@#$%^&*()_{}:''?//><|".Contains(password[x])) special = true; // Checks if password has special characters

            if (number && special && lower && upper) break; // If all these conditions have been met stop checking
        }

        if (number && special && lower && upper)
        {
            return true;
        }
        return false;
    }
}
