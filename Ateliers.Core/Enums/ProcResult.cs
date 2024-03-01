using System;
using System.Collections.Generic;
using System.Text;

namespace Ateliers
{
    /// <summary>
    /// 処理結果
    /// </summary>
    /// <remarks>
    /// <para> 概要: なんらかの処理や操作を行った際に、どのような結果となったかを示す汎用結果。 </para>
    /// <para> テストやログの記録などに使用する。 </para>
    /// <para> データ処理(保存, 更新, 削除)の場合は <see cref="DataOperationResult"/> を使用する。 </para>
    /// <para> ※データが関連する場合、処理は成功 (Success = 100) し、更新の必要がなかった (Skip = 220) となるケースもあるため、混同しないため別ステータスとする。 </para>
    /// </remarks>
    public enum ProcResult
    {
        // --- [100~199] 正常終了 ---

        /// <summary> 成功 </summary>
        Success = 100,

        /// <summary> 警告終了 </summary>
        Warning = 150,

        // --- [200~299] 処理未完走（理由有） ---

        /// <summary> 中止 
        /// <para> => ユーザー意思による処理終了 </para>
        /// </summary>
        Cancel = 200,
        /// <summary> 中断 
        /// <para> => 処理中に、以降を処理する必要のない終了 </para>
        /// </summary>
        Break = 210,
        /// <summary> 処理無し 
        /// <para> => 判定により、処理不要と判断した場合の終了 </para>
        /// </summary>
        Skip = 220,

        // --- [300~399] 処理失敗（異常系） ---

        /// <summary> 失敗 </summary>
        Failure = 300,
    }
}
