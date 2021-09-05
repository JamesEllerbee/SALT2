using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALT2.Scripts.Enumerations
{
    /// <summary>
    /// Frog enemy type enumeration.
    /// </summary>
    public enum FrogType
    {
        /// <summary>
        /// The basic type, this enemy should respond to the player detected by shooting at them.
        /// </summary>
        BASIC,

        /// <summary>
        /// The rusher type, this type should respond to the player detected by running at them.
        /// </summary>
        RUSHER,
    }
}
