﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Modas.Models;
using Modas.Models.ViewModels;
using System.Linq;

namespace Modas.Controllers
{
    public class HomeController : Controller
    {
        //add a custom 20 page
        private readonly int PageSize = 20;
        private IEventRepository repository;
        public HomeController(IEventRepository repo)
        {
            repository = repo;
        }
        //default page is 1 if nothing is passed
        public ViewResult Index(int page = 1) => View(new EventListViewModel
        {
            Events = repository.Events
               .Include(e => e.Location)
               .OrderByDescending(e => e.TimeStamp)
               .Skip((page - 1) * PageSize)
               .Take(PageSize),
            PagingInfo = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = PageSize,
                TotalItems = repository.Events.Count()
            }
        });
    }
}