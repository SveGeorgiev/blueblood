using BlueBloodSystem.Models;
using BlueBloodSystem.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BlueBloodSystem.Controllers
{
    public class TransactionController : Controller
    {
        private TransactionService transactionService = new TransactionService();

        // GET: Transaction
        public async Task<ActionResult> Index(int? month, int? year, string sortBy)
        {
            int chosenMonth = month.HasValue ? month.Value : DateTime.Now.Month;
            int chosenYear = year.HasValue ? year.Value : DateTime.Now.Year;

            List<Transaction> transactions = await transactionService.GetTransactionsByMonthAndYearAsync(chosenMonth, chosenYear);
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

        // GET: Transaction/Create
        public ActionResult Create()
        {
            return View(new Transaction
            {
                Date = DateTime.Today,
                Value = 20,
                IsDividend = true
            });
        }

        // POST: Transaction/Create
        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Date,Value,IsDividend")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                transaction.Id = Guid.NewGuid();
                await transactionService.CreateTransactionAsync(transaction);
                return RedirectToAction("Index");
            }

            return View(transaction);
        }

        // GET: Transaction/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = await transactionService.GetTransactionAsync(id.Value);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Transaction/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Date,Value,IsDividend")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                await transactionService.UpdateTransactionAsync(transaction);
                return RedirectToAction("Index");
            }
            return View(transaction);
        }

        // GET: Transaction/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = await transactionService.GetTransactionAsync(id.Value);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            await transactionService.DeleteTransactionAsync(id);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Search(string transactionName)
        {
            if (!string.IsNullOrEmpty(transactionName))
            {
                List<Transaction> transactions = await transactionService.GetTransactionsByNameAsync(transactionName);
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

        public async Task<ActionResult> AllMonths(int year)
        {
            List<Transaction> transactions = await transactionService.GetTransactionsByYearAsync(year);
            var transactionsViewModel = new List<TransactionsViewModel>();

            for (int i = 1; i <= 12; i++)
            {
                transactionsViewModel = SetTransactionsViewModel(transactionsViewModel, transactions, year, i);
            }

            return View(new TransactionsAllViewModel
            {
                TransactionsViewModel = transactionsViewModel,
                IncomingSumYear = transactions.Where(t => t.IsDividend).Sum(t => t.Value),
                OutgoingSumYear = transactions.Where(t => !t.IsDividend).Sum(t => t.Value),
                TotalSumYear = transactions.Where(t => t.IsDividend).Sum(t => t.Value) - transactions.Where(t => !t.IsDividend).Sum(t => t.Value)
            });
        }

        private List<TransactionsViewModel> SetTransactionsViewModel(List<TransactionsViewModel> transactionsViewModel, List<Transaction> transactions, int year, int month)
        {
            var transactionsByMotn = transactions.Where(x => x.Date.Month == month);
            var incomingTransactionsSum = transactionsByMotn.Where(t => t.IsDividend).Sum(t => t.Value);
            var outgoingTransactionsSum = transactionsByMotn.Where(t => !t.IsDividend).Sum(t => t.Value);

            transactionsViewModel.Add(new TransactionsViewModel
            {
                MonthName = SetMonthName(month),
                Year = year,
                IncomingSum = incomingTransactionsSum,
                OutgoingSum = outgoingTransactionsSum,
                Total = incomingTransactionsSum - outgoingTransactionsSum
            });

            return transactionsViewModel;
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
