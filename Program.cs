using EFGetStarted.Classes;
using EFGetStarted.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EFGetStarted
{
    internal class Program
    {
        private static void Main()
        {

            using (var context = new BloggingContext())
            {
                //Data Seeding
                context.Database.EnsureCreated();
                //add more rating (efficient updating)
                //context.Database.ExecuteSqlRaw("UPDATE [Blogs] SET [Rating] = [Rating] + 2");

                //efficent quyerying
                var blogsStart = context.Blogs.Include(b=>b.Posts).Where(b => b.Url.StartsWith("http")).ToList();
                
                
                //count post softdeted
                var countPost = context.Posts.Count(p => p.IsDeleted);
                
                //filter blog.com
                var filteredBlogsInclude = context.Blogs.Include(b => b.Posts).ToList();

                
                
                
                

                //blog test.com
                var testBlog = context.Blogs.FirstOrDefault(b => b.Url == "http://test.com");
                if (testBlog != null)
                {
                    Console.WriteLine("Existing Blog test !!");
                }
                else
                {
                    testBlog = new Blog
                    {
                        Url = "http://test.com",
                        Rating = 2,
                        Posts = new List<Post>
                    {
                      new Post { Title = "Intro to C#" , Content="Welcome to c#",Rating= 4},
                      new Post { Title = "Intro to VB.NET", Content="VB.NET",Rating=3 },
                      new Post { Title = "Intro to F#", Content="F#",Rating=2}
                    }
                    };

                    context.Blogs.Add(testBlog);
                }
                // blog abc.com
                var testBlog1 = context.Blogs.FirstOrDefault(b => b.Url == "http://abc.com");
                if (testBlog1 != null)
                {
                    Console.WriteLine("Existing Blog abc !!");
                }
                else
                {
                    testBlog1 = new Blog
                    {
                        Url = "http://abc.com",
                        Rating=5,
                        Posts = new List<Post>
                    {
                      new Post { Title = "Intro to C++" , Content="Welcome to C++",Rating=4},
                      new Post { Title = "Intro to Visual Studio", Content="Visual Studio",Rating=5 }
                    }
                    };

                    context.Blogs.Add(testBlog1);
                }
                var testBlog3 = context.Blogs.FirstOrDefault(b => b.Url == "testSoftdeleted.com");
                if (testBlog3 != null)
                {
                    Console.WriteLine("Existing Blog  !!");
                }
                else
                {
                    testBlog3 = new Blog
                    {
                        Url = "testSoftdeleted.com",
                        Rating = 4,
                        Posts = new List<Post>
                    {
                      new Post { Title = "Intro to C#" , Content="Welcome to c#",Rating= 4,IsDeleted = true},
                      new Post { Title = "Intro to VB.NET", Content="VB.NET",Rating=3 ,IsDeleted=true},
                      new Post { Title = "Intro to F#", Content="F#",Rating=2,IsDeleted=false}
                    }
                    };

                    context.Blogs.Add(testBlog3);
                }

                //blog dev.com
                var testBlog2 = context.Blogs.FirstOrDefault(b => b.Url == "http://dev.com");
                if (testBlog2 != null)
                {
                    Console.WriteLine("Existing Blog dev !!");
                }
                else
                {
                    testBlog2 = new Blog
                    {
                        Url = "http://dev.com",
                        Rating=5,
                        Posts = new List<Post>
                    {
                      new Post { Title = "Intro to Java" , Content="Welcome to Java",Rating=4},
                      new Post { Title = "Intro to JavaScript", Content="JavaScript",Rating=3 },
                      new Post { Title = "Intro to NetBean", Content="NetBean",Rating=3},
                      new Post { Title = "Intro to sqlite", Content="Sqlite",Rating=4}
                    }
                    };

                    context.Blogs.Add(testBlog2);
                }
                context.SaveChanges();
            }

            using (var context = new BloggingContext())
           {
              var blogfilter = context.Blogs
                .Include(b => b.Posts)
                .IgnoreQueryFilters()
                .ToList();
           }

           

            //eager loading

            using (var db = new BloggingContext())
            {
                var blogs = db.Blogs
                        .Include(b => b.Posts)
                        .ToList();
               
                Console.WriteLine();
                //take post have starwith I
                var blogs25 = db.Posts
                .Where(p => p.Title.StartsWith("I"))
                .Take(25)
                .ToList();

            }

            using (var db = new BloggingContext())
            {
                //main

                var BlogManager = new BlogManager(db);
                uint n;
                Console.WriteLine("Choose a number:");
                Console.WriteLine("1:Add a Blog");
                Console.WriteLine("2:Remove a Blog");
                Console.WriteLine("3:Find a Blog");
                Console.WriteLine("4:Update a Blog");//2 choice
                Console.WriteLine("5:Add a Post");//2 choice
                Console.WriteLine("6:Remove a Post");
                Console.WriteLine("7:Find a Post");
                Console.WriteLine("8:Update a Post");
                Console.WriteLine("9:Soft delete a Post");
                n = Convert.ToUInt32(Console.ReadLine());

                switch (n)
                {
                    case 1:
                        Console.WriteLine("Input URL Blog to add:");
                        string p = Convert.ToString(Console.ReadLine());
                        BlogManager.Add(new Blog() { Url = p });

                        break;
                    case 2:
                        Console.WriteLine("Input id Blog to remove:");
                        int q = Convert.ToInt32(Console.ReadLine());
                        BlogManager.Remove(id: q);
                        break;
                    case 3:
                        Console.WriteLine("Input id Blog to find:");
                        int e = Convert.ToInt32(Console.ReadLine());
                        var ex = BlogManager.Find(id: e);
                        Console.WriteLine($"Existing Blog id: {ex.BlogId},URL: {ex.Url} ");

                        break;
                    case 4://update blog
                        //chinh sua url va rating cua blog
                        Console.WriteLine("Update your Url Blog? [y/n]");
                        string u = Convert.ToString(Console.ReadLine());
                        if (u.StartsWith("y"))
                        {
                            Console.WriteLine("Input id Blog:");
                            int x = Convert.ToInt32(Console.ReadLine());

                            var x2 = BlogManager.Find(id: x);

                            Console.WriteLine("Input new Url Blog:");
                            var x1 = Convert.ToString(Console.ReadLine());

                            Console.WriteLine("Input rating Blog:");
                            int x3 = Convert.ToInt32(Console.ReadLine());
                            //update vao object
                            x2.Url = x1;
                            x2.Rating = x3;
                            //update database
                            BlogManager.Update(id: x, blog: x2, rating: x3);
                        }
                        else
                        {
                            Console.WriteLine("Input Url Blog:");
                            var x5 = Convert.ToString(Console.ReadLine());
                            var x6 = BlogManager.FindUrlBlog(url: x5);
                            Console.WriteLine("Input rating Blog:");
                            int x7 = Convert.ToInt32(Console.ReadLine());

                            x6.Rating = x7;
                            BlogManager.Update(id: x6.BlogId, blog: x6, rating: x7);

                        }



                        break;

                    case 5://add a post
                        Console.WriteLine("Do you have blog? [y/n]");
                        string a = Convert.ToString(Console.ReadLine());
                        if (a.StartsWith("n"))
                        {
                            //add new blog
                            Console.WriteLine("Input URL Blog to add:");
                            string p1 = Convert.ToString(Console.ReadLine());
                            BlogManager.Add(new Blog() { Url = p1 });
                            //tim id  new blog 
                            var f1 = BlogManager.FindUrlBlog(url: p1);

                            //add post
                            Console.WriteLine("Input Title Post:");
                            String T1 = Convert.ToString(Console.ReadLine());
                            Console.WriteLine("Input Content Post:");
                            String C1 = Convert.ToString(Console.ReadLine());
                            int f3 = f1.BlogId;
                            var f2 = BlogManager.Find(id: f3);
                            //add Post
                            BlogManager.AddPost(new Post() { Title = T1, Content = C1, BlogId = f2.BlogId, IsDeleted = false });

                        }
                        else
                        {
                            Console.WriteLine("Input your id blog");
                            int a3 = Convert.ToInt32(Console.ReadLine());

                            var a1 = BlogManager.Find(id: a3);
                            //udate vao post neu da co blog:
                            a1.BlogId = a3;

                            //add Title and Content
                            Console.WriteLine("Input Title Post:");
                            String T = Convert.ToString(Console.ReadLine());
                            Console.WriteLine("Input Content Post:");
                            String C = Convert.ToString(Console.ReadLine());
                            //add Post
                            BlogManager.AddPost(new Post() { Title = T, Content = C, BlogId = a1.BlogId, IsDeleted = false });

                        }
                        break;
                    case 6:
                        Console.WriteLine("Input id Post to remove:");
                        int w = Convert.ToInt32(Console.ReadLine());
                        BlogManager.RemovePost(id: w);

                        break;
                    case 7:
                        Console.WriteLine("Input id Post to find:");
                        int f = Convert.ToInt32(Console.ReadLine());
                        var ex1 = BlogManager.FindPost(id: f);
                        Console.WriteLine($"Existing Post Title={ex1.Title}, Content {ex1.Content}");
                        break;
                    case 8:
                        Console.WriteLine("Input id Post to update your post:");
                        int z = Convert.ToInt32(Console.ReadLine());

                        var z2 = BlogManager.FindPost(id: z);

                        Console.WriteLine("Input Title Post:");
                        var z1 = Convert.ToString(Console.ReadLine());
                        Console.WriteLine("Input Content Post:");
                        var z3 = Convert.ToString(Console.ReadLine());

                        Console.WriteLine("Input rating Post:");
                        int z4 = Convert.ToInt32(Console.ReadLine());

                        //update vao object
                        z2.Title = z1;
                        z2.Content = z3;
                        z2.Rating = z4;
                        //update database
                        BlogManager.UpdatePost(id: z, post: z2, rating: z4);
                        break;
                    case 9:
                        Console.WriteLine("Input id Post to soft delete your post");
                        int sd = Convert.ToInt32(Console.ReadLine());

                        var sd2 = BlogManager.FindPost(id: sd);

                        var sd5 = BlogManager.UpdateSoftDelete(id: sd, isdeleted: false);

                        if (sd5.IsDeleted == true||sd2.IsDeleted==true)
                        {
                            Console.WriteLine("post has been soft deleted");
                        }
                        else 
                        {
                            sd5.IsDeleted = true;
                            sd2.IsDeleted = true;
                            BlogManager.UpdateSoftDelete(id: sd,isdeleted:true);
                        }

                        break;

                }
                
                




            }
        }
    }
}