using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GAM.Models
{
    public class Settings
    {
        public int SettingsId { get; set; }
        public TimeSpan? HappyHourBegin { get; set; }
        public TimeSpan? HappyHourEnd { get; set; }
        public string MatchFormula { get; set; }
        public double PhotoMatchValue { get; set; }
    }
}
