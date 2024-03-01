using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Ateliers
{
    /// <summary>
    /// STA - 暗号化サービス
    /// </summary>
    /// <remarks>
    /// <para> 概要: 文字列の暗号化と復号化を実行する。 </para>
    /// </remarks>
    public static class EncryptService
    {
        /*--- Property/Field Definitions ----------------------------------------------------------------------------------------------------------*/

        #region --- エラーメッセージ ---

        //Todo: EncryptService: エラーメッセージの多言語対応
        /// <summary> 異常終了メッセージ: 暗号化する文字列は必須です。 </summary>
        public static string MSG_ERR_010_0010 => "暗号化する文字列は必須です。";
        /// <summary> 異常終了メッセージ: 暗号化に使用するパスワードは必須です。 </summary>
        public static string MSG_ERR_010_0020 => "暗号化に使用するパスワードは必須です。";
        /// <summary> 異常終了メッセージ: 復号化する文字列は必須です。 </summary>
        public static string MSG_ERR_020_0010 => "復号化する文字列は必須です。";
        /// <summary> 異常終了メッセージ: 復号化に使用するパスワードは必須です。 </summary>
        public static string MSG_ERR_020_0020 => "復号化に使用するパスワードは必須です。";
        /// <summary> 異常終了メッセージ: 文字列の復号に失敗しました。 </summary>
        public static string MSG_ERR_030_0010 => "文字列の復号に失敗しました。";
        #endregion

        /// <summary>
        /// RFC2898による暗号化と復号化
        /// </summary>
        public static class RFC2898
        {
            /*--- Property/Field Definitions ----------------------------------------------------------------------------------------------------------*/
            /*--- Method: public ----------------------------------------------------------------------------------------------------------------------*/

            /// <summary>
            /// ＜RFC2898＞ 文字列を暗号化して返却します。
            /// </summary>
            /// <param name="sourceString"> 暗号化する文字列を指定します。(必須) </param>
            /// <param name="password"> 暗号化に使用するパスワードを指定します。(必須) </param>
            /// <returns> 暗号化された文字列を返します。 </returns>
            /// <exception cref="ArgumentNullException"> 暗号化に必要な文字列またはパスワードが未指定の場合に発生します。 </exception>
            public static string EncryptString(string sourceString, string password)
            {
                if (sourceString == default || string.IsNullOrWhiteSpace(sourceString))
                    throw new ArgumentNullException(MSG_ERR_010_0010);

                if (password == default || string.IsNullOrWhiteSpace(password))
                    throw new ArgumentNullException(MSG_ERR_010_0020);

                // RijndaelManagedオブジェクトを作成
                var rijndael = new RijndaelManaged();

                // パスワードから共有キーと初期化ベクタを作成
                var generateResult = GenerateKeyFromPassword(password, rijndael.KeySize, rijndael.BlockSize);
                rijndael.Key = generateResult.Item1;
                rijndael.IV = generateResult.Item2;

                // 文字列をバイト型配列に変換する
                var strBytes = Encoding.UTF8.GetBytes(sourceString);

                // 暗号化オブジェクトの作成
                ICryptoTransform encryptor = rijndael.CreateEncryptor();
                var encBytes = encryptor.TransformFinalBlock(strBytes, 0, strBytes.Length);
                encryptor.Dispose();

                // バイト型配列を文字列に変換して返す
                return Convert.ToBase64String(encBytes);
            }

            /// <summary>
            /// ＜RFC2898＞ ロジックで暗号化された文字列を復号して返却します。
            /// </summary>
            /// <param name="sourceString"> 暗号化された文字列を指定します。(必須) </param>
            /// <param name="password"> 複合に使用するパスワードを指定します。(必須) </param>
            /// <returns> 復号化された文字列を返します。複合化に失敗した場合は例外エラーを返します。 </returns>
            /// <exception cref="ArgumentNullException"> 複合化に必要な文字列またはパスワードが未指定の場合に発生します。 </exception>
            /// <exception cref="DecryptSecurityException"> 文字列の復号に失敗した場合に発生します。 </exception>
            public static string DecryptString(string sourceString, string password)
            {
                if (sourceString == default || string.IsNullOrWhiteSpace(sourceString))
                    throw new ArgumentNullException(MSG_ERR_020_0010);

                if (password == default || string.IsNullOrWhiteSpace(password))
                    throw new ArgumentNullException(MSG_ERR_020_0020);

                // RijndaelManagedオブジェクトを作成
                var rijndael = new RijndaelManaged();

                // パスワードから共有キーと初期化ベクタを作成
                var generateResult = GenerateKeyFromPassword(password, rijndael.KeySize, rijndael.BlockSize);
                rijndael.Key = generateResult.Item1;
                rijndael.IV = generateResult.Item2;

                // 文字列をバイト型配列に戻す
                var strBytes = Convert.FromBase64String(sourceString);

                try
                {
                    // バイト型配列を復号化する
                    var decryptor = rijndael.CreateDecryptor();
                    byte[] decBytes = decryptor.TransformFinalBlock(strBytes, 0, strBytes.Length);

                    decryptor.Dispose();

                    // バイト型配列を文字列に戻して返す
                    return Encoding.UTF8.GetString(decBytes);
                }
                catch (CryptographicException e)
                {
                    throw new DecryptSecurityException(MSG_ERR_030_0010, e);
                }
            }

            /*--- Method: internal --------------------------------------------------------------------------------------------------------------------*/

            /// <summary>
            /// ＜RFC2898＞ パスワードから共有キーと初期化ベクタを生成します。
            /// </summary>
            /// <param name="password"> 基になるパスワードを指定します。 </param>
            /// <param name="keySize"> 共有キーのサイズ（ビット）を指定します。 </param>
            /// <param name="blockSize"> 初期化ベクタのサイズ（ビット）を指定します。 </param>
            /// <param name="iterationCount"> 反復処理をする回数を指定します。(デフォルト: 1000 回) </param>
            /// <param name="salt"> セキュリティ用にランダムバイト化する文字列を指定します。 </param>
            /// <returns> 作成された共有キー(Key)と初期化ベクタ(Iv)を返します。 </returns>
            internal static Tuple<byte[], byte[]> GenerateKeyFromPassword(string password, int keySize, int blockSize, int iterationCount = 1000, string salt = "ATELIERS")
            {
                // Rfc2898DeriveBytesオブジェクトを作成する
                var deriveBytes = new Rfc2898DeriveBytes(password, Encoding.UTF8.GetBytes(salt))
                {
                    // 反復処理回数を指定
                    IterationCount = iterationCount
                };

                // 共有キーと初期化ベクタを生成する
                return new Tuple<byte[], byte[]>(deriveBytes.GetBytes(keySize / 8), deriveBytes.GetBytes(blockSize / 8));
            }

            /*--- Method: private ---------------------------------------------------------------------------------------------------------------------*/

        }

        /*--- Method: public ----------------------------------------------------------------------------------------------------------------------*/
        /*--- Method: internal --------------------------------------------------------------------------------------------------------------------*/
        /*--- Method: private ---------------------------------------------------------------------------------------------------------------------*/

    }
}