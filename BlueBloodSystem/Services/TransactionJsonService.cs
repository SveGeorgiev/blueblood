using BlueBloodSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlueBloodSystem.Services
{
    public class TransactionJsonService
    {
        private string budgetFileName = "budget2018.json";

        public JsonConvertService JCS { get; set; }

        public TransactionJsonService()
        {
            JCS = new JsonConvertService();
        }

        public List<Transaction> GetTransactions()
        {
            return JCS.GetAllTransactions(budgetFileName).ToList();
        }

        public Transaction GetTransactionById(Guid Id)
        {
            return JCS.GetTransactionById(Id, budgetFileName);
        }

        public void AddTransaction(Transaction transaction)
        {
            List<Transaction> transactions = JCS.GetAllTransactions(budgetFileName);
            transaction.Id = Guid.NewGuid();
            transactions.Add(transaction);
            JCS.WriteToFile(budgetFileName, transactions);
        }

        public void UpdateTransaction(Transaction item)
        {
            List<Transaction> transactions = JCS.GetAllTransactions(budgetFileName);
            Transaction transaction = transactions.FirstOrDefault(t => t.Id == item.Id);
            transactions.Remove(transaction);
            transactions.Add(item);
            JCS.WriteToFile(budgetFileName, transactions);
        }

        public void DeleteTransaction(Guid Id)
        {
            List<Transaction> transactions = JCS.GetAllTransactions(budgetFileName);
            Transaction transaction = transactions.FirstOrDefault(t => t.Id == Id);
            transactions.Remove(transaction);
            JCS.WriteToFile(budgetFileName, transactions);
        }
    }
}