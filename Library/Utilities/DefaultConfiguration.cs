using Antlr.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksLibrary.Utilities
{
    public class DefaultConfiguration
    {
        public DefaultConfiguration(bool useDefaultSettings)
        {
            DefaultSettings = useDefaultSettings;
        }
        public bool DefaultSettings { get; }
    }
}
