using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ateliers
{
    /// <summary>
    /// [IF] DbContex
    /// </summary>
    public interface IEntityFrameworkContext : IDatabaseController, IDisposable
    {
        /*--- Property/Field Definitions ----------------------------------------------------------------------------------------------------------*/

        /*--- Method: public ----------------------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// <typeparamref name="TEntity"/> で指定された DbSet{TEntity} に対して DbContext.Set{TEntity} を実行します。
        /// </summary>
        /// <typeparam name="TEntity"> DbContext.Set{TEntity} の対象となる型を指定します。 </typeparam>
        /// <returns> DbContext.Set{TEntity} の結果となるエンティティコレクションを返します。 </returns>
        IEnumerable<TEntity> SetEntity<TEntity>()
            where TEntity : class;

        /// <summary>
        /// このコンテキストに <typeparamref name="TEntity"/> の DbSet{TEntity} が実装されているかを確認します。
        /// </summary>
        /// <typeparam name="TEntity"> 確認する型を指定します。 </typeparam>
        /// <returns> 実装されている場合は true, 実装されていない場合は false を返します。 </returns>
        bool CheckDbSet<TEntity>()
            where TEntity : class;

        bool EnsureCreated();

        Task<bool> EnsureCreatedAsync(CancellationToken token = default);
    }
}
