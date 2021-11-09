using BusinessThemeProject.Models;
using BusinessThemeProject.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessThemeProject.Controllers
{
	public class AccountController : Controller
	{
		ApplicationContext db;
		public AccountController(ApplicationContext context)
		{
			db = context;
		}
		public IActionResult Index()
		{
			return View();
		}
		[HttpGet]
		public IActionResult Registration()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> RegistrationAsync(RegistrationViewModel model)
		{
			if (model.Password == model.ConfirmPassword)
			{
				if (ModelState.IsValid)
				{
					User user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
					if (user == null)
					{
						var currentUser = new User
						{
							Id = Guid.NewGuid(),
							Email = model.Email,
							Login = model.Login,
							Password = model.Password,
							Lastname = "",
							Name = ""
						};
						db.Users.Add(currentUser);
						await db.SaveChangesAsync();

						return RedirectToAction("Index", "Home");
					}
					else
						ModelState.AddModelError("", "Аккаунт с данной почтой уже существует");
				}
				ModelState.AddModelError("", "Неверный формат почты");
			}
			else
			{
				ModelState.AddModelError("", "Пароли не совпадают");
			}
			return View();
		}
	}
}
