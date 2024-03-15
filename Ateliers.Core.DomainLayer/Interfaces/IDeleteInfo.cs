using System;
using System.Collections.Generic;
using System.Text;

namespace Ateliers
{
    /// <summary>
    /// 削除情報
    /// </summary>
    /// <remarks>
    /// <para> 概要: エンティティの論理削除が行われた際の情報を保持していることを示す。 </para>
    /// </remarks>
    public interface IDeleteInfo
    {
        /*--- Property/Field Definitions ----------------------------------------------------------------------------------------------------------*/

        /// <summary> 
        /// データ削除プログラムの識別文字列を取得または設定します。
        /// </summary>
        string DeleteProgram { get; set; }

        /// <summary> 
        /// データ削除ユーザーの識別文字列を取得または設定します。
        /// </summary>
        string DeleteUser { get; set; }

        /// <summary> 
        /// データ削除日時を取得または設定します。
        /// </summary>
        DateTime? DeleteDateTime { get; set; }

        /// <summary>
        /// 削除フラグを取得または設定します。
        /// </summary>
        bool IsDeleted { get; set; }

        /*--- Method: public ----------------------------------------------------------------------------------------------------------------------*/

    }
}
