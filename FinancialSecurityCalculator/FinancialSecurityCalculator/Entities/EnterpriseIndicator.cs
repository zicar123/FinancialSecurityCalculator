using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FinancialSecurityCalculator.Entities
{
    public class EnterpriseIndicator
    {
        [Key, Required]
        public int EnterpriseIndicatorId { get; set; }

        public string IndicatorName { get; set; }
        public double IndicatorValue { get; set; }

        public virtual Record Record { get; set; }
    }
}
