using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FinancialSecurityCalculator.Context
{
     public class NationLimitIndicator
    {
        [Key, Required]
        public int NationLimitIndicatorId { get; set; }

        public string NationLimitIndicatorName { get; set; }
        public double NationLimitIndicatorValue { get; set; }
    }
}
