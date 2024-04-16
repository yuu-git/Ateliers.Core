using Ateliers;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ateliers
{
    /// <summary>
    /// [IF] データベース型リポジトリ
    /// </summary>
    public interface IDbContextRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        /*--- Property/Field Definitions ----------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// データベース接続に利用するインタフェース生成デリゲードを取得します。
        /// </summary>
        Func<IEntityFrameworkContext> CreateDbContext { get; }

        /// <summary>
        /// 条件を指定してエンティティを非同期で検索し、検索結果を返却します。
        /// </summary>
        /// <param name="findExpression"> エンティティの検索条件を指定します。 </param>
        /// <param name="token"> 非同期処理キャンセルトークンを指定します。 </param>
        /// <returns> 検索結果となるエンティティコレクションを返します。 </returns>
        Task<IEnumerable<TEntity>> FindExpressionAsync(Expression<Func<TEntity, bool>> findExpression, CancellationToken token = default);

        /// <summary>
        /// キーオブジェクトを指定してエンティティを非同期で検索し、検索結果を返します。
        /// </summary>
        /// <typeparam name="TEntity"> 取得する型を指定します。 </typeparam>
        /// <param name="keyValues"> キーとなるオブジェクトを指定します。 </param>
        /// <param name="token"> 非同期処理キャンセルトークンを指定します。 </param>
        /// <returns> 一致するエンティティを返します。 </returns>
        Task<TEntity> FindByKeyObjectAsync(IEnumerable<object> keyObjects, CancellationToken token = default);

        /// <summary>
        /// 条件を指定してエンティティの物理削除を非同期で実行します。
        /// </summary>
        /// <param name="pDeleteExpression"> 削除するエンティティの条件を指定します。 </param>
        /// <param name="token"> 非同期処理キャンセルトークンを指定します。 </param>
        /// <returns> 削除をデータベースに反映した件数を返します。 </returns>
        Task<int> DeleteAsync(Expression<Func<TEntity, bool>> pDeleteExpression, CancellationToken token = default);
        
        /*--- Method: public ----------------------------------------------------------------------------------------------------------------------*/

    }
    /// <summary>
    /// [IF] データベース型リポジトリ
    /// </summary>
    public interface IDbContextRepository : IRepository
    {
        /*--- Property/Field Definitions ----------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// データベース接続インタフェースを取得します。
        /// </summary>
        IEntityFrameworkContext DbContext { get; }

        /// <summary>
        /// 条件を指定してエンティティを非同期で検索し、検索結果を返却します。
        /// </summary>
        /// <typeparam name="TEntity"> 検索するエンティティの型を指定します。 </typeparam>
        /// <param name="findExpression"> エンティティの検索条件を指定します。 </param>
        /// <param name="token"> 非同期処理キャンセルトークンを指定します。 </param>
        /// <returns> 検索結果となるエンティティコレクションを返します。 </returns>
        Task<IEnumerable<TEntity>> FindExpressionAsync<TEntity>(Expression<Func<TEntity, bool>> findExpression, CancellationToken token = default)
            where TEntity : class;

        /// <summary>
        /// キーオブジェクトを指定してエンティティを非同期で検索し、検索結果を返します。
        /// </summary>
        /// <typeparam name="TEntity"> 取得する型を指定します。 </typeparam>
        /// <param name="keyObjects"> キーとなるオブジェクトを指定します。 </param>
        /// <param name="token"> 非同期処理キャンセルトークンを指定します。 </param>
        /// <returns> 一致するエンティティを返します。 </returns>
        Task<TEntity> FindByKeyObjectAsync<TEntity>(IEnumerable<object> keyObjects, CancellationToken token = default)
            where TEntity : class;

        /// <summary>
        /// 条件を指定してエンティティの物理削除を非同期で実行します。
        /// </summary>
        /// <typeparam name="TEntity"> 削除するエンティティの型を指定します。 </typeparam>
        /// <param name="pDeleteExpression"> 削除するエンティティの条件を指定します。 </param>
        /// <param name="token"> 非同期処理キャンセルトークンを指定します。 </param>
        /// <returns> 削除をデータベースに反映した件数を返します。 </returns>
        Task<int> DeleteExpressionAsync<TEntity>(Expression<Func<TEntity, bool>> pDeleteExpression, CancellationToken token = default)
            where TEntity : class;

        /*--- Method: public ----------------------------------------------------------------------------------------------------------------------*/
    
    }

}
