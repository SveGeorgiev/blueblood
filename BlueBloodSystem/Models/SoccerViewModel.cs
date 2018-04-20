using System.Collections.Generic;
using System.Linq;

namespace BlueBloodSystem.Models
{
    public class SoccerViewModel
    {
        public SoccerViewModel(List<Soccer> entity)
        {
            this.SoccerList = entity;
            this.WinsCount = entity.Count(e => e.Score == Score.Win);
            this.DrawsCount = entity.Count(e => e.Score == Score.Draw);
            this.LostCount = entity.Count(e => e.Score == Score.Lose);
        }

        public List<Soccer> SoccerList { get; set; }
        public int WinsCount { get; set; }
        public int DrawsCount { get; set; }
        public int LostCount { get; set; }
    }
}