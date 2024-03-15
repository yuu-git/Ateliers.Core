using Ateliers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ateliers
{
    /// <summary>
    /// 日付情報
    /// </summary>
    public class DayInfo
    {
        /*--- * structers -------------------------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// コンストラクタ
        /// </summary>
        private DayInfo() { }

        /*--- Property/Field Definitions ----------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// 日付の数値を取得します。
        /// </summary>
        public int Value { get; private set; }

        /// <summary>
        /// 日付の列挙を取得します。
        /// </summary>
        public DayFlags Flag { get; private set; }

        /// <summary>
        /// 日付の列挙をビット値に変換して取得します。
        /// </summary>
        public int FlagValue => (int)this.Flag;

        /// <summary>
        /// 日付を英語簡略名称で取得します。
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 日付を英語正式名称で取得します。
        /// </summary>
        public string FullName { get; private set; }

        /// <summary>
        /// 日付を日本語名称で取得します。
        /// </summary>
        public string JpnName { get; private set; }


        /*--- Method: public ----------------------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// システム日付の当日情報を取得します。
        /// </summary>
        /// <returns> システム日付の当日情報 </returns>
        public static DayInfo GetToday() => GetDay(DateTime.Now.Day);

        /// <summary>
        /// 引数指定の日付情報を取得します。
        /// </summary>
        /// <param name="day"> 1~31の日付 </param>
        /// <returns> 指定した日付情報 </returns>
        /// <exception cref="ArgumentOutOfRangeException"> 1~31 以外の数値は指定できません。 </exception>
        public static DayInfo GetDay(int day)
        {
            if (day > 31 || day < 1)
                throw new ArgumentOutOfRangeException(nameof(day));

            return Days[day];
        }

        /// <summary>
        /// 引数指定の日付情報を取得します。
        /// </summary>
        /// <param name="days">  </param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static IEnumerable<DayInfo> GetDays(IEnumerable<int> days)
        {
            if (days is null) 
                throw new ArgumentNullException(nameof(days));

            if (!days.Any())
                return Enumerable.Empty<DayInfo>();
            
            if (days.Where(day => day > 0 && day < 32).Any())
                throw new ArgumentOutOfRangeException(nameof(days));

            return Days.Where(x => days.Contains(x.Key)).Select(x => x.Value).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="days"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static IEnumerable<DayInfo> GetDays(params int[] days)
        {
            if (days is null)
                throw new ArgumentNullException(nameof(days));

            if (!days.Any())
                return Enumerable.Empty<DayInfo>();

            if (days.Where(day => day > 0 && day < 32).Any())
                throw new ArgumentOutOfRangeException(nameof(days));

            return Days.Where(x => days.Contains(x.Key)).Select(x => x.Value).ToList();
        }

        /// <summary>
        /// ハッシュコードを取得します。
        /// </summary>
        /// <returns> ハッシュコード数値 </returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is DayInfo dayInfo)
            {
                return this.Value == dayInfo.Value;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{this}";
        }

        /*--- Method: internal --------------------------------------------------------------------------------------------------------------------*/
        /*--- Method: protected -------------------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// 日付ディクショナリ
        /// - Key:日付数値  Value:日付情報
        /// </summary>
        protected static Dictionary<int, DayInfo> Days = new Dictionary<int, DayInfo>()
        {
            {  1, new DayInfo { Value =  1, Name =  "1st", FullName = "First",          Flag = DayFlags.First,          JpnName =  "1日", } },
            {  2, new DayInfo { Value =  2, Name =  "2nd", FullName = "Second",         Flag = DayFlags.Second,         JpnName =  "2日", } },
            {  3, new DayInfo { Value =  3, Name =  "3rd", FullName = "Third",          Flag = DayFlags.Third,          JpnName =  "3日", } },
            {  4, new DayInfo { Value =  4, Name =  "4th", FullName = "Fourth",         Flag = DayFlags.Fourth,         JpnName =  "4日", } },
            {  5, new DayInfo { Value =  5, Name =  "5th", FullName = "Fifth",          Flag = DayFlags.Fifth,          JpnName =  "5日", } },
            {  6, new DayInfo { Value =  6, Name =  "6th", FullName = "Sixth",          Flag = DayFlags.Sixth,          JpnName =  "6日", } },
            {  7, new DayInfo { Value =  7, Name =  "7th", FullName = "Seventh",        Flag = DayFlags.Seventh,        JpnName =  "7日", } },
            {  8, new DayInfo { Value =  8, Name =  "8th", FullName = "Eighth",         Flag = DayFlags.Eighth,         JpnName =  "8日", } },
            {  9, new DayInfo { Value =  9, Name =  "9th", FullName = "Ninth",          Flag = DayFlags.Ninth,          JpnName =  "9日", } },
            { 10, new DayInfo { Value = 10, Name = "10th", FullName = "Tenth",          Flag = DayFlags.Tenth,          JpnName = "10日", } },
            { 11, new DayInfo { Value = 11, Name = "11th", FullName = "Eleventh",       Flag = DayFlags.Eleventh,       JpnName = "11日", } },
            { 12, new DayInfo { Value = 12, Name = "12th", FullName = "Twelfth",        Flag = DayFlags.Twelfth,        JpnName = "12日", } },
            { 13, new DayInfo { Value = 13, Name = "13th", FullName = "Thirteenth",     Flag = DayFlags.Thirteenth,     JpnName = "13日", } },
            { 14, new DayInfo { Value = 14, Name = "14th", FullName = "Fourteenth",     Flag = DayFlags.Fourteenth,     JpnName = "14日", } },
            { 15, new DayInfo { Value = 15, Name = "15th", FullName = "Fifteenth",      Flag = DayFlags.Fifteenth,      JpnName = "15日", } },
            { 16, new DayInfo { Value = 16, Name = "16th", FullName = "Sixteenth",      Flag = DayFlags.Sixteenth,      JpnName = "16日", } },
            { 17, new DayInfo { Value = 17, Name = "17th", FullName = "Seventeenth",    Flag = DayFlags.Seventeenth,    JpnName = "17日", } },
            { 18, new DayInfo { Value = 18, Name = "18th", FullName = "Eighteenth",     Flag = DayFlags.Eighteenth,     JpnName = "18日", } },
            { 19, new DayInfo { Value = 19, Name = "19th", FullName = "Nineteenth",     Flag = DayFlags.Nineteenth,     JpnName = "19日", } },
            { 20, new DayInfo { Value = 20, Name = "20th", FullName = "Twentieth",      Flag = DayFlags.Twentieth,      JpnName = "20日", } },
            { 21, new DayInfo { Value = 21, Name = "21st", FullName = "Twenty First",   Flag = DayFlags.TwentyFirst,    JpnName = "21日", } },
            { 22, new DayInfo { Value = 22, Name = "22nd", FullName = "Twenty Second",  Flag = DayFlags.TwentySecond,   JpnName = "22日", } },
            { 23, new DayInfo { Value = 23, Name = "23rd", FullName = "Twenty Third",   Flag = DayFlags.TwentyThird,    JpnName = "23日", } },
            { 24, new DayInfo { Value = 24, Name = "24th", FullName = "Twenty Fourth",  Flag = DayFlags.TwentyFourth,   JpnName = "24日", } },
            { 25, new DayInfo { Value = 25, Name = "25th", FullName = "Twenty Fifth",   Flag = DayFlags.TwentyFifth,    JpnName = "25日", } },
            { 26, new DayInfo { Value = 26, Name = "26th", FullName = "Twenty Sixth",   Flag = DayFlags.TwentySixth,    JpnName = "26日", } },
            { 27, new DayInfo { Value = 27, Name = "27th", FullName = "Twenty Seventh", Flag = DayFlags.TwentySeventh,  JpnName = "27日", } },
            { 28, new DayInfo { Value = 28, Name = "28th", FullName = "Twenty Eighth",  Flag = DayFlags.TwentyEighth,   JpnName = "28日", } },
            { 29, new DayInfo { Value = 29, Name = "29th", FullName = "Twenty Ninth",   Flag = DayFlags.TwentyNinth,    JpnName = "29日", } },
            { 30, new DayInfo { Value = 30, Name = "30th", FullName = "Thirtieth",      Flag = DayFlags.Thirtieth,      JpnName = "30日", } },
            { 31, new DayInfo { Value = 31, Name = "31st", FullName = "Thirty First",   Flag = DayFlags.ThirtyFirst,    JpnName = "31日", } },
        };

        /*--- Method: private ---------------------------------------------------------------------------------------------------------------------*/

    }
}
