using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ateliers
{
    /// <summary> 
    /// ディスプレイ型
    /// </summary>
    /// <remarks>
    /// <para> 概要: 起動している端末の大きさを示す。 </para>
    /// <para> 特定サイズの画面時に何か処理をすべき場合などの判定に使用する。 </para>
    /// </remarks>
    public enum DisplayType
    {
        /// <summary> 未設定（デフォルト） </summary>
        Non, 

        /// <summary> HD720 </summary>
        HD720,
        /// <summary> FHD </summary>
        FHD,
        /// <summary> WUXGA </summary>
        WUXGA,

        /// <summary> 4K </summary>
        QFHD_UHD_4K,

        /// <summary> iPad Pro (9.7型) </summary>
        iPad_Pro_97i,
        /// <summary> iPad Pro (12.9型) </summary>
        iPad_Pro_129i,
    }
}
