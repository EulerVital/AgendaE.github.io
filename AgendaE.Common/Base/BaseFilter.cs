using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaE.Common.Base
{
    public abstract class BaseFilter
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
