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

                if (model.Email == "admin@gmail.com")
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

                        return RedirectToAction("Login", "Home");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            Console.WriteLine($"Code: {error.Code}, Description: {error.Description}");
                        }
                    }
                }
            }

            TempData["roles"] = _roleManager.Roles.Where(res => res.Name != "Admin").ToList();

            return View(model);
        }
    }
}
