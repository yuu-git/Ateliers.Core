using System;

namespace Ateliers
{
    /// <summary>
    /// 日時型(<see cref="DateTime"/>) 拡張メソッド
    /// </summary>
    public static class DateTimeExtensions
    {
        /*--- Property/Field Definitions ----------------------------------------------------------------------------------------------------------*/
        /*--- Method: public ----------------------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// 翌日を取得します。
        /// </summary>
        /// <param name="self"> 判定する基準となる日時（自身） </param>
        /// <returns> 翌日の日付を返します。時刻は00:00に設定します。 日付が <see cref="DateTime.MaxValue"/> の場合、そのまま返します。 </returns>
        public static DateTime GetNextDay(this DateTime self) =>
            self.Date == DateTime.MaxValue.Date ? self : self.AddDays(1).Date;

        /// <summary>
        /// 次週の月曜日を取得します。引数の日付が既に月曜日の場合、翌週の月曜日を取得します。
        /// </summary>
        /// <param name="self"> 判定する基準となる日時（自身） </param>
        /// <returns> 次週の月曜日の日付を返します。時刻は00:00に設定します。日付が <see cref="DateTime.MaxValue"/> の場合、そのまま返します。 </returns>
        public static DateTime GetNextMonday(this DateTime self) =>
            self == DateTime.MaxValue ? self : GetNextWeekOfDate(self, DayOfWeek.Monday);

        /// <summary>
        /// 次週の火曜日を取得します。引数の日付が既に火曜日の場合、翌週の火曜日を取得します。
        /// </summary>
        /// <param name="self"> 判定する基準となる日時（自身） </param>
        /// <returns> 次週の火曜日の日付を返します。時刻は00:00に設定します。日付が <see cref="DateTime.MaxValue"/> の場合、そのまま返します。 </returns>
        public static DateTime GetNextTuesday(this DateTime self) =>
            self == DateTime.MaxValue ? self : GetNextWeekOfDate(self, DayOfWeek.Tuesday);

        /// <summary>
        /// 次週の水曜日を取得します。引数の日付が既に水曜日の場合、翌週の水曜日を取得します。
        /// </summary>
        /// <param name="self"> 判定する基準となる日時（自身） </param>
        /// <returns> 次週の水曜日の日付を返します。時刻は00:00に設定します。日付が <see cref="DateTime.MaxValue"/> の場合、そのまま返します。 </returns>
        public static DateTime GetNextWednesday(this DateTime self) =>
            self == DateTime.MaxValue ? self : GetNextWeekOfDate(self, DayOfWeek.Wednesday);

        /// <summary>
        /// 次週の木曜日を取得します。引数の日付が既に木曜日の場合、翌週の木曜日を取得します。
        /// </summary>
        /// <param name="self"> 判定する基準となる日時（自身） </param>
        /// <returns> 次週の木曜日の日付を返します。時刻は00:00に設定します。日付が <see cref="DateTime.MaxValue"/> の場合、そのまま返します。 </returns>
        public static DateTime GetNextThursday(this DateTime self) =>
            self == DateTime.MaxValue ? self : GetNextWeekOfDate(self, DayOfWeek.Thursday);

        /// <summary>
        /// 次週の金曜日を取得します。引数の日付が既に金曜日の場合、翌週の金曜日を取得します。
        /// </summary>
        /// <param name="self"> 判定する基準となる日時（自身） </param>
        /// <returns> 次週の金曜日の日付を返します。時刻は00:00に設定します。日付が <see cref="DateTime.MaxValue"/> の場合、そのまま返します。 </returns>
        public static DateTime GetNextFriday(this DateTime self) =>
            self == DateTime.MaxValue ? self : GetNextWeekOfDate(self, DayOfWeek.Friday);

        /// <summary>
        /// 次週の土曜日を取得します。引数の日付が既に土曜日の場合、翌週の土曜日を取得します。
        /// </summary>
        /// <param name="self"> 判定する基準となる日時（自身） </param>
        /// <returns> 次週の土曜日の日付を返します。時刻は00:00に設定します。日付が <see cref="DateTime.MaxValue"/> の場合、そのまま返します。 </returns>
        public static DateTime GetNextSaturday(this DateTime self) =>
            self == DateTime.MaxValue ? self : GetNextWeekOfDate(self, DayOfWeek.Saturday);

        /// <summary>
        /// 次週の日曜日を取得します。引数の日付が既に日曜日の場合、翌週の日曜日を取得します。
        /// </summary>
        /// <param name="self"> 判定する基準となる日時（自身） </param>
        /// <returns> 次週の日曜日の日付を返します。時刻は00:00に設定します。日付が <see cref="DateTime.MaxValue"/> の場合、そのまま返します。 </returns>
        public static DateTime GetNextSunday(this DateTime self) =>
            self == DateTime.MaxValue ? self : GetNextWeekOfDate(self, DayOfWeek.Sunday);

        /// <summary>
        /// 前日を取得します。
        /// </summary>
        /// <param name="self"> 判定する基準となる日時（自身） </param>
        /// <returns> 前日の日付を返します。 日付が <see cref="DateTime.MinValue"/> の場合、そのまま返します。時刻は00:00に設定します。 </returns>
        public static DateTime GetPreviousDay(this DateTime self) =>
            self.Date == DateTime.MinValue.Date ? self : self.AddDays(-1).Date;

        /// <summary>
        /// 前週の月曜日を取得します。引数の日付が既に月曜日の場合、翌週の月曜日を取得します。
        /// </summary>
        /// <param name="self"> 判定する基準となる日時（自身） </param>
        /// <returns> 前週の月曜日の日付を返します。時刻は00:00に設定します。日付が <see cref="DateTime.MinValue"/> の場合、そのまま返します。 </returns>
        public static DateTime GetPreviousMonday(this DateTime self) =>
            self == DateTime.MinValue ? self : GetPreviousWeekOfDate(self, DayOfWeek.Monday);

        /// <summary>
        /// 前週の火曜日を取得します。引数の日付が既に火曜日の場合、翌週の火曜日を取得します。
        /// </summary>
        /// <param name="self"> 判定する基準となる日時（自身） </param>
        /// <returns> 前週の火曜日の日付を返します。時刻は00:00に設定します。日付が <see cref="DateTime.MinValue"/> の場合、そのまま返します。 </returns>
        public static DateTime GetPreviousTuesday(this DateTime self) =>
            self == DateTime.MinValue ? self : GetPreviousWeekOfDate(self, DayOfWeek.Tuesday);

        /// <summary>
        /// 前週の水曜日を取得します。引数の日付が既に水曜日の場合、翌週の水曜日を取得します。
        /// </summary>
        /// <param name="self"> 判定する基準となる日時（自身） </param>
        /// <returns> 前週の水曜日の日付を返します。時刻は00:00に設定します。日付が <see cref="DateTime.MinValue"/> の場合、そのまま返します。 </returns>
        public static DateTime GetPreviousWednesday(this DateTime self) =>
            self == DateTime.MinValue ? self : GetPreviousWeekOfDate(self, DayOfWeek.Wednesday);

        /// <summary>
        /// 前週の木曜日を取得します。引数の日付が既に木曜日の場合、翌週の木曜日を取得します。
        /// </summary>
        /// <param name="self"> 判定する基準となる日時（自身） </param>
        /// <returns> 前週の木曜日の日付を返します。時刻は00:00に設定します。日付が <see cref="DateTime.MinValue"/> の場合、そのまま返します。 </returns>
        public static DateTime GetPreviousThursday(this DateTime self) =>
            self == DateTime.MinValue ? self : GetPreviousWeekOfDate(self, DayOfWeek.Thursday);

        /// <summary>
        /// 前週の金曜日を取得します。引数の日付が既に金曜日の場合、翌週の金曜日を取得します。
        /// </summary>
        /// <param name="self"> 判定する基準となる日時（自身） </param>
        /// <returns> 前週の金曜日の日付を返します。時刻は00:00に設定します。日付が <see cref="DateTime.MinValue"/> の場合、そのまま返します。 </returns>
        public static DateTime GetPreviousFriday(this DateTime self) =>
            self == DateTime.MinValue ? self : GetPreviousWeekOfDate(self, DayOfWeek.Friday);

        /// <summary>
        /// 前週の土曜日を取得します。引数の日付が既に土曜日の場合、翌週の土曜日を取得します。
        /// </summary>
        /// <param name="self"> 判定する基準となる日時（自身） </param>
        /// <returns> 前週の土曜日の日付を返します。時刻は00:00に設定します。日付が <see cref="DateTime.MinValue"/> の場合、そのまま返します。 </returns>
        public static DateTime GetPreviousSaturday(this DateTime self) =>
            self == DateTime.MinValue ? self : GetPreviousWeekOfDate(self, DayOfWeek.Saturday);

        /// <summary>
        /// 前週の日曜日を取得します。引数の日付が既に日曜日の場合、翌週の日曜日を取得します。
        /// </summary>
        /// <param name="self"> 判定する基準となる日時（自身） </param>
        /// <returns> 前週の日曜日の日付を返します。時刻は00:00に設定します。日付が <see cref="DateTime.MinValue"/> の場合、そのまま返します。 </returns>
        public static DateTime GetPreviousSunday(this DateTime self) =>
            self == DateTime.MinValue ? self : GetPreviousWeekOfDate(self, DayOfWeek.Sunday);

        /*--- Method: internal --------------------------------------------------------------------------------------------------------------------*/
        /*--- Method: private ---------------------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// 翌週の指定された曜日に該当する日付を取得します。
        /// </summary>
        /// <param name="baseDate"> 基準となる日付を指定します。 </param>
        /// <param name="targetDayOfWeek"> 取得する対象の曜日を指定します。 </param>
        /// <returns> 結果の日付を返します。 時刻は00:00に設定します。 </returns>
        private static DateTime GetNextWeekOfDate(DateTime baseDate, DayOfWeek targetDayOfWeek)
        {
            var day = baseDate.GetNextDay();

            while (true)
            {
                if (day.DayOfWeek == targetDayOfWeek)
                    return day;
                else
                    day = day.GetNextDay();
            }
        }

        /// <summary>
        /// 前週の指定された曜日に該当する日付を取得します。
        /// </summary>
        /// <param name="baseDate"> 基準となる日付を指定します。 </param>
        /// <param name="targetDayOfWeek"> 取得する対象の曜日を指定します。 </param>
        /// <returns> 結果の日付を返します。 時刻は00:00に設定します。 </returns>
        private static DateTime GetPreviousWeekOfDate(DateTime baseDate, DayOfWeek targetDayOfWeek)
        {
            var day = baseDate.GetPreviousDay();

            while (true)
            {
                if (day.DayOfWeek == targetDayOfWeek)
                    return day;
                else
                    day = day.GetPreviousDay();
            }
        }
    }
}
