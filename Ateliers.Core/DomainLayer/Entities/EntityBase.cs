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
    /// エンティティの基底クラス
    /// </summary>
    /// <typeparam name="T"> エンティティの型を指定します。 </typeparam> 
    public abstract class EntityBase<T> : ICreateInfo, IUpdateInfo, IDeleteInfo
        where T : class
    {
        /*--- * structers -------------------------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// エンティティ基底 / コンストラクタ
        /// </summary>
        /// <remarks>
        /// 注意：このコンストラクタは、作成情報を初期設定しない場合に使用します。<br/>
        /// 忘れずに <see cref="SetCreateInfo(IOperator, DateTime)"/> または <see cref="SetCreateInfo(string, string, DateTime)"/> を呼び出してください。 <br/>
        /// 作成者情報を初期設定する場合は、他のコンストラクタを使用してください。
        /// </remarks>
        public EntityBase()
        {
        }

        /// <summary>
        /// エンティティ基底 / コンストラクタ
        /// </summary>
        /// <param name="entityOperator"> エンティティ操作者を指定します。 </param>
        /// <param name="createDateTime"> 作成日時を指定します。 null の場合は現在日時が設定されます。 </param>
        public EntityBase(IOperator entityOperator, DateTime? createDateTime = null)
            : this(entityOperator.OperationProgram, entityOperator.OperationUser, createDateTime)
        {
        }

        /// <summary>
        /// エンティティ基底 / コンストラクタ
        /// </summary>
        /// <param name="createProgramId"> 作成プログラムIDを指定します。 </param>
        /// <param name="createUserId"> 作成ユーザーIDを指定します。 </param>
        /// <param name="createDateTime"> 作成日時を指定します。 null の場合は現在日時が設定されます。 </param>
        public EntityBase(string createProgramId, string createUserId, DateTime? createDateTime = null)
        {
            SetCreateInfo(createProgramId, createUserId, createDateTime ?? DateTime.Now);
        }

        /*--- Property/Field Definitions ----------------------------------------------------------------------------------------------------------*/

        /// <inheritdoc/>
        [Required]
        [MaxLength(30)]
        public string CreateProgramId
        {
            get => _CreateProgramId;
            set => SetProperty(ref _CreateProgramId, value);
        } 

        /// <inheritdoc/>
        [Required]
        [MaxLength(30)]
        public string CreateUserId
        {
            get => _CreateUserId;
            set => SetProperty(ref _CreateUserId, value);
        }

        /// <inheritdoc/>
        [Required]
        public DateTime? CreateDateTime
        {
            get => _CreateDateTime;
            set => SetProperty(ref _CreateDateTime, value);
        }

        /// <inheritdoc/>
        [MaxLength(30)]
        public string UpdateProgramId
        {
            get => _UpdateProgramId;
            set => SetProperty(ref _UpdateProgramId, value);
        }

        /// <inheritdoc/>
        [MaxLength(30)]
        public string UpdateUserId
        {
            get => _UpdateUserId;
            set => SetProperty(ref _UpdateUserId, value);
        }

        /// <inheritdoc/>
        public DateTime? UpdateDateTime
        {
            get => _UpdateDateTime;
            set => SetProperty(ref _UpdateDateTime, value);
        }

        /// <inheritdoc/>
        [MaxLength(30)]
        public string DeleteProgramId 
        {
            get => _DeleteProgramId;
            set => SetProperty(ref _DeleteProgramId, value);
        }

        /// <inheritdoc/>
        [MaxLength(30)]
        public string DeleteUserId 
        { 
            get => _DeleteUserId;
            set => SetProperty(ref _DeleteUserId, value);
        }

        /// <inheritdoc/>
        public DateTime? DeleteDateTime 
        { 
            get => _DeleteDateTime;
            set => SetProperty(ref _DeleteDateTime, value);
        }

        /// <inheritdoc/>
        public bool IsDeleted 
        {
            get => _IsDeleted;
            set => SetProperty(ref _IsDeleted, value);
        }

        /// <summary>
        /// このエンティティが変更されたかどうかを取得します。
        /// </summary>
        /// <remarks>
        /// エンティティが変更された場合は true、それ以外は false。
        /// </remarks>
        protected bool IsDirty { get; set; }

        #region --- プロパティの実体フィールド (コメント省略) ---------------

        private string _CreateProgramId;
        private string _CreateUserId;
        private DateTime? _CreateDateTime;
        private string _UpdateProgramId;
        private string _UpdateUserId;
        private DateTime? _UpdateDateTime;
        private string _DeleteProgramId;
        private string _DeleteUserId;
        private DateTime? _DeleteDateTime;
        private bool _IsDeleted;

        #endregion

        /*--- Method: public ----------------------------------------------------------------------------------------------------------------------*/


        /// <summary>
        /// エンティティの変更フラグをリセットします。
        /// </summary>
        public void ResetDirtyFlag() => IsDirty = false;

        /// <summary>
        /// エンティティが一時的なものかどうかを判断します。
        /// </summary>
        /// <returns>エンティティが一時的なものの場合は true、それ以外は false。</returns>
        /// <remarks>
        /// 一時的とは、一意のキー情報が設定されていない、または一意のキー情報がデフォルト値の場合を指します。<br/>
        /// </remarks>
        public abstract bool IsTransient();

        /// <summary>
        /// このエンティティの追加処理に対するバリデーションを実行します。
        /// </summary>
        /// <returns> 検証結果リストを返します。 </returns>
        /// <remarks>
        /// 抽象の実装内容: バリデーションエラーが発生した場合は、エラー情報を <see cref="IValidationDetail"/> インスタンスとして返します。<br/>
        /// 推奨する実装は、プロパティなどに異常があっても <see cref="Exception"/> で異常終了させない事とし、戻り値を受け取った側で異常終了させるか判断して下さい。
        /// </remarks>
        public abstract IEnumerable<IValidationDetail> CreateValidation();

        /// <summary>
        /// このエンティティの更新処理に対するバリデーションを実行します。
        /// </summary>
        /// <returns> 検証結果リストを返します。 </returns>
        /// <remarks>
        /// 抽象の実装内容: バリデーションエラーが発生した場合は、エラー情報を <see cref="IValidationDetail"/> インスタンスとして返します。<br/>
        /// 推奨する実装は、プロパティなどに異常があっても <see cref="Exception"/> で異常終了させない事とし、戻り値を受け取った側で異常終了させるか判断して下さい。
        /// </remarks>
        public abstract IEnumerable<IValidationDetail> UpdateValidation();

        /// <summary>
        /// このエンティティの削除処理に対するバリデーションを実行します。
        /// </summary>
        /// <returns>検証結果リストを返します。</returns>
        /// <remarks>
        /// 抽象の実装内容: バリデーションエラーが発生した場合は、エラー情報を <see cref="IValidationDetail"/> インスタンスとして返します。<br/>
        /// 推奨する実装は、プロパティなどに異常があっても <see cref="Exception"/> で異常終了させない事とし、戻り値を受け取った側で異常終了させるか判断して下さい。
        /// </remarks>
        public abstract IEnumerable<IValidationDetail> DeleteValidation();

        /// <inheritdoc/>
        public virtual void SetCreateInfo(IOperator entityOperator, DateTime createDateTime)
            => SetCreateInfo(entityOperator.OperationProgram, entityOperator.OperationUser, createDateTime);
        
        /// <exception cref="ArgumentNullException"> 指定されたプログラムIDまたはユーザーIDがnullまたは空白の場合にスローされます。 </exception>
        /// <exception cref="ArgumentException"> 指定された作成日時がデフォルト値の場合にスローされます。 </exception>
        /// <inheritdoc/>
        public virtual void SetCreateInfo(string createProgramId, string createUserId, DateTime createDateTime)
        {
            if (string.IsNullOrWhiteSpace(createProgramId))
                throw new ArgumentNullException(nameof(createProgramId));

            if (string.IsNullOrWhiteSpace(createUserId))
                throw new ArgumentNullException(nameof(createUserId));

            if (createDateTime == default)
                throw new ArgumentException(nameof(createDateTime));

            _CreateProgramId = createProgramId;
            _CreateUserId = createUserId;
            _CreateDateTime = createDateTime;
            IsDirty = true;
        }

        /// <inheritdoc/>
        public virtual void SetUpdateInfo(IOperator entityOperator, DateTime updateDateTime)
            => SetUpdateInfo(entityOperator.OperationProgram, entityOperator.OperationUser, updateDateTime);

        /// <exception cref="ArgumentNullException"> 指定されたプログラムIDまたはユーザーIDがnullまたは空白の場合にスローされます。 </exception>
        /// <exception cref="ArgumentException"> 指定された作成日時がデフォルト値の場合にスローされます。 </exception>
        /// <inheritdoc/>
        public virtual void SetUpdateInfo(string updateProgramId, string updateUserId, DateTime updateDateTime)
        {
            if (string.IsNullOrWhiteSpace(updateProgramId))
                throw new ArgumentNullException(nameof(updateProgramId));

            if (string.IsNullOrWhiteSpace(updateUserId))
                throw new ArgumentNullException(nameof(updateUserId));

            if (updateDateTime == default)
                throw new ArgumentException(nameof(updateDateTime));

            _UpdateProgramId = updateProgramId;
            _UpdateUserId = updateUserId;
            _UpdateDateTime = updateDateTime;
            IsDirty = true;
        }

        /// <inheritdoc/>
        public virtual void SetDeleteInfo(IOperator entityOperator, DateTime deleteDateTime)
            => SetDeleteInfo(entityOperator.OperationProgram, entityOperator.OperationUser, deleteDateTime);

        /// <exception cref="ArgumentNullException"> 指定されたプログラムIDまたはユーザーIDがnullまたは空白の場合にスローされます。 </exception>
        /// <exception cref="ArgumentException"> 指定された作成日時がデフォルト値の場合にスローされます。 </exception>
        /// <inheritdoc/>
        public virtual void SetDeleteInfo(string deleteProgramId, string deleteUserId, DateTime deleteDateTime)
        {
            if (string.IsNullOrWhiteSpace(deleteProgramId))
                throw new ArgumentNullException(nameof(deleteProgramId));

            if (string.IsNullOrWhiteSpace(deleteUserId))
                throw new ArgumentNullException(nameof(deleteUserId));

            if (deleteDateTime == default)
                throw new ArgumentException(nameof(deleteDateTime));

            _DeleteProgramId = deleteProgramId;
            _DeleteUserId = deleteUserId;
            _DeleteDateTime = deleteDateTime;
            _IsDeleted = true;
            IsDirty = true;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is T entity && EqualsCore(entity);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return GetHashCodeCore();
        }

        /// <summary>
        /// <see cref="ValueObject{T}"/> 同士の等価性比較演算を実行します。
        /// </summary>
        /// <param name="a"> 1つ目のオブジェクトを指定します。 </param>
        /// <param name="b"> 2つ目のオブジェクトを指定します。 </param>
        /// <returns> オブジェクトが同一である場合は true, それ以外は false を返します。 </returns>
        public static bool operator ==(EntityBase<T> a, EntityBase<T> b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        /// <summary>
        /// <see cref="ValueObject{T}"/> 同士の非等価性比較演算を実行します。
        /// </summary>
        /// <param name="a"> 1つ目のオブジェクトを指定します。 </param>
        /// <param name="b"> 2つ目のオブジェクトを指定します。 </param>
        /// <returns> オブジェクトが同一ではない場合は true, それ以外は false を返します。 </returns>
        public static bool operator !=(EntityBase<T> a, EntityBase<T> b)
        {
            return !(a == b);
        }

        /*--- Method: internal --------------------------------------------------------------------------------------------------------------------*/
        /*--- Method: protected -------------------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// 等価性比較を実行します。
        /// </summary>
        /// <param name="other"> 比較対象の <see cref="ValueObject{T}"/> を指定します。 </param>
        /// <returns> オブジェクトが同一である場合は true, それ以外は false を返します。 </returns>
        /// <remarks>
        /// 抽象の実装内容: <see cref="ValueObject{T}.Equals(object)"/> で実行される等価性比較メソッドを実装します。
        /// </remarks>
        protected abstract bool EqualsCore(T other);

        /// <summary>
        /// オブジェクトのハッシュコード生成を実行します。
        /// </summary>
        /// <returns> ハッシュ値を返します。 </returns>
        /// <remarks>
        /// 抽象の実装内容: <see cref="ValueObject{T}.GetHashCode()"/> で実行されるハッシュコード生成メソッドを実装します。 
        /// </remarks>
        protected abstract int GetHashCodeCore();

        /// <summary>
        /// プロパティの値を設定し、プロパティが変更された場合には IsDirty フラグを true に設定します。
        /// </summary>
        /// <typeparam name="TFieldType"> プロパティの型を指定します。 </typeparam>
        /// <param name="field"> 設定するプロパティの参照を指定します。 </param>
        /// <param name="value"> プロパティに設定する新しい値を指定します。 </param>
        protected virtual void SetProperty<TFieldType>(ref TFieldType field, TFieldType value)
        {
            if (!EqualityComparer<TFieldType>.Default.Equals(field, value))
            {
                field = value;
                IsDirty = true;
            }
        }

        /*--- Method: private ---------------------------------------------------------------------------------------------------------------------*/


    }
}
