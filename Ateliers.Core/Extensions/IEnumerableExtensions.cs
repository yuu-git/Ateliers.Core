using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ateliers
{
    /// <summary>
    /// <see cref="IEnumerable{T}"/> 拡張メソッド
    /// </summary>
    public static class IEnumerableExtensions
    {
        /*--- Property/Field Definitions ----------------------------------------------------------------------------------------------------------*/

        #region --- 例外メッセージ ---------

        /// <summary>
        /// <see cref="IsEmpty{T}(IEnumerable{T})"/> の検索する文字列がNULL指定された場合の例外メッセージを取得します。
        /// </summary>
        public static string ExceptionMessage100100 { get; } = $"カラを確認するコレクションのインスタンスがNullです。";

        /// <summary>
        /// <see cref="ToAllUpper(IEnumerable{string})"/> の確認するコレクションがNullに指定された場合の例外メッセージを取得します。
        /// </summary>
        public static string ExceptionMessage100200 { get; } = $"文字列を変換するコレクションのインスタンスがNullです。";

        /// <summary>
        /// <see cref="ToAllLower(IEnumerable{string})"/> の確認するコレクションがNullに指定された場合の例外メッセージを取得します。
        /// </summary>
        public static string ExceptionMessage100300 { get; } = $"文字列を変換するコレクションのインスタンスがNullです。";

        #endregion

        /*--- Method: internal --------------------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// コレクションがカラであるかを確認します。
        /// </summary>
        /// <typeparam name="T"> 確認するコレクションの型を指定します。 </typeparam>
        /// <param name="self"> 確認するコレクション（自身） </param>
        /// <returns> コレクションがカラであった場合は true, そうでなかった場合は false を返します。 </returns>
        /// <exception cref="NullReferenceException"> 確認するコレクションのインスタンスは生成されている必要があります。 </exception>
        public static bool IsEmpty<T>(this IEnumerable<T> self)
        {
            if (self == default) 
                throw new NullReferenceException(ExceptionMessage100100);

            return !self.Any();
        }

        /// <summary>
        /// コレクションが既定値またはカラであるかを確認します。
        /// </summary>
        /// <typeparam name="T"> 確認するコレクションの型を指定します。 </typeparam>
        /// <param name="self"> 確認するコレクション（自身） </param>
        /// <returns> コレクションが既定値またはカラであった場合は true, そうでなかった場合は false を返します。 </returns>
        public static bool IsDefaultOrEmpty<T>(this IEnumerable<T> self)
        {
            return self == default || self.IsEmpty();
        }

        /// <summary>
        /// コレクションが規定値の場合、新しいコレクションを生成して返却します。
        /// </summary>
        /// <typeparam name="T"> コレクションに格納する型を指定します。 </typeparam>
        /// <param name="self"> 確認するコレクションを指定します。(自身) </param>
        /// <returns> 規定値であった場合は新しいコレクション、それ以外の場合は引数のコレクションをそのまま返します。 </returns>
        public static IEnumerable<T> Sanitize<T>(this IEnumerable<T> self)
        {
            return self ?? new List<T>();
        }

        /// <summary>
        /// コレクション内の文字列を全て大文字に変換します。
        /// </summary>
        /// <param name="self"> 対象の文字列コレクション(自身) </param>
        /// <returns> 文字列を大文字に変換した新しいリストを返します。 </returns>
        /// <exception cref="NullReferenceException"> 返還対象を格納しているリストを Null にすることはできません。 </exception>
        public static IEnumerable<string> ToAllUpper(this IEnumerable<string> self)
        {
            if (self == default)
                throw new NullReferenceException(ExceptionMessage100200);

            return self.Select(x => x.ToUpper()).ToList();
        }

        /// <summary>
        /// コレクション内の文字列を全て小文字に変換します。
        /// </summary>
        /// <param name="self"> 対象の文字列コレクション(自身) </param>
        /// <returns> 文字列を小文字に変換した新しいリストを返します。 </returns>
        /// <exception cref="NullReferenceException"> 返還対象を格納しているリストを Null にすることはできません。 </exception>
        public static IEnumerable<string> ToAllLower(this IEnumerable<string> self)
        {
            if (self == default)
                throw new NullReferenceException(ExceptionMessage100300);

            return self.Select(x => x.ToLower()).ToList();
        }
    }
}
