using System;
using System.Collections.Generic;
using System.Text;

namespace Ateliers
{
    /// <summary>
    /// 登録情報
    /// </summary>
    /// <remarks>
    /// <para> 概要: エンティティのデータ登録が行われた際の情報を保持していることを示す。 </para>
    /// </remarks>
    public interface IRegistInfo
    {
        /*--- Property/Field Definitions ----------------------------------------------------------------------------------------------------------*/

        /// <summary> 
        /// データ登録プログラムの識別文字列を取得または設定します。
        /// </summary>
        string RegistProgram { get; set; }

        /// <summary> 
        /// データ登録ユーザーの識別文字列を取得または設定します。
        /// </summary>
        string RegistUser { get; set; }

        /// <summary> 
        /// データ登録日時を取得または設定します。
        /// </summary>
        DateTime? RegistDateTime { get; set; }

        /// <summary>
        /// データ登録の状態を示す値を取得します。
        /// </summary>
        bool IsEnteredRegist { get; }

        /*--- Method: public ----------------------------------------------------------------------------------------------------------------------*/

    }
}
