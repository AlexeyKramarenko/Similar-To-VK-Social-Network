using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  Core.POCO
{
    public class FriendInvitation
    {
        public int AccountID { get; internal set; }
        public int BecameAccountID { get; internal set; }
        public string Email { get; internal set; }
        public Guid GUID { get; internal set; }
    }
}
