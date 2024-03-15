using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ateliers
{
    /// <summary>
    /// [IF] タイムスタンプエンティティ
    /// </summary>
    public interface ITimeStampEntity : IEntity, IRegistInfo, IUpdateInfo, IDeleteInfo
    {
        /*--- Property/Field Definitions ----------------------------------------------------------------------------------------------------------*/

        /*--- Method: public ----------------------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// エンティティの追加を非同期で実行します。
        /// </summary>
        /// <param name="entityOperator"> 登録情報に記録するエンティティ操作者を指定します。 </param>
        /// <param name="cancellationToken"> 非同期操作のキャンセルトークンを指定します。 </param>
        /// <returns> エンティティの追加後にコミットした件数を返します。 </returns>
        /// <exception cref="InvalidOperationException"> 追加の実行にはリポジトリが実装されている必要があります。 </exception>
        Task<int> InsertEntityAsync(IOperator entityOperator, CancellationToken cancellationToken = default);

        /// <summary>
        /// エンティティの追加を非同期で実行します。
        /// </summary>
        /// <param name="entityOperator"> 登録情報に記録するエンティティ操作者を指定します。 </param>
        /// <param name="registTime"> 登録の時刻を指定します。 </param>
        /// <param name="cancellationToken"> 非同期操作のキャンセルトークンを指定します。 </param>
        /// <returns> エンティティの追加後にコミットした件数を返します。 </returns>
        /// <exception cref="InvalidOperationException"> 追加の実行にはリポジトリが実装されている必要があります。 </exception>
        Task<int> InsertEntityAsync(IOperator entityOperator, DateTime registTime, CancellationToken cancellationToken = default);

        /// <summary>
        /// エンティティの更新を非同期で実行します。
        /// </summary>
        /// <param name="entityOperator"> 更新情報に記録するエンティティ操作者を指定します。 </param>
        /// <param name="cancellationToken"> 非同期操作のキャンセルトークンを指定します。 </param>
        /// <returns> エンティティの更新後にコミットした件数を返します。 </returns>
        /// <exception cref="InvalidOperationException"> 更新の実行にはリポジトリが実装されている必要があります。 </exception>
        Task<int> UpdateEntityAsync(IOperator entityOperator, CancellationToken cancellationToken = default);

        /// <summary>
        /// エンティティの更新を非同期で実行します。
        /// </summary>
        /// <param name="entityOperator"> 更新情報に記録するエンティティ操作者を指定します。 </param>
        /// <param name="updateTime"> 更新の時刻を指定します。 </param>
        /// <param name="cancellationToken"> 非同期操作のキャンセルトークンを指定します。 </param>
        /// <returns> エンティティの更新後にコミットした件数を返します。 </returns>
        /// <exception cref="InvalidOperationException"> 更新の実行にはリポジトリが実装されている必要があります。 </exception>
        Task<int> UpdateEntityAsync(IOperator entityOperator, DateTime updateTime, CancellationToken cancellationToken = default);

        /// <summary>
        /// エンティティの論理削除を非同期で実行します。
        /// </summary>
        /// <param name="entityOperator"> 削除情報に記録するエンティティ操作者を指定します。 </param>
        /// <param name="cancellationToken"> 非同期操作のキャンセルトークンを指定します。 </param>
        /// <returns> エンティティの削除後にコミットを実行した件数を返します。 </returns>
        Task<int> DeleteEntityOfLogicalAsync(IOperator entityOperator, CancellationToken cancellationToken = default);

        /// <summary>
        /// エンティティの論理削除を非同期で実行します。
        /// </summary>
        /// <param name="entityOperator"> 削除情報に記録するエンティティ操作者を指定します。 </param>
        /// <param name="deleteDateTime"> 削除の時刻を指定します。 </param>
        /// <param name="cancellationToken"> 非同期操作のキャンセルトークンを指定します。 </param>
        /// <returns> エンティティの削除後にコミットを実行した件数を返します。 </returns>
        Task<int> DeleteEntityOfLogicalAsync(IOperator entityOperator, DateTime deleteDateTime, CancellationToken cancellationToken = default);

        void SetRegistInfo(IOperator entityOperator, DateTime? insertTime = null);

        void SetUpdateInfo(IOperator entityOperator, DateTime? updateTime = null);

        void SetDeleteInfo(IOperator entityOperator, DateTime? deleteTime = null);
    }
}
