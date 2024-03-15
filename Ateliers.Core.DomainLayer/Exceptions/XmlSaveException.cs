using System;
using System.Collections.Generic;
using System.Text;

namespace Ateliers
{
    /// <summary>
    /// XMLファイル保存例外
    /// </summary>
    /// <remarks>
    /// <para> 概要: <see cref="XmlFileService"/> などでXMLファイルを保存した際に異常終了した場合に発行される例外。 </para>
    /// </remarks>
    public class XmlSaveException : Exception
    {
        /*--- Constructers ------------------------------------------------------------------------------------------------------------------------*/

        public XmlSaveException()
            : base()
        { }

        public XmlSaveException(string pMessage)
            : base(pMessage)
        { }

        public XmlSaveException(string pMessage, Exception pInnerException)
            : base(pMessage, pInnerException)
        { }

        /*--- Property/Field Definitions ----------------------------------------------------------------------------------------------------------*/
        /*--- Method: public ----------------------------------------------------------------------------------------------------------------------*/
        /*--- Method: internal --------------------------------------------------------------------------------------------------------------------*/
        /*--- Method: protected -------------------------------------------------------------------------------------------------------------------*/
        /*--- Method: private ---------------------------------------------------------------------------------------------------------------------*/

    }
}
