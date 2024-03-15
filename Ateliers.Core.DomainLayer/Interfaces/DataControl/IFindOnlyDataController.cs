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
    /// 検索専用 データコントローラ
    /// </summary>
    public interface IFindOnlyDataController
    {
        /*--- Property/Field Definitions ----------------------------------------------------------------------------------------------------------*/

        /*--- Method: public ----------------------------------------------------------------------------------------------------------------------*/

        Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(CancellationToken cancelToken = default)
            where TEntity : class;

        IEnumerable<TEntity> GetAll<TEntity>()
            where TEntity : class;

        /// <summary>
        /// 条件を指定してエンティティを非同期で検索し、コレクションを返却します。
        /// </summary>
        /// <typeparam name="TEntity"> 取得する型を指定します。 </typeparam>
        /// <param name="pFindExpression"> 取得条件を指定します。 </param>
        /// <param name="pCancellationToken"> 非同期処理キャンセルトークンを指定します。 </param>
        /// <returns> <typeparamref name="TEntity"/> で指定したコレクションを取得します。 </returns>
        Task<IEnumerable<TEntity>> FindAsync<TEntity>(Expression<Func<TEntity, bool>> pFindExpression, CancellationToken pCancellationToken = default)
            where TEntity : class;

        /// <summary>
        /// 条件を指定してエンティティを検索し、コレクションを返却します。
        /// </summary>
        /// <typeparam name="TEntity"> 取得する型を指定します。 </typeparam>
        /// <param name="pFindExpression"> 取得条件を指定します。 </param>
        /// <returns> <typeparamref name="TEntity"/> で指定したコレクションを取得します。 </returns>
        IEnumerable<TEntity> Find<TEntity>(Expression<Func<TEntity, bool>> pFindExpression)
            where TEntity : class;

        /// <summary>
        /// キーオブジェクトを指定してエンティティを非同期で検索し、エンティティを返します。
        /// </summary>
        /// <typeparam name="TEntity"> 取得する型を指定します。 </typeparam>
        /// <param name="keyValues"> キーとなるオブジェクトを指定します。 </param>
        /// <param name="cancellationToken"> 非同期処理キャンセルトークンを指定します。 </param>
        /// <returns> 一致するエンティティを返します。 </returns>
        Task<TEntity> FindByKeyObjectAsync<TEntity>(IEnumerable<object> keyObjects, CancellationToken cancellationToken = default)
            where TEntity : class;

        /// <summary>
        /// キーオブジェクトを指定してエンティティを検索し、エンティティを返します。
        /// </summary>
        /// <typeparam name="TEntity"> 取得する型を指定します。 </typeparam>
        /// <param name="keyValues"> キーとなるオブジェクトを指定します。 </param>
        /// <returns> 一致するエンティティを返します。 </returns>
        TEntity FindByKeyObject<TEntity>(params object[] keyValues)
            where TEntity : class;
    }
}
