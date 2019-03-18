namespace MVCSmartClient01.Models
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterViewModel : HeaderViewModel 
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}