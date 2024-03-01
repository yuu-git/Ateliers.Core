using System;
using System.Collections.Generic;
using System.Text;

namespace Ateliers
{
    /// <summary>
    /// [オペレーター
    /// </summary>
    /// <remarks>
    /// <para> 概要: 使用しているユーザーやプログラム名の情報を保持していることを示す。 </para>
    /// <para> データの登録更新削除などの処理時に『誰が何をしたのか』を記録するために使用したり、権限の判定を行ったりするために使用する。 </para>
    /// </remarks>
    public interface IOperator
    {
        /*--- Property/Field Definitions ----------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// 操作プログラムの識別文字列（コードや名称など）を取得します。
        /// </summary>
        string OperationProgram { get; }

        /// <summary>
        /// 操作ユーザーの識別文字列（コードや名称など）を取得します。
        /// </summary>
        string OperationUser { get; }

        /*--- Method: public ----------------------------------------------------------------------------------------------------------------------*/

    }
}
