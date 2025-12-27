using Microsoft.AspNetCore.Mvc;
using SantaGift.DBContext;
using SantaGift.Models;
using System.Runtime.CompilerServices;
using System.Xml;

namespace SantaGift.Controllers
{
    public class SantaHomeController : Controller
    {
        private readonly RegisterDb _dbContext;
        public SantaHomeController(RegisterDb db)
        {
            _dbContext = db;
        }

        [HttpGet]
        public IActionResult UsersList()
        {
            var userList = _dbContext.registers.ToList();
            return View(userList); // This will render Register.cshtml
        }

        [HttpGet]
        public IActionResult _Registe_Login()
        {
            return View("_Registe_Login");
        }
        [HttpPost]
        public IActionResult Register(Register model)
        {
            // Check if user already exists
            try
            {
                if (model != null)
                {
                    if (_dbContext.registers.Any(u => u.UserName == model.UserName))
                    {
                        return Json(new { success = false, message = "Duplicate user" });
                    }

                    _dbContext.registers.Add(model);
                    _dbContext.SaveChanges();

                    return Json(new { success = true, message = "Registered successfully" });
                }
                else
                {
                    return Json(new { success = false, message = "Invalid Request" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }

         }

        public IActionResult Login(Register model)
        {
            try
            {
                if (model != null)
                {
                    HttpContext.Session.Remove("isAdmin"); 
                    HttpContext.Session.Remove("isUser");
                    var isAdminPasswordCheck = _dbContext.registers.Where(x => x.Password == model.Password && x.UserName == model.UserName).FirstOrDefault();
                    var isUserPasswordCheck = _dbContext.registers.Where(x => x.UserPassword == model.Password && x.UserName == model.UserName).FirstOrDefault();
                    if (isAdminPasswordCheck != null)
                        HttpContext.Session.SetString("isAdmin", "true");
                    if (isUserPasswordCheck != null)
                        HttpContext.Session.SetString("isUser", "true");

                    var isValidAdmin = _dbContext.registers.Where(x => x.UserName == model.UserName && x.Password == model.Password).FirstOrDefault();
                    var isValidUser = _dbContext.registers.Where(x => x.UserName == model.UserName && x.UserPassword == model.Password).FirstOrDefault();

                    if (isValidAdmin != null)
                    {
                        HttpContext.Session.SetString("UserName", isValidAdmin.UserName);
                        return Json(new { success = true, message = "Login Admin successfully", redirectUrl = Url.Action("PlayGame", "PlayGame") });
                      
                    }
                    else if(isValidUser != null)
                    {
                        HttpContext.Session.SetString("UserName", isValidUser.UserName);
                        return Json(new { success = true, message = "Login User successfully", redirectUrl = Url.Action("PlayGame", "PlayGame") });

                    }
                    else
                    {
                        return Json(new { success = false, message = "No Records Found , Kindly Register " });
                    }

                }
                else
                {
                    return Json(new { success = false, message = "Invalid Request" });
                }
            }
            catch(Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
                
        }
    }
}
