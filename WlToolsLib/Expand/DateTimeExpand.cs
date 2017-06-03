using System;
using System.Collections.Generic;
using static WlToolsLib.KV.KVEntityNew;

namespace WlToolsLib.Expand
{
    /// <summary>
    /// 时间扩展方法类
    /// </summary>
    public static class DateTimeExpand
    {
        #region --时间扩展，时间扩展和结构--
        /// <summary>
        /// 根据日期时间返回当月首日
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static DateTime CurrMonthFirstDay(this DateTime self)
        {
            var currMonthFirstDay = new DateTime(self.Year, self.Month, 1);
            return currMonthFirstDay;
        }

        /// <summary>
        /// 根据日期时间返回当月最后一日(23点59分59秒999毫秒999微妙)
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static DateTime CurrMonthLastDay(this DateTime self)
        {
            var lastMonth = self.AddMonths(1);
            // 进到下一月首日（这里定义的是最后 1 毫秒）
            var firstDay = new DateTime(lastMonth.Year, lastMonth.Month, 1, 23, 59, 59, 999);
            // 再回退一天
            var currMonthLastDay = firstDay.AddDays(-1);
            return currMonthLastDay;
        }

        /// <summary>
        /// 根据日期时间返回前一个月最后一日(23点59分59秒999毫秒999微妙)
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static DateTime PreviousMonthLastDay(this DateTime self)
        {
            var thisMonth = self;
            // 进到下一月首日（这里定义的是最后 1 毫秒）
            var firstDay = new DateTime(thisMonth.Year, thisMonth.Month, 1, 23, 59, 59, 999);
            // 再回退一天
            var previousMonthLastDay = firstDay.AddDays(-1);
            return previousMonthLastDay;
        }

        /// <summary>
        /// 根据日期时间返回下月首日
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static DateTime NextMonthFirstDay(this DateTime self)
        {
            var currMonthFirstDay = new DateTime(self.Year, self.Month, 1);
            var nextMonthFirstDay = currMonthFirstDay.AddMonths(1);
            return nextMonthFirstDay;
        }

        /// <summary>
        /// 返回日期时间日期部分
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string ShortDataStr(this DateTime self, string dateIntervalChar = "-")
        {
            var shortDateStr = self.ToString($"yyyy{dateIntervalChar}MM{dateIntervalChar}dd");
            return shortDateStr;
        }

        /// <summary>
        /// 返回日期时间完整字符串
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string FullStr(this DateTime self, string dateIntervalChar = "-", string timeIntervalChar = ":", string msIntervalChar = ".")
        {
            var shortDateStr = self.ToString($"yyyy{dateIntervalChar}MM{dateIntervalChar}dd HH{timeIntervalChar}mm{timeIntervalChar}ss{msIntervalChar}fff");
            return shortDateStr;
        }

        /// <summary>
        /// 时间元组转换字符串类型时间元组
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static List<Tuple<string, string>> TupleDateTimeToTupleDateStr(this List<Tuple<DateTime, DateTime>> self)
        {
            List<Tuple<string, string>> monthList = new List<Tuple<string, string>>();
            foreach (var item in self)
            {
                monthList.Add(new Tuple<string, string>(item.Item1.ShortDataStr(), item.Item2.ShortDataStr()));
            }
            return monthList;
        }

        /// <summary>
        /// 计算月限制，默认3月，含当前月
        /// </summary>
        /// <param name="self"></param>
        /// <param name="startDate"></param>
        /// <param name="maxMonth"></param>
        public static void LimitMonthForEndDate(this DateTime self, DateTime startDate, int maxMonth = 3)
        {
            if (maxMonth <= 1) return;
            if (self <= DateTime.MinValue || startDate <= DateTime.MinValue) return;
            DateTime endDate = self;

            int maxBackMonth = maxMonth - 1;// 最大后移月数(包括当月（结束日期为准）)
            // 计算3个月前的首日。包括本月
            var last3month = endDate.AddMonths(-maxBackMonth).AddDays(-(endDate.AddMonths(-maxBackMonth).Day - 1));
            // 不超当年
            if (last3month.Year != endDate.Year)
            {
                last3month = new DateTime(endDate.Year, 1, 1);
            }
            // 如果超标限制到 3月以内
            if (startDate < last3month)
            {
                startDate = last3month;
            }
            // 这里计算月差，即最后分几个月提取
            var diffMonth = (endDate.Month - startDate.Month) + 1;
        }

        /// <summary>
        /// 计算日限制，默认90天，含当前日
        /// </summary>
        /// <param name="self"></param>
        /// <param name="startDate"></param>
        /// <param name="maxDay"></param>
        public static void LimitDayForEndDate(this DateTime self, DateTime startDate, int maxDay = 90)
        {
            if (maxDay <= 1) return;
            DateTime endDate = self;

            int maxBackDay = maxDay - 1;// 最大后移日数(包含当日)
            // 计算N天前的日期
            var lastNDay = endDate.AddDays(-maxBackDay);
            // 不超当年
            if (lastNDay.Year != endDate.Year)
            {
                lastNDay = new DateTime(endDate.Year, 1, 1);
            }
            // 如果超标限制到N天以内
            if (startDate < lastNDay)
            {
                startDate = lastNDay;
            }
        }

        /// <summary>
        /// 计算从某时间到某时间的时差
        /// </summary>
        /// <param name="self"></param>
        /// <param name="sinceYear"></param>
        /// <returns></returns>
        public static TimeSpan SinceXYearTime(this DateTime self, DateTime sinceYear)
        {
            if (self.IsNotNull() && sinceYear.IsNotNull())
            {
                var timeDiff = self - sinceYear;
                return timeDiff;
            }
            return new TimeSpan(0);
        }

        /// <summary>
        /// 计算从指定时间到1970年1月1日0时的时差
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static TimeSpan Since1970Milliseconds(this DateTime self)
        {
            var sinceTime = self.SinceXYearTime(new DateTime(1970, 1, 1, 0, 0, 0));
            return sinceTime;
        }


