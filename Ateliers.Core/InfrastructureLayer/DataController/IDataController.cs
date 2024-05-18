using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ateliers
{
    /// <summary>
    /// [IF] データコントローラ
    /// </summary>
    public interface IDataController : IFindOnlyDataController
    {
        /*--- Property/Field Definitions ----------------------------------------------------------------------------------------------------------*/

        /*--- Method: public ----------------------------------------------------------------------------------------------------------------------*/

        #region --- Insert ---

        /// <summary>
        /// エンティティコレクションの追加を非同期で実行します。
        /// </summary>
        /// <typeparam name="TEntity"> エンティティの型を指定します。 </typeparam>
        /// <param name="addEntitys"> 追加するエンティティコレクションを指定します。 </param>
        /// <param name="token"> 非同期処理キャンセルトークンを指定します。 </param>
        /// <returns> データの操作結果を返します。 </returns>
        Task<int> InsertEntitysAsync<TEntity>(IEnumerable<TEntity> addEntitys, CancellationToken token = default)
            where TEntity : class;

        /// <summary>
        /// エンティティの追加を非同期で実行します。
        /// </summary>
        /// <typeparam name="TEntity"> エンティティの型を指定します。 </typeparam>
        /// <param name="addEntity"> 追加するエンティティを指定します。 </param>
        /// <param name="token"> 非同期処理キャンセルトークンを指定します。 </param>
        /// <returns> データの操作結果を返します。 </returns>
        Task<int> InsertEntityAsync<TEntity>(TEntity addEntity, CancellationToken token = default)
            where TEntity : class;

        #endregion

        #region --- Update ---

        /// <summary>
        /// エンティティコレクションの更新を非同期で実行します。
        /// </summary>
        /// <typeparam name="TEntity"> 更新するエンティティの型を指定します。 </typeparam>
        /// <param name="updateEntitys"> 更新するエンティティコレクションを指定します。 </param>
        /// <param name="token"> 非同期処理キャンセルトークンを指定します。 </param>
        /// <returns> データの操作結果を返します。 </returns>
        Task<int> UpdateEntitysAsync<TEntity>(IEnumerable<TEntity> updateEntitys, CancellationToken token = default)
            where TEntity : class;

        /// <summary>
        /// エンティティの更新を非同期で実行します。
        /// </summary>
        /// <typeparam name="TEntity"> 更新するエンティティの型を指定します。 </typeparam>
        /// <param name="updateEntity"> 更新するエンティティを指定します。 </param>
        /// <param name="token"> 非同期処理キャンセルトークンを指定します。 </param>
        /// <returns> データの操作結果を返します。 </returns>
        Task<int> UpdateEntityAsync<TEntity>(TEntity updateEntity, CancellationToken token = default)
            where TEntity : class;

        #endregion

        #region --- Delete ---

        /// <summary>
        /// エンティティコレクションの物理削除を非同期で実行します。
        /// </summary>
        /// <typeparam name="TEntity"> 削除するエンティティの型を指定します。 </typeparam>
        /// <param name="deleteEntitys"> 削除するエンティティコレクションを指定します。 </param>
        /// <param name="token"> 非同期処理キャンセルトークンを指定します。 </param>
        /// <returns> データの操作結果を返します。 </returns>
        Task<int> DeleteEntitysAsync<TEntity>(IEnumerable<TEntity> deleteEntitys, CancellationToken token = default)
            where TEntity : class;

        /// <summary>
        /// エンティティの物理削除を非同期で実行します。
        /// </summary>
        /// <typeparam name="TEntity"> 削除するエンティティの型を指定します。 </typeparam>
        /// <param name="deleteEntity"> 削除するエンティティを指定します。 </param>
        /// <param name="token"> 非同期処理キャンセルトークンを指定します。 </param>
        /// <returns> データの操作結果を返します。 </returns>
        Task<int> DeleteEntityAsync<TEntity>(TEntity deleteEntity, CancellationToken token = default)
            where TEntity : class;

        /// <summary>
        /// 条件を指定してエンティティの削除を非同期で実行します。
        /// </summary>
        /// <typeparam name="TEntity"> 削除するエンティティの型を指定します。 </typeparam>
        /// <param name="deleteExpression"> 削除するエンティティの条件を指定します。 </param>
        /// <param name="token"> 非同期処理キャンセルトークンを指定します。 </param>
        /// <returns> データの操作結果を返します。 </returns>
        Task<int> DeleteEntityExpressionAsync<TEntity>(Expression<Func<TEntity, bool>> deleteExpression, CancellationToken token = default)
            where TEntity : class;

        #endregion
    }
}
