using System.ComponentModel.DataAnnotations;

namespace zwajapp.API.Dto
{
  public class UserForRegisterDto
  {
    [Required]
    public string Username { get; set; }

    [StringLength(8, MinimumLength = 6, ErrorMessage = "The password length must be more than or equal to 6 and maximum 8 characters")]
    public string Password { get; set; }
  }
}