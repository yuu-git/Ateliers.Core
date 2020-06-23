using System;
using Xunit;

namespace Ateliers.UnitTest.Exceptions
{
    public class DecryptSecurityExceptionTest
    {
        public const string TESTNAME_001_00100 = nameof(DecryptSecurityException) + ".Constructer_正しく初期化されたインスタンスを生成";
        public const string TESTNAME_001_00200 = nameof(DecryptSecurityException) + ".Constructer_エラーメッセージを指定できる";
        public const string TESTNAME_001_00300 = nameof(DecryptSecurityException) + ".Constructer_エラーメッセージと内部例外を指定できる";

        [Fact(DisplayName = TESTNAME_001_00100)]
        public void TEST_001_00100()
        {
            var ex = Assert.ThrowsAny<DecryptSecurityException>(() =>
                throw new DecryptSecurityException()
            );

            ex.InnerException.IsNull();
        }

        [Fact(DisplayName = TESTNAME_001_00200)]
        public void TEST_001_00200()
        {
            var ex = Assert.ThrowsAny<DecryptSecurityException>(() =>
                throw new DecryptSecurityException("例外発生テスト")
            );

            ex.Message.Contains("例外発生テスト");
            ex.InnerException.IsNull();
        }

        [Fact(DisplayName = TESTNAME_001_00300)]
        public void TEST_001_00300()
        {
            var innerEx = new NotSupportedException("内部例外の確認");

            var ex = Assert.ThrowsAny<DecryptSecurityException>(() =>
                throw new DecryptSecurityException("例外発生テスト", innerEx)
            );

            ex.Message.Contains("例外発生テスト");
            ex.InnerException.GetType().Is(typeof(NotSupportedException));
            ex.InnerException.Message.Contains("内部例外の確認");
            ex.InnerException.InnerException.IsNull();
        }
    }
}
