using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ateliers
{
    /// <summary>
    /// 整合性チェック例外
    /// </summary>
    /// <typeparam name="T"> 整合性を確認した型を指定します。 </typeparam>
    /// <remarks>
    /// <para> 概要: バリデーションを行った結果、異常となった場合に発行する例外。 </para>
    /// <para> クラス内にチェック結果と問題のあるオブジェクトを指定して発行することを推奨する。 </para>
    /// </remarks>
    public class ValidationErrorException<T> : ValidationErrorException
    {
        /*--- Constructers ------------------------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// 整合性チェック例外 / コンストラクタ
        /// </summary>
        /// <param name="messageTitle"> 例外のタイトルメッセージを指定します。 </param>
        /// <param name="validations"> 例外の対象となる整合性チェック結果コレクションを指定します。 </param>
        /// <param name="validationObject"> 例外となった整合性チェック対象のオブジェクトを指定します。 </param>
        /// <param name="innerException"> 内部例外を指定します。 </param>
        public ValidationErrorException(string messageTitle, IEnumerable<IValidationDetail> validations, T validationObject, Exception innerException)
            : base(messageTitle, validations, null, innerException)
        {
            this.ValidationObject = validationObject;
        }

        /// <summary>
        /// 整合性チェック例外 / コンストラクタ
        /// </summary>
        /// <param name="messageTitle"> 例外のタイトルメッセージを指定します。 </param>
        /// <param name="validations"> 例外の対象となる整合性チェック結果コレクションを指定します。 </param>
        /// <param name="validationObject"> 例外となった整合性チェック対象のオブジェクトを指定します。 </param>
        public ValidationErrorException(string messageTitle, IEnumerable<IValidationDetail> validations, T validationObject)
            : base(messageTitle, validations, null)
        {
            this.ValidationObject = validationObject;
        }

        /// <summary>
        /// 整合性チェック例外 / コンストラクタ
        /// </summary>
        public ValidationErrorException()
            : base()
        {
        }

        /*--- Property/Field Definitions ----------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// 例外となった整合性チェック対象のオブジェクトを取得します。
        /// </summary>
        public new T ValidationObject { get; }

        /*--- Method: public ----------------------------------------------------------------------------------------------------------------------*/
        /*--- Method: internal --------------------------------------------------------------------------------------------------------------------*/
        /*--- Method: protected -------------------------------------------------------------------------------------------------------------------*/
        /*--- Method: private ---------------------------------------------------------------------------------------------------------------------*/

    }

    /// <summary>
    /// 整合性チェック例外
    /// </summary>
    /// <remarks>
    /// <para> 概要: バリデーションを行った結果、異常となった場合に発行する例外。 </para>
    /// <para> クラス内にチェック結果と問題のあるオブジェクトを指定して発行することを推奨する。 </para>
    /// </remarks>
    public class ValidationErrorException : Exception
    {
        /*--- Constructers ------------------------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// 整合性チェック例外 / コンストラクタ
        /// </summary>
        /// <param name="messageTitle"> 例外のタイトルメッセージを指定します。 </param>
        /// <param name="validations"> 例外の対象となる整合性チェック結果コレクションを指定します。 </param>
        /// <param name="validationObject"> 例外となった整合性チェック対象のオブジェクトを指定します。 </param>
        /// <param name="innerException"> 内部例外を指定します。 </param>
        public ValidationErrorException(string messageTitle, IEnumerable<IValidationDetail> validations, object validationObject, Exception innerException)
            : base(CreateMessage(messageTitle, validations), innerException)
        {
            this.ValidationDetails = validations;
            this.ValidationObject = validationObject;
        }

        /// <summary>
        /// 整合性チェック例外 / コンストラクタ
        /// </summary>
        /// <param name="messageTitle"> 例外のタイトルメッセージを指定します。 </param>
        /// <param name="validations"> 例外の対象となる整合性チェック結果コレクションを指定します。 </param>
        /// <param name="validationObject"> 例外となった整合性チェック対象のオブジェクトを指定します。 </param>
        public ValidationErrorException(string messageTitle, IEnumerable<IValidationDetail> validations, object validationObject)
            : base(CreateMessage(messageTitle, validations))
        {
            this.ValidationDetails = validations;
            this.ValidationObject = validationObject;
        }

        /// <summary>
        /// 整合性チェック例外 / コンストラクタ
        /// </summary>
        public ValidationErrorException()
            : base("データチェックの結果、不正なデータを検知しました。")
        {
        }

        /*--- Property/Field Definitions ----------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// 例外となった整合性チェック対象のオブジェクトを取得します。
        /// </summary>
        public object ValidationObject { get; }

        /// <summary>
        /// 異常の整合性確認結果を取得します。
        /// </summary>
        public IEnumerable<IValidationDetail> ErrorValidations => this.ValidationDetails.Where(x => x.Level == ValidationLevel.Error);

        /// <summary>
        /// 警告の整合性確認結果を取得します。
        /// </summary>
        public IEnumerable<IValidationDetail> WarningValidations => this.ValidationDetails.Where(x => x.Level == ValidationLevel.Warning);

        /// <summary>
        /// 全ての整合性確認結果を取得します。
        /// </summary>
        public IEnumerable<IValidationDetail> ValidationDetails { get; }

        /*--- Method: public ----------------------------------------------------------------------------------------------------------------------*/
        /*--- Method: internal --------------------------------------------------------------------------------------------------------------------*/
        /*--- Method: protected -------------------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// 基本例外クラスに引き渡す例外メッセージを生成します。
        /// </summary>
        /// <param name="messageTitle"> 例外メッセージのタイトルを指定します。 </param>
        /// <param name="validations"> 整合性チェック結果コレクションを指定します。 </param>
        /// <returns> 生成された例外メッセージを返します。 </returns>
        protected static string CreateMessage(string messageTitle, IEnumerable<IValidationDetail> validations)
        {
            var msg = messageTitle + Environment.NewLine;
            msg += string.Join(Environment.NewLine, validations.Where(x => x.Level == ValidationLevel.Error).Select(x => x.ToString()));

            return msg;
        }

        /*--- Method: private ---------------------------------------------------------------------------------------------------------------------*/

    }
}
