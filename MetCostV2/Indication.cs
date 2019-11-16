using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetCostV2
{
    public class Indication
    {
        public Water Water_Kitchen { get; set; }
        public Water Water_Bathroom { get; set; }
        public Electricity Electricity { get; set; }
        public DateTime Date { get; set; }

        public Indication(Water _water_k, Water _water_b, Electricity _electricity, DateTime _date)
        {
            Water_Kitchen = _water_k;
            Water_Bathroom = _water_b;
            Electricity = _electricity;
            Date = _date;
        }

        public double GetSumm()
        {
            return Water_Kitchen.GetSumm() + Water_Bathroom.GetSumm() + Electricity.GetSumm();
        }

        public static double GetDifference(Indication self, Indication record)
        {
            return Math.Ceiling(record.GetSumm() - self.GetSumm());
        }
    }
}
