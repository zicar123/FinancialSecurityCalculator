using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinancialSecurityCalculator.Entities
{
    public class Enterprise
    {
        [Key, Required]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string  EnterpriseId { get; set; }
        public string EnterpriseName { get; set; }
        public string Region { get; set; }
        public string Branch { get; set; }

        public virtual List<Record> Records { get; set; }
    }
}
