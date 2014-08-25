using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities
{
    public class FormulaInterestOnlySchedule : FormulaSchedule
    {
        public FormulaInterestOnlySchedule(Environment env, List<int> nperiods, List<Rate> rates, DateTime firstdate, int day_of_month, DateTime drawdowndate, double drawdownamount)
            : base(env, nperiods, rates, firstdate, day_of_month, drawdowndate, drawdownamount)
        {
            double balance = drawdownamount;
            for (int iper = 0; iper < nper; ++iper)
            {
                ReducingBalanceCalc rbc = new ReducingBalanceCalc();
                rbc.interest = rate(iper) / 12 * balance;
                rbc.capital = 0e0;

                rbcs.Add(rbc);
            }
            ReducingBalanceCalc last_rbc = rbcs[rbcs.Count - 1];
            last_rbc.capital += balance;
        }
    }
}
