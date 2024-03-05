/*
 * Gaspard TORTERAT stu-74536
 */
using System.ComponentModel.DataAnnotations;

namespace BankAppV2.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter a firstname")]
        public string? Firstname { get; set; }
        [Required(ErrorMessage = "Please enter a Lastname")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Please enter an email")]
        public string? Email { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime? birthDate { get; set; }
        public string? Genre { get; set; }
        
        [Required(ErrorMessage = "Please enter a password")]
        [DataType(DataType.Password)]
        public string? password { get; set; }
        
    }
}
