using System;
using System.ComponentModel.DataAnnotations;

namespace BlueBloodSystem.Models
{
    public class Soccer
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Полето е задължително.")]
        [Display(Name = "Опоненти")]
        [StringLength(200)]
        public string Opponents { get; set; }
        [Display(Name = "Резултат")]
        [StringLength(100)]
        public string Result { get; set; }
        [Required(ErrorMessage = "Полето е задължително.")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Дата")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Полето е задължително.")]
        public Score? Score { get; set; }
    }
}