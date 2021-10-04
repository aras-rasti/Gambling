using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gambling.Data.Tools.Enums
{
	public enum Provider : int
    {
        SqlServer = 0,
        MySql = 1,
        PostgreSQL = 2,
        Oracle = 3,
        InMemory = 4,
    }
}
