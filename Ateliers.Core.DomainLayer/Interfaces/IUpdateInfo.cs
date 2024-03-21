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
        /// データ更新プログラムの識別文字列を取得します。
        /// </summary>
        string UpdateProgramId { get; }

        /// <summary> 
        /// データ更新ユーザーの識別文字列を取得します。
        /// </summary>
        string UpdateUserId { get; set; }

        /// <summary> 
        /// データ更新日時を取得します。
        /// </summary>
        DateTime? UpdateDateTime { get; set; }

        /*--- Method: public ----------------------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// エンティティの更新情報を設定します。
        /// </summary>
        /// <param name="updateProgramId"> 更新プログラムの識別子を指定します。 </param>
        /// <param name="updateUserId"> 更新ユーザーの識別子を指定します。 </param>
        /// <param name="updateDateTime"> 更新日付を指定します。 </param>
        void SetUpdateInfo(string updateProgramId, string updateUserId, DateTime updateDateTime);
    }
}
