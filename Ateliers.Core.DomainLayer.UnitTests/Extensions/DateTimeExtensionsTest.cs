using Xunit;

namespace Ateliers.UnitTests
{
    public class DateTimeExtensionsTest
    {
        public const string TESTNAME_001_00100 = nameof(DateTimeExtensions) + "." + nameof(DateTimeExtensions.GetNextDay) + "_" + "Nowに正しく加算する";
        public const string TESTNAME_001_00200 = nameof(DateTimeExtensions) + "." + nameof(DateTimeExtensions.GetNextDay) + "_" + "最大値には加算しない";
        public const string TESTNAME_001_00300 = nameof(DateTimeExtensions) + "." + nameof(DateTimeExtensions.GetNextDay) + "_" + "翌日が正しく取得できる";
        public const string TESTNAME_001_00400 = nameof(DateTimeExtensions) + "." + nameof(DateTimeExtensions.GetNextDay) + "_" + "翌月を正しく取得できる";
        public const string TESTNAME_001_00500 = nameof(DateTimeExtensions) + "." + nameof(DateTimeExtensions.GetNextDay) + "_" + "翌年を正しく取得できる";

        public const string TESTNAME_002_00100 = nameof(DateTimeExtensions) + "." + nameof(DateTimeExtensions.GetPreviousDay) + "_" + "Nowに正しく減算する";
        public const string TESTNAME_002_00200 = nameof(DateTimeExtensions) + "." + nameof(DateTimeExtensions.GetPreviousDay) + "_" + "最小値には減算しない";
        public const string TESTNAME_002_00300 = nameof(DateTimeExtensions) + "." + nameof(DateTimeExtensions.GetPreviousDay) + "_" + "前日が正しく取得できる";
        public const string TESTNAME_002_00400 = nameof(DateTimeExtensions) + "." + nameof(DateTimeExtensions.GetPreviousDay) + "_" + "前月を正しく取得できる";
        public const string TESTNAME_002_00500 = nameof(DateTimeExtensions) + "." + nameof(DateTimeExtensions.GetPreviousDay) + "_" + "前年を正しく取得できる";

        public const string TESTNAME_003_00100 = nameof(DateTimeExtensions) + "." + nameof(DateTimeExtensions.GetNextMonday) + "_" + "正しく次週の月曜日を取得できる";
        public const string TESTNAME_003_00200 = nameof(DateTimeExtensions) + "." + nameof(DateTimeExtensions.GetNextMonday) + "_" + "最大値の場合はそのまま返る";
        public const string TESTNAME_003_00300 = nameof(DateTimeExtensions) + "." + nameof(DateTimeExtensions.GetNextMonday) + "_" + "既に月曜日の場合は翌週の月曜日を取得する";

        public const string TESTNAME_004_00100 = nameof(DateTimeExtensions) + "." + nameof(DateTimeExtensions.GetNextTuesday) + "_" + "正しく次週の火曜日を取得できる";
        public const string TESTNAME_004_00200 = nameof(DateTimeExtensions) + "." + nameof(DateTimeExtensions.GetNextTuesday) + "_" + "最大値の場合はそのまま返る";
        public const string TESTNAME_004_00300 = nameof(DateTimeExtensions) + "." + nameof(DateTimeExtensions.GetNextTuesday) + "_" + "既に火曜日の場合は翌週の火曜日を取得する";

        public const string TESTNAME_005_00100 = nameof(DateTimeExtensions) + "." + nameof(DateTimeExtensions.GetNextWednesday) + "_" + "正しく次週の水曜日を取得できる";
        public const string TESTNAME_005_00200 = nameof(DateTimeExtensions) + "." + nameof(DateTimeExtensions.GetNextWednesday) + "_" + "最大値の場合はそのまま返る";
        public const string TESTNAME_005_00300 = nameof(DateTimeExtensions) + "." + nameof(DateTimeExtensions.GetNextWednesday) + "_" + "既に水曜日の場合は翌週の水曜日を取得する";

        public const string TESTNAME_006_00100 = nameof(DateTimeExtensions) + "." + nameof(DateTimeExtensions.GetNextThursday) + "_" + "正しく次週の木曜日を取得できる";
        public const string TESTNAME_006_00200 = nameof(DateTimeExtensions) + "." + nameof(DateTimeExtensions.GetNextThursday) + "_" + "最大値の場合はそのまま返る";
        public const string TESTNAME_006_00300 = nameof(DateTimeExtensions) + "." + nameof(DateTimeExtensions.GetNextThursday) + "_" + "既に木曜日の場合は翌週の木曜日を取得する";

