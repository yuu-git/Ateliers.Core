using System;
using System.Collections.Generic;
using System.Text;

namespace Ateliers
{
    /// <summary>
    /// 検証チェック結果
    /// </summary>
    /// <remarks>
    /// <para> 概要: <see cref="IValidationImplementation"/> によってバリデーションチェックされた結果を格納しているオブジェクトであることを示す。 </para>
    /// </remarks>
    public interface IValidationDetail
    {
        /*--- Property/Field Definitions ----------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// 検証の結果レベルを取得します。
        /// </summary>
        ValidationLevel Level { get; }

        /// <summary>
        /// 検査の名称を取得します。
        /// </summary>
        string ValidationName { get; }

        /// <summary>
        /// 検証したプロパティの名称を取得します。
        /// </summary>
        string PropertyName { get; }

        /// <summary>
        /// 検証結果の警告や異常のコードを取得します。
        /// </summary>
        string Code { get; }

        /// <summary>
        /// 検証結果の概要やタイトルを取得します。
        /// </summary>
        string Summary { get; }

        /// <summary>
        /// 検証結果の詳細なメッセージを取得します。
        /// </summary>
        string DetailMsg { get; }

        /// <summary>
        /// 検証を実施した時間を取得します。
        /// </summary>
        DateTime? CheckTime { get; }

        /*--- Method ------------------------------------------------------------------------------------------------------------------------------*/


    }
}
