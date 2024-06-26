﻿using DoAnFramework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using System.Drawing.Printing;

namespace DoAnFramework.Controllers
{
    public class BlogController : Controller
    {
		private readonly book_shop_dbContext _context;

		public BlogController(book_shop_dbContext context)
		{
			_context = context;
		}

		public IActionResult Index()
        {
			var listBlogs = _context.Blogs	
                .Select(item => new Blog
                {
                    BlogId = item.BlogId,
                    BlogImage = item.BlogImage,
                    BlogTitle = item.BlogTitle,
                    Content = item.Content
                })
                .OrderBy(x => x.BlogId)
                .ToList();

			return View(listBlogs);					// Trả dữ liệu lấy được về View.
        }

        public IActionResult BlogDetail(int? id)
        {
			if (id == null)
			{
				return NotFound();
			}

			var blogDetail = _context.Blogs
			   .Where(blog => blog.BlogId == id)
			   .FirstOrDefault();
			return View(blogDetail);
        }
    }
}
