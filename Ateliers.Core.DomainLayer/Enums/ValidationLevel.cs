using System;
using System.Collections.Generic;
using System.Text;

namespace Ateliers
{
    /// <summary>
    /// 検証検査レベル
    /// </summary>
    /// <remarks>
    /// <para> 概要: バリデーションチェックの結果を示す。 </para>
    /// <para> エンティティなどのバリデーションを行った後に処理を分岐させるケースなどに使用する。 </para>
    /// </remarks>
    public enum ValidationLevel
    {
        /// <summary> 初期または未検査である事を示します。 </summary>
        Init = 0,

        /// <summary> 問題が無かったことを示します。 </summary>
        Ok = 1,

        /// <summary> 警告状態である事を示します。 </summary>
        Warning = 2,

        /// <summary> 問題がある状態である事を示します。 </summary>
        Error = 3,
    }
}
