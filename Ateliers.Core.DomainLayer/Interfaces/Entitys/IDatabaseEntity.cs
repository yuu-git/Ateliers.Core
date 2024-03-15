using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ateliers
{
    /// <summary>
    /// データベース型エンティティ
    /// </summary>
    public interface IDatabaseEntity<T> : IEntity<T>
        where T : class
    {
        /*--- Property/Field Definitions ----------------------------------------------------------------------------------------------------------*/
        
        /*--- Method: public ----------------------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// エンティティに登録者の情報を設定後、追加を非同期で実行します。
        /// </summary>
        /// <param name="token"> 非同期操作のキャンセルトークンを指定します。 </param>
        /// <returns> エンティティの追加後にコミットした件数を返します。 </returns>
        Task<int> InsertEntityAsync(CancellationToken token = default);

        /// <summary>
        /// エンティティの更新を非同期で実行します。
        /// </summary>
        /// <param name="token"> 非同期操作のキャンセルトークンを指定します。 </param>
        /// <returns> エンティティの更新後にコミットした件数を返します。 </returns>
        Task<int> UpdateEntityAsync(CancellationToken token = default);

        /// <summary>
        /// エンティティの物理削除を非同期で実行します。
        /// </summary>
        /// <param name="token"> 非同期操作のキャンセルトークンを指定します。 </param>
        /// <returns> エンティティの削除後にコミットを実行した件数を返します。 </returns>
        Task<int> DeleteEntityAsync(CancellationToken token = default);
    }
}
