using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  Core.POCO
{
    public class OperationResult
    {
        public OperationResult()
        { }
        public OperationResult(bool succedeed, string message = null, string prop = null)
        {
            Succedeed = succedeed;
            Message = message;
            Property = prop;
        }
        public bool Succedeed { get; set; }
        public string Message { get; set; }
        public string Property { get; set; }

    }
}
