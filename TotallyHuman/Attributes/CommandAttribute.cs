using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotallyHuman.Attributes
{
    class CommandAttribute : Attribute
    {
        public string m_CommandName;

        public CommandAttribute(string commandName)
        {
            m_CommandName = commandName;
        }
    }
}
