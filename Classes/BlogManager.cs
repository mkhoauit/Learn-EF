using EFGetStarted.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFGetStarted.Classes
{
    class BlogManager : IBlogManager
    {

        private BloggingContext _dbContext;
        public BlogManager(BloggingContext dbContext)
        {
            _dbContext = dbContext;

        }

        public void Add(IBlog blog)
        {
            var transation = _dbContext.Database.BeginTransaction();
            try {
                //check 
                var existingBlog = _dbContext.Blogs.SingleOrDefault(b => b.BlogId == blog.BlogId);
                if (existingBlog != null)
                {
                    throw new Exception("Existing Blog !!");

                }


                _dbContext.Blogs.Add(blog as Blog);//ep kieu
                _dbContext.SaveChanges();
                transation.Commit();


            }
            catch (Exception) 
            {
                transation.Rollback();

            }

            } 

        public void AddPost(IPost post)
        {
            var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                //check
                var existingPost = _dbContext.Posts.SingleOrDefault(p => p.PostId == post.PostId);
                if (existingPost != null)
                {
                    throw new Exception("Existing Post !!");
                }

                _dbContext.Posts.Add(post as Post);//ep kieu
                _dbContext.SaveChanges();
                transaction.Commit();
            }
            catch (Exception) 
            {
                transaction.Rollback();
            }

        }

        public IBlog Find(int id)
        {
            //check

            var existingBlog = _dbContext.Blogs.SingleOrDefault(b => b.BlogId == id);
            if (existingBlog == null)
            {
                Console.WriteLine($"NotExisting Blog: {id}!!");
            }

            return existingBlog;
            
        }
        public IBlog FindUrlBlog(string url)
        {
            //check

            var existingBlog = _dbContext.Blogs.SingleOrDefault(b => b.Url == url);
            if (existingBlog == null)
            {
                Console.WriteLine($"NotExisting Blog: {url}!!");
            }

            return existingBlog;

        }

        public IPost FindPost(int id)
        {
            //check
            var existingPost = _dbContext.Posts.SingleOrDefault(p => p.PostId == id);
            if (existingPost == null)
            {
                throw new Exception($"NotExisting Post: {id}!!");
            }

            return existingPost;

            
        }

        public void Remove(int id)
        {
            var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                //check 
                var existingBlog = _dbContext.Blogs.SingleOrDefault(b => b.BlogId == id);
                if (existingBlog == null)
                {
                    throw new Exception("NotExisting Blog !!");

                }

                _dbContext.Blogs.Remove(existingBlog);
                _dbContext.SaveChanges();
                transaction.Commit();
            }
            catch (Exception) 
            {
                transaction.Rollback();
            }
            
        }

        public void RemovePost(int id)
        {
            var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                //check 
                var existingPost = _dbContext.Posts.SingleOrDefault(p => p.PostId == id);
                if (existingPost == null)
                {
                    throw new Exception("NotExisting Post !!");

                }

                _dbContext.Posts.Remove(existingPost);
                _dbContext.SaveChanges();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
            }
        }

        public void Update(int id, IBlog blog,int rating)
        {
            var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                //check 
                var existingBlog = _dbContext.Blogs.SingleOrDefault(b => b.BlogId == id);
                if (existingBlog == null)
                {
                    throw new Exception("NotExisting Blog !!");

                }

                _dbContext.Blogs.Update(blog as Blog);
                _dbContext.SaveChanges();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
            }

        }

        public void UpdatePost(int id, IPost post,int rating)
        {
            var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                //check 
                var existingPost = _dbContext.Posts.SingleOrDefault(p => p.PostId == id);
                if (existingPost == null)
                {
                    throw new Exception("NotExisting Post !!");

                }

                _dbContext.Posts.Update(post as Post);
                _dbContext.SaveChanges();
                transaction.Commit();
            }
            catch (Exception) 
            {
                transaction.Rollback();
            }
            
        }
        public IPost UpdateSoftDelete(int id, bool isdeleted )
        {
            //check
            var existingPost = _dbContext.Posts.SingleOrDefault(p => p.PostId== id);
            if (existingPost == null)
            {
                throw new Exception($"NotExisting Post to soft deleted!!");
            }
            existingPost.IsDeleted = isdeleted;
            _dbContext.Posts.Update(existingPost);
            _dbContext.SaveChanges();
            return existingPost;


        }

    }
}
