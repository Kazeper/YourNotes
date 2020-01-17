using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using YourNotes.Models;

namespace YourNotes.Controllers
{
	public class HomeController : Controller
	{
		private readonly AppDbContext _db;
		private UserInfo UserInfo { get; set; }

		public HomeController(AppDbContext db)
		{
			_db = db;
			UserInfo = null;

			//TODO ogarnąć zmianę koloru

			//String[] colors =
			//{
			//	"white",
			//	"red",
			//	"green",
			//	"Yellow"
			//};
			//ViewBag.color = colors;
		}

		/// <summary>
		/// display Home.IndexView
		/// </summary>
		/// <returns></returns>
		public IActionResult Index()
		{
			return View();
		}

		/// <summary>
		/// displays Home.SignInView
		/// </summary>
		/// <returns></returns>
		public IActionResult SignIn()
		{
			return View();
		}

		/// <summary>
		/// Sign in when ModelState is valid
		/// </summary>
		/// <param name="loginData"></param>
		/// <returns></returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult SignIn(AttempLoginData loginData)
		{
			if (ModelState.IsValid)
			{
				List<User> users = _db.Users.ToList();
				User foundedUser = users.Find(u => u.Email == loginData.Email);

				//Set userInfo Into Session
				if (foundedUser != null && foundedUser.Password.Equals(loginData.Password))
				{
					this.UserInfo = new UserInfo(foundedUser.Id, foundedUser.Email);
					HttpContext.Session.SetString("SessionUser", JsonConvert.SerializeObject(UserInfo));

					return RedirectToAction("Index", "Notes");
				}
				else
				{
					ModelState.AddModelError(string.Empty, "Email or password is incorrect.");
				}
			}
			return View(loginData);
		}

		/// <summary>
		/// logout user
		/// </summary>
		/// <returns></returns>
		public IActionResult Logout()
		{
			HttpContext.Session.Clear();

			return RedirectToAction("Index");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}