        /// <summary>
        /// 计算时差的，按级别时间数。比如两个时间差是1时15分，两个时间差是1天5小时20分
        /// </summary>
        /// <param name="self"></param>
        /// <param name="timeLevel"></param>
        /// <returns></returns>
        public static string TimeDiffStr(this TimeSpan self, TimeLevelEnum timeLevel)
        {
            if (self.IsNull()) return "0";
            var tms = self.TotalMilliseconds;
            var currTimeLevel = TimeLevelMap.E2E(timeLevel);
            var resultStr = "";
            var m_day = self.Days;
            var m_hou = self.Hours;
            var m_min = self.Minutes;
            var m_sec = self.Seconds;
            var m_mse = self.Milliseconds;
            var hEnum = TimeLevelMap.E2E(TimeLevelEnum.Hour);
            var mEnum = TimeLevelMap.E2E(TimeLevelEnum.Minute);
            var sEnum = TimeLevelMap.E2E(TimeLevelEnum.Second);
            var msEnum = TimeLevelMap.E2E(TimeLevelEnum.Millisecond);
            switch (currTimeLevel.Enum)
            {
                case TimeLevelEnum.None:
                    resultStr = $"{tms}{msEnum.Name}";
                    break;
                case TimeLevelEnum.Year:
                    var s_day1 = $"{m_day}{currTimeLevel.Name}";
                    var s_hou1 = $"{m_hou}{hEnum.Name}";
                    var s_min1 = $"{m_min}{mEnum.Name}";
                    var s_sec1 = $"{m_sec}{sEnum.Name}";
                    var s_mse1 = $"{m_mse}{msEnum.Name}";
                    resultStr = $"{s_day1}{s_hou1}{s_min1}{s_sec1}{s_mse1}";
                    break;
                case TimeLevelEnum.Month:
                    var s_day2 = $"{m_day}{currTimeLevel.Name}";
                    var s_hou2 = $"{m_hou}{hEnum.Name}";
                    var s_min2 = $"{m_min}{mEnum.Name}";
                    var s_sec2 = $"{m_sec}{sEnum.Name}";
                    var s_mse2 = $"{m_mse}{msEnum.Name}";
                    resultStr = $"{s_day2}{s_hou2}{s_min2}{s_sec2}{s_mse2}";
                    break;
                case TimeLevelEnum.Day:
                    var s_day3 = $"{m_day}{currTimeLevel.Name}";
                    var s_hou3 = $"{m_hou}{hEnum.Name}";
                    var s_min3 = $"{m_min}{mEnum.Name}";
                    var s_sec3 = $"{m_sec}{sEnum.Name}";
                    var s_mse3 = $"{m_mse}{msEnum.Name}";
                    resultStr = $"{s_day3}{s_hou3}{s_min3}{s_sec3}{s_mse3}";
                    break;
                case TimeLevelEnum.Hour:
                    var t_hou4 = m_hou + m_day * 24;
                    var s_hou4 = $"{t_hou4}{currTimeLevel.Name}";
                    var s_min4 = $"{m_min}{mEnum.Name}";
                    var s_sec4 = $"{m_sec}{sEnum.Name}";
                    var s_mse4 = $"{m_mse}{msEnum.Name}";
                    resultStr = $"{s_hou4}{s_min4}{s_sec4}{s_mse4}";
                    break;
                case TimeLevelEnum.Minute:
                    var t_min5 = m_min + m_hou * 60 + m_day * 1440;
                    var s_min5 = $"{t_min5}{currTimeLevel.Name}";
                    var s_sec5 = $"{m_sec}{sEnum.Name}";
                    var s_mse5 = $"{m_mse}{msEnum.Name}";
                    resultStr = $"{s_min5}{s_sec5}{s_mse5}";
                    break;
                case TimeLevelEnum.Second:
                    var t_sec6 = m_sec + m_min * 60 + m_hou * 3600 + m_day * 86400;
                    var s_sec6 = $"{t_sec6}{currTimeLevel.Name}";
                    var s_mse6 = $"{m_mse}{msEnum.Name}";
                    resultStr = $"{s_sec6}{s_mse6}";
                    break;
                case TimeLevelEnum.Millisecond:
                    double t_mse7 = m_mse + m_sec * 1000 + m_min * 60000 + m_hou * 3600000 + m_day * 86400000;
                    var s_mse7 = $"{t_mse7}{currTimeLevel.Name}";
                    resultStr = $"{s_mse7}";
                    break;
                default:
                    resultStr = $"{tms}{msEnum.Name}";
                    break;
            }
            return resultStr;
        }

        /// <summary>
        /// 时间级别枚举
        /// </summary>
        public enum TimeLevelEnum { None = 0, Year = 1, Month = 2, Day = 3, Hour = 4, Minute = 5, Second = 6, Millisecond=7 };

        /// <summary>
        /// 时间级别枚举对照级别名字
        /// </summary>
        public static List<CodeNameMap<TimeLevelEnum, int, string>> TimeLevelMap= new List<CodeNameMap<TimeLevelEnum, int, string>>()
        {
            new CodeNameMap<TimeLevelEnum, int, string>() {  Enum = TimeLevelEnum.None, Code =0, Name ="N"},
            new CodeNameMap<TimeLevelEnum, int, string>() {  Enum = TimeLevelEnum.Year, Code =1, Name ="年"},
            new CodeNameMap<TimeLevelEnum, int, string>() {  Enum = TimeLevelEnum.Month, Code =2, Name ="月"},
            new CodeNameMap<TimeLevelEnum, int, string>() {  Enum = TimeLevelEnum.Day, Code =3, Name ="日"},
            new CodeNameMap<TimeLevelEnum, int, string>() {  Enum = TimeLevelEnum.Hour, Code =4, Name ="时"},
            new CodeNameMap<TimeLevelEnum, int, string>() {  Enum = TimeLevelEnum.Minute, Code =5, Name ="分"},
            new CodeNameMap<TimeLevelEnum, int, string>() {  Enum = TimeLevelEnum.Second, Code =6, Name ="秒"},
            new CodeNameMap<TimeLevelEnum, int, string>() {  Enum = TimeLevelEnum.Millisecond, Code =7, Name ="毫秒"}
        };
        #endregion --时间扩展，时间扩展和结构--
    }
}
