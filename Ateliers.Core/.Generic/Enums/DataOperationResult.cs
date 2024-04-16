using System;
using System.Collections.Generic;
using System.Text;

namespace Ateliers
{
    /// <summary>
    /// データ操作結果
    /// </summary>
    /// <remarks>
    /// <para> 概要: 主な用途として、データ操作を行った際に、どのような結果となったかを示すために使用する。 </para>
    /// <para> <see cref="DataOperationMethod"/> と組み合わせることで Insert-Failure [挿入-失敗] や Update-Warning [更新-警告終了] のような記録を作成できる。 </para>
    /// <para> <see cref="ProcResult"/> と用途が似ているが、こちらはデータ処理のみに使用する。 </para>
    /// </remarks>
    public enum DataOperationResult
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
