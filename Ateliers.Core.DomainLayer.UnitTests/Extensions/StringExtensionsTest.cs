using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Ateliers.UnitTest.Extensions
{
    public class StringExtensionsTest
    {
        public const string TESTNAME_001_00100 = nameof(StringExtensions) + "." + nameof(StringExtensions.Sanitize) + "_" + "NullがEmptyに置き換えられる";
        public const string TESTNAME_001_00200 = nameof(StringExtensions) + "." + nameof(StringExtensions.Sanitize) + "_" + "Null以外の場合はそのままの文字列が返る";
        public const string TESTNAME_001_00300 = nameof(StringExtensions) + "." + nameof(StringExtensions.Sanitize) + "_" + "Emptyでも正常終了する";

        public const string TESTNAME_002_00100 = nameof(StringExtensions) + "." + nameof(StringExtensions.IsNullOrWhiteSpace) + "_" + "Nullと空白はtrueが返る";
        public const string TESTNAME_002_00200 = nameof(StringExtensions) + "." + nameof(StringExtensions.IsNullOrWhiteSpace) + "_" + "Nullと空白以外はfalseが返る";

        public const string TESTNAME_003_00100 = nameof(StringExtensions) + "." + nameof(StringExtensions.SubstringLeft) + "_" + "正しく文字列を切り取って抽出できる";
        public const string TESTNAME_003_00200 = nameof(StringExtensions) + "." + nameof(StringExtensions.SubstringLeft) + "_" + "対象文字列が存在しない場合はそのままの文字列が返る";
        public const string TESTNAME_003_00300 = nameof(StringExtensions) + "." + nameof(StringExtensions.SubstringLeft) + "_" + "検索キーの文字列がNullの場合は例外エラー";
        public const string TESTNAME_003_00400 = nameof(StringExtensions) + "." + nameof(StringExtensions.SubstringLeft) + "_" + "検索キーの文字列がEmptyの場合は例外エラー";

        [Fact(DisplayName = TESTNAME_001_00100)]
        public void TEST_001_00100()
        {
            var assumedResult = string.Empty;
            var testResult = default(string).Sanitize();

            testResult.Is(assumedResult);
        }

        [Fact(DisplayName = TESTNAME_001_00200)]
        public void TEST_001_00200()
        {
            var assumedResult = "TestString";
            var testResult = "TestString".Sanitize();

            testResult.Is(assumedResult);
        }

        [Fact(DisplayName = TESTNAME_001_00300)]
        public void TEST_001_00300()
        {
            var assumedResult = string.Empty;
            var testResult = string.Empty.Sanitize();

            testResult.Is(assumedResult);
        }

        [Fact(DisplayName = TESTNAME_002_00100)]
        public void TEST_002_00100()
        {
            foreach (var testStr in new[] { default, string.Empty, " " })
            {
                testStr.IsNullOrWhiteSpace().IsTrue();
            }
        }

        [Fact(DisplayName = TESTNAME_002_00200)]
        public void TEST_002_00200()
        {
            foreach (var testStr in new[] { "AAA", "あああ", "111", "１１１" })
            {
                testStr.IsNullOrWhiteSpace().IsFalse();
            }
        }

        [Fact(DisplayName = TESTNAME_003_00100)]
        public void TEST_003_00100()
        {
            var testDatas = new List<SubstringLeftTestModel>
            {
                new SubstringLeftTestModel { TestString = "TestString", KeyString = "Test", AssumedString = "String" },
                new SubstringLeftTestModel { TestString = "AABBCC", KeyString = "AB", AssumedString = "BCC" },
                new SubstringLeftTestModel { TestString = "AABBCC", KeyString = "BB", AssumedString = "CC" },
                new SubstringLeftTestModel { TestString = "AABBCC", KeyString = "CC", AssumedString = string.Empty },
                new SubstringLeftTestModel { TestString = "AA BB CC", KeyString = " ", AssumedString = "BB CC" },
            };

            foreach (var testData in testDatas)
            {
                var testResult = testData.TestString.SubstringLeft(testData.KeyString);
                testResult.Is(testData.AssumedString);
            }
        }

        [Fact(DisplayName = TESTNAME_003_00200)]
        public void TEST_003_00200()
        {
            var testDatas = new List<SubstringLeftTestModel>
            {
                new SubstringLeftTestModel { TestString = "AABBCC", KeyString = "Z", AssumedString = "AABBCC" },
                new SubstringLeftTestModel { TestString = "AABBCC", KeyString = "AAA", AssumedString = "AABBCC" },
                new SubstringLeftTestModel { TestString = "AABBCC", KeyString = "BBB", AssumedString = "AABBCC" },
                new SubstringLeftTestModel { TestString = "AABBCC", KeyString = "CCC", AssumedString = "AABBCC" },
                new SubstringLeftTestModel { TestString = "AABBCC", KeyString = " ", AssumedString = "AABBCC" },
            };

            foreach (var testData in testDatas)
            {
                var testResult = testData.TestString.SubstringLeft(testData.KeyString);
                testResult.Is(testData.AssumedString);
            }
        }

        [Fact(DisplayName = TESTNAME_003_00300)]
        public void TEST_003_00300()
        {
            Assert.ThrowsAny<ArgumentNullException>(() => "A".SubstringLeft(default(string))).Message.Contains(StringExtensions.ExceptionMessage100100);
        }

        [Fact(DisplayName = TESTNAME_003_00400)]
        public void TEST_003_00400()
        {
            Assert.ThrowsAny<ArgumentException>(() => "A".SubstringLeft(string.Empty)).Message.Contains(StringExtensions.ExceptionMessage100101);
        }

        /// <summary>
        /// <see cref="StringExtensions.SubstringLeft(string, string)"/> テスト用データモデル
        /// </summary>
        private class SubstringLeftTestModel
        {
            /// <summary>
            /// 想定する結果の文字列 
            /// </summary>
            public string AssumedString { get; set; }

            /// <summary>
            /// テストする文字列
            /// </summary>
            public string TestString { get; set; }
            
            /// <summary>
            /// 検索キーとなる文字列
            /// </summary>
            public string KeyString { get; set; }
        }
    }
}
