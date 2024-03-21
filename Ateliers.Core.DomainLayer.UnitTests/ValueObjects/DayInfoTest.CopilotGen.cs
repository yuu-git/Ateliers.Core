// DayInfoTest.CopilotGen.cs - このテストは GitHub Copilot によって自動生成されました。
// 手動テストが必要な場合は DayInfoTest.cs を作成し、partialクラスでテストを追加してください。

using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Ateliers.Core.ValueObjects;
using System.CodeDom.Compiler;

namespace Ateliers.UnitTests
{
    public partial class DayInfoTest
    {
        public const string TESTNAME_001_00100 = nameof(DayInfo) + "." + nameof(DayInfo.GetToday) + "_" + "今日の日付を取得";
        public const string TESTNAME_002_00100 = nameof(DayInfo) + "." + nameof(DayInfo.GetDay) + "_" + "有効な日付を取得";
        public const string TESTNAME_002_00200 = nameof(DayInfo) + "." + nameof(DayInfo.GetDay) + "_" + "無効な日付を取得";
        public const string TESTNAME_003_00100 = nameof(DayInfo) + "." + nameof(DayInfo.GetDays) + "_" + "有効な日付リストを取得";
        public const string TESTNAME_003_00200 = nameof(DayInfo) + "." + nameof(DayInfo.GetDays) + "_" + "nullの日付リストを取得";
        public const string TESTNAME_003_00300 = nameof(DayInfo) + "." + nameof(DayInfo.GetDays) + "_" + "無効な日付リストを取得";
        public const string TESTNAME_004_00100 = nameof(DayInfo) + "." + nameof(DayInfo.GetDays) + "_" + "有効な日付リストを取得(params版)";
        public const string TESTNAME_004_00200 = nameof(DayInfo) + "." + nameof(DayInfo.GetDays) + "_" + "nullの日付リストを取得(params版)";
        public const string TESTNAME_004_00300 = nameof(DayInfo) + "." + nameof(DayInfo.GetDays) + "_" + "無効な日付リストを取得(params版)";

        [Fact(DisplayName = TESTNAME_001_00100)]
        public void TEST_001_00100()
        {
            // Act
            var result = DayInfo.GetToday();
            
            // Assert
            Assert.Equal(DateTime.Now.Day, result.Value);
        }

        [Theory(DisplayName = TESTNAME_002_00100)]
        [InlineData(1)]
        [InlineData(15)]
        [InlineData(31)]
        public void TEST_002_00100(int day)
        {
            // Act
            var result = DayInfo.GetDay(day);

            // Assert
            Assert.Equal(day, result.Value);
        }

        [Theory(DisplayName = TESTNAME_002_00200)]
        [InlineData(0)]
        [InlineData(32)]
        public void TEST_002_00200(int day)
        {
            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => DayInfo.GetDay(day));
        }

        [Fact(DisplayName = TESTNAME_003_00100)]
        public void TEST_003_00100()
        {
            // Arrange
            var days = new List<int> { 1, 15, 31 };

            // Act
            var result = DayInfo.GetDays(days);

            // Assert
            Assert.Equal(days, result.Select(d => d.Value));
        }

        [Fact(DisplayName = TESTNAME_003_00200)]
        public void TEST_003_00200()
        {
            // Arrange
            IEnumerable<int> days = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => DayInfo.GetDays(days));
        }

        [Fact(DisplayName = TESTNAME_003_00300)]
        public void TEST_003_00300()
        {
            // Arrange
            var days = new List<int> { 0, 32 };

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => DayInfo.GetDays(days));
        }

        [Fact(DisplayName = TESTNAME_004_00100)]
        public void TEST_004_00100()
        {
            // Act
            var result = DayInfo.GetDays(1, 15, 31);

            // Assert
            Assert.Equal(new List<int> { 1, 15, 31 }, result.Select(d => d.Value));
        }

        [Fact(DisplayName = TESTNAME_004_00200)]
        public void TEST_004_00200()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => DayInfo.GetDays(null));
        }

        [Fact(DisplayName = TESTNAME_004_00300)]
        public void TEST_004_00300()
        {
            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => DayInfo.GetDays(0, 32));
        }
    }
}
