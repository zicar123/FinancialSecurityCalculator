using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FinancialSecurityCalculator.Context
{
    public class EnterpriseLimitIndicator
    {
        [Key, Required]
        public int EnterpriseLimitIndicatorId { get; set; }

        public string EnterpriseLimitIndicatorName { get; set; }
        public string EnterpriseLimitIndicatorValue { get; set; }
    }
}
