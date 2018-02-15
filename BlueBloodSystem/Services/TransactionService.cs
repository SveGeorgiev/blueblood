using BlueBloodSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace BlueBloodSystem.Services
{
    public class TransactionService
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public async Task<List<Transaction>> GetTransactionsByMonthAndYearAsync(int month, int year)
        {
            return await db.Transactions.Where(t => t.Date.Month == month && t.Date.Year == year).ToListAsync();
        }

        public async Task<List<Transaction>> GetTransactionsByNameAsync(string name)
        {
            return await db.Transactions.Where(t => t.Name.Contains(name)).OrderBy(t => t.Name).ThenBy(t => t.Date).ToListAsync();
        }

        public async Task CreateTransactionAsync(Transaction transaction)
        {
            db.Transactions.Add(transaction);
            await db.SaveChangesAsync();
        }

        public async Task<Transaction> GetTransactionAsync(Guid id)
        {
            return await db.Transactions.FindAsync(id);
        }

        public async Task UpdateTransactionAsync(Transaction transaction)
        {
            db.Entry(transaction).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task DeleteTransactionAsync(Guid id)
        {
            Transaction transaction = await this.GetTransactionAsync(id);
            db.Transactions.Remove(transaction);
            await db.SaveChangesAsync();
        }
    }
}