using System.ComponentModel.DataAnnotations;


namespace ChatRoom.Models.AuthorizationModel
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Input user name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Input password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords are different")]
        public string ConfirmPassword { get; set; }
    }
}
