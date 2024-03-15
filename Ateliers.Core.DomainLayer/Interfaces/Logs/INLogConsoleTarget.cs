using System;
using System.Collections.Generic;
using System.Text;

namespace Ateliers
{
    public interface INLogConsoleTarget
    {
        /*--- Property/Field Definitions ----------------------------------------------------------------------------------------------------------*/

        string TargetName { get; }

        string LogLayout { get; }

        /*--- Method: public ----------------------------------------------------------------------------------------------------------------------*/

    }
}
