﻿using Dapper;
using ENPDotNetCoreBatch5.ConsoleApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ENPDotNetCoreBatch5.ConsoleApp
{
    public class EFCoreExample
    {
        public void Read()
        {
            AppDbContext db = new AppDbContext();
            var lst = db.Blogs.Where(x => x.DeleteFlag == false).ToList();
            foreach (var item in lst)
            {
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
            }
        }

        public void Create(string title, string author, string content)
        {
            BlogDataModel blog = new BlogDataModel
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content,
            };

            AppDbContext db = new AppDbContext();
            db.Blogs.Add(blog);
            var result = db.SaveChanges();
            Console.WriteLine(result == 1 ? "Saving Successful" : "Saving Failed");

        }

        public void Edit(int id)
        {

            AppDbContext db = new AppDbContext();
            //db.Blogs.Where(x => x.BlogId == id).FirstOrDefault();
            var item = db.Blogs.FirstOrDefault(x => x.BlogId == id);

            if (item is null)
            {
                Console.WriteLine("No data found");
                return;
            }
            Console.WriteLine(item.BlogId);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);

        }

        public void Update(int id, string title, string author, string content)
        {
            AppDbContext db = new AppDbContext();
            var item = db.Blogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);

            if (item is null)
            {
                Console.WriteLine("No data found");
                return;
            }
            
            if(!string.IsNullOrEmpty(title))
            {
                item.BlogTitle = title;
            }
            
            if(!string.IsNullOrEmpty(author))
            {
                item.BlogAuthor = author;
            }
          
            if(!string.IsNullOrEmpty(content))
            {
                item.BlogContent = content;
            }
            //because of AsnoTracking need to add below line
            db.Entry(item).State = EntityState.Modified;
            var result = db.SaveChanges();

            Console.WriteLine(result == 1 ? "Updating Successful" : "Updating Failed");
        }

        public void Delete(int id)
        {
            AppDbContext db = new AppDbContext();
            var item = db.Blogs.
                AsNoTracking().
                FirstOrDefault(x => x.BlogId == id);

            if (item is null)
            {
                Console.WriteLine("No data found");
                return;
            }

            db.Entry(item).State = EntityState.Deleted;
            var result = db.SaveChanges();
            Console.WriteLine(result == 1 ? "Deleting Successful" : "Deleting Failed");
        }
    }
}

    



