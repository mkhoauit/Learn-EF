using EFGetStarted.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFGetStarted.Classes
{
    public class Post :IPost
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public int Rating { get; set; }
        public bool IsDeleted { get; set; }

        public Blog Blog { get; set; }
    }
}
