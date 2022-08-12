namespace ModsenTask.Services.Auth.Abstractions.Models;

public class RegisterUserDto
{
    public string Name { get; set; }

    public string UserName { get; set; }

    public string Surname { get; set; }

    public string MiddleName { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public string Password { get; set; }
}