using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ateliers
{
    /// <summary>
    /// EntityFramework リポジトリ
    /// </summary>
    public interface IEntityFrameworkRepository : IRepository
    {
        /*--- Property/Field Definitions ----------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// データベース接続に利用するインタフェース生成デリゲードを取得します。
        /// </summary>
        Func<IEntityFrameworkContext> CreateDbContext { get; }

        /*--- Method: public ----------------------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// 条件を指定してエンティティの論理削除を非同期で実行します。
        /// </summary>
        /// <param name="deleteExpression"> 削除するエンティティの条件を指定します。 </param>
        /// <param name="deleteProgramId"> 削除プログラムの識別子を指定します。 </param>
        /// <param name="deleteUserId"> 削除ユーザーの識別子を指定します。 </param>
        /// <param name="token"> (任意) 非同期処理キャンセルトークンを指定します。 </param>
        /// <returns> 削除をデータベースに反映した件数を返します。 </returns>
        Task<int> LogicalDeleteAsync<TEntity>(Expression<Func<TEntity, bool>> deleteExpression, string deleteProgramId, string deleteUserId, CancellationToken token = default)
            where TEntity : class, IDeleteInfo;
    }
}
