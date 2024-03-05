/*
 * Gaspard TORTERAT stu-74536
 */
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BankAppV2.Models
{
    public enum transacs
    {
        Withdrawal,
        Transfer,
        Deposit
    }
    public class Transaction
    {
        public int Id { get; set; }
        [Display(Name = "Account sender")]
        public string? ACCsender { get; set; }
        
        [Required]
        [Display(Name="Account receiver")]
        public string ACCreceiver { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public int Amount { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        
        [Required]
        public string Action { get; set; }

    }
}
