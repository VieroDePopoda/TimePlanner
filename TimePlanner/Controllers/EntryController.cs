using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TimePlanner.Interfaces;
using TimePlanner.Models;

namespace TimePlanner.Controllers
{
    public class EntryController : Controller
    {
        private readonly IEntryRepository entryRepository;
        private readonly IHubContext<SignalServer> context;

        public EntryController(IEntryRepository entryRepository, IHubContext<SignalServer> context)
        {
            this.entryRepository = entryRepository;
            this.context = context;
        }

        public IActionResult Index()
        {
            return View(entryRepository.All());
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Entry entry, TimeSpan[] time)
        {
            if (!ModelState.IsValid)
            {
                return View(entry);
            }

            entry.StartTime = entry.StartTime.Date + time[0];

            if (time.Length > 1)
                entry.EndTime = entry.EndTime.Date + time[1];

            entryRepository.Add(entry);
            entryRepository.SaveChanges();
            context.Clients.All.SendAsync("refreshEntries");

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            if (id != null)
            {
                var entry = entryRepository.Find(id);
                return View(entry);
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(Entry entry, TimeSpan[] time)
        {
            if (!ModelState.IsValid)
            {
                return View(entry);
            }

            entry.StartTime = entry.StartTime.Date + time[0];

            if (time.Length > 1)
                entry.EndTime = entry.EndTime.Date + time[1];

            entryRepository.Update(entry);
            entryRepository.SaveChanges();

            context.Clients.All.SendAsync("refreshEntries");
            return RedirectToActionPermanent("Index", "Home");
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            var entry = entryRepository.Find(id);
            entryRepository.Delete(entry);
            entryRepository.SaveChanges();
            context.Clients.All.SendAsync("refreshEntries");
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult ChangeCompleteStatus(bool currentStatus, Guid id)
        {
            var entry = entryRepository.Find(id);
            entry.IsCompleted = !currentStatus;
            entryRepository.SaveChanges();
            context.Clients.All.SendAsync("refreshEntries");
            return RedirectToAction("Index", "Home");
        }
    }
}
