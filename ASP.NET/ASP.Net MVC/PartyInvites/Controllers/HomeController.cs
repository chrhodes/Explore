﻿using PartyInvites.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PartyInvites.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home

        public ViewResult Index()
        {
            int hour = DateTime.Now.Hour;
            ViewBag.Greeting = hour < 12 ? "Good Morning" : "Good Afternoon";
            return View();
        }
        //public ViewResult Index()
        //{
        //    return View();
        //}

        //public string Index()
        //{
        //    return "Hello World";
        //}

        [HttpGet]
        public ViewResult RsvpForm()
        {
            return View();
        }

        [HttpPost]
        public ViewResult RsvpForm(GuestResponse guestResponse)
        {
            //// TODO: Email response to the party organizer
            //return View("Thanks", guestResponse);

            if (ModelState.IsValid)
            {
                // TODO: Email response to the party organizer
                return View("Thanks", guestResponse);
            }
            else
            {
                // there is a validation error
                return View();
            }
        }

    }
}