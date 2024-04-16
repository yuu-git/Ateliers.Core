using System;
using System.Collections.Generic;
using System.Text;

namespace Ateliers
{
    public interface INLogFileTargetSetting : INLogConsoleTarget
    {
        /*--- Property/Field Definitions ----------------------------------------------------------------------------------------------------------*/

        string Directory { get; }

        string FileName { get; }

        /*--- Method: public ----------------------------------------------------------------------------------------------------------------------*/

    }
}
