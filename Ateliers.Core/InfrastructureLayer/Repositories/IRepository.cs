using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Ateliers
{
    /// <summary>
    /// リポジトリ
    /// </summary>
    /// </remarks>
    public interface IRepository
    {
        /*--- Property/Field Definitions ----------------------------------------------------------------------------------------------------------*/

        /*--- Method: public ----------------------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// 全てのエンティティを非同期で取得します。
        /// </summary>
        /// <typeparam name="TEntity"> 取得するエンティティの型を指定します。 </typeparam>
        /// <param name="token"> (任意) 非同期処理のキャンセルトークンを指定します。 </param>
        /// <returns> 取得したエンティティコレクションを返します。 </returns>
        Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(CancellationToken token = default)
            where TEntity : class;

        /// <summary>
        /// 条件を指定してエンティティを非同期で条件検索し、条件に一致する1件のエンティティを取得します。
        /// </summary>
        /// <typeparam name="TEntity"> 取得するエンティティの型を指定します。 </typeparam>
        /// <param name="findExpression"> エンティティの検索条件を指定します。 </param>
        /// <param name="token"> (任意) 非同期処理のキャンセルトークンを指定します。 </param>
        /// <returns> 検索結果となるエンティティを返します。 </returns>
        Task<TEntity> FindByExpressionAsync<TEntity>(Expression<Func<TEntity, bool>> findExpression, CancellationToken token = default)
            where TEntity : class;

        /// <summary>
        /// 条件を指定してエンティティを非同期で条件検索し、条件に一致するエンティティコレクションを取得します。
        /// </summary>
        /// <typeparam name="TEntity"> 取得するエンティティの型を指定します。 </typeparam>
        /// <param name="searchExpression"> エンティティの検索条件を指定します。 </param>
        /// <param name="token"> (任意) 非同期処理のキャンセルトークンを指定します。 </param>
        /// <returns> 検索結果となるエンティティコレクションを返します。 </returns>
        Task<IEnumerable<TEntity>> SearchByExpression<TEntity>(Expression<Func<TEntity, bool>> searchExpression, CancellationToken token = default)
            where TEntity : class;

        /// <summary>
        /// 非同期処理で複数のエンティティ登録を実行します。
        /// </summary>
        /// <typeparam name="TEntity"> エンティティの型を指定します。 </typeparam>
        /// <param name="entitys"> 登録処理をするエンティティコレクションを指定します。 </param>
        /// <param name="token"> (任意) 非同期処理のキャンセルトークンを指定します。 </param>
        /// <returns> 処理した件数を返します。 </returns>
        Task<int> InsertAsync<TEntity>(IEnumerable<TEntity> entitys, CancellationToken token = default) 
            where TEntity : class, IObjectKey;

        /// <summary>
        /// 非同期処理でエンティティ登録を実行します。
        /// </summary>
        /// <typeparam name="TEntity"> エンティティの型を指定します。 </typeparam>
        /// <param name="entity"> 登録処理をするエンティティを指定します。 </param>
        /// <param name="token"> (任意) 非同期処理のキャンセルトークンを指定します。 </param>
        /// <returns> 処理した件数を返します。 </returns>
        Task<int> InsertAsync<TEntity>(TEntity entity, CancellationToken token = default) 
            where TEntity : class, IObjectKey;

        /// <summary>
        /// 非同期処理で複数のエンティティ更新を実行します。
        /// </summary>
        /// <typeparam name="TEntity"> エンティティの型を指定します。 </typeparam>
        /// <param name="entitys"> 更新処理をするエンティティコレクションを指定します。 </param>
        /// <param name="token"> (任意) 非同期処理のキャンセルトークンを指定します。 </param>
        /// <returns> 処理した件数を返します。 </returns>
        Task<int> UpdateAsync<TEntity>(IEnumerable<TEntity> entitys, CancellationToken token = default)
            where TEntity : class, IObjectKey;

        /// <summary>
        /// 非同期処理でエンティティ更新を実行します。
        /// </summary>
        /// <typeparam name="TEntity"> エンティティの型を指定します。 </typeparam>
        /// <param name="entity"> 更新処理をするエンティティを指定します。 </param>
        /// <param name="token"> (任意) 非同期処理のキャンセルトークンを指定します。 </param>
        /// <returns> 処理した件数を返します。 </returns>
        Task<int> UpdateAsync<TEntity>(TEntity entity, CancellationToken token = default)
            where TEntity : class, IObjectKey;

        /// <summary>
        /// 非同期処理で複数のエンティティ物理削除を実行します。
        /// </summary>
        /// <typeparam name="TEntity"> 削除するエンティティの型を指定します。 </typeparam>
        /// <param name="entitys"> 削除するエンティティコレクションを指定します。 </param>
        /// <param name="token"> (任意) 非同期処理のキャンセルトークンを指定します。 </param>
        /// <returns> 削除をデータベースに反映した件数を返します。 </returns>
        Task<int> DeleteAsync<TEntity>(IEnumerable<TEntity> entitys, CancellationToken token = default)
            where TEntity : class, IObjectKey;

        /// <summary>
        /// 非同期処理でエンティティ物理削除を実行します。
        /// </summary>
        /// <typeparam name="TEntity"> 削除するエンティティの型を指定します。 </typeparam>
        /// <param name="entity"> 削除するエンティティを指定します。 </param>
        /// <param name="token"> (任意) 非同期処理のキャンセルトークンを指定します。 </param>
        /// <returns> 削除をデータベースに反映した件数を返します。 </returns>
        Task<int> DeleteAsync<TEntity>(TEntity entity, CancellationToken token = default)
            where TEntity : class, IObjectKey;
    }
}
