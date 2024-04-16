using System;
using System.Collections.Generic;
using System.Text;

namespace Ateliers
{
    /// <summary>
    /// トランザクションステータス
    /// </summary>
    /// <remarks>
    /// 概要: トランザクションの状態を示す。
    /// </remarks>
    public enum TransactionStatus
    {
        /// <summary> 稼働中のトランザクションである事を示します。 </summary>
        Run,
        /// <summary> コミットが終了している事を示します。 </summary>
        Commit,
        /// <summary> ロールバックが終了している事を示します。 </summary>
        Rollback,
    }
}
