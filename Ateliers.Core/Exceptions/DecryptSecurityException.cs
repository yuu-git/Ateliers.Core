﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ateliers
{
    /// <summary>
    /// 復号セキュリティ例外
    /// </summary>
    public class DecryptSecurityException : Exception
    {
        /*--- * structers -------------------------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// 復号セキュリティ例外 / コンストラクタ
        /// </summary>
        public DecryptSecurityException()
        {
        }

        /// <summary>
        ///  復号セキュリティ例外 / コンストラクタ
        /// </summary>
        /// <param name="message"> 例外メッセージを指定します。 </param>
        public DecryptSecurityException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// 復号セキュリティ例外 / コンストラクタ
        /// </summary>
        /// <param name="message"> 例外メッセージを指定します。 </param>
        /// <param name="innerException"> 内部例外を指定します。 </param>
        public DecryptSecurityException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /*--- Property/Field Definitions ----------------------------------------------------------------------------------------------------------*/
        /*--- Method: public ----------------------------------------------------------------------------------------------------------------------*/
        /*--- Method: internal --------------------------------------------------------------------------------------------------------------------*/
        /*--- Method: protected -------------------------------------------------------------------------------------------------------------------*/
        /*--- Method: private ---------------------------------------------------------------------------------------------------------------------*/

    }
}
