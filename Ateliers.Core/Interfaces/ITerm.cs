using System;
using System.Collections.Generic;
using System.Text;

namespace Ateliers
{
    /// <summary>
    /// 期間
    /// </summary>
    /// <remarks>
    /// <para> 概要: 特定の日付～特定の日付　を示す <see cref="DateTime"/> のプロパティを保持していることを示す。 </para>
    /// <para> 主な用途として、指定期間のみ処理を行う判定や、現在のシステム日付や指定日付が期間内にあるかを判定するのに使用する。  </para>
    /// </remarks>
    public interface ITerm
    {
        /*--- Property/Field Definitions ----------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// 開始日時を取得します。
        /// </summary>
        DateTime StartDateTime { get; }

        /// <summary>
        /// 終了日時を取得します。
        /// </summary>
        DateTime EndDateTime { get; }

        /*--- Method: public ----------------------------------------------------------------------------------------------------------------------*/

    }
}
