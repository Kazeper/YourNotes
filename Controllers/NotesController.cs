using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YourNotes.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace YourNotes.Controllers
{
	public class NotesController : Controller
	{
		private readonly AppDbContext _db;

		public NotesController(AppDbContext db)
		{
			_db = db;
		}

		/// <summary>
		/// displays Notes.IndexView
		/// </summary>
		/// <returns></returns>
		public IActionResult Index()
		{
			var tempUserInfo = HttpContext.Session.GetString("SessionUser");

			if (tempUserInfo != null)
			{
				UserInfo userInfo = JsonConvert.DeserializeObject<UserInfo>(tempUserInfo);
				List<Note> userNotes = (_db.Notes.ToList()).FindAll(u => u.UserId == userInfo.UserId);

				return View(userNotes);
			}

			return View();
		}

		/// <summary>
		/// Displays Notes.AddNoteView
		/// </summary>
		/// <returns></returns>
		public IActionResult AddNote()
		{
			return View();
		}

		/// <summary>
		/// Add note to database
		/// </summary>
		/// <param name="note"></param>
		/// <returns></returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> AddNote(Note note)
		{
			if (ModelState.IsValid)
			{
				_db.Notes.Add(note);
				await _db.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			return View(note);
		}

		/// <summary>
		/// Displays Notes.EditNoteView
		/// </summary>
		/// <param name="Id"></param>
		/// <returns></returns>
		[HttpGet]
		public IActionResult EditNote(int? Id)
		{
			Note note;

			if (Id == null)
			{
				return NotFound();
			}
			else
			{
				note = _db.Notes.SingleOrDefault(n => n.Id == Id);
			}

			if (note == null)
			{
				return NotFound();
			}

			return View(note);
		}

		/// <summary>
		/// Update note when ModelState is valid
		/// </summary>
		/// <param name="note"></param>
		/// <returns></returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> EditNote(Note note)
		{
			if (ModelState.IsValid)
			{
				_db.Update(note);
				await _db.SaveChangesAsync();

				return RedirectToAction("Index");
			}
			return View(note);
		}

		/// <summary>
		/// Displays Note.DeleteNoteView
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet]
		public IActionResult DeleteNote(int? id)
		{
			Note note;

			if (id == null)
			{
				return NotFound();
			}
			else
			{
				note = _db.Notes.SingleOrDefault(n => n.Id == id);
			}

			if (note == null)
			{
				return NotFound();
			}

			return View(note);
		}

		/// <summary>
		/// Remove note from database
		/// </summary>
		/// <param name="note"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<IActionResult> DeleteNote(Note note)
		{
			if (note == null)
			{
				return NotFound();
			}

			_db.Notes.Remove(note);
			await _db.SaveChangesAsync();

			return RedirectToAction("Index");
		}
	}
}