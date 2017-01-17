using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.POCO
{
    public class OperationResult
    {
        public OperationResult()
        { }
        public OperationResult(bool succedeed, string message = null, string prop = null, string id=null)
        {
            Succedeed = succedeed;
            Message = message;
            Property = prop;
            Id = id;
        }
        public bool Succedeed { get; set; }
        public string Message { get; set; }
        public string Property { get; set; }
        public string Id { get; set; }
    }
}
