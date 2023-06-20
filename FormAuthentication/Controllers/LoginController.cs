using BankingSystem.Helper;
using FormAuthentication.Data;
using FormAuthentication.Models;
using Microsoft.AspNetCore.Mvc;

namespace FormAuthentication.Controllers
{
    public class LoginController : Controller
    {
        private readonly AuthDbContext db;

        public LoginController(AuthDbContext authDbContext)
        {
            db = authDbContext;
        }
        public ActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(SignUp signUp)
        {
            var user = db.SignUps.Where(model => model.Email == signUp.Email).FirstOrDefault();
            bool passwordMatch = PasswordUtility.VerifyPassword(signUp.Password, user.salt, user.hashedPass);
            if (passwordMatch)
            {                
                ModelState.Clear();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
        public ActionResult Logout()
        {
            return RedirectToAction("SignIn");
        }
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp([Bind("Id, Name , Email, Password, ConfirmPassword")] SignUp signUp)
        {
            string salt = PasswordUtility.GenerateSalt();
            string hashedPassword = PasswordUtility.HashPassword(signUp.Password, salt);

            if (ModelState.IsValid)
            {
                signUp.salt = salt;
                signUp.hashedPass = hashedPassword;                

                db.SignUps.Add(signUp);
                int check = db.SaveChanges();
                if (check > 0)
                {
                    ModelState.Clear();
                    return RedirectToAction("SignIn");
                }                
            }
            return View();
        }

    }
}
