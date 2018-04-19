using System.ComponentModel.DataAnnotations;

namespace BlueBloodSystem.Models
{
    public enum Score
    {
        [Display(Name = "Победа")]
        Win = 1,
        [Display(Name = "Равен")]
        Draw = 3,
        [Display(Name = "Загуба")]
        Lose = 2
    }
}