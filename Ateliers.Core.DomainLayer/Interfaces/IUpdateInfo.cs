using System;
using System.Collections.Generic;
using System.Text;

namespace Ateliers
{
    /// <summary>
    /// 更新情報
    /// </summary>
    /// <remarks>
    /// <para> 概要: エンティティのデータ更新が行われた際の情報を保持していることを示す。 </para>
    /// </remarks>
    public interface IUpdateInfo
    {
        /*--- Property/Field Definitions ----------------------------------------------------------------------------------------------------------*/

        /// <summary> 
        /// データ更新プログラムの識別文字列を取得または設定します。
        /// </summary>
        string UpdateProgram { get; set; }

        /// <summary> 
        /// データ更新ユーザーの識別文字列を取得または設定します。
        /// </summary>
        string UpdateUser { get; set; }

        /// <summary> 
        /// データ更新日時を取得または設定します。
        /// </summary>
        DateTime? UpdateDateTime { get; set; }

        /// <summary>
        /// データ更新の状態を示す値を取得します。
        /// </summary>
        bool IsEnteredUpdate { get; }

        /*--- Method: public ----------------------------------------------------------------------------------------------------------------------*/

    }
}
