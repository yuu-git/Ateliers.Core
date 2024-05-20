using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ateliers
{
    public interface IFileRepository : IRepository
    {
        /*--- Property/Field Definitions ----------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// ファイルが格納されているデータディレクトリ情報を取得します。
        /// </summary>
        DirectoryInfo DataDictionary { get; }

        /*--- Method: public ----------------------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// ファイル名を指定してエンティティを非同期で取得します。
        /// </summary>
        /// <typeparam name="TEntity"> 取得するエンティティの型を指定します。 </typeparam>
        /// <param name="fileName"> 取得するエンティティのファイル名を指定します。 </param>
        /// <param name="token"> 未使用、指定不要 </param>
        /// <returns> 検索結果となるエンティティを返します。 </returns>
        Task<TEntity> GetByFileNameAsync<TEntity>(string fileName, CancellationToken token = default)
            where TEntity : class;

        /// <summary>
        /// ファイル名を指定してエンティティを非同期で取得します。
        /// </summary>
        /// <typeparam name="TEntity"> 取得するエンティティの型を指定します。 </typeparam>
        /// <param name="fileName"> 取得するエンティティのファイル名を指定します。 </param>
        /// <param name="token"> (任意) 非同期処理のキャンセルトークンを指定します。 </param>
        /// <returns> 取得したエンティティを返します。 </returns>
        Task<TEntity> FindByFileNameAsync<TEntity>(string fileName, CancellationToken token = default) 
            where TEntity : class;
    }
}
