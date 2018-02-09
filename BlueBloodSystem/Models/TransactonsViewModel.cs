﻿using System.Collections.Generic;

namespace BlueBloodSystem.Models
{
    public class TransactonsViewModel
    {
        public List<Transaction> Transactions { get; set; }
        public decimal Incoming { get; set; }
        public decimal Outgoin { get; set; }
        public decimal Total { get; set; }
        public string Month { get; set; }
        public int Year { get; set; }
    }
}