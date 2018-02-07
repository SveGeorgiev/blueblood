using BlueBloodSystem.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BlueBloodSystem.Services
{
    public class JsonConvertService
    {
        public void WriteToFile(string fileName, List<Transaction> items)
        {
            File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data\" + fileName), JsonConvert.SerializeObject(items));
        }

        public List<Transaction> GetAllTransactions(string filePath)
        {
            using (StreamReader r = new StreamReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data\" + filePath)))
            {
                string json = r.ReadToEnd();
                List<Transaction> transactions = JsonConvert.DeserializeObject<List<Transaction>>(json);

                if (transactions != null)
                {
                    return transactions;
                }

                return new List<Transaction>();
            }
        }

        public Transaction GetTransactionById(Guid Id, string filePath)
        {
            using (StreamReader r = new StreamReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data\" + filePath)))
            {
                string json = r.ReadToEnd();
                List<Transaction> transactions = JsonConvert.DeserializeObject<List<Transaction>>(json);
                return transactions.FirstOrDefault(t => t.Id == Id);
            }
        }
    }
}