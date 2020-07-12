using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TimePlanner.Interfaces;
using TimePlanner.Models;

namespace TimePlanner.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEntryRepository entryRepository;

        public HomeController(IEntryRepository entryRepository)
        {
            this.entryRepository = entryRepository;
        }

        [HttpGet]
        public IActionResult GetEntries()
        {
            return Ok(entryRepository.All());
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
