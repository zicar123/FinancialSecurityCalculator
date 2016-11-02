using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FinancialSecurityCalculator.Context
{
    public class Nation
    {
        [Key, Required]
        public int NationId { get; set; }

        public string NationName { get; set; }

        public virtual List<NationIndicator> NationIndicators { get; set; }
    }
}
