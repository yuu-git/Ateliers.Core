using System;
using System.Collections.Generic;
using System.Text;

namespace Ateliers
{
    public interface IDbContextEntity<TEntity> : IEntity<TEntity>
        where TEntity : class
    {
        /*--- Property/Field Definitions ----------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// このエンティティで利用しているリポジトリを取得します。
        /// </summary>
        IDbContextRepository<TEntity> DbContextRepository { get; }

        /*--- Method: public ----------------------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// このエンティティのリポジトリを設定し、リポジトリを設定したエンティティを返却します。
        /// </summary>
        /// <param name="repository"> 設定するリポジトリを指定します。 </param>
        /// <returns> リポジトリを設定したエンティティを返します。 </returns>
        TEntity SetDatabaseRepository(IDbContextRepository<TEntity> repository);
    }
}
