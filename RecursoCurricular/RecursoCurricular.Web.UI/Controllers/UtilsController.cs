﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecursoCurricular.Web.UI.Controllers
{
    public class UtilsController : RecursoCurricular.Web.Controller
    {
        [Authorize]
        [HttpGet]
        public ActionResult Header()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public JsonResult GenerateId()
        {
            return this.Json(Guid.NewGuid(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Error()
        {
            return this.View();
        }
    }
}