﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorld.Models;
using TheWorld.Services;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.web
{
    public class AppController :Controller
    {
        private IMailService _mailService;
        private IConfigurationRoot _config;
        private IWordlRepository _repository;
        private ILogger<AppController> _logger;

        public AppController(IMailService mailService, IConfigurationRoot config, 
            IWordlRepository repository, ILogger<AppController> logger)
        {
            _mailService = mailService;
            _config = config;
            _repository = repository;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Trips()
        {
            return View();
            //angular will handle this part
            //try
            //{
            //    var data = _repository.GetAllTrips();
            //    return View(data);
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError($"Failed to get trips in Index page: {ex.Message}");
            //    return Redirect("/error");
            //}
        }

        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            if (model.Email.Contains("aol.com"))
            {
                ModelState.AddModelError("Email", "We dont support AOL email addresses");
            }

            if (ModelState.IsValid)
            {
                _mailService.SendMail(_config["MailSettings:ToAddress"], model.Email, "From the World", model.Message);               
                ModelState.Clear();
                ViewBag.UserMessage = "Message sent";
            }
            

            return View();
        }
        public IActionResult About()
        {
            return View();
        }
    }
}