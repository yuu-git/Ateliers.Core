using System;
using System.Collections.Generic;
using System.Text;

namespace Ateliers
{
    /// <summary>
    /// 処理進捗
    /// </summary>
    /// <remarks>
    /// <para> 概要: 主に非同期処理の進捗状況管理に使用する。 </para>
    /// <para> 進捗の問い合わせ処理や通知処理と合わせて画面に表示したり、ログの記録を行ったりする。 </para>
    /// </remarks>
    public enum Progress
    {
        /// <summary> 未分類 </summary>
        Non = 0,

        // --- [10~19] 処理開始前 ---

        /// <summary> 未処理 </summary>
        Init = 10,
        /// <summary> 開始待ち </summary>
        Wait = 11,

        // --- [20~29] 処理実行中 ---

        /// <summary> 処理中 </summary>
        Run = 20,

        // --- [100~199] 正常終了 ---

        /// <summary> 成功 </summary>
        Success = 100,

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
