using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Ateliers
{
    /// <summary>
    /// リポジトリ
    /// </summary>
    /// <typeparam name="TEntity"> エンティティの型を指定します。 </typeparam>
    /// <remarks>
    /// </remarks>
    public interface IRepository<TEntity>
        where TEntity : class
    {
        /*--- Property/Field Definitions ----------------------------------------------------------------------------------------------------------*/

        /*--- Method: public ----------------------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// 非同期処理で複数のエンティティ登録を実行します。
        /// </summary>
        /// <param name="entitys"> 登録処理をするエンティティコレクションを指定します。 </param>
        /// <param name="token"> (任意) 非同期処理のキャンセルトークンを指定します。 </param>
        /// <returns> 処理した件数を返します。 </returns>
        Task<int> InsertAsync(IEnumerable<TEntity> entitys, CancellationToken token = default);

        /// <summary>
        /// 非同期処理でエンティティ登録を実行します。
        /// </summary>
        /// <param name="entity"> 登録処理をするエンティティを指定します。 </param>
        /// <param name="token"> (任意) 非同期処理のキャンセルトークンを指定します。 </param>
        /// <returns> 処理した件数を返します。 </returns>
        Task<int> InsertAsync(TEntity entity, CancellationToken token = default);

        /// <summary>
        /// 非同期処理で複数のエンティティ更新を実行します。
        /// </summary>
        /// <param name="entitys"> 更新処理をするエンティティコレクションを指定します。 </param>
        /// <param name="token"> (任意) 非同期処理のキャンセルトークンを指定します。 </param>
        /// <returns> 処理した件数を返します。 </returns>
        Task<int> UpdateAsync(IEnumerable<TEntity> entitys, CancellationToken token = default);

        /// <summary>
        /// 非同期処理でエンティティ更新を実行します。
        /// </summary>
        /// <param name="entity"> 更新処理をするエンティティを指定します。 </param>
        /// <param name="token"> (任意) 非同期処理のキャンセルトークンを指定します。 </param>
        /// <returns> 処理した件数を返します。 </returns>
        Task<int> UpdateAsync(TEntity entity, CancellationToken token = default);

        /// <summary>
        /// 非同期処理で複数のエンティティ物理削除を実行します。
        /// </summary>
        /// <param name="entitys"> 削除するエンティティコレクションを指定します。 </param>
        /// <param name="token"> (任意) 非同期処理のキャンセルトークンを指定します。 </param>
        /// <returns> 削除をデータベースに反映した件数を返します。 </returns>
        Task<int> DeleteAsync(IEnumerable<TEntity> entitys, CancellationToken token = default);

        /// <summary>
        /// 非同期処理でエンティティ物理削除を実行します。
        /// </summary>
        /// <param name="entity"> 削除するエンティティを指定します。 </param>
        /// <param name="token"> (任意) 非同期処理のキャンセルトークンを指定します。 </param>
        /// <returns> 削除をデータベースに反映した件数を返します。 </returns>
        Task<int> DeleteAsync(TEntity entity, CancellationToken token = default);
    }

    /// <summary>
    /// リポジトリ
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    public interface IRepository
    {
        /*--- Property/Field Definitions ----------------------------------------------------------------------------------------------------------*/

        /*--- Method: public ----------------------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// 非同期処理で複数のエンティティ登録を実行します。
        /// </summary>
        /// <typeparam name="TEntity"> エンティティの型を指定します。 </typeparam>
        /// <param name="entitys"> 登録処理をするエンティティコレクションを指定します。 </param>
        /// <param name="token"> (任意) 非同期処理のキャンセルトークンを指定します。 </param>
        /// <returns> 処理した件数を返します。 </returns>
        Task<int> InsertAsync<TEntity>(IEnumerable<TEntity> entitys, CancellationToken token = default) 
            where TEntity : class;

        /// <summary>
        /// 非同期処理でエンティティ登録を実行します。
        /// </summary>
        /// <typeparam name="TEntity"> エンティティの型を指定します。 </typeparam>
        /// <param name="entity"> 登録処理をするエンティティを指定します。 </param>
        /// <param name="token"> (任意) 非同期処理のキャンセルトークンを指定します。 </param>
        /// <returns> 処理した件数を返します。 </returns>
        Task<int> InsertAsync<TEntity>(TEntity entity, CancellationToken token = default) 
            where TEntity : class;

        /// <summary>
        /// 非同期処理で複数のエンティティ更新を実行します。
        /// </summary>
        /// <typeparam name="TEntity"> エンティティの型を指定します。 </typeparam>
        /// <param name="entitys"> 更新処理をするエンティティコレクションを指定します。 </param>
        /// <param name="token"> (任意) 非同期処理のキャンセルトークンを指定します。 </param>
        /// <returns> 処理した件数を返します。 </returns>
        Task<int> UpdateAsync<TEntity>(IEnumerable<TEntity> entitys, CancellationToken token = default)
            where TEntity : class;

        /// <summary>
        /// 非同期処理でエンティティ更新を実行します。
        /// </summary>
        /// <typeparam name="TEntity"> エンティティの型を指定します。 </typeparam>
        /// <param name="entity"> 更新処理をするエンティティを指定します。 </param>
        /// <param name="token"> (任意) 非同期処理のキャンセルトークンを指定します。 </param>
        /// <returns> 処理した件数を返します。 </returns>
        Task<int> UpdateAsync<TEntity>(TEntity entity, CancellationToken token = default)
            where TEntity : class;

        /// <summary>
        /// 非同期処理で複数のエンティティ物理削除を実行します。
        /// </summary>
        /// <typeparam name="TEntity"> 削除するエンティティの型を指定します。 </typeparam>
        /// <param name="entitys"> 削除するエンティティコレクションを指定します。 </param>
        /// <param name="token"> (任意) 非同期処理のキャンセルトークンを指定します。 </param>
        /// <returns> 削除をデータベースに反映した件数を返します。 </returns>
        Task<int> DeleteAsync<TEntity>(IEnumerable<TEntity> entitys, CancellationToken token = default)
            where TEntity : class;

        /// <summary>
        /// 非同期処理でエンティティ物理削除を実行します。
        /// </summary>
        /// <typeparam name="TEntity"> 削除するエンティティの型を指定します。 </typeparam>
        /// <param name="entity"> 削除するエンティティを指定します。 </param>
        /// <param name="token"> (任意) 非同期処理のキャンセルトークンを指定します。 </param>
        /// <returns> 削除をデータベースに反映した件数を返します。 </returns>
        Task<int> DeleteAsync<TEntity>(TEntity entity, CancellationToken token = default)
            where TEntity : class;
    }
}
