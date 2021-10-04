using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gambling.Data.Tools
{
	public class Options : object
    {
        public Options() : base()
        {
        }

        public Enums.Provider Provider { get; set; }

        public string ConnectionString { get; set; }
    }
}
