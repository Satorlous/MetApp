using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetCostV2
{
    public class Water : IMeter
    {
        private double tariff_Cold = 49.97;
        private double tariff_Hot = 117.05;

        public int Cold { get; set; }
        public int Hot { get; set; }

        public Water(int cold, int hot)
        {
            Cold = cold; Hot = hot;
        }
        public void SetTariff(string name, double cost)
        {
            if (name == "hot")
                tariff_Hot = cost;
            else
                tariff_Cold = cost;
        }
        
        public double GetSumm()
        {
            double result = 0;
            result += Cold * tariff_Cold;
            result += Hot * tariff_Hot;
            return result;
        }
    }
}
