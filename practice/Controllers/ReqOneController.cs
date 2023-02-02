using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using practice.Models;
using practice.Services;
using practice.Utils;
using practice.ViewModels;
using System.Collections.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace practice.Controllers
{
    public class ReqOneController : Controller
    {
        [Route("r1")]
        public async Task<IActionResult> Index(int pg = 1, string q = "Cahoot")
        {
            string[] s = (string[])TempData["q"];
            if (s != null && s[0] != null)
            {
                q = s[0];
            }
            Console.WriteLine("pg: " + pg);
            Console.WriteLine("q: " + q);
            const int pageSize = 10;
            int recSkip = (pg - 1) * pageSize;
            if (pg < 1) pg = 1;

            ReqOneService ros = new ReqOneService();
            List<PostViewModel> ans = ros.GetReqAns(q);
            List<ReqOneViewModel> res = PostConverter.MyCconverter(ans);
            int recsCont = res.Count();
            var pager = new Pager(recsCont, pg, pageSize);
            var data = res.Skip(recSkip).Take(pager.PageSize);
            this.ViewBag.Pager = pager;
            return View(data);
        }

        [Route("r1/search")]
        [HttpPost]
        public IActionResult Search(IFormCollection f)
        {
            TempData["q"] = f["querystring"];
            return RedirectToAction("Index", "ReqOne");
        }
    }
}
