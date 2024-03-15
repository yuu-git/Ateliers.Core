using System;
using System.Collections.Generic;
using System.Text;

namespace Ateliers
{
    /// <summary>
    /// 曜日フラグ
    /// </summary>
    /// <remarks>
    /// <para> 概要: 月曜日~日曜日をフラグとして扱う。<see cref="Enum.HasFlag"/>と合わせて使用することで曜日判定を行う。 </para>
    /// <para> メリットとしては『土曜日と日曜日に特定処理をする』の場合、データとして『32 + 64 = 96』のint値で簡単に保持することができる。 </para>
    /// <para> ※ .NET Core 2.1 より以前は <see cref="Enum.HasFlag"/> の処理が重いため注意 </para>
    /// </remarks>
    [Flags]
    public enum WeekFlags : int
    {
        /// <summary> 月曜日(Monday) </summary>
        Mon = 1,
        /// <summary> 火曜日(Tuesday) </summary>
        Tue = 2,
        /// <summary> 水曜日(Wednesday) </summary>
        Wed = 4,
        /// <summary> 木曜日(Thursday) </summary>
        Thu = 8,
        /// <summary> 金曜日(Friday) </summary>
        Fri = 16,
        /// <summary> 土曜日(Saturday) </summary>
        Sat = 32,
        /// <summary> 日曜日(Sunday) </summary>
        Sun = 64,
    }
}
