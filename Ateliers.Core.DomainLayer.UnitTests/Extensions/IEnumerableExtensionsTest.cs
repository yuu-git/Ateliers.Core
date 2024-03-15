using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Ateliers.UnitTest.Extensions
{
    public class IEnumerableExtensionsTest
    {
        public const string TESTNAME_001_00100 = nameof(IEnumerableExtensions) + "." + nameof(IEnumerableExtensions.IsDefaultOrEmpty) + "_" + "インスタンスがNullの時はtrue";
        public const string TESTNAME_001_00200 = nameof(IEnumerableExtensions) + "." + nameof(IEnumerableExtensions.IsDefaultOrEmpty) + "_" + "インスタンスのコレクションが0件の時はtrue";
        public const string TESTNAME_001_00300 = nameof(IEnumerableExtensions) + "." + nameof(IEnumerableExtensions.IsDefaultOrEmpty) + "_" + "インスタンスのコレクションにデータがある時はfalse";

        public const string TESTNAME_002_00100 = nameof(IEnumerableExtensions) + "." + nameof(IEnumerableExtensions.IsEmpty) + "_" + "インスタンスのコレクションが0件の時はtrue";
        public const string TESTNAME_002_00200 = nameof(IEnumerableExtensions) + "." + nameof(IEnumerableExtensions.IsEmpty) + "_" + "インスタンスのコレクションにデータがある時はfalse";
        public const string TESTNAME_002_00300 = nameof(IEnumerableExtensions) + "." + nameof(IEnumerableExtensions.IsEmpty) + "_" + "インスタンスがNullの時は例外エラー";

        public const string TESTNAME_003_00100 = nameof(IEnumerableExtensions) + "." + nameof(IEnumerableExtensions.Sanitize) + "_" + "インスタンスがNullの時はインスタンスが生成される";
        public const string TESTNAME_003_00200 = nameof(IEnumerableExtensions) + "." + nameof(IEnumerableExtensions.Sanitize) + "_" + "インスタンスがNull以外の時はそのまま返る";

        public const string TESTNAME_004_00100 = nameof(IEnumerableExtensions) + "." + nameof(IEnumerableExtensions.ToAllUpper) + "_" + "文字列が全て大文字に変換される";
        public const string TESTNAME_004_00200 = nameof(IEnumerableExtensions) + "." + nameof(IEnumerableExtensions.ToAllUpper) + "_" + "インスタンスのコレクションがカラでも正常終了する";
        public const string TESTNAME_004_00300 = nameof(IEnumerableExtensions) + "." + nameof(IEnumerableExtensions.ToAllUpper) + "_" + "インスタンスのコレクションがNullの時は例外エラー";

        public const string TESTNAME_005_00100 = nameof(IEnumerableExtensions) + "." + nameof(IEnumerableExtensions.ToAllLower) + "_" + "文字列が全て小文字に変換される";
        public const string TESTNAME_005_00200 = nameof(IEnumerableExtensions) + "." + nameof(IEnumerableExtensions.ToAllLower) + "_" + "インスタンスのコレクションがカラでも正常終了する";
        public const string TESTNAME_005_00300 = nameof(IEnumerableExtensions) + "." + nameof(IEnumerableExtensions.ToAllLower) + "_" + "インスタンスのコレクションがNullの時は例外エラー";

        [Fact(DisplayName = TESTNAME_001_00100)]
        public void TEST_001_00100()
        {
            default(List<string>).IsDefaultOrEmpty().IsTrue();
        }

        [Fact(DisplayName = TESTNAME_001_00200)]
        public void TEST_001_00200()
        {
            new List<string>().IsDefaultOrEmpty().IsTrue();
        }

        [Fact(DisplayName = TESTNAME_001_00300)]
        public void TEST_001_00300()
        {
            new List<string> { "TEST" }.IsDefaultOrEmpty().IsFalse();
        }

        [Fact(DisplayName = TESTNAME_002_00100)]
        public void TEST_002_00100()
        {
            new List<string>().IsEmpty().IsTrue();
        }

        [Fact(DisplayName = TESTNAME_002_00200)]
        public void TEST_002_00200()
        {
            new List<string> { "TEST" }.IsEmpty().IsFalse();
        }

        [Fact(DisplayName = TESTNAME_002_00300)]
        public void TEST_002_00300()
        {
            Assert.ThrowsAny<NullReferenceException>(() => default(List<string>).IsEmpty()).Message.Contains(IEnumerableExtensions.ExceptionMessage100100);
        }

        [Fact(DisplayName = TESTNAME_003_00100)]
        public void TEST_003_00100()
        {
            var testResult = default(List<string>).Sanitize();

            testResult.IsNotNull();
            testResult.IsEmpty().IsTrue();
        }

        [Fact(DisplayName = TESTNAME_003_00200)]
        public void TEST_003_00200()
        {
            var testResult = new List<string>().Sanitize();

            testResult.IsNotNull();
            testResult.IsEmpty().IsTrue();

            testResult = new List<string> { "A" }.Sanitize();

            testResult.IsNotNull();
            testResult.IsEmpty().IsFalse();
            testResult.Contains("A").IsTrue();
        }

        [Fact(DisplayName = TESTNAME_004_00100)]
        public void TEST_004_00100()
        {
            var testDatas = new List<Tuple<string, string>>
            {
                new Tuple<string, string>("abcdefghijklmnopqrstuvwxyz", "ABCDEFGHIJKLMNOPQRSTUVWXYZ"),
                new Tuple<string, string>("aAbBcC", "AABBCC"),
                new Tuple<string, string>("a b_c", "A B_C"),
                new Tuple<string, string>("oo00", "OO00"),
                new Tuple<string, string>("TEST", "TEST"),
            };

            var testResult = testDatas.Select(x => x.Item1).ToAllUpper().ToList();

            for (int i = 0; i < testDatas.Count(); i++)
            {
                testResult[i].Is(testDatas[i].Item2);
            }
        }

        [Fact(DisplayName = TESTNAME_004_00200)]
        public void TEST_004_00200()
        {
            new List<string>().ToAllUpper();
        }

        [Fact(DisplayName = TESTNAME_004_00300)]
        public void TEST_004_00300()
        {
            Assert.ThrowsAny<NullReferenceException>(() => default(List<string>).ToAllUpper()).Message.Contains(IEnumerableExtensions.ExceptionMessage100200).IsTrue();
        }

        [Fact(DisplayName = TESTNAME_004_00100)]
        public void TEST_005_00100()
        {
            var testDatas = new List<Tuple<string, string>>
            {
                new Tuple<string, string>("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "abcdefghijklmnopqrstuvwxyz"),
                new Tuple<string, string>("aAbBcC", "aabbcc"),
                new Tuple<string, string>("A B_C", "a b_c"),
                new Tuple<string, string>("OO00", "oo00"),
                new Tuple<string, string>("TEST", "test"),
            };

            var testResult = testDatas.Select(x => x.Item1).ToAllLower().ToList();

            for (int i = 0; i < testDatas.Count(); i++)
            {
                testResult[i].Is(testDatas[i].Item2);
            }
        }

        [Fact(DisplayName = TESTNAME_004_00200)]
        public void TEST_005_00200()
        {
            new List<string>().ToAllLower();
        }

        [Fact(DisplayName = TESTNAME_004_00300)]
        public void TEST_005_00300()
        {
            Assert.ThrowsAny<NullReferenceException>(() => default(List<string>).ToAllLower()).Message.Contains(IEnumerableExtensions.ExceptionMessage100300).IsTrue();
        }
    }
}
