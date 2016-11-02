using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace FinancialSecurityCalculator.Context
{
    public class FSCContext : DbContext
    {
        public DbSet<Enterprise> Enterprise { get; set; }
        public DbSet<EnterpriseIndicator> EnterpriseIndicator { get; set; }
        public DbSet<Nation> Nation { get; set; }
        public DbSet<NationIndicator> NationIndicator { get; set; }
        public DbSet<EnterpriseLimitIndicator> EnterpriseLimitIndicators { get; set; }
        public DbSet<NationLimitIndicator> NationLimitIndicators { get; set; }

        public FSCContext() : base()
        {
            Database.SetInitializer<FSCContext>(new FSCContextInitializer());
        }
    }
}
