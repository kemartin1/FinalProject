using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    class Tax: Product
    {
        double taxAmount = 0.06 ;

        public double TaxAdded(double total)
        {
            return (total * taxAmount) + total;
        }
    }
}
