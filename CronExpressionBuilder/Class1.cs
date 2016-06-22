using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CronExpressionBuilder
{
    public class CronExpression
    {
        public enum Frequency
        {
            Minute,
            Hour,
            Daily,
            Weekly,
            Monthly,
            Yearly
        }

        public enum Weekdays
        {
            MON,
            TUE,
            WED,
            THU,
            FRI,
            SAT,
            SUN
        }

        public enum Months
        {
            January = 1,
            February = 2,
            March = 3,
            April = 4,
            May = 5,
            June = 6,
            July = 7,
            August = 8,
            September = 9,
            October = 10,
            November = 11,
            December = 12

        }

        public enum Rank
        {
            First = 1,
            Second = 2,
            Third = 3,
            Fourth = 4
        }

        public List<Weekdays> weekdays = new List<Weekdays>();


        /// <summary>
        /// Run a task Every x minute
        /// </summary>
        /// <param name="MinuteInterval">Interval In Minutes</param>
        /// <returns> (String) Returns a cron Expression that runs after every x minute</returns>
        ///<example>IntervalinMinutes(2); output : "0 0/2 * 1/1 * ? *"</example>
        public string IntervalinMinutes(int MinuteInterval)
        {
            try
            {
                if (MinuteInterval <= 0)
                {
                    throw new CronExpressionBuilderError("Minimum Interval should be 1");
                }
                else
                {
                    return @"0 0/" + MinuteInterval + @" * 1/1 * ? *";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Run a task Every x Hour
        /// </summary>
        /// <param name="HourInterval">Interval In Hours</param>
        /// <returns>(String) Returns a cron Expression that runs after every x hours</returns>
        /// <example>IntervalInHours(2); output : "0 0 0/2 1/1 * ? *"</example>
        public string IntervalInHours(int HourInterval)
        {
            try
            {
                if (HourInterval <= 0)
                {
                    throw new CronExpressionBuilderError("Minimum Interval should be 1");
                }
                else
                {
                    return @"0 0 0/" + HourInterval + @" 1/1 * ? *";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        ///  Run a task Every x Day at specified time
        /// </summary>
        /// <param name="DayInterval">Interval In Days</param>
        /// <param name="StartHour">Min-0 Max-23</param>
        /// <param name="StartMinute">Min-0 Max-59</param>
        /// <returns>(String) Returns a cron Expression that runs at specified time every x days</returns>
        /// <example>IntervalInDays(1,20,10); output : "0 10 20 1/1 * ? *"</example>
        public string IntervalInDays(int DayInterval, int StartHour, int StartMinute)
        {
            try
            {
                if (DayInterval <= 0)
                {
                    throw new CronExpressionBuilderError("Minimum Interval should be 1");
                }
                else if (StartHour < 0 || StartHour > 23)
                {
                    throw new CronExpressionBuilderError("Hour should be between 0 to 24");
                }
                else if (StartMinute < 0 || StartMinute > 59)
                {
                    throw new CronExpressionBuilderError("Minute should be between 0 to 59");
                }
                else
                {
                    return "0 " + StartMinute + " " + StartHour + @" 1/" + DayInterval + " * ? *";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Run a task on Weekdays i.e. Monday to Friday at specified time
        /// </summary>
        /// <param name="StartHour">Min-0 Max-23</param>
        /// <param name="StartMinute">Min-0 Max-59</param>
        /// <returns>(String) Returns a cron Expression that runs at specified time on weekdays i.e. Monday to Friday</returns>
        /// <example>EveryWeekDay(21,22); output : "0 22 21 ? * MON-FRI *"</example>
        public string EveryWeekDay(int StartHour, int StartMinute)
        {
            try
            {
                if (StartHour < 0 || StartHour > 23)
                {
                    throw new CronExpressionBuilderError("Hour should be between 0 to 24");
                }
                else if (StartMinute < 0 || StartMinute > 59)
                {
                    throw new CronExpressionBuilderError("Minute should be between 0 to 59");
                }
                else
                {

                    return "0 " + StartMinute + " " + StartHour + " ? * MON-FRI *";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Run a task on specified days in a week at specified time 
        /// </summary>
        /// <param name="weekdays">(List) Days of the week </param>
        /// <param name="StartHour">Min-0 Max-23</param>
        /// <param name="StartMinute">Min-0 Max-59</param>
        /// <returns>(String) Returns a cron Expression that runs on specified days in a week at specified time</returns>
        /// <example>SpecificDaysInAWeek(c.weekdays,22,21); output : "0 21 22 ? * MON,WED,FRI *"</example>
        public string SpecificDaysInAWeek(List<Weekdays> weekdays, int StartHour, int StartMinute)
        {
            try
            {
                if (weekdays.Count <= 0)
                {
                    throw new CronExpressionBuilderError("Weekdays Not Specified");
                }
                else
                    if (StartHour < 0 || StartHour > 23)
                    {
                        throw new CronExpressionBuilderError("Hour should be between 0 to 24");
                    }
                    else if (StartMinute < 0 || StartMinute > 59)
                    {
                        throw new CronExpressionBuilderError("Minute should be between 0 to 59");
                    }
                    else
                    {

                        string days = "";
                        foreach (Weekdays w in weekdays)
                        {
                            days += w + ",";
                        }
                        days = days.Remove(days.Length - 1);
                        return "0 " + StartMinute + " " + StartHour + " ? * " + days + " *";
                    }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        ///  Run a task on Day x of every x month(s)
        /// </summary>
        /// <param name="Day">Min-1 Max-31</param>
        /// <param name="MonthInterval">Interval in Months</param>
        /// <param name="StartHour">Min-0 Max-23</param>
        /// <param name="StartMinute">Min-0 Max-59</param>
        /// <returns>(String) Returns a cron Expression that runs on specified day in a month every x months</returns>
        ///<example>SpecificDayOfAMonth(4,2,23,52); output : "0 52 23 4 1/2 ? *"</example>
        public string SpecificDayOfAMonth(int Day, int MonthInterval, int StartHour, int StartMinute)
        {
            try
            {
                if (MonthInterval <= 0)
                {
                    throw new CronExpressionBuilderError("Minimum Interval should be 1");
                }
                else if (Day < 1 || Day > 31)
                {
                    throw new CronExpressionBuilderError("Minimum Day should be 1 or Maximum Day can be 31");
                }

                else if (StartHour < 0 || StartHour > 23)
                {
                    throw new CronExpressionBuilderError("Hour should be between 0 to 24");
                }
                else if (StartMinute < 0 || StartMinute > 59)
                {
                    throw new CronExpressionBuilderError("Minute should be between 0 to 59");
                }
                else
                {
                    //0 10 14 2 1/5 ? *
                    return "0 " + StartMinute + " " + StartHour + " " + Day + @" 1/" + MonthInterval + " ? *";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Run a task at  (Rank) (Day) of every x month(s) \n Example :- The Fourth Saturday of every 5 months
        /// </summary>
        /// <param name="rank">Rank of the Week</param>
        /// <param name="day">Day of the Week</param>
        /// <param name="MonthInterval">Interval in Months</param>
        /// <param name="StartHour">Min-0 Max-23</param>
        /// <param name="StartMinute">Min-0 Max-59</param>
        /// <returns>(String) Returns a cron Expression that runs on specified day on the specified week rank on specified time every x month(s)</returns>
        /// <example>SpecificDayofAMonth(CronExpressionBuilder.Rank.Fourth,CronExpressionBuilder.Weekdays.SAT,1,5,20) output : "0 20 5 ? 1/1 SAT#4 *"</example>
        public string SpecificDayOfAMonth(Rank rank, Weekdays day, int MonthInterval, int StartHour, int StartMinute)
        {
            try
            {
                if (MonthInterval <= 0)
                {
                    throw new CronExpressionBuilderError("Minimum Interval should be 1");
                }
                else if (StartHour < 0 || StartHour > 23)
                {
                    throw new CronExpressionBuilderError("Hour should be between 0 to 24");
                }
                else if (StartMinute < 0 || StartMinute > 59)
                {
                    throw new CronExpressionBuilderError("Minute should be between 0 to 59");
                }
                else
                {
                    //0 10 14 ? 1/2 WED#3 *
                    return "0 " + StartMinute + " " + StartHour + @" ? 1/" + MonthInterval + " " + day + @"#" + (int)rank + " *";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        ///  Run a task Every (Day) of a (Month) every year
        /// </summary>
        /// <param name="Day">Min-1 Max-31</param>
        /// <param name="month">Month of the Year</param>
        /// <param name="StartHour">Min-0 Max-23</param>
        /// <param name="StartMinute">Min-0 Max-59</param>
        /// <returns>(String) Returns a cron Expression that runs on specified day on the specified month  every year on specified time</returns>
        /// <example>YearlyOnSpecificDateofAMonth(20, CronExpressionBuilder.Months.June, 0, 0); output : "0 0 0 20 6 ? *"</example>
        public string YearlyOnSpecificDayOfAMonth(int Day, Months month, int StartHour, int StartMinute)
        {
            try
            {
                if (Day < 1 || Day > 31)
                {
                    throw new CronExpressionBuilderError("Minimum Day should be 1 or Maximum Day can be 31");
                }

                else if (StartHour < 0 || StartHour > 23)
                {
                    throw new CronExpressionBuilderError("Hour should be between 0 to 24");
                }
                else if (StartMinute < 0 || StartMinute > 59)
                {
                    throw new CronExpressionBuilderError("Minute should be between 0 to 59");
                }
                else
                {
                    //0 10 2 5 8 ? *
                    return "0 " + StartMinute + " " + StartHour + " " + Day + " " + (int)month + " ? *";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Run A task at (Rank) (Day) of a (Month) every year Example :- The Fourth Saturday of September every Year.
        /// </summary>
        /// <param name="rank">Rank of the Week</param> 
        /// <param name="day">Min-1 Max-31</param>
        /// <param name="month">Month of the Year</param>
        /// <param name="StartHour">Min-0 Max-23</param>
        /// <param name="StartMinute">Min-0 Max-59</param>
        /// <returns>(String) Returns a cron Expression that runs on specified day on the specified week rank of the specified month  every year on specified time</returns>
        /// <example>YearlyOnSpecificDayOfAMonth(CronExpressionBuilder.Rank.Second,CronExpressionBuilder.Weekdays.TUE,CronExpressionBuilder.Months.May,7,0); output : "0 0 7 ? 5 TUE#2 *"</example>
        public string YearlyOnSpecificDayOfAMonth(Rank rank, Weekdays day, Months month, int StartHour, int StartMinute)
        {
            try
            {
                if (StartHour < 0 || StartHour > 23)
                {
                    throw new CronExpressionBuilderError("Hour should be between 0 to 24");
                }
                else if (StartMinute < 0 || StartMinute > 59)
                {
                    throw new CronExpressionBuilderError("Minute should be between 0 to 59");
                }
                else
                {
                    //0 10 2 ? 9 THU#4 *
                    return "0 " + StartMinute + " " + StartHour + " ? " + (int)month + " " + day + @"#" + (int)rank + " *";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

    class CronExpressionBuilderError : ApplicationException
    {
        public CronExpressionBuilderError(string message)
            : base(message)
        {

        }
    }
}
