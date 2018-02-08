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
        public TransactionService TransactionService => new TransactionService();

        public ActionResult Index(int? month)
        {
            List<Transaction> transactions = TransactionService.GetTransactions();
            string monthName = string.Empty;
            int currentMoth = month.HasValue ? month.Value : DateTime.Now.Month;

            switch (currentMoth)
            {
                case 1:
                    monthName = "Януари";
                    break;
                case 2:
                    monthName = "Февруари";
                    break;
                case 3:
                    monthName = "Март";
                    break;
                case 4:
                    monthName = "Април";
                    break;
                case 5:
                    monthName = "Май";
                    break;
                case 6:
                    monthName = "Юни";
                    break;
                case 7:
                    monthName = "Юли";
                    break;
                case 8:
                    monthName = "Август";
                    break;
                case 9:
                    monthName = "Септември";
                    break;
                case 10:
                    monthName = "Октомври";
                    break;
                case 11:
                    monthName = "Ноември";
                    break;
                case 12:
                    monthName = "Декември";
                    break;
                default:
                    break;
            }

            var data = new TransactonsViewModel
            {
                Transactions = transactions,
                Incoming = transactions.Where(t => t.IsDividend).Sum(x => x.Value),
                Outgoin = transactions.Where(t => !t.IsDividend).Sum(x => x.Value),
                Month = monthName
            };
            data.Total = data.Incoming - data.Outgoin;

            return View(data);
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Transaction transaction)//FormCollection collection)
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
    }
}
