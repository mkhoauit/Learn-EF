using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFGetStarted.Interfaces
{
    public interface IBlog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }
        public int Rating { get; set; }
       // public bool IsDeleted { get; set; }

    }
}
