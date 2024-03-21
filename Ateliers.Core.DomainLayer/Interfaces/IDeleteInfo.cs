using System;
using System.Collections.Generic;
using System.Text;

namespace Ateliers
{
    /// <summary>
    /// 削除情報
    /// </summary>
    /// <remarks>
    /// 概要: エンティティの論理削除が行われた際の情報を保持していることを示す。
    /// </remarks>
    public interface IDeleteInfo : IDeletedFlag
    {
        /*--- Property/Field Definitions ----------------------------------------------------------------------------------------------------------*/

        /// <summary> 
        /// データ削除プログラムの識別文字列を取得します。
        /// </summary>
        string DeleteProgramId { get; }

        /// <summary> 
        /// データ削除ユーザーの識別文字列を取得します。
        /// </summary>
        string DeleteUserId { get; }

        /// <summary> 
        /// データ削除日時を取得または設定します。
        /// </summary>
        DateTime? DeleteDateTime { get; }

        /*--- Method: public ----------------------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// エンティティの論理削除情報を設定します。
        /// </summary>
        /// <param name="deleteProgramId"> 削除プログラムの識別子を指定します。 </param>
        /// <param name="deleteUserId"> 削除ユーザーの識別子を指定します。 </param>
        /// <param name="deleteDateTime"> 削除日付を指定します。 </param>
        void SetDeleteInfo(string deleteProgramId, string deleteUserId, DateTime deleteDateTime);
    }
}
