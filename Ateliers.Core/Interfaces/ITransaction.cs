using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Ateliers
{
    /// <summary>
    /// トランザクション
    /// </summary>
    /// <remarks>
    /// <para> 概要: トランザクションの情報保持や操作の実装がされていることを示す。 </para>
    /// <para> プロパティで現在の処理状況を確認でき、コミットやロールバックの処理を実行することができる。 </para>
    /// </remarks>
    public interface ITransaction : IDisposable
    {
        /*--- Property/Field Definitions ----------------------------------------------------------------------------------------------------------*/

        Guid TransactionId { get; }

        IsolationLevel IsolationLevel { get; }

        TransactionStatus TransactionStatus { get; }

        /*--- Method: public ----------------------------------------------------------------------------------------------------------------------*/

        void Commit();

        void Rollback();

    }
}
