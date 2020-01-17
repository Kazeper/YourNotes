using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YourNotes.Models;

namespace YourNotes.Controllers
{
	public class RegisterController : Controller
	{
		private readonly AppDbContext _db;

		public RegisterController(AppDbContext db)
		{
			_db = db;
		}

		/// <summary>
		/// Displays Register.IndexView
		/// </summary>
		/// <returns></returns>
		public IActionResult Index()
		{
			return View();
		}

		/// <summary>
		/// Displays Register.SuccessView
		/// </summary>
		/// <returns></returns>
		public IActionResult Success()
		{
			return View();
		}

		/// <summary>
		/// Creates account when ModelState is vaild
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateAccount(User user)
		{
			if (ModelState.IsValid)
			{
				_db.Add(user);
				await _db.SaveChangesAsync();
				return RedirectToAction(nameof(Success));
			}

			return View(user);
		}
	}
}