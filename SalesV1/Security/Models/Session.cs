using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Models
{
    public class Session
    {
        public int SessionId { get; set; }
        public int UserId { get; set; }
        public DateTime Expiration { get; set; }
        public bool IsActive { get; set; }
    }
}
