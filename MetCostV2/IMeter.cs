using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetCostV2
{
    interface IMeter
    {
        double GetSumm();
        void SetTariff(string name, double cost);
    }
}
