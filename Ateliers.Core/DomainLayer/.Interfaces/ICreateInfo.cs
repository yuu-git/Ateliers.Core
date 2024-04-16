using System;

namespace Ateliers
{
    /// <summary>
    /// 作成情報
    /// </summary>
    /// <remarks>
    /// 概要: エンティティの作成が行われた際の情報を保持していることを示す。
    /// </remarks>
    public interface ICreateInfo
    {
        /// <summary> 
        /// データ作成プログラムの識別文字列を取得します。
        /// </summary>
        string CreateProgramId { get; }

        /// <summary> 
        /// データ作成ユーザーの識別文字列を取得します。
        /// </summary>
        string CreateUserId { get; }

        /// <summary> 
        /// データ作成日時を取得または設定します。
        /// </summary>
        DateTime? CreateDateTime { get; }

        /// <summary>
        /// エンティティの作成情報を設定します。
        /// </summary>
        /// <param name="entityOperator"> 作成操作者を指定します。 </param>
        /// <param name="createDateTime"> 作成日時を指定します。 </param>
        void SetCreateInfo(IOperator entityOperator, DateTime createDateTime);

        /// <summary>
        /// エンティティの作成情報を設定します。
        /// </summary>
        /// <param name="createProgramId"> 作成プログラムの識別子を指定します。 </param>
        /// <param name="createUserId"> 作成ユーザーの識別子を指定します。 </param>
        /// <param name="createDateTime"> 作成日付を指定します。 </param>
        void SetCreateInfo(string createProgramId, string createUserId, DateTime createDateTime);
    }
}
