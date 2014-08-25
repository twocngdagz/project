using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities
{
    public class FormulaRepaymentSchedule : FormulaSchedule
    {
        public FormulaRepaymentSchedule(Environment env, List<int> nperiods, List<Rate> rates, DateTime firstdate, int day_of_month, DateTime drawdowndate, double drawdownamount)
            : base(env, nperiods, rates, firstdate, day_of_month, drawdowndate, drawdownamount)
        {
            double balance = drawdownamount;
            for (int iper = 0; iper < nper; ++iper)
            {
                ReducingBalanceCalc rbc = new ReducingBalanceCalc();
                rbc.interest = interest_reducingbalance(iper, balance);
                rbc.capital = capital_reducingbalance(iper, balance);
                balance -= rbc.capital;

                rbcs.Add(rbc);
            }
            // balance should be zero here, but just in case of some rounding:
            ReducingBalanceCalc last_rbc = rbcs[rbcs.Count - 1];
            last_rbc.capital += balance;
        }

        public double interest_reducingbalance(int period, double balance)
        {
            if (period < 0)
                return 0e0;
            else
                return Ipmt(rate(period) / 12, 1, nper-period, -balance, 0, 0);
        }

        public double capital_reducingbalance(int period, double balance)
        {
            if (period < 0)
                return 0e0;
            else
                return Ppmt(rate(period) / 12, 1, nper - period, -balance, 0, 0);
        }

        static double Pmt(double r, int nper, double pv, double fv, int type)
        {
            double pmt = r / (Math.Pow(1 + r, nper) - 1)
                    * -(pv * Math.Pow(1 + r, nper) + fv);

            return pmt;
        }

        static double Fv(double r, int nper, double c, double pv, int type)
        {
            double fv = -(c * (Math.Pow(1 + r, nper) - 1) / r + pv
                    * Math.Pow(1 + r, nper));

            return fv;
        }
        static double Ipmt(double r, int per, int nper, double pv, double fv, int type)
        {

            double ipmt = Fv(r, per - 1, Pmt(r, nper, pv, fv, type), pv, type) * r;

            if (type == 1) ipmt /= (1 + r);

            return ipmt;
        }
        static double Ppmt(double r, int per, int nper, double pv, double fv, int type)
        {
            return Pmt(r, nper, pv, fv, type) - Ipmt(r, per, nper, pv, fv, type);
        }
    }
}
