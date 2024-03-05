/*
 * Gaspard TORTERAT stu-74536
 */
namespace BankAppV2.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
