using System;
using System.Collections.Generic;
using System.Text;

namespace Ateliers
{
    /// <summary>
    /// データ操作手法
    /// </summary>
    /// <remarks>
    /// <para> 概要: 主な用途として、データを扱う際の処理ステータスを示すために使用する。 </para>
    /// <para> ログとして記録することで、どのような処理を行ったかを追跡しやすくする。 </para>
    /// </remarks>
    public enum DataOperationMethod
    {
        /// <summary> 登録操作である事を示します。 </summary>
        Insert,
        /// <summary> 更新操作である事を示します。 </summary>
        Update,
        /// <summary> 削除操作である事を示します。 </summary>
        Delete,
    }
}
