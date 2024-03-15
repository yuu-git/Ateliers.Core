using System;
using System.Collections.Generic;
using System.Text;

namespace Ateliers
{
    /// <summary>
    /// 論理削除フラグ
    /// </summary>
    /// <remarks>
    /// <para> 概要: エンティティが論理削除されているかを判定できるフラグを保持していることを示す。 </para>
    /// </remarks>
    public interface IDeletedFlag
    {
        /*--- Property/Field Definitions ----------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// 論理削除フラグを取得します。
        /// </summary>
        bool IsDeleted { get; }

        /*--- Method: public ----------------------------------------------------------------------------------------------------------------------*/

    }
}
