using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FinancialSecurityCalculator.Context
{
    public class NationIndicator
    {
        [Key, Required]
        public int IndicatorId { get; set; }

        public string IndicatorName { get; set; }
        public double IndicatorValue { get; set; }

        public virtual Nation Nation { get; set; }
    }
}
