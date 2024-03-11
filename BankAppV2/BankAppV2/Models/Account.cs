/*
 * Gaspard TORTERAT stu-74536
 */
using System.ComponentModel.DataAnnotations;

namespace BankAppV2.Models
{

    public enum account_type
    {
        Savings,
        Current
    }
    public class Account
    {
        public int Id { get; set; }

        public string Holder { get; set; }

        [DataType(DataType.Currency)]
        public int Balance { get; set; }

        public string? AccountName { get; set; }

        public account_type? AccType { get; set; }
    }
}
