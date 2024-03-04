using System.ComponentModel.DataAnnotations;

namespace BankAppV2.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string? ACCsender { get; set; }
        public string ACCreceiver { get; set; }

        public int Amount { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public string Action { get; set; }

    }
}
