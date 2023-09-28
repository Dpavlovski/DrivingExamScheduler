using DrivingExamScheduler.Domain.DTO;
using DrivingExamScheduler.Domain.Models.Identity;
using DrivingExamScheduler.Repository.Interface;
using DrivingExamScheduler.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DrivingExamScheduler.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Candidate> userManager;
        private readonly SignInManager<Candidate> signInManager;
        private readonly IDocumentTypeService _documentTypeService;
        private readonly IUserRepository _userRepository;

        public AccountController(UserManager<Candidate> userManager,
            SignInManager<Candidate> signInManager, IDocumentTypeService documentTypeService, IUserRepository userRepository)
        {

            this.userManager = userManager;
            this.signInManager = signInManager;
            _documentTypeService = documentTypeService;
            _userRepository = userRepository;
        }

        [HttpGet, AllowAnonymous]
        public IActionResult Register()
        {
            CandidateRegistrationDto model = new();
            return View(model);
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Register(CandidateRegistrationDto request)
        {
            if (ModelState.IsValid)
            {
                var userCheck = await userManager.FindByEmailAsync(request.Email);
                if (userCheck == null)
                {
                    var user = new Candidate
                    {
                        Name = request.Name,
                        EMBG = request.EMBG,
                        Age = Candidate.CalculateAgeFromEMBG(request.EMBG),
                        Address = request.Address,
                        UserName = request.Email,
                        NormalizedUserName = request.Email,
                        Email = request.Email,
                        PhoneNumber = request.PhoneNumber,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        DrivingSchool = request.DrivingSchool
                    };
                    var result = await userManager.CreateAsync(user, request.Password);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        if (result.Errors.Count() > 0)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("message", error.Description);
                            }
                        }
                        return View(request);
                    }
                }
                else
                {
                    ModelState.AddModelError("message", "Email already exists.");
                    return View(request);
                }
            }
            return View(request);

        }


        [HttpGet, AllowAnonymous]
        public IActionResult Login()
        {
            CandidateLoginDto model = new();
            return View(model);
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(CandidateLoginDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null && !user.EmailConfirmed)
                {
                    ModelState.AddModelError("message", "Email not confirmed yet");
                    return View(model);

                }
                if (await userManager.CheckPasswordAsync(user, model.Password) == false)
                {
                    ModelState.AddModelError("message", "Invalid credentials");
                    return View(model);

                }

                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, true, true);

                if (result.Succeeded)
                {
                    await userManager.AddClaimAsync(user, new Claim("UserRole", "Admin"));
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("message", "Invalid login attempt");
                    return View(model);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("login", "account");
        }

        public IActionResult CandidateProfile()
        {
            var user = _userRepository.Get(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            if (user == null)
            {
                return NotFound();
            }

            ViewData["DocumentTypes"] = _documentTypeService.ListAllDocumentTypes();
            return View(user);
        }

    }
}