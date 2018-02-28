using System.Collections.Generic;

namespace BlueBloodSystem.Models
{
    public class TransactionsViewModel
    {
        public List<Transaction> Transactions { get; set; }
        public IEnumerable<Transaction> Incoming { get; set; }
        public IEnumerable<Transaction> Outgoing { get; set; }
        public decimal IncomingSum { get; set; }
        public decimal OutgoingSum { get; set; }
        public decimal Total { get; set; }
        public string MonthName { get; set; }
        public int Year { get; set; }
        public int? Month { get; set; }
    }
}