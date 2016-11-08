using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using FinancialSecurityCalculator.Entities;

namespace FinancialSecurityCalculator.Context
{
    public class FSCContext : DbContext
    {
        public DbSet<Enterprise> Enterprise { get; set; }
        public DbSet<EnterpriseIndicator> EnterpriseIndicator { get; set; }
        public DbSet<Record> Record { get; set; }
        public DbSet<EnterpriseLimitIndicator> EnterpriseLimitIndicators { get; set; }

        public FSCContext() : base()
        {
            Database.SetInitializer<FSCContext>(new FSCContextInitializer());
        }
    }
}
