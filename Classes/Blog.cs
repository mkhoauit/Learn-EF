using EFGetStarted.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFGetStarted.Classes
{
    [Index(nameof(Url), Name="Index_Url")]
    public class Blog : IBlog
    {
        [Key]
        public int BlogId { get; set; }

        [MaxLength(25)]
        public string Url { get; set; }

        public int Rating { get; set; }
        //public bool IsDeleted { get; set; }
        public List<Post> Posts { get; set; } = new List<Post>();

        


    }
}
