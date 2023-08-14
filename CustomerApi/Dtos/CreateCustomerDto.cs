using System.ComponentModel.DataAnnotations;

namespace CustomerApi.Dtos
{
    public class CreateCustomerDto
    {
        [Required(ErrorMessage ="Proper name has to be specified")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "the last name has to be specified")]
        public string LastName { get; set; }
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "the email is not correct")]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
