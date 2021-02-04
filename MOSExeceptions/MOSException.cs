using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOSExeceptionsLib
{
    public class MOSException:Exception
    {
        public   MOSException (string errmsg):base(errmsg)
        {
           
        }
    }
}
