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

        public ActionResult Index()
        {
            List<Transaction> transactions = TransactionService.GetTransactions();
            var data = new TransactonsViewModel
            {
                Transactions = transactions,
                Incoming = transactions.Where(t => t.IsDividend).Sum(x => x.Value),
                Outgoin = transactions.Where(t => !t.IsDividend).Sum(x => x.Value),
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
