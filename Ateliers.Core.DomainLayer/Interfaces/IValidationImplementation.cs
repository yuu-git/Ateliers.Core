using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ateliers
{
    /// <summary>
    /// [IF] 検証実装
    /// </summary>
    /// <remarks>
    /// <para> 概要: オブジェクトがバリデーションを実装していることを示す。</para>
    /// <para> 主にデータの入出力が存在するエンティティに実装し、保存前にデータが正常である事を確認する。 </para>
    /// </remarks>
    public interface IValidationImplementation
    {
        /*--- Property/Field Definitions ----------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// 非同期処理で整合性の検証を実行します。
        /// </summary>
        /// <param name="token"> 検証を中止するキャンセルトークンを指定します。 </param>
        /// <returns> 整合性チェックの結果を返します。 </returns>
        Task<IEnumerable<IValidationDetail>> ExecuteValidationAsync(CancellationToken token = default);

        /// <summary>
        /// 整合性の検証を実行します。
        /// </summary>
        /// <returns> 整合性チェックの結果を返します。 </returns>
        IEnumerable<IValidationDetail> ExecuteValidation();

        /*--- Method: public ----------------------------------------------------------------------------------------------------------------------*/

    }
}
