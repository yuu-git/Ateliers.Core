using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Ateliers
{
    /// <summary>
    /// 文字列型(<see cref="string"/>) 拡張メソッド
    /// </summary>
    public static class StringExtensions
    {
        /*--- Property/Field Definitions ----------------------------------------------------------------------------------------------------------*/

        #region --- 例外メッセージ ---------

        /// <summary>
        /// <see cref="SubstringLeft(string, string)"/> の検索する文字列がNULL指定された場合の例外メッセージを取得します。
        /// </summary>
        public static string ExceptionMessage100100 { get; } = $"検索キーとする文字列の指定は必須です。";

        /// <summary>
        /// <see cref="SubstringLeft(string, string)"/> の検索する文字列が <see cref="string.Empty"/> に指定された場合の例外メッセージを取得します。
        /// </summary>
        public static string ExceptionMessage100101 { get; } = $"検索キーする文字列を {nameof(string.Empty)} に指定することはできません。";

        #endregion

        /*--- Method: internal --------------------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// 指定された文字列を左側から検索し、最初に一致する対象文字列より左側を取り除いた文字列を返却します。
        /// </summary>
        /// <param name="self"> 取り除きの対象となる文字列(自身) </param>
        /// <param name="keyStr"> 検索する(取り除く)文字列を指定します。 </param>
        /// <returns> 対象文字列より左側を切り取った文字列を返します。対象が見つからない場合は、そのままの文字列を返します。 </returns>
        /// <exception cref="ArgumentNullException"> 取り除く文字列を Null にすることはできません。 </exception>
        /// <exception cref="ArgumentException"> 取り除く文字列を <see cref="string.Empty"/> に指定することはできません。 </exception>
        /// <example>
        /// 構文: "AAABBBCCC".SubstringLeft("A")            結果: "AABBBCCC"
        /// 構文: "AAABBBCCC".SubstringLeft("BB")           結果: "BCCC"
        /// 構文: "AAABBBCCC".SubstringLeft("BC")           結果: "CC"
        /// 構文: "TESTRESULT_xxxx".SubstringLeft("_")      結果: "xxxx"
        /// 構文: "TESTRESULT_xxxx_yyy".SubstringLeft("_")  結果: "xxxx_yyy"
        /// 構文: "2020/06/29".SubstringLeft("/")           結果: "06/29"
        /// 構文: "2020/06/29".SubstringLeft("A")           結果: "2020/06/29"
        /// </example>
        public static string SubstringLeft(this string self, string keyStr)
        {
            if (keyStr == default)
                throw new ArgumentNullException(nameof(keyStr), ExceptionMessage100100);
            else if (keyStr == string.Empty)
                throw new ArgumentException(ExceptionMessage100101, nameof(keyStr));

            if (!self.Contains(keyStr))
                return self;
 
            return self.Substring(self.IndexOf(keyStr) + keyStr.Length);
        }

        /// <summary>
        /// 文字列がNullまたは空白("", " ")であるかを確認し、結果を返却します。
        /// </summary>
        /// <param name="src"> 確認する文字列(自身) </param>
        /// <returns> Null または 空白 である場合は true, そうでない場合は false を返します。 </returns>
        public static bool IsNullOrWhiteSpace(this string src) => string.IsNullOrWhiteSpace(src);

        /// <summary>
        /// 文字列が Null の場合、空白に置き換えて返却します。
        /// </summary>
        /// <param name="src"> 確認する文字列(自身) </param>
        /// <returns> 対象が Null の場合は <see cref="string.Empty"/>, そうでない場合はそのままの文字列を返します。 </returns>
        public static string Sanitize(this string src) => src ?? string.Empty;

        /*--- Method: private ---------------------------------------------------------------------------------------------------------------------*/

    }
}
