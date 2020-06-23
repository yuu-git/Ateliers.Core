using System;
using System.Security.Cryptography;
using Xunit;

namespace Ateliers.UnitTest.Services
{
    public class EncryptServiceTest
    {
        public const string TESTNAME_001_00100 = nameof(EncryptService) + "." + nameof(EncryptService.RFC2898) + "." + nameof(EncryptService.RFC2898.EncryptString) + "_" + "正しく暗号化できる";
        public const string TESTNAME_001_00200 = nameof(EncryptService) + "." + nameof(EncryptService.RFC2898) + "." + nameof(EncryptService.RFC2898.EncryptString) + "_" + "暗号化する文字列は必須";
        public const string TESTNAME_001_00300 = nameof(EncryptService) + "." + nameof(EncryptService.RFC2898) + "." + nameof(EncryptService.RFC2898.EncryptString) + "_" + "パスワードは必須";

        public const string TESTNAME_002_00100 = nameof(EncryptService) + "." + nameof(EncryptService.RFC2898) + "." + nameof(EncryptService.RFC2898.DecryptString) + "_" + "正しく複合化できる";
        public const string TESTNAME_002_00200 = nameof(EncryptService) + "." + nameof(EncryptService.RFC2898) + "." + nameof(EncryptService.RFC2898.DecryptString) + "_" + "複合化する文字列は必須";
        public const string TESTNAME_002_00300 = nameof(EncryptService) + "." + nameof(EncryptService.RFC2898) + "." + nameof(EncryptService.RFC2898.DecryptString) + "_" + "パスワードは必須";
        public const string TESTNAME_002_00400 = nameof(EncryptService) + "." + nameof(EncryptService.RFC2898) + "." + nameof(EncryptService.RFC2898.DecryptString) + "_" + "パスワードが合わない場合は例外エラー";

        [Fact(DisplayName = TESTNAME_001_00100)]
        public void TEST_001_00100()
        {
            var testStr = "TestStr";
            var pass = "testPassword";

            var assumedResult = "uE41N9yGgoWWQSc2nvkuwA==";
            var testResult = EncryptService.RFC2898.EncryptString(testStr, pass);

            testResult.Is(assumedResult);
        }

        [Fact(DisplayName = TESTNAME_001_00200)]
        public void TEST_001_00200()
        {
            foreach (var testStr in new[] { default, string.Empty, " " })
            {
                var ex = Assert.ThrowsAny<ArgumentNullException>(() =>
                {
                    EncryptService.RFC2898.EncryptString(testStr, "testPassword");
                });

                ex.Message.Contains("暗号化する文字列は必須です。");
            }
        }

        [Fact(DisplayName = TESTNAME_001_00300)]
        public void TEST_001_00300()
        {
            foreach (var testPassword in new[] { default, string.Empty, " " })
            {
                var ex = Assert.ThrowsAny<ArgumentNullException>(() =>
                {
                    EncryptService.RFC2898.EncryptString("TestStr", testPassword);
                });

                ex.Message.Contains("暗号化に使用するパスワードは必須です。");
            }
        }

        [Fact(DisplayName = TESTNAME_002_00100)]
        public void TEST_002_00100()
        {
            var testStr = "uE41N9yGgoWWQSc2nvkuwA==";
            var pass = "testPassword";

            var assumedResult = "TestStr";
            var testResult = EncryptService.RFC2898.DecryptString(testStr, pass);

            testResult.Is(assumedResult);
        }

        [Fact(DisplayName = TESTNAME_002_00200)]
        public void TEST_002_00200()
        {
            foreach (var testStr in new[] { default, string.Empty, " " })
            {
                var ex = Assert.ThrowsAny<ArgumentNullException>(() =>
                {
                    EncryptService.RFC2898.DecryptString(testStr, "testPassword");
                });

                ex.Message.Contains("複合化する文字列は必須です。");
            }
        }

        [Fact(DisplayName = TESTNAME_002_00300)]
        public void TEST_002_00300()
        {
            foreach (var testPassword in new[] { default, string.Empty, " " })
            {
                var ex = Assert.ThrowsAny<ArgumentNullException>(() =>
                {
                    EncryptService.RFC2898.DecryptString("TestStr", testPassword);
                });

                ex.Message.Contains("複合化に使用するパスワードは必須です。");
            }
        }

        [Fact(DisplayName = TESTNAME_002_00400)]
        public void TEST_002_00400()
        {
            var ex = Assert.ThrowsAny<DecryptSecurityException>(() =>
            {
                EncryptService.RFC2898.DecryptString("uE41N9yGgoWWQSc2nvkuwA==", "aaa");
            });

            ex.Message.Contains("文字列の復号に失敗しました。");
            
            // 内部例外の型も確認
            ex.InnerException.GetType().Is(typeof(CryptographicException));
            
        }
    }
}
