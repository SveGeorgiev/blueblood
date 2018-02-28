using System.Collections.Generic;

namespace BlueBloodSystem.Models
{
    public class TransactionsAllViewModel
    {
        public List<TransactionsViewModel> TransactionsViewModel { get; set; }
        public decimal IncomingSumYear { get; set; }
        public decimal OutgoingSumYear { get; set; }
        public decimal TotalSumYear { get; set; }
    }
}