        public const string TESTNAME_007_00100 = nameof(DateTimeExtensions) + "." + nameof(DateTimeExtensions.GetNextFriday) + "_" + "正しく次週の金曜日を取得できる";
        public const string TESTNAME_007_00200 = nameof(DateTimeExtensions) + "." + nameof(DateTimeExtensions.GetNextFriday) + "_" + "最大値の場合はそのまま返る";
        public const string TESTNAME_007_00300 = nameof(DateTimeExtensions) + "." + nameof(DateTimeExtensions.GetNextFriday) + "_" + "既に金曜日の場合は翌週の金曜日を取得する";

        public const string TESTNAME_008_00100 = nameof(DateTimeExtensions) + "." + nameof(DateTimeExtensions.GetNextSaturday) + "_" + "正しく次週の土曜日を取得できる";
        public const string TESTNAME_008_00200 = nameof(DateTimeExtensions) + "." + nameof(DateTimeExtensions.GetNextSaturday) + "_" + "最大値の場合はそのまま返る";
        public const string TESTNAME_008_00300 = nameof(DateTimeExtensions) + "." + nameof(DateTimeExtensions.GetNextSaturday) + "_" + "既に土曜日の場合は翌週の土曜日を取得する";

        public const string TESTNAME_009_00100 = nameof(DateTimeExtensions) + "." + nameof(DateTimeExtensions.GetNextSunday) + "_" + "正しく次週の日曜日を取得できる";
        public const string TESTNAME_009_00200 = nameof(DateTimeExtensions) + "." + nameof(DateTimeExtensions.GetNextSunday) + "_" + "最大値の場合はそのまま返る";
        public const string TESTNAME_009_00300 = nameof(DateTimeExtensions) + "." + nameof(DateTimeExtensions.GetNextSunday) + "_" + "既に日曜日の場合は翌週の日曜日を取得する";

        public const string TESTNAME_010_00100 = nameof(DateTimeExtensions) + "." + nameof(DateTimeExtensions.GetPreviousMonday) + "_" + "正しく前週の月曜日を取得できる";
        public const string TESTNAME_010_00200 = nameof(DateTimeExtensions) + "." + nameof(DateTimeExtensions.GetPreviousMonday) + "_" + "最小値の場合はそのまま返る";
        public const string TESTNAME_010_00300 = nameof(DateTimeExtensions) + "." + nameof(DateTimeExtensions.GetPreviousMonday) + "_" + "既に月曜日の場合は翌週の月曜日を取得する";

        public const string TESTNAME_011_00100 = nameof(DateTimeExtensions) + "." + nameof(DateTimeExtensions.GetPreviousTuesday) + "_" + "正しく前週の火曜日を取得できる";
        public const string TESTNAME_011_00200 = nameof(DateTimeExtensions) + "." + nameof(DateTimeExtensions.GetPreviousTuesday) + "_" + "最小値の場合はそのまま返る";
        public const string TESTNAME_011_00300 = nameof(DateTimeExtensions) + "." + nameof(DateTimeExtensions.GetPreviousTuesday) + "_" + "既に火曜日の場合は翌週の火曜日を取得する";

        public const string TESTNAME_012_00100 = nameof(DateTimeExtensions) + "." + nameof(DateTimeExtensions.GetPreviousWednesday) + "_" + "正しく前週の水曜日を取得できる";
        public const string TESTNAME_012_00200 = nameof(DateTimeExtensions) + "." + nameof(DateTimeExtensions.GetPreviousWednesday) + "_" + "最小値の場合はそのまま返る";
        public const string TESTNAME_012_00300 = nameof(DateTimeExtensions) + "." + nameof(DateTimeExtensions.GetPreviousWednesday) + "_" + "既に水曜日の場合は翌週の水曜日を取得する";

        public const string TESTNAME_013_00100 = nameof(DateTimeExtensions) + "." + nameof(DateTimeExtensions.GetPreviousThursday) + "_" + "正しく前週の木曜日を取得できる";
        public const string TESTNAME_013_00200 = nameof(DateTimeExtensions) + "." + nameof(DateTimeExtensions.GetPreviousThursday) + "_" + "最小値の場合はそのまま返る";
        public const string TESTNAME_013_00300 = nameof(DateTimeExtensions) + "." + nameof(DateTimeExtensions.GetPreviousThursday) + "_" + "既に木曜日の場合は翌週の木曜日を取得する";

        public const string TESTNAME_014_00100 = nameof(DateTimeExtensions) + "." + nameof(DateTimeExtensions.GetPreviousFriday) + "_" + "正しく前週の金曜日を取得できる";
        public const string TESTNAME_014_00200 = nameof(DateTimeExtensions) + "." + nameof(DateTimeExtensions.GetPreviousFriday) + "_" + "最小値の場合はそのまま返る";
        public const string TESTNAME_014_00300 = nameof(DateTimeExtensions) + "." + nameof(DateTimeExtensions.GetPreviousFriday) + "_" + "既に金曜日の場合は翌週の金曜日を取得する";

        public const string TESTNAME_015_00100 = nameof(DateTimeExtensions) + "." + nameof(DateTimeExtensions.GetPreviousSaturday) + "_" + "正しく前週の土曜日を取得できる";
        public const string TESTNAME_015_00200 = nameof(DateTimeExtensions) + "." + nameof(DateTimeExtensions.GetPreviousSaturday) + "_" + "最小値の場合はそのまま返る";
        public const string TESTNAME_015_00300 = nameof(DateTimeExtensions) + "." + nameof(DateTimeExtensions.GetPreviousSaturday) + "_" + "既に土曜日の場合は翌週の土曜日を取得する";

        public const string TESTNAME_016_00100 = nameof(DateTimeExtensions) + "." + nameof(DateTimeExtensions.GetPreviousSunday) + "_" + "正しく前週の日曜日を取得できる";
        public const string TESTNAME_016_00200 = nameof(DateTimeExtensions) + "." + nameof(DateTimeExtensions.GetPreviousSunday) + "_" + "最小値の場合はそのまま返る";
        public const string TESTNAME_016_00300 = nameof(DateTimeExtensions) + "." + nameof(DateTimeExtensions.GetPreviousSunday) + "_" + "既に日曜日の場合は翌週の日曜日を取得する";


        [Fact(DisplayName = TESTNAME_001_00100)]
        public void TEST_001_00100()
        {
            var assumedResult = DateTime.Now.AddDays(1);
            var testResult = DateTime.Now.GetNextDay();

            testResult.Year.Is(assumedResult.Year);
            testResult.Month.Is(assumedResult.Month);
            testResult.Day.Is(assumedResult.Day);

            testResult.Hour.Is(0);
            testResult.Minute.Is(0);
            testResult.Second.Is(0);
            testResult.Millisecond.Is(0);
        }

        [Fact(DisplayName = TESTNAME_001_00200)]
        public void TEST_001_00200()
        {
            var testResult = DateTime.MaxValue.GetNextDay();

            testResult.Year.Is(DateTime.MaxValue.Year);
            testResult.Month.Is(DateTime.MaxValue.Month);
            testResult.Day.Is(DateTime.MaxValue.Day);

            testResult.Hour.Is(DateTime.MaxValue.Hour);
            testResult.Minute.Is(DateTime.MaxValue.Minute);
            testResult.Second.Is(DateTime.MaxValue.Second);
            testResult.Millisecond.Is(DateTime.MaxValue.Millisecond);
        }

        [Fact(DisplayName = TESTNAME_001_00300)]
        public void TEST_001_00300()
        {
            var baseDate        = new DateTime(2020, 6, 23, 23, 23, 23);
            var assumedResult   = new DateTime(2020, 6, 24,  0,  0,  0);

            var testResult = baseDate.GetNextDay();

            this.ResultDateVerification(testResult, assumedResult);
        }

        [Fact(DisplayName = TESTNAME_001_00400)]
        public void TEST_001_00400()
        {
            var baseDate        = new DateTime(2020, 6, 30, 23, 23, 23);
            var assumedResult   = new DateTime(2020, 7,  1,  0,  0,  0);

            var testResult = baseDate.GetNextDay();

            this.ResultDateVerification(testResult, assumedResult);
        }

        [Fact(DisplayName = TESTNAME_001_00500)]
        public void TEST_001_00500()
        {
            var baseDate        = new DateTime(2020, 12, 31, 23, 23, 23);
            var assumedResult   = new DateTime(2021,  1,  1,  0,  0,  0);

            var testResult = baseDate.GetNextDay();

            this.ResultDateVerification(testResult, assumedResult);
        }

        [Fact(DisplayName = TESTNAME_002_00100)]
        public void TEST_002_00100()
        {
            var baseDate = DateTime.Now.AddDays(-1);
            var testResult = DateTime.Now.GetPreviousDay();

            testResult.Year.Is(baseDate.Year);
            testResult.Month.Is(baseDate.Month);
            testResult.Day.Is(baseDate.Day);

            testResult.Hour.Is(0);
            testResult.Minute.Is(0);
            testResult.Second.Is(0);
            testResult.Millisecond.Is(0);
        }

        [Fact(DisplayName = TESTNAME_002_00200)]
        public void TEST_002_00200()
        {
            var testResult = DateTime.MinValue.GetPreviousDay();

            testResult.Year.Is(DateTime.MinValue.Year);
            testResult.Month.Is(DateTime.MinValue.Month);
            testResult.Day.Is(DateTime.MinValue.Day);

            testResult.Hour.Is(DateTime.MinValue.Hour);
            testResult.Minute.Is(DateTime.MinValue.Minute);
            testResult.Second.Is(DateTime.MinValue.Second);
            testResult.Millisecond.Is(DateTime.MinValue.Millisecond);
        }

        [Fact(DisplayName = TESTNAME_002_00300)]
        public void TEST_002_00300()
        {
            var baseDate        = new DateTime(2020, 6, 23, 23, 23, 23);
            var assumedResult   = new DateTime(2020, 6, 22,  0,  0,  0);

            var testResult = baseDate.GetPreviousDay();

            this.ResultDateVerification(testResult, assumedResult);
        }

        [Fact(DisplayName = TESTNAME_002_00400)]
        public void TEST_002_00400()
        {
            var baseDate        = new DateTime(2020, 6,  1, 23, 23, 23);
            var assumedResult   = new DateTime(2020, 5, 31,  0,  0,  0);

            var testResult = baseDate.GetPreviousDay();

            this.ResultDateVerification(testResult, assumedResult);
        }

        [Fact(DisplayName = TESTNAME_002_00500)]
        public void TEST_002_00500()
        {
            var baseDate        = new DateTime(2020,  1,  1, 23, 23, 23);
            var assumedResult   = new DateTime(2019, 12, 31,  0,  0,  0);

            var testResult = baseDate.GetPreviousDay();

            this.ResultDateVerification(testResult, assumedResult);
        }

        [Fact(DisplayName = TESTNAME_003_00100)]
        public void TEST_003_00100()
        {
            var baseDate = new DateTime(2020, 6, 16, 23, 23, 23);

            var assumedResult = new DateTime(2020, 6, 22, 0, 0, 0);
            var testResult = baseDate.GetNextMonday();

            baseDate.DayOfWeek.IsNot(DayOfWeek.Monday);
            assumedResult.DayOfWeek.Is(DayOfWeek.Monday);
            testResult.DayOfWeek.Is(DayOfWeek.Monday);

            this.ResultDateVerification(testResult, assumedResult);
        }

        [Fact(DisplayName = TESTNAME_003_00200)]
        public void TEST_003_00200()
        {
            var baseDate = DateTime.MaxValue;

            var assumedResult = DateTime.MaxValue;
            var testResult = baseDate.GetNextMonday();

            this.ResultDateVerification(testResult, assumedResult);
        }

        [Fact(DisplayName = TESTNAME_003_00300)]
        public void TEST_003_00300()
        {
            var baseDate = new DateTime(2020, 6, 8, 23, 23, 23);

            var assumedResult = new DateTime(2020, 6, 15, 0, 0, 0);
            var testResult = baseDate.GetNextMonday();

            baseDate.DayOfWeek.Is(DayOfWeek.Monday);
            assumedResult.DayOfWeek.Is(DayOfWeek.Monday);
            testResult.DayOfWeek.Is(DayOfWeek.Monday);

            this.ResultDateVerification(testResult, assumedResult);
        }

        [Fact(DisplayName = TESTNAME_004_00100)]
        public void TEST_004_00100()
        {
            var baseDate        = new DateTime(2020, 6, 14, 23, 23, 23);
            var assumedResult   = new DateTime(2020, 6, 16,  0,  0,  0);

            var testResult = baseDate.GetNextTuesday();

            baseDate.DayOfWeek.IsNot(DayOfWeek.Tuesday);
            assumedResult.DayOfWeek.Is(DayOfWeek.Tuesday);
            testResult.DayOfWeek.Is(DayOfWeek.Tuesday);

            this.ResultDateVerification(testResult, assumedResult);
        }

        [Fact(DisplayName = TESTNAME_004_00200)]
        public void TEST_004_00200()
        {
            var baseDate = DateTime.MaxValue;
            var assumedResult = DateTime.MaxValue;

            var testResult = baseDate.GetNextTuesday();

            this.ResultDateVerification(testResult, assumedResult);
        }

        [Fact(DisplayName = TESTNAME_004_00300)]
        public void TEST_004_00300()
        {
            var baseDate        = new DateTime(2020, 6, 16, 23, 23, 23);
            var assumedResult   = new DateTime(2020, 6, 23,  0,  0,  0);

            var testResult = baseDate.GetNextTuesday();

            baseDate.DayOfWeek.Is(DayOfWeek.Tuesday);
            assumedResult.DayOfWeek.Is(DayOfWeek.Tuesday);
            testResult.DayOfWeek.Is(DayOfWeek.Tuesday);

            this.ResultDateVerification(testResult, assumedResult);
        }

        [Fact(DisplayName = TESTNAME_005_00100)]
        public void TEST_005_00100()
        {
            var baseDate        = new DateTime(2020, 6, 14, 23, 23, 23);
            var assumedResult   = new DateTime(2020, 6, 17,  0,  0,  0);

            var testResult = baseDate.GetNextWednesday();

            baseDate.DayOfWeek.IsNot(DayOfWeek.Wednesday);
            assumedResult.DayOfWeek.Is(DayOfWeek.Wednesday);
            testResult.DayOfWeek.Is(DayOfWeek.Wednesday);

            this.ResultDateVerification(testResult, assumedResult);
        }

        [Fact(DisplayName = TESTNAME_005_00200)]
        public void TEST_005_00200()
        {
            var baseDate = DateTime.MaxValue;
            var assumedResult = DateTime.MaxValue;

            var testResult = baseDate.GetNextWednesday();

            this.ResultDateVerification(testResult, assumedResult);
        }

        [Fact(DisplayName = TESTNAME_005_00300)]
        public void TEST_005_00300()
        {
            var baseDate        = new DateTime(2020, 6, 17, 23, 23, 23);
            var assumedResult   = new DateTime(2020, 6, 24,  0,  0,  0);

            var testResult = baseDate.GetNextWednesday();

            baseDate.DayOfWeek.Is(DayOfWeek.Wednesday);
            assumedResult.DayOfWeek.Is(DayOfWeek.Wednesday);
            testResult.DayOfWeek.Is(DayOfWeek.Wednesday);

            this.ResultDateVerification(testResult, assumedResult);
        }

        [Fact(DisplayName = TESTNAME_006_00100)]
        public void TEST_006_00100()
        {
            var baseDate        = new DateTime(2020, 6, 14, 23, 23, 23);
            var assumedResult   = new DateTime(2020, 6, 18, 0, 0, 0);

            var testResult = baseDate.GetNextThursday();

            baseDate.DayOfWeek.IsNot(DayOfWeek.Thursday);
            assumedResult.DayOfWeek.Is(DayOfWeek.Thursday);
            testResult.DayOfWeek.Is(DayOfWeek.Thursday);

            this.ResultDateVerification(testResult, assumedResult);
        }

        [Fact(DisplayName = TESTNAME_006_00200)]
        public void TEST_006_00200()
        {
            var baseDate = DateTime.MaxValue;
            var assumedResult = DateTime.MaxValue;

            var testResult = baseDate.GetNextThursday();

            this.ResultDateVerification(testResult, assumedResult);
        }

        [Fact(DisplayName = TESTNAME_006_00300)]
        public void TEST_006_00300()
        {
            var baseDate        = new DateTime(2020, 6, 18, 23, 23, 23);
            var assumedResult   = new DateTime(2020, 6, 25, 0, 0, 0);

            var testResult = baseDate.GetNextThursday();

            baseDate.DayOfWeek.Is(DayOfWeek.Thursday);
            assumedResult.DayOfWeek.Is(DayOfWeek.Thursday);
            testResult.DayOfWeek.Is(DayOfWeek.Thursday);

            this.ResultDateVerification(testResult, assumedResult);
        }

        [Fact(DisplayName = TESTNAME_007_00100)]
        public void TEST_007_00100()
        {
            var baseDate        = new DateTime(2020, 6, 14, 23, 23, 23);
            var assumedResult   = new DateTime(2020, 6, 19,  0,  0,  0);

            var testResult = baseDate.GetNextFriday();

            baseDate.DayOfWeek.IsNot(DayOfWeek.Friday);
            assumedResult.DayOfWeek.Is(DayOfWeek.Friday);
            testResult.DayOfWeek.Is(DayOfWeek.Friday);

            this.ResultDateVerification(testResult, assumedResult);
        }

        [Fact(DisplayName = TESTNAME_007_00200)]
        public void TEST_007_00200()
        {
            var baseDate = DateTime.MaxValue;
            var assumedResult = DateTime.MaxValue;

            var testResult = baseDate.GetNextFriday();

            this.ResultDateVerification(testResult, assumedResult);
        }

        [Fact(DisplayName = TESTNAME_007_00300)]
        public void TEST_007_00300()
        {
            var baseDate        = new DateTime(2020, 6, 19, 23, 23, 23);
            var assumedResult   = new DateTime(2020, 6, 26,  0,  0,  0);

            var testResult = baseDate.GetNextFriday();

            baseDate.DayOfWeek.Is(DayOfWeek.Friday);
            assumedResult.DayOfWeek.Is(DayOfWeek.Friday);
            testResult.DayOfWeek.Is(DayOfWeek.Friday);

            this.ResultDateVerification(testResult, assumedResult);
        }

        [Fact(DisplayName = TESTNAME_008_00100)]
        public void TEST_008_00100()
        {
            var baseDate        = new DateTime(2020, 6, 14, 23, 23, 23);
            var assumedResult   = new DateTime(2020, 6, 20, 0, 0, 0);

            var testResult = baseDate.GetNextSaturday();

            baseDate.DayOfWeek.IsNot(DayOfWeek.Saturday);
            assumedResult.DayOfWeek.Is(DayOfWeek.Saturday);
            testResult.DayOfWeek.Is(DayOfWeek.Saturday);

            this.ResultDateVerification(testResult, assumedResult);
        }

