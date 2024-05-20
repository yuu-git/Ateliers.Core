using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ateliers
{
    /// <summary>
    /// [IF] データベースコントローラ
    /// </summary>
    public interface IDatabaseController : IDataController
    {
        /*--- Property/Field Definitions ----------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// トランザクションが実行中であるかを示す値を取得します。
        /// </summary>
        bool IsBeginTransaction { get; }

        /*--- Method: public ----------------------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// トランザクションを開始します。
        /// </summary>
        /// <param name="isolationLevel"> トランザクションのロックレベルを指定します。 </param>
        /// <returns> トランザクションをコントロールできるインタフェースを返します。 </returns>
        ITransaction BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Serializable);

        /// <summary>
        /// 非同期処理でデータベースに対してSQLを直接実行します。
        /// </summary>
        /// <param name="executeSql"> 実行するSQLを指定します。 </param>
        /// <param name="sqlParameters"> SQLで使用するパラメータオブジェクトを指定します。 </param>
        /// <param name="cancellationToken"> 非同期処理キャンセルトークンを指定します。 </param>
        /// <returns> SQLの実行結果を返します。 </returns>
        Task<int> ExecuteSqlAsync(string executeSql, IEnumerable<object> sqlParameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// 非同期処理でデータベースに対してSQLを直接実行します。
        /// </summary>
        /// <param name="executeSql"> 実行するSQLを指定します。 </param>
        /// <param name="cancellationToken"> 非同期処理キャンセルトークンを指定します。 </param>
        /// <returns> SQLの実行結果を返します。 </returns>
        Task<int> ExecuteSqlAsync(string executeSql, CancellationToken cancellationToken = default);
    }
}
