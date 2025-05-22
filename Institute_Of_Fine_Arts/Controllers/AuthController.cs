using Institute_Of_Fine_Arts.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Institute_Of_Fine_Arts.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email!);

            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, model.Password!, model.RememberMe, false);

                if (result.Succeeded)
                {
                    var roles = await _userManager.GetRolesAsync(user);

                    if (roles.Contains("Admin"))
                        return RedirectToAction("Admin", "Home");

                    if (roles.Contains("Admin"))
                        return RedirectToAction("Manager", "Home");

                    if (roles.Contains("Staff"))
                        return RedirectToAction("Staff", "Home");

                    if (roles.Contains("Student"))
                        return RedirectToAction("Student", "Home");

                    return RedirectToAction("Index", "Home"); // fallback
                }
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }

        public IActionResult Register()
        {
            TempData["roles"] = _roleManager.Roles.Where(res => res.Name != "Admin").ToList();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!(await _roleManager.RoleExistsAsync("Admin")))
                {
                    IdentityRole role = new()
                    {
                        Name = "Admin"
                    };

                    await _roleManager.CreateAsync(role);
                }

                if (!(await _roleManager.RoleExistsAsync("Manager")))
                {
                    IdentityRole role = new()
                    {
                        Name = "Manager"
                    };

                    await _roleManager.CreateAsync(role);
                }

                if (!(await _roleManager.RoleExistsAsync("Staff")))
                {
                    IdentityRole role = new()
                    {
                        Name = "Staff"
                    };

                    await _roleManager.CreateAsync(role);
                }

                if (!(await _roleManager.RoleExistsAsync("Student")))
                {
                    IdentityRole role = new()
                    {
                        Name = "Student"
                    };

                    await _roleManager.CreateAsync(role);
                }

                IdentityUser user = new()
                {
                    UserName = model.FullName,
                    Email = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Password!);

                if (model.Email == "admin@finearts.com")
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                }
                else
                {
                    if (result.Succeeded)
                    {
                        var role = await _roleManager.FindByIdAsync(model.Role!);

                        if (role != null)
                        {
                            await _userManager.AddToRoleAsync(user, role.Name!);
                        }

                        return RedirectToAction("Login", "Auth");
                    }
                }
            }

            TempData["roles"] = _roleManager.Roles.Where(res => res.Name != "Admin").ToList();

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