        [Fact(DisplayName = TESTNAME_008_00200)]
        public void TEST_008_00200()
        {
            var baseDate = DateTime.MaxValue;
            var assumedResult = DateTime.MaxValue;

            var testResult = baseDate.GetNextSaturday();

            this.ResultDateVerification(testResult, assumedResult);
        }

        [Fact(DisplayName = TESTNAME_008_00300)]
        public void TEST_008_00300()
        {
            var baseDate        = new DateTime(2020, 6, 20, 23, 23, 23);
            var assumedResult   = new DateTime(2020, 6, 27, 0, 0, 0);

            var testResult = baseDate.GetNextSaturday();

            baseDate.DayOfWeek.Is(DayOfWeek.Saturday);
            assumedResult.DayOfWeek.Is(DayOfWeek.Saturday);
            testResult.DayOfWeek.Is(DayOfWeek.Saturday);

            this.ResultDateVerification(testResult, assumedResult);
        }

        [Fact(DisplayName = TESTNAME_009_00100)]
        public void TEST_009_00100()
        {
            var baseDate        = new DateTime(2020, 6, 18, 23, 23, 23);
            var assumedResult   = new DateTime(2020, 6, 21, 0, 0, 0);

            var testResult = baseDate.GetNextSunday();

            baseDate.DayOfWeek.IsNot(DayOfWeek.Sunday);
            assumedResult.DayOfWeek.Is(DayOfWeek.Sunday);
            testResult.DayOfWeek.Is(DayOfWeek.Sunday);

            this.ResultDateVerification(testResult, assumedResult);
        }

        [Fact(DisplayName = TESTNAME_009_00200)]
        public void TEST_009_00200()
        {
            var baseDate = DateTime.MaxValue;
            var assumedResult = DateTime.MaxValue;

            var testResult = baseDate.GetNextSunday();

            this.ResultDateVerification(testResult, assumedResult);
        }

        [Fact(DisplayName = TESTNAME_009_00300)]
        public void TEST_009_00300()
        {
            var baseDate        = new DateTime(2020, 6, 21, 23, 23, 23);
            var assumedResult   = new DateTime(2020, 6, 28, 0, 0, 0);

            var testResult = baseDate.GetNextSunday();

            baseDate.DayOfWeek.Is(DayOfWeek.Sunday);
            assumedResult.DayOfWeek.Is(DayOfWeek.Sunday);
            testResult.DayOfWeek.Is(DayOfWeek.Sunday);

            this.ResultDateVerification(testResult, assumedResult);
        }

        [Fact(DisplayName = TESTNAME_010_00100)]
        public void TEST_010_00100()
        {
            var baseDate        = new DateTime(2020, 6, 16, 23, 23, 23);
            var assumedResult   = new DateTime(2020, 6, 15,  0,  0,  0);

            var testResult = baseDate.GetPreviousMonday();

            baseDate.DayOfWeek.IsNot(DayOfWeek.Monday);
            assumedResult.DayOfWeek.Is(DayOfWeek.Monday);
            testResult.DayOfWeek.Is(DayOfWeek.Monday);

            this.ResultDateVerification(testResult, assumedResult);
        }

        [Fact(DisplayName = TESTNAME_010_00200)]
        public void TEST_010_00200()
        {
            var baseDate = DateTime.MinValue;

            var assumedResult = DateTime.MinValue;
            var testResult = baseDate.GetPreviousMonday();

            this.ResultDateVerification(testResult, assumedResult);
        }

        [Fact(DisplayName = TESTNAME_010_00300)]
        public void TEST_010_00300()
        {
            var baseDate        = new DateTime(2020, 6, 15, 23, 23, 23);
            var assumedResult   = new DateTime(2020, 6,  8, 0, 0, 0);

            var testResult = baseDate.GetPreviousMonday();

            baseDate.DayOfWeek.Is(DayOfWeek.Monday);
            assumedResult.DayOfWeek.Is(DayOfWeek.Monday);
            testResult.DayOfWeek.Is(DayOfWeek.Monday);

            this.ResultDateVerification(testResult, assumedResult);
        }

        [Fact(DisplayName = TESTNAME_011_00100)]
        public void TEST_011_00100()
        {
            var baseDate        = new DateTime(2020, 6, 15, 23, 23, 23);
            var assumedResult   = new DateTime(2020, 6,  9, 0, 0, 0);

            var testResult = baseDate.GetPreviousTuesday();

            baseDate.DayOfWeek.IsNot(DayOfWeek.Tuesday);
            assumedResult.DayOfWeek.Is(DayOfWeek.Tuesday);
            testResult.DayOfWeek.Is(DayOfWeek.Tuesday);

            this.ResultDateVerification(testResult, assumedResult);
        }

        [Fact(DisplayName = TESTNAME_011_00200)]
        public void TEST_011_00200()
        {
            var baseDate = DateTime.MinValue;
            var assumedResult = DateTime.MinValue;

            var testResult = baseDate.GetPreviousTuesday();

            this.ResultDateVerification(testResult, assumedResult);
        }

        [Fact(DisplayName = TESTNAME_011_00300)]
        public void TEST_011_00300()
        {
            var baseDate        = new DateTime(2020, 6, 16, 23, 23, 23);
            var assumedResult   = new DateTime(2020, 6,  9,  0,  0,  0);

            var testResult = baseDate.GetPreviousTuesday();

            baseDate.DayOfWeek.Is(DayOfWeek.Tuesday);
            assumedResult.DayOfWeek.Is(DayOfWeek.Tuesday);
            testResult.DayOfWeek.Is(DayOfWeek.Tuesday);

            this.ResultDateVerification(testResult, assumedResult);
        }

        [Fact(DisplayName = TESTNAME_012_00100)]
        public void TEST_012_00100()
        {
            var baseDate        = new DateTime(2020, 6, 15, 23, 23, 23);
            var assumedResult   = new DateTime(2020, 6, 10,  0,  0,  0);

            var testResult = baseDate.GetPreviousWednesday();

            baseDate.DayOfWeek.IsNot(DayOfWeek.Wednesday);
            assumedResult.DayOfWeek.Is(DayOfWeek.Wednesday);
            testResult.DayOfWeek.Is(DayOfWeek.Wednesday);

            this.ResultDateVerification(testResult, assumedResult);
        }

        [Fact(DisplayName = TESTNAME_012_00200)]
        public void TEST_012_00200()
        {
            var baseDate = DateTime.MinValue;
            var assumedResult = DateTime.MinValue;

            var testResult = baseDate.GetPreviousWednesday();

            this.ResultDateVerification(testResult, assumedResult);
        }

        [Fact(DisplayName = TESTNAME_012_00300)]
        public void TEST_012_00300()
        {
            var baseDate        = new DateTime(2020, 6, 17, 23, 23, 23);
            var assumedResult   = new DateTime(2020, 6, 10,  0,  0,  0);

            var testResult = baseDate.GetPreviousWednesday();

            baseDate.DayOfWeek.Is(DayOfWeek.Wednesday);
            assumedResult.DayOfWeek.Is(DayOfWeek.Wednesday);
            testResult.DayOfWeek.Is(DayOfWeek.Wednesday);

            this.ResultDateVerification(testResult, assumedResult);
        }

        [Fact(DisplayName = TESTNAME_013_00100)]
        public void TEST_013_00100()
        {
            var baseDate        = new DateTime(2020, 6, 14, 23, 23, 23);
            var assumedResult   = new DateTime(2020, 6, 11,  0,  0,  0);

            var testResult = baseDate.GetPreviousThursday();

            baseDate.DayOfWeek.IsNot(DayOfWeek.Thursday);
            assumedResult.DayOfWeek.Is(DayOfWeek.Thursday);
            testResult.DayOfWeek.Is(DayOfWeek.Thursday);

            this.ResultDateVerification(testResult, assumedResult);
        }

        [Fact(DisplayName = TESTNAME_013_00200)]
        public void TEST_013_00200()
        {
            var baseDate = DateTime.MinValue;
            var assumedResult = DateTime.MinValue;

            var testResult = baseDate.GetPreviousThursday();

            this.ResultDateVerification(testResult, assumedResult);
        }

        [Fact(DisplayName = TESTNAME_013_00300)]
        public void TEST_013_00300()
        {
            var baseDate        = new DateTime(2020, 6, 18, 23, 23, 23);
            var assumedResult   = new DateTime(2020, 6, 11,  0,  0,  0);

            var testResult = baseDate.GetPreviousThursday();

            baseDate.DayOfWeek.Is(DayOfWeek.Thursday);
            assumedResult.DayOfWeek.Is(DayOfWeek.Thursday);
            testResult.DayOfWeek.Is(DayOfWeek.Thursday);

            this.ResultDateVerification(testResult, assumedResult);
        }

        [Fact(DisplayName = TESTNAME_014_00100)]
        public void TEST_014_00100()
        {
            var baseDate        = new DateTime(2020, 6, 14, 23, 23, 23);
            var assumedResult   = new DateTime(2020, 6, 12,  0,  0,  0);

            var testResult = baseDate.GetPreviousFriday();

            baseDate.DayOfWeek.IsNot(DayOfWeek.Friday);
            assumedResult.DayOfWeek.Is(DayOfWeek.Friday);
            testResult.DayOfWeek.Is(DayOfWeek.Friday);

            this.ResultDateVerification(testResult, assumedResult);
        }

        [Fact(DisplayName = TESTNAME_014_00200)]
        public void TEST_014_00200()
        {
            var baseDate = DateTime.MinValue;
            var assumedResult = DateTime.MinValue;

            var testResult = baseDate.GetPreviousFriday();

            this.ResultDateVerification(testResult, assumedResult);
        }

        [Fact(DisplayName = TESTNAME_014_00300)]
        public void TEST_014_00300()
        {
            var baseDate        = new DateTime(2020, 6, 19, 23, 23, 23);
            var assumedResult   = new DateTime(2020, 6, 12,  0,  0,  0);

            var testResult = baseDate.GetPreviousFriday();

            baseDate.DayOfWeek.Is(DayOfWeek.Friday);
            assumedResult.DayOfWeek.Is(DayOfWeek.Friday);
            testResult.DayOfWeek.Is(DayOfWeek.Friday);

            this.ResultDateVerification(testResult, assumedResult);
        }

        [Fact(DisplayName = TESTNAME_015_00100)]
        public void TEST_015_00100()
        {
            var baseDate        = new DateTime(2020, 6, 14, 23, 23, 23);
            var assumedResult   = new DateTime(2020, 6, 13,  0,  0,  0);

            var testResult = baseDate.GetPreviousSaturday();

            baseDate.DayOfWeek.IsNot(DayOfWeek.Saturday);
            assumedResult.DayOfWeek.Is(DayOfWeek.Saturday);
            testResult.DayOfWeek.Is(DayOfWeek.Saturday);

            this.ResultDateVerification(testResult, assumedResult);
        }

        [Fact(DisplayName = TESTNAME_015_00200)]
        public void TEST_015_00200()
        {
            var baseDate = DateTime.MinValue;
            var assumedResult = DateTime.MinValue;

            var testResult = baseDate.GetPreviousSaturday();

            this.ResultDateVerification(testResult, assumedResult);
        }

        [Fact(DisplayName = TESTNAME_015_00300)]
        public void TEST_015_00300()
        {
            var baseDate        = new DateTime(2020, 6, 20, 23, 23, 23);
            var assumedResult   = new DateTime(2020, 6, 13,  0,  0,  0);

            var testResult = baseDate.GetPreviousSaturday();

            baseDate.DayOfWeek.Is(DayOfWeek.Saturday);
            assumedResult.DayOfWeek.Is(DayOfWeek.Saturday);
            testResult.DayOfWeek.Is(DayOfWeek.Saturday);

            this.ResultDateVerification(testResult, assumedResult);
        }

        [Fact(DisplayName = TESTNAME_016_00100)]
        public void TEST_016_00100()
        {
            var baseDate        = new DateTime(2020, 6, 18, 23, 23, 23);
            var assumedResult   = new DateTime(2020, 6, 14,  0,  0,  0);

            var testResult = baseDate.GetPreviousSunday();

            baseDate.DayOfWeek.IsNot(DayOfWeek.Sunday);
            assumedResult.DayOfWeek.Is(DayOfWeek.Sunday);
            testResult.DayOfWeek.Is(DayOfWeek.Sunday);

            this.ResultDateVerification(testResult, assumedResult);
        }

        [Fact(DisplayName = TESTNAME_016_00200)]
        public void TEST_016_00200()
        {
            var baseDate = DateTime.MinValue;
            var assumedResult = DateTime.MinValue;

            var testResult = baseDate.GetPreviousSunday();

            this.ResultDateVerification(testResult, assumedResult);
        }

        [Fact(DisplayName = TESTNAME_016_00300)]
        public void TEST_016_00300()
        {
            var baseDate        = new DateTime(2020, 6, 21, 23, 23, 23);
            var assumedResult   = new DateTime(2020, 6, 14,  0,  0,  0);

            var testResult = baseDate.GetPreviousSunday();

            baseDate.DayOfWeek.Is(DayOfWeek.Sunday);
            assumedResult.DayOfWeek.Is(DayOfWeek.Sunday);
            testResult.DayOfWeek.Is(DayOfWeek.Sunday);

            this.ResultDateVerification(testResult, assumedResult);
        }

        /// <summary>
        /// 日付の結果検証を実行します
        /// </summary>
        /// <param name="testResult"> テストで得た結果の日付を指定します </param>
        /// <param name="assumedResult"> 想定する結果の日付を指定します </param>
        private void ResultDateVerification(DateTime testResult, DateTime assumedResult)
        {
            testResult.Year.Is(assumedResult.Year);
            testResult.Month.Is(assumedResult.Month);
            testResult.Day.Is(assumedResult.Day);

            testResult.Hour.Is(assumedResult.Hour);
            testResult.Minute.Is(assumedResult.Minute);
            testResult.Second.Is(assumedResult.Second);
            testResult.Millisecond.Is(assumedResult.Millisecond);
        }
    }
}
