using System.Threading.Tasks;
using asp_autonews.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace asp_autonews.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> um, SignInManager<IdentityUser> sim)
        {
            _userManager = um;
            _signInManager = sim;
        }
        
        // регистрация для новых пользователей
        [AllowAnonymous]
        public IActionResult Login(string rUrl)
        {
            ViewBag.returnUrl = rUrl;
            return View(new LoginViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string rUrl)
        {
            // если введены логин и пароль
            if (ModelState.IsValid)
            {   
                // ищем пользователя, если он ввел правильные логин и пароль
                IdentityUser user = await _userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    // если нашелся, то выходим из аккаунта, и пытаемся войти по логину и паролю из формы
                    await _signInManager.SignOutAsync();
                    SignInResult result = await _signInManager.PasswordSignInAsync(user, model.Password, model.Remember, false);
                    // если всё прошло успешно, то переход на страницу, откуда пользователь входил 
                    if (result.Succeeded)
                    {
                        return Redirect(rUrl ?? "/");
                    }
                }
                // если он не нашелся, выводим ошибку
                ModelState.AddModelError(nameof(LoginViewModel.UserName), "Неверные логин или пароль");
            }

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home"); 
        }
        
    }
}