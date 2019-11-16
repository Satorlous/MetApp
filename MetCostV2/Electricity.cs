using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetCostV2
{
    public class Electricity : IMeter
    {
        private double tariff_Day = 2.01;
        private double tariff_Night = 0.99;

        public int Day { get; set; }
        public int Night { get; set; }

        public Electricity(int day, int night)
        {
            Day = day; Night = night;
        }

        public double GetSumm()
        {
            double result = 0;
            result += Day * tariff_Day;
            result += Night * tariff_Night;
            return result;
        }

        public void SetTariff(string name, double cost)
        {
            if (name == "day")
                tariff_Day = cost;
            else
                tariff_Night = cost;
        }
    }
}
