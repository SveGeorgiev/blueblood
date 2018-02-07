using System;
using System.ComponentModel.DataAnnotations;

namespace BlueBloodSystem.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Името е задължително!")]
        [Display(Name = "Име")]
        public string Name { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Дата")]
        [Required(ErrorMessage = "Датата е задължителна!")]
        public DateTime Date { get; set; }
        [Display(Name = "Стойност")]
        [Required(ErrorMessage = "Стойноста е задължителна!")]
        public decimal Value { get; set; }
        [Display(Name = "Приход/Разход")]
        public bool IsDividend { get; set; }
    }
}