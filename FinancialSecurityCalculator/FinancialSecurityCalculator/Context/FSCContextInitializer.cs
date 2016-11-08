using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using FinancialSecurityCalculator.Entities;

namespace FinancialSecurityCalculator.Context
{
    public class FSCContextInitializer : CreateDatabaseIfNotExists<FSCContext>
    {
        protected override void Seed(FSCContext context)
        {
            context.EnterpriseLimitIndicators.Add(new EnterpriseLimitIndicator() { EnterpriseLimitIndicatorName = "Коефіцієнт фінансової незалежності", EnterpriseLimitIndicatorValue = "0.5" });
            context.EnterpriseLimitIndicators.Add(new EnterpriseLimitIndicator() { EnterpriseLimitIndicatorName = "Коефіцієнт фінансової стабільності", EnterpriseLimitIndicatorValue = "0.8" });
            context.EnterpriseLimitIndicators.Add(new EnterpriseLimitIndicator() { EnterpriseLimitIndicatorName = "Коефіцієнт фінансової стійкості", EnterpriseLimitIndicatorValue = "0.75-0.9" });
            context.EnterpriseLimitIndicators.Add(new EnterpriseLimitIndicator() { EnterpriseLimitIndicatorName = "Коефіцієнт маневреності власних засобів", EnterpriseLimitIndicatorValue = "0.3-0.5" });
            context.EnterpriseLimitIndicators.Add(new EnterpriseLimitIndicator() { EnterpriseLimitIndicatorName = "Коефіцієнт забезпечення власними оборотними засобами", EnterpriseLimitIndicatorValue = ">0.1" });
            context.EnterpriseLimitIndicators.Add(new EnterpriseLimitIndicator() { EnterpriseLimitIndicatorName = "Коефіцієнт грошової платоспроможності", EnterpriseLimitIndicatorValue = "0.2-0.35" });
            context.EnterpriseLimitIndicators.Add(new EnterpriseLimitIndicator() { EnterpriseLimitIndicatorName = "Коефіцієнт розрахункової платоспроможності", EnterpriseLimitIndicatorValue = "збільшення" });
            context.EnterpriseLimitIndicators.Add(new EnterpriseLimitIndicator() { EnterpriseLimitIndicatorName = "Коефіцієнт критичної ліквідності", EnterpriseLimitIndicatorValue = "0.7-1" });
            context.EnterpriseLimitIndicators.Add(new EnterpriseLimitIndicator() { EnterpriseLimitIndicatorName = "Коефіцієнт співвідношення", EnterpriseLimitIndicatorValue = "<1" });
            context.EnterpriseLimitIndicators.Add(new EnterpriseLimitIndicator() { EnterpriseLimitIndicatorName = "Коефіцієнт мобільності активів", EnterpriseLimitIndicatorValue = ">0.1" });
            context.EnterpriseLimitIndicators.Add(new EnterpriseLimitIndicator() { EnterpriseLimitIndicatorName = "Коефіцієнт оборотності активів", EnterpriseLimitIndicatorValue = "збільшення" });
            context.EnterpriseLimitIndicators.Add(new EnterpriseLimitIndicator() { EnterpriseLimitIndicatorName = "Коефіцієнт оборотності дебіторської заборгованості", EnterpriseLimitIndicatorValue = "збільшення" });
            context.EnterpriseLimitIndicators.Add(new EnterpriseLimitIndicator() { EnterpriseLimitIndicatorName = "Коефіцієнт оборотності кредиторської заборгованості", EnterpriseLimitIndicatorValue = "збільшення" });
            context.EnterpriseLimitIndicators.Add(new EnterpriseLimitIndicator() { EnterpriseLimitIndicatorName = "Коефіцієнт оборотності матеріальних запасів", EnterpriseLimitIndicatorValue = "збільшення" });
            context.EnterpriseLimitIndicators.Add(new EnterpriseLimitIndicator() { EnterpriseLimitIndicatorName = "Коефіцієнт оборотності основних засобів", EnterpriseLimitIndicatorValue = "збільшення" });
            context.EnterpriseLimitIndicators.Add(new EnterpriseLimitIndicator() { EnterpriseLimitIndicatorName = "Коефіцієнт оборотності власного капіталу", EnterpriseLimitIndicatorValue = "збільшення" });
            context.EnterpriseLimitIndicators.Add(new EnterpriseLimitIndicator() { EnterpriseLimitIndicatorName = "Рентабельність витрат", EnterpriseLimitIndicatorValue = "збільшення" });
            context.EnterpriseLimitIndicators.Add(new EnterpriseLimitIndicator() { EnterpriseLimitIndicatorName = "Рентабельність продажів", EnterpriseLimitIndicatorValue = "збільшення" });
            context.EnterpriseLimitIndicators.Add(new EnterpriseLimitIndicator() { EnterpriseLimitIndicatorName = "Рентабельність всіх активів", EnterpriseLimitIndicatorValue = "збільшення" });
            context.EnterpriseLimitIndicators.Add(new EnterpriseLimitIndicator() { EnterpriseLimitIndicatorName = "Рентабельність власного капіталу", EnterpriseLimitIndicatorValue = "збільшення" });

            base.Seed(context);
        }
    }
}
