using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities
{
    public class UKCalendar : Calendar
    {
        public UKCalendar()
        {
            weekend_days.Add(DayOfWeek.Saturday, true);
            weekend_days.Add(DayOfWeek.Sunday, true);

            set_predefined_holidays();
        }

        public override bool isBankHoliday_rules(DateTime date)
        {
            int d = date.Day;
            DayOfWeek w = date.DayOfWeek;
            Month m = (Month)date.Month;
            int y = date.Year;
            DateTime em = easterMonday(date);

            return w == DayOfWeek.Saturday || w == DayOfWeek.Sunday
                // New Year's Day (possibly moved to Monday)
                || ((d == 1 || ((d == 2 || d == 3) && w == DayOfWeek.Monday)) && m == Month.January)
                // Good Friday
                || (date == em.AddDays(-3))
                // Easter Monday
                || (date == em)
                // first Monday of May (Early May Bank Holiday)
                || (d <= 7 && w == DayOfWeek.Monday && m == Month.May)
                // last Monday of May (Spring Bank Holiday)
                || (d >= 25 && w == DayOfWeek.Monday && m == Month.May && y != 2002 && y != 2012)
                // last Monday of August (Summer Bank Holiday)
                || (d >= 25 && w == DayOfWeek.Monday && m == Month.August)
                // Christmas (possibly moved to Monday or Tuesday)
                || ((d == 25 || (d == 27 && (w == DayOfWeek.Monday || w == DayOfWeek.Tuesday))) && m == Month.December)
                // Boxing Day (possibly moved to Monday or Tuesday)
                || ((d == 26 || (d == 28 && (w == DayOfWeek.Monday || w == DayOfWeek.Tuesday))) && m == Month.December)
                // June 3rd, 2002 only (Golden Jubilee Bank Holiday)
                // June 4rd, 2002 only (special Spring Bank Holiday)
                || ((d == 3 || d == 4) && m == Month.June && y == 2002)
                // June 4th, 2012 only (Golden Jubilee Bank Holiday)
                // June 5th, 2012 only (Golden Jubilee Bank Holiday)
                || ((d == 4 || d == 5) && m == Month.June && y == 2012)
                // December 31st, 1999 only
                || (d == 31 && m == Month.December && y == 1999);
        }


        private void set_predefined_holidays()
        {
            MinDate = new DateTime(2000, 1, 1);
            holidays.Add(new DateTime(2000, 1, 3), true);
            holidays.Add(new DateTime(2000, 4, 21), true);
            holidays.Add(new DateTime(2000, 4, 24), true);
            holidays.Add(new DateTime(2000, 5, 1), true);
            holidays.Add(new DateTime(2000, 5, 29), true);
            holidays.Add(new DateTime(2000, 8, 28), true);
            holidays.Add(new DateTime(2000, 12, 25), true);
            holidays.Add(new DateTime(2000, 12, 26), true);
            holidays.Add(new DateTime(2001, 1, 1), true);
            holidays.Add(new DateTime(2001, 4, 13), true);
            holidays.Add(new DateTime(2001, 4, 16), true);
            holidays.Add(new DateTime(2001, 5, 7), true);
            holidays.Add(new DateTime(2001, 5, 28), true);
            holidays.Add(new DateTime(2001, 8, 27), true);
            holidays.Add(new DateTime(2001, 12, 25), true);
            holidays.Add(new DateTime(2001, 12, 26), true);
            holidays.Add(new DateTime(2002, 1, 1), true);
            holidays.Add(new DateTime(2002, 3, 29), true);
            holidays.Add(new DateTime(2002, 4, 1), true);
            holidays.Add(new DateTime(2002, 5, 6), true);
            holidays.Add(new DateTime(2002, 6, 3), true);
            holidays.Add(new DateTime(2002, 6, 4), true);
            holidays.Add(new DateTime(2002, 8, 26), true);
            holidays.Add(new DateTime(2002, 12, 25), true);
            holidays.Add(new DateTime(2002, 12, 26), true);
            holidays.Add(new DateTime(2003, 1, 1), true);
            holidays.Add(new DateTime(2003, 4, 18), true);
            holidays.Add(new DateTime(2003, 4, 21), true);
            holidays.Add(new DateTime(2003, 5, 5), true);
            holidays.Add(new DateTime(2003, 5, 26), true);
            holidays.Add(new DateTime(2003, 8, 25), true);
            holidays.Add(new DateTime(2003, 12, 25), true);
            holidays.Add(new DateTime(2003, 12, 26), true);
            holidays.Add(new DateTime(2004, 1, 1), true);
            holidays.Add(new DateTime(2004, 4, 9), true);
            holidays.Add(new DateTime(2004, 4, 12), true);
            holidays.Add(new DateTime(2004, 5, 3), true);
            holidays.Add(new DateTime(2004, 5, 31), true);
            holidays.Add(new DateTime(2004, 8, 30), true);
            holidays.Add(new DateTime(2004, 12, 27), true);
            holidays.Add(new DateTime(2004, 12, 28), true);
            holidays.Add(new DateTime(2005, 1, 3), true);
            holidays.Add(new DateTime(2005, 3, 25), true);
            holidays.Add(new DateTime(2005, 3, 28), true);
            holidays.Add(new DateTime(2005, 5, 2), true);
            holidays.Add(new DateTime(2005, 5, 30), true);
            holidays.Add(new DateTime(2005, 8, 29), true);
            holidays.Add(new DateTime(2005, 12, 26), true);
            holidays.Add(new DateTime(2005, 12, 27), true);
            holidays.Add(new DateTime(2006, 1, 2), true);
            holidays.Add(new DateTime(2006, 4, 14), true);
            holidays.Add(new DateTime(2006, 4, 17), true);
            holidays.Add(new DateTime(2006, 5, 1), true);
            holidays.Add(new DateTime(2006, 5, 29), true);
            holidays.Add(new DateTime(2006, 8, 28), true);
            holidays.Add(new DateTime(2006, 12, 25), true);
            holidays.Add(new DateTime(2006, 12, 26), true);
            holidays.Add(new DateTime(2007, 1, 1), true);
            holidays.Add(new DateTime(2007, 4, 6), true);
            holidays.Add(new DateTime(2007, 4, 9), true);
            holidays.Add(new DateTime(2007, 5, 7), true);
            holidays.Add(new DateTime(2007, 5, 28), true);
            holidays.Add(new DateTime(2007, 8, 27), true);
            holidays.Add(new DateTime(2007, 12, 25), true);
            holidays.Add(new DateTime(2007, 12, 26), true);
            holidays.Add(new DateTime(2008, 1, 1), true);
            holidays.Add(new DateTime(2008, 3, 21), true);
            holidays.Add(new DateTime(2008, 3, 24), true);
            holidays.Add(new DateTime(2008, 5, 5), true);
            holidays.Add(new DateTime(2008, 5, 26), true);
            holidays.Add(new DateTime(2008, 8, 25), true);
            holidays.Add(new DateTime(2008, 12, 25), true);
            holidays.Add(new DateTime(2008, 12, 26), true);
            holidays.Add(new DateTime(2009, 1, 1), true);
            holidays.Add(new DateTime(2009, 4, 10), true);
            holidays.Add(new DateTime(2009, 4, 13), true);
            holidays.Add(new DateTime(2009, 5, 4), true);
            holidays.Add(new DateTime(2009, 5, 25), true);
            holidays.Add(new DateTime(2009, 8, 31), true);
            holidays.Add(new DateTime(2009, 12, 25), true);
            holidays.Add(new DateTime(2009, 12, 28), true);
            holidays.Add(new DateTime(2010, 1, 1), true);
            holidays.Add(new DateTime(2010, 4, 2), true);
            holidays.Add(new DateTime(2010, 4, 5), true);
            holidays.Add(new DateTime(2010, 5, 3), true);
            holidays.Add(new DateTime(2010, 5, 31), true);
            holidays.Add(new DateTime(2010, 8, 30), true);
            holidays.Add(new DateTime(2010, 12, 27), true);
            holidays.Add(new DateTime(2010, 12, 28), true);
            holidays.Add(new DateTime(2011, 1, 3), true);
            holidays.Add(new DateTime(2011, 4, 22), true);
            holidays.Add(new DateTime(2011, 4, 25), true);
            holidays.Add(new DateTime(2011, 5, 2), true);
            holidays.Add(new DateTime(2011, 5, 30), true);
            holidays.Add(new DateTime(2011, 8, 29), true);
            holidays.Add(new DateTime(2011, 12, 26), true);
            holidays.Add(new DateTime(2011, 12, 27), true);
            holidays.Add(new DateTime(2012, 1, 2), true);
            holidays.Add(new DateTime(2012, 4, 6), true);
            holidays.Add(new DateTime(2012, 4, 9), true);
            holidays.Add(new DateTime(2012, 5, 7), true);
            holidays.Add(new DateTime(2012, 6, 4), true);
            holidays.Add(new DateTime(2012, 6, 5), true);
            holidays.Add(new DateTime(2012, 8, 27), true);
            holidays.Add(new DateTime(2012, 12, 25), true);
            holidays.Add(new DateTime(2012, 12, 26), true);
            holidays.Add(new DateTime(2013, 1, 1), true);
            holidays.Add(new DateTime(2013, 3, 29), true);
            holidays.Add(new DateTime(2013, 4, 1), true);
            holidays.Add(new DateTime(2013, 5, 6), true);
            holidays.Add(new DateTime(2013, 5, 27), true);
            holidays.Add(new DateTime(2013, 8, 26), true);
            holidays.Add(new DateTime(2013, 12, 25), true);
            holidays.Add(new DateTime(2013, 12, 26), true);
            holidays.Add(new DateTime(2014, 1, 1), true);
            holidays.Add(new DateTime(2014, 4, 18), true);
            holidays.Add(new DateTime(2014, 4, 21), true);
            holidays.Add(new DateTime(2014, 5, 5), true);
            holidays.Add(new DateTime(2014, 5, 26), true);
            holidays.Add(new DateTime(2014, 8, 25), true);
            holidays.Add(new DateTime(2014, 12, 25), true);
            holidays.Add(new DateTime(2014, 12, 26), true);
            holidays.Add(new DateTime(2015, 1, 1), true);
            holidays.Add(new DateTime(2015, 4, 3), true);
            holidays.Add(new DateTime(2015, 4, 6), true);
            holidays.Add(new DateTime(2015, 5, 4), true);
            holidays.Add(new DateTime(2015, 5, 25), true);
            holidays.Add(new DateTime(2015, 8, 31), true);
            holidays.Add(new DateTime(2015, 12, 25), true);
            holidays.Add(new DateTime(2015, 12, 28), true);
            holidays.Add(new DateTime(2016, 1, 1), true);
            holidays.Add(new DateTime(2016, 3, 25), true);
            holidays.Add(new DateTime(2016, 3, 28), true);
            holidays.Add(new DateTime(2016, 5, 2), true);
            holidays.Add(new DateTime(2016, 5, 30), true);
            holidays.Add(new DateTime(2016, 8, 29), true);
            holidays.Add(new DateTime(2016, 12, 26), true);
            holidays.Add(new DateTime(2016, 12, 27), true);
            holidays.Add(new DateTime(2017, 1, 2), true);
            holidays.Add(new DateTime(2017, 4, 14), true);
            holidays.Add(new DateTime(2017, 4, 17), true);
            holidays.Add(new DateTime(2017, 5, 1), true);
            holidays.Add(new DateTime(2017, 5, 29), true);
            holidays.Add(new DateTime(2017, 8, 28), true);
            holidays.Add(new DateTime(2017, 12, 25), true);
            holidays.Add(new DateTime(2017, 12, 26), true);
            holidays.Add(new DateTime(2018, 1, 1), true);
            holidays.Add(new DateTime(2018, 3, 30), true);
            holidays.Add(new DateTime(2018, 4, 2), true);
            holidays.Add(new DateTime(2018, 5, 7), true);
            holidays.Add(new DateTime(2018, 5, 28), true);
            holidays.Add(new DateTime(2018, 8, 27), true);
            holidays.Add(new DateTime(2018, 12, 25), true);
            holidays.Add(new DateTime(2018, 12, 26), true);
            holidays.Add(new DateTime(2019, 1, 1), true);
            holidays.Add(new DateTime(2019, 4, 19), true);
            holidays.Add(new DateTime(2019, 4, 22), true);
            holidays.Add(new DateTime(2019, 5, 6), true);
            holidays.Add(new DateTime(2019, 5, 27), true);
            holidays.Add(new DateTime(2019, 8, 26), true);
            holidays.Add(new DateTime(2019, 12, 25), true);
            holidays.Add(new DateTime(2019, 12, 26), true);
            holidays.Add(new DateTime(2020, 1, 1), true);
            holidays.Add(new DateTime(2020, 4, 10), true);
            holidays.Add(new DateTime(2020, 4, 13), true);
            holidays.Add(new DateTime(2020, 5, 4), true);
            holidays.Add(new DateTime(2020, 5, 25), true);
            holidays.Add(new DateTime(2020, 8, 31), true);
            holidays.Add(new DateTime(2020, 12, 25), true);
            holidays.Add(new DateTime(2020, 12, 28), true);
            MaxDate = new DateTime(2020, 12, 31);
        }

        static int[] DayOfYear = {
                  98,  90, 103,  95, 114, 106,  91, 111, 102,   // 1901-1909
             87, 107,  99,  83, 103,  95, 115,  99,  91, 111,   // 1910-1919
             96,  87, 107,  92, 112, 103,  95, 108, 100,  91,   // 1920-1929
            111,  96,  88, 107,  92, 112, 104,  88, 108, 100,   // 1930-1939
             85, 104,  96, 116, 101,  92, 112,  97,  89, 108,   // 1940-1949
            100,  85, 105,  96, 109, 101,  93, 112,  97,  89,   // 1950-1959
            109,  93, 113, 105,  90, 109, 101,  86, 106,  97,   // 1960-1969
             89, 102,  94, 113, 105,  90, 110, 101,  86, 106,   // 1970-1979
             98, 110, 102,  94, 114,  98,  90, 110,  95,  86,   // 1980-1989
            106,  91, 111, 102,  94, 107,  99,  90, 103,  95,   // 1990-1999
            115, 106,  91, 111, 103,  87, 107,  99,  84, 103,   // 2000-2009
             95, 115, 100,  91, 111,  96,  88, 107,  92, 112,   // 2010-2019
            104,  95, 108, 100,  92, 111,  96,  88, 108,  92,   // 2020-2029
            112, 104,  89, 108, 100,  85, 105,  96, 116, 101,   // 2030-2039
             93, 112,  97,  89, 109, 100,  85, 105,  97, 109,   // 2040-2049
            101,  93, 113,  97,  89, 109,  94, 113, 105,  90,   // 2050-2059
            110, 101,  86, 106,  98,  89, 102,  94, 114, 105,   // 2060-2069
             90, 110, 102,  86, 106,  98, 111, 102,  94, 114,   // 2070-2079
             99,  90, 110,  95,  87, 106,  91, 111, 103,  94,   // 2080-2089
            107,  99,  91, 103,  95, 115, 107,  91, 111, 103,   // 2090-2099
             88, 108, 100,  85, 105,  96, 109, 101,  93, 112,   // 2100-2109
             97,  89, 109,  93, 113, 105,  90, 109, 101,  86,   // 2110-2119
            106,  97,  89, 102,  94, 113, 105,  90, 110, 101,   // 2120-2129
             86, 106,  98, 110, 102,  94, 114,  98,  90, 110,   // 2130-2139
             95,  86, 106,  91, 111, 102,  94, 107,  99,  90,   // 2140-2149
            103,  95, 115, 106,  91, 111, 103,  87, 107,  99,   // 2150-2159
             84, 103,  95, 115, 100,  91, 111,  96,  88, 107,   // 2160-2169
             92, 112, 104,  95, 108, 100,  92, 111,  96,  88,   // 2170-2179
            108,  92, 112, 104,  89, 108, 100,  85, 105,  96,   // 2180-2189
            116, 101,  93, 112,  97,  89, 109, 100,  85, 105    // 2190-2199
        };

        private static DateTime easterMonday(DateTime input)
        {
            int index = input.Year - 1901;
            if (index < 0 || index >= DayOfYear.Length)
                throw new ApplicationException("Date out of range in EasterMonday() : " + input.ToString());
            return new DateTime(input.Year, 1, 1).AddDays(DayOfYear[index]-1);
        }
    }
}
