using System.ComponentModel.DataAnnotations;

namespace ModsenTask.Web.Models.Request;

public class RegistrationRequestVm
{
    public string UserName { get; set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public string? MiddleName { get; set; }

    public string DateOfBirth { get; set; }

    public string Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string Password { get; set; }

    public string ConfirmPassword { get; set; }
}