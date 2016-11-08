using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FinancialSecurityCalculator.Entities
{
    public class Record
    {
        [Key, Required]
        public int RecordId { get; set; }

        public int Year { get; set; }

        public virtual Enterprise Enterprise { get; set; }

        public virtual List<EnterpriseIndicator> EnterpriseIndicators { get; set; }
    }
}
