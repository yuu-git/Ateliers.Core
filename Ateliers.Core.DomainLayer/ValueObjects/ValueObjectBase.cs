using System;
using System.Collections.Generic;
using System.Text;

namespace Ateliers.Core.ValueObjects
{
    /// <summary>
    /// 値オブジェクト基底クラス
    /// </summary>
    /// <typeparam name="T"> 継承先の値オブジェクト型を指定します。 </typeparam>
    public abstract class ValueObjectBase<T> where T : ValueObjectBase<T>
    {
        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is T valueObject && EqualsCore(valueObject);
        }

        /// <summary>
        /// 等価性比較を実行します。
        /// </summary>
        /// <param name="other"> 比較対象の <see cref="ValueObject{T}"/> を指定します。 </param>
        /// <returns> オブジェクトが同一である場合は true, それ以外は false を返します。 </returns>
        /// <remarks>
        /// 抽象の実装内容: <see cref="ValueObject{T}.Equals(object?)"/> で実行される等価性比較メソッドを実装します。
        /// </remarks>
        /// <example lang="C#">
        /// 継承先の簡易実装例: 引数 (別の値オブジェクト) の null ケースの対応と、値オブジェクトの同値確認を実行して下さい。
        /// <![CDATA[
        /// protected override bool EqualsCore(T other)
        /// {
        ///     return other is null ? false : Value == other.Value && Name == other.Name;
        /// }
        /// ]]>
        /// </example>
        protected abstract bool EqualsCore(T other);

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return GetHashCodeCore();
        }

        /// <summary>
        /// オブジェクトのハッシュコード生成を実行します。
        /// </summary>
        /// <returns> ハッシュ値を返します。 </returns>
        /// <remarks>
        /// 抽象の実装内容: <see cref="ValueObjectBase{T}.GetHashCode()"/> で実行されるハッシュコード生成メソッドを実装します。 
        /// </remarks>
        /// <example lang="C#">
        /// 継承先の簡易実装例: 値オブジェクトに応じたハッシュ値生成のメソッドを実行して下さい。
        /// <![CDATA[
        /// protected override int GetHashCodeCore()
        /// {
        ///     return Value.GetHashCode() ^ Name?.GetHashCode ?? 0;
        /// }
        /// ]]>
        /// </example>
        protected abstract int GetHashCodeCore();

        /// <summary>
        /// <see cref="ValueObject{T}"/> 同士の等価性比較演算を実行します。
        /// </summary>
        /// <param name="a"> 1つ目のオブジェクトを指定します。 </param>
        /// <param name="b"> 2つ目のオブジェクトを指定します。 </param>
        /// <returns> オブジェクトが同一である場合は true, それ以外は false を返します。 </returns>
        public static bool operator ==(ValueObjectBase<T> a, ValueObjectBase<T> b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        /// <summary>
        /// <see cref="ValueObject{T}"/> 同士の非等価性比較演算を実行します。
        /// </summary>
        /// <param name="a"> 1つ目のオブジェクトを指定します。 </param>
        /// <param name="b"> 2つ目のオブジェクトを指定します。 </param>
        /// <returns> オブジェクトが同一ではない場合は true, それ以外は false を返します。 </returns>
        public static bool operator !=(ValueObjectBase<T> a, ValueObjectBase<T> b)
        {
            return !(a == b);
        }
    }
}
