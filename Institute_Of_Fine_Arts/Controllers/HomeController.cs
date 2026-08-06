using Institute_Of_Fine_Arts.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Institute_Of_Fine_Arts.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _Context;
        private readonly IWebHostEnvironment _root;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(ApplicationDbContext context, IWebHostEnvironment root, UserManager<IdentityUser> userManager)
        {
            _Context = context;
            _root = root;
            _userManager = userManager;
        }

        

        public IActionResult Index()
        {
            return View();
        }

        // Competitions

        public IActionResult Competitions()
        {
            var competitions = _Context.Competitions.Include(res => res.award).ThenInclude(s => s.Student ).ToList();

            ViewBag.upcoming =  competitions.Where(c => c.Status == "UpComming").ToList();
            ViewBag.ongoing = competitions.Where(c => c.Status == "OnGoing").ToList();
            ViewBag.past = competitions.Where(c => c.Status == "Completed").ToList();

            return View(competitions);
        }
            
        public IActionResult CreateCompetitions()
        {
            ViewBag.awards = _Context.Awards.ToList(); 

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompetitions(Competition competition, IFormFile competitionimage)
        {
            ViewBag.awards = await _Context.Awards.ToListAsync();

            if (ModelState.IsValid && competitionimage != null && competitionimage.Length > 0)
            {
                var rootPath = _root.WebRootPath;
                var location = Path.Combine(rootPath, "Uploads", "images");

                if (!Directory.Exists(location))
                {
                    Directory.CreateDirectory(location);
                }

                var fileLocation = Path.Combine(location, competitionimage.FileName);

                using (var stream = new FileStream(fileLocation, FileMode.Create))
                {
                    await competitionimage.CopyToAsync(stream);
                }

                competition.Banner = competitionimage.FileName;
                await _Context.Competitions.AddAsync(competition);
                await _Context.SaveChangesAsync();

                return RedirectToAction("staff");
            }

                return View();
        }

        public async Task<IActionResult> UpdateCompetitions(int id)
        {
            ViewBag.awards = await _Context.Awards.ToListAsync();

            var competition = await _Context.Competitions.FindAsync(id);
            HttpContext.Session.SetString("previouseimage", competition!.Banner!);

            return View(competition);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCompetitions(Competition competition, IFormFile competitionimage)
        {
            ViewBag.awards = await _Context.Awards.ToListAsync();

            if (competitionimage != null)
            {
                var rootPath = _root.WebRootPath;
                var location = Path.Combine(rootPath, "Uploads", "images");

                if (!Directory.Exists(location))
                {
                    Directory.CreateDirectory(location);
                }

                var previouseimage = HttpContext.Session.GetString("previouseimage");
                var oldfilelocaation = Path.Combine(location, previouseimage!);

                if (System.IO.File.Exists(oldfilelocaation))
                {
                    System.IO.File.Delete(oldfilelocaation);
                }

                var newfilelocation = Path.Combine(location, competitionimage.FileName!);

                using (var stream = new FileStream(newfilelocation, FileMode.Create))
                {
                    await competitionimage.CopyToAsync(stream);
                }

                competition.Banner = competitionimage.FileName;
                _Context.Entry(competition).State = EntityState.Modified;
                await _Context.SaveChangesAsync();

                return RedirectToAction("staff");
            }
            else
            {
                var previouseImage = HttpContext.Session.GetString("previouseimage");
                competition.Banner = previouseImage;

                _Context.Entry(competition).State = EntityState.Modified;
                await _Context.SaveChangesAsync();
                return RedirectToAction("staff");
            }
        }

        public async Task<IActionResult> DeleteCompetitions(int id)
        {
            var competition = await _Context.Competitions.FindAsync(id);
            _Context.Competitions.Remove(competition!);
            await _Context.SaveChangesAsync();

            return RedirectToAction("staff");
        }

        // Competitions End

        // Exhibitions

        public IActionResult Exhibitions()
        {
            var exhibitions = _Context.Exhibitions.ToList();

            ViewBag.upcoming = exhibitions.Where(e => e.Status == "UpComming").ToList();
            ViewBag.ongoing = exhibitions.Where(e => e.Status == "OnGoing").ToList();
            ViewBag.past = exhibitions.Where(e => e.Status == "Completed").ToList();

            return View(exhibitions);
        }

        public IActionResult CreateExhibitions()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateExhibitions(Exhibition exhibition, IFormFile exhibitionimage)
        {
            if (ModelState.IsValid && exhibitionimage != null && exhibitionimage.Length > 0)
            {
                var rootPath = _root.WebRootPath;
                var location = Path.Combine(rootPath, "Uploads", "images");

                if (!Directory.Exists(location))
                {
                    Directory.CreateDirectory(location);
                }

                var fileLocation = Path.Combine(location, exhibitionimage.FileName);

                using (var stream = new FileStream(fileLocation, FileMode.Create))
                {
                    await exhibitionimage.CopyToAsync(stream);
                }

                exhibition.Banner = exhibitionimage.FileName;
                await _Context.Exhibitions.AddAsync(exhibition);
                await _Context.SaveChangesAsync();

                return RedirectToAction("staff");
            }
            return View();
        }

        public async Task<IActionResult> UpdateExhibitions(int id)
        {
            var exhibition = await _Context.Exhibitions.FindAsync(id);
            HttpContext.Session.SetString("previouseimage", exhibition!.Banner!);

            return View(exhibition);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateExhibitions(Exhibition exhibition, IFormFile exhibitionimage)
        {
            if (exhibitionimage != null)
            {
                var rootPath = _root.WebRootPath;
                var location = Path.Combine(rootPath, "Uploads", "images");

                if (!Directory.Exists(location))
                {
                    Directory.CreateDirectory(location);
                }

                var previouseimage = HttpContext.Session.GetString("previouseimage");
                var oldfilelocaation = Path.Combine(location, previouseimage!);

                if (System.IO.File.Exists(oldfilelocaation))
                {
                    System.IO.File.Delete(oldfilelocaation);
                }

                var newfilelocation = Path.Combine(location, exhibitionimage.FileName!);

                using (var stream = new FileStream(newfilelocation, FileMode.Create))
                {
                    await exhibitionimage.CopyToAsync(stream);
                }

                exhibition.Banner = exhibitionimage.FileName;
                _Context.Entry(exhibition).State = EntityState.Modified;
                await _Context.SaveChangesAsync();

                return RedirectToAction("staff");
            }
            else
            {
                var previouseImage = HttpContext.Session.GetString("previouseimage");
                exhibition.Banner = previouseImage;

                _Context.Entry(exhibition).State = EntityState.Modified;
                await _Context.SaveChangesAsync();
                return RedirectToAction("staff");
            }
        }

        public async Task<IActionResult> DeleteExhibitions(int id)
        {
            var exhibition = await _Context.Exhibitions.FindAsync(id);
            _Context.Exhibitions.Remove(exhibition!);
            await _Context.SaveChangesAsync();

            return RedirectToAction("staff");
        }

        // Exhibitions End

        // Awards

        public IActionResult Awards()
        {
            var awards = _Context.Awards.ToList();
            return View(awards);
        }

        public async Task<IActionResult> CreateAwards()
        {
            var students = await _userManager.GetUsersInRoleAsync("student");
            ViewBag.students = students;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAwards(Award award)
        {
            var students = await _userManager.GetUsersInRoleAsync("Student");
            ViewBag.students = students;

            if (ModelState.IsValid)
            {
                await _Context.Awards.AddAsync(award);
                await _Context.SaveChangesAsync();

                return RedirectToAction("Staff");
            }

            return View();
        }

        public async Task<IActionResult> UpdateAwards(int id)
        {
            var students = await _userManager.GetUsersInRoleAsync("student");
            ViewBag.students = students;

            var award = await _Context.Awards.FindAsync(id);
            return View(award);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAwards(Award award)
        {
            var students = await _userManager.GetUsersInRoleAsync("student");
            ViewBag.students = students;

            if (ModelState.IsValid)
            {
                _Context.Awards.Update(award);
                await _Context.SaveChangesAsync();

                return RedirectToAction("Staff");
            }

            return View();
        }

        public async Task<IActionResult> DeleteAwards(int id)
        {
            var award = await _Context.Awards.FindAsync(id);
            _Context.Awards.Remove(award);
            await _Context.SaveChangesAsync();

            return RedirectToAction("Staff");
        }

        // Awards End

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult SubmitPaintings()
        {
            ViewData["UserId"] = _userManager.GetUserId(User);
            TempData["competitions"] = _Context.Competitions.Where(w => w.Status == "OnGoing").ToList();
            return View();
        }

        [HttpPost]
        public IActionResult SubmitPaintings(Painting painting, IFormFile paintingimage)
        {
            ViewData["UserId"] = _userManager.GetUserId(User);
            TempData["competitions"] = _Context.Competitions.Where(w => w.Status == "OnGoing").ToList();

            if (ModelState.IsValid && paintingimage != null && paintingimage.Length > 0)
            {
                var rootPath = _root.WebRootPath;
                var location = Path.Combine(rootPath, "Uploads", "paintings");
                if (!Directory.Exists(location))
                {
                    Directory.CreateDirectory(location);
                }
                var fileLocation = Path.Combine(location, paintingimage.FileName);
                using (var stream = new FileStream(fileLocation, FileMode.Create))
                {
                    paintingimage.CopyToAsync(stream);
                }
                painting.PaintingImage = paintingimage.FileName;
                _Context.Paintings.Add(painting);
                _Context.SaveChanges();
                return RedirectToAction("ViewPaintings");
            }

            return View();
        }

        public IActionResult ViewPaintings()
        {
            return View();
        }

        public async Task<IActionResult> Student()
        {
            var model = new ModelsCollectionVeiewModel
            {
                Awards = await _Context.Awards.Include(user => user.Student).ToListAsync(),
                Competitions = await _Context.Competitions.Include(res => res.award).ToListAsync(),
                Exhibitions = await _Context.Exhibitions.ToListAsync()
                //Competitions = await _Context.Competitions.ToListAsync()
            };

            return View(model);
        }

        public async Task<IActionResult> Staff()
        {
            var model = new ModelsCollectionVeiewModel
            {
                Awards = await _Context.Awards.Include(user => user.Student).ToListAsync(),
                Competitions = await _Context.Competitions.Include(res => res.award).ToListAsync(),
                Exhibitions = await _Context.Exhibitions.ToListAsync()
                //Competitions = await _Context.Competitions.ToListAsync()
            };

            return View(model);
        }

        public async Task<IActionResult> Manager()
        {
            var users = _userManager.Users.ToList();
            var competitions = await _Context.Competitions.Include(res => res.award).ThenInclude(s => s.Student).ToListAsync();
            var awards = await _Context.Awards.Include(user => user.Student).ToListAsync();

            ViewBag.students = await _userManager.GetUsersInRoleAsync("Student");
            ViewBag.staff = await _userManager.GetUsersInRoleAsync("Staff");
            ViewBag.competitions = competitions;
            ViewBag.awards = awards;

            var exhibitions = _Context.Exhibitions.ToList();
            ViewBag.upcoming = exhibitions.Where(e => e.Status == "UpComming").ToList();

            return View(users);
        }

        // Admin

        public async Task<IActionResult> Admin()
        {
            var users = _userManager.Users.ToList();

            ViewBag.students = await _userManager.GetUsersInRoleAsync("Student");
            ViewBag.staff = await _userManager.GetUsersInRoleAsync("Staff");

            return View(users);
        }

        public async Task<IActionResult> EditUser(string id)
        {
            var user =  await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(IdentityUser model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);

            if (user == null)
                return NotFound();

            user.UserName = model.UserName;
            user.Email = model.Email;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
                return RedirectToAction("Admin");

            return BadRequest("Unable to Update User");
        }

        public async Task<IActionResult> DeleteUser(string id) {
            var user =  await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            var result = await _userManager.DeleteAsync(user!);

            if (result.Succeeded)
                return RedirectToAction("Admin");

            return View(user);
        }

        // Admin End
    }
}
