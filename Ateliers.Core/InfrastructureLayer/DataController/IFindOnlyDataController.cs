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

        /// <summary>
        /// 全てのエンティティを非同期で検索し、コレクションを取得します。
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="cancelToken"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(CancellationToken cancelToken = default)
            where TEntity : class;

        /// <summary>
        /// 条件を指定してエンティティを非同期で検索し、コレクションを取得します。
        /// </summary>
        /// <typeparam name="TEntity"> 取得する型を指定します。 </typeparam>
        /// <param name="findExpression"> 取得条件を指定します。 </param>
        /// <param name="token"> 非同期処理キャンセルトークンを指定します。 </param>
        /// <returns> <typeparamref name="TEntity"/> で指定したコレクションを取得します。 </returns>
        Task<IEnumerable<TEntity>> FindAsync<TEntity>(Expression<Func<TEntity, bool>> findExpression, CancellationToken token = default)
            where TEntity : class;
    }
}
