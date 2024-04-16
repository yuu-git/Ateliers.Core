using Ateliers.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Ateliers.Core.DomainLayer.Entities
{
    /// <summary>
    /// スレッドセーフエンティティの基底クラス
    /// </summary>
    /// <typeparam name="T"> エンティティの型を指定します。 </typeparam> 
    /// <remarks>
    /// 概要：マルチスレッド環境でのエンティティ操作をサポートするための基底クラスです。<br/>
    /// プロパティセット時のロック処理を行い、スレッドセーフを実現します。<br/>
    /// 一方で、lock による排他制御は、パフォーマンスの低下を招くため、ロックが不要であれば <see cref="EntityBase{T}"/> を使用してください。
    /// </remarks>
    public abstract class LockEntityBase<T> : EntityBase<T>
        where T : class
    {
        /*--- Field Definitions -------------------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// スレッドセーフ用 ロックオブジェクト
        /// </summary>
        private readonly object _lock = new object();

        /*--- * structers -------------------------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// スレッドセーフエンティティ基底 / コンストラクタ
        /// </summary>
        /// <remarks>
        /// 注意：このコンストラクタは、作成情報を初期設定しない場合に使用します。<br/>
        /// 忘れずに <see cref="SetCreateInfo(IOperator, DateTime)"/> または <see cref="SetCreateInfo(string, string, DateTime)"/> を呼び出してください。 <br/>
        /// 作成者情報を初期設定する場合は、他のコンストラクタを使用してください。
        /// </remarks>
        public LockEntityBase()
            : base()
        {
        }

        /// <summary>
        /// スレッドセーフエンティティ基底 / コンストラクタ
        /// </summary>
        /// <param name="entityOperator"> エンティティ操作者を指定します。 </param>
        /// <param name="createDateTime"> 作成日時を指定します。 null の場合は現在日時が設定されます。 </param>
        public LockEntityBase(IOperator entityOperator, DateTime? createDateTime = null)
            : base(entityOperator.OperationProgram, entityOperator.OperationUser, createDateTime)
        {
        }

        /// <summary>
        /// スレッドセーフエンティティ基底 / コンストラクタ
        /// </summary>
        /// <param name="createProgramId"> 作成プログラムIDを指定します。 </param>
        /// <param name="createUserId"> 作成ユーザーIDを指定します。 </param>
        /// <param name="createDateTime"> 作成日時を指定します。 null の場合は現在日時が設定されます。 </param>
        public LockEntityBase(string createProgramId, string createUserId, DateTime? createDateTime = null)
            : base(createProgramId, createUserId, createDateTime)
        {
        }

        /*--- Property/Field Definitions ----------------------------------------------------------------------------------------------------------*/
        /*--- Method: public ----------------------------------------------------------------------------------------------------------------------*/
        /*--- Method: internal --------------------------------------------------------------------------------------------------------------------*/
        /*--- Method: protected -------------------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// プロパティの値を設定し、プロパティが変更された場合には IsDirty フラグを true に設定します。
        /// </summary>
        /// <typeparam name="TFieldType"> プロパティの型を指定します。 </typeparam>
        /// <param name="field"> 設定するプロパティの参照を指定します。 </param>
        /// <param name="value"> プロパティに設定する新しい値を指定します。 </param>

        protected override void SetProperty<TFieldType>(ref TFieldType field, TFieldType value)
        {
            if (!EqualityComparer<TFieldType>.Default.Equals(field, value))
            {
                lock (_lock)
                {
                    field = value;
                    IsDirty = true;
                }
            }
        }

        /*--- Method: private ---------------------------------------------------------------------------------------------------------------------*/


    }
}
