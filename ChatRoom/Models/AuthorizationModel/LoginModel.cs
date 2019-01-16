using System.ComponentModel.DataAnnotations;


namespace ChatRoom.Models.AuthorizationModel
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Input user name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Input password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
