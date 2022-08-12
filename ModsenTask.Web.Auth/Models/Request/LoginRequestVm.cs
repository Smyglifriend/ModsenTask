using System.ComponentModel.DataAnnotations;

namespace ModsenTask.Web.Models.Request;

public class LoginRequestVm
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}