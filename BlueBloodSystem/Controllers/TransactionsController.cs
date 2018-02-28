using BlueBloodSystem.Models;
using BlueBloodSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BlueBloodSystem.Controllers
{
    public class TransactionsController : Controller
    {
        public TransactionJsonService TransactionService => new TransactionJsonService();

        public ActionResult Index(int? month, int? year, string sortBy)
        {
            List<Transaction> transactions = TransactionService.GetTransactions();
            int chosenMonth = month.HasValue ? month.Value : DateTime.Now.Month;
            int chosenYear = year.HasValue ? year.Value : DateTime.Now.Year;
            transactions = transactions.Where(t => t.Date.Month == chosenMonth && t.Date.Year == chosenYear).ToList();
            transactions = SortTransactions(transactions, sortBy);
            string monthName = SetMonthName(chosenMonth);
            var incoming = transactions.Where(t => t.IsDividend);
            var outgoing = transactions.Where(t => !t.IsDividend);

            var data = new TransactionsViewModel
            {
                Transactions = transactions,
                Incoming = incoming,
                Outgoing = outgoing,
                IncomingSum = incoming.Sum(x => x.Value),
                OutgoingSum = outgoing.Sum(x => x.Value),
                MonthName = monthName,
                Year = chosenYear,
                Month = chosenMonth
            };
            data.Total = data.IncomingSum - data.OutgoingSum;

            return View(data);
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            var transaction = new Transaction
            {
                Date = DateTime.Today,
                Value = 20,
                IsDividend = true
            };
            return View(transaction);
        }

        [HttpPost]
        public ActionResult Create(Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                TransactionService.AddTransaction(transaction);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(Guid id)
        {
            Transaction transation = TransactionService.GetTransactionById(id);
            return View(transation);
        }

        [HttpPost]
        public ActionResult Edit(Guid id, Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                TransactionService.UpdateTransaction(transaction);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(Guid id)
        {
            Transaction transation = TransactionService.GetTransactionById(id);
            return View(transation);
        }

        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                TransactionService.DeleteTransaction(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Search(string transactionName)
        {
            if (!string.IsNullOrEmpty(transactionName))
            {
                List<Transaction> transactions = TransactionService.GetTransactions();
                transactions = transactions.Where(t => t.Name.ToLower().Contains(transactionName.ToLower())).OrderBy(t => t.Date).ToList();
                var incoming = transactions.Where(t => t.IsDividend);
                var outgoing = transactions.Where(t => !t.IsDividend);
                var incomingSum = incoming.Sum(t => t.Value);
                var outgoingSum = outgoing.Sum(t => t.Value);
                var totalSum = incomingSum - outgoingSum;

                return View(new TransactionsViewModel
                {
                    Incoming = incoming,
                    Outgoing = outgoing,
                    IncomingSum = incomingSum,
                    OutgoingSum = outgoingSum,
                    Total = totalSum
                });
            }

            return View();
        }

        private string SetMonthName(int chosenMonth)
        {
            switch (chosenMonth)
            {
                case 1:
                    return "Януари";
                case 2:
                    return "Февруари";
                case 3:
                    return "Март";
                case 4:
                    return "Април";
                case 5:
                    return "Май";
                case 6:
                    return "Юни";
                case 7:
                    return "Юли";
                case 8:
                    return "Август";
                case 9:
                    return "Септември";
                case 10:
                    return "Октомври";
                case 11:
                    return "Ноември";
                case 12:
                    return "Декември";
                default:
                    return "Януари";
            }
        }

        private List<Transaction> SortTransactions(List<Transaction> transactions, string sortBy)
        {
            if (sortBy == null || sortBy == "Date")
            {
                return transactions.OrderBy(t => t.Date).ThenBy(t => t.Name).ToList();
            }
            else if (sortBy == "Name")
            {
                return transactions.OrderBy(t => t.Name).ToList();
            }
            else if (sortBy == "Value")
            {
                return transactions.OrderByDescending(t => t.Value).ThenBy(t => t.Name).ToList();
            }
            else if (sortBy == "IsDividend")
            {
                return transactions.OrderByDescending(t => t.IsDividend).ThenByDescending(t => t.Value).ThenBy(t => t.Name).ToList();
            }

            return transactions;
        }
    }
}
