using EFGetStarted.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFGetStarted.Interfaces
{
    interface IBlogManager
    {
       
        void Add(IBlog blog);
        void Remove(int id);
        void Update(int id,IBlog blog ,int rating);
        IBlog Find(int id);
        IBlog FindUrlBlog(string url);
        void AddPost(IPost post);
        void RemovePost(int id);
        void UpdatePost(int id, IPost post,int rating);
        IPost FindPost(int id);


    }
}
