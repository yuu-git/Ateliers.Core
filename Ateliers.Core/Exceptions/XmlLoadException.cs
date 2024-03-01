using System;
using System.Collections.Generic;
using System.Text;

namespace Ateliers
{
    /// <summary>
    /// XMLファイル読込例外
    /// </summary>
    /// <remarks>
    /// <para> 概要: <see cref="XmlFileService"/> などでXMLファイルを読込した際に異常終了した場合に発行される例外。 </para>
    /// </remarks>
    public class XmlLoadException : Exception
    {
        /*--- Constructers ------------------------------------------------------------------------------------------------------------------------*/

        public XmlLoadException()
            : base()
        { }

        public XmlLoadException(string pMessage)
            : base(pMessage)
        { }

        public XmlLoadException(string pMessage, Exception pInnerException)
            : base(pMessage, pInnerException)
        { }

        /*--- Property/Field Definitions ----------------------------------------------------------------------------------------------------------*/

        /*--- Method: public ----------------------------------------------------------------------------------------------------------------------*/
        /*--- Method: internal --------------------------------------------------------------------------------------------------------------------*/
        /*--- Method: protected -------------------------------------------------------------------------------------------------------------------*/
        /*--- Method: private ---------------------------------------------------------------------------------------------------------------------*/

    }
}
