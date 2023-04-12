using System.Security.Claims;
using EMS.Data;
using EMS.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace EMS.Controllers;

public class LoginController :Controller
{
    private  ApplicationDbContext _db ;
    public LoginController(ApplicationDbContext db)
    {
        _db = db;
    }
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
     public IActionResult Login(Login model)
    {
        if(ModelState.IsValid){
            var data = _db.Logins.Where(m=>m.userName==model.userName).SingleOrDefault();
            if(data!= null){
               bool IsValid = (data.userName == model.userName && data.password == model.password);
               
                if(IsValid){
                    var identity =new ClaimsIdentity(new[] {new Claim(ClaimTypes.Name, model.userName)},
                    CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    HttpContext.Session.SetString("UserName", model.userName);
                    TempData["success"]="Login successfully";
                    return RedirectToAction("Index", "Home");

                }
                else{
                    TempData["error"] = "Invalid Password!";
                    return View(model);
                }
            }
           else{
                    TempData["error"] = "User does not exist";
                    return View(model);
                }
         
       }
       else {
          return View(model);
       }
       
    }
     
    public IActionResult LogOut()
    {
        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        var storedCookies = Request.Cookies.Keys;
        foreach(var cookies in storedCookies)
        {
            Response.Cookies.Delete(cookies);
        }
        TempData["success"]="Logout successfully";
        return RedirectToAction("Login", "Login");
    }

     public IActionResult Profile()
        {
            return View();
        }
}