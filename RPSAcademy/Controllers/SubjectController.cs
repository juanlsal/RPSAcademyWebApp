using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RPSAcademy.Data;
using RPSAcademy.Factories;
using RPSAcademy.Models;
using RPSAcademy.Models.DTOs;
using RPSAcademy.Repository;

namespace RPSAcademy.Controllers
{
    public class SubjectController : Controller
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly UserManager<ApplicationUser>  _userManager;
        private readonly IUserCreatedSubjectsFactory _userCreatedSubjectsFactory;
        private readonly ApplicationDbContext _context;

        public SubjectController(ISubjectRepository subjectRepository, UserManager<ApplicationUser> userManger, IUserCreatedSubjectsFactory userCreatedSubjectsFactory, ApplicationDbContext context)
        {
            _subjectRepository = subjectRepository;
            _userManager = userManger;
            _userCreatedSubjectsFactory = userCreatedSubjectsFactory;
            _context = context;
        }
        public async Task<IActionResult> GetAllSubjects()
        {
            var defaultSubjects = await _subjectRepository.GetDefaultSubjects();

            var user = await _userManager.GetUserAsync(User);
            if (user is not null)
            {
                var userId = user.Id;

                var usersCreatedSubjects = await _subjectRepository.GetUsersCreatedSubjects(userId);

                var displayModel1 = new SubjectsAndQuestionsDisplayModel
                {
                    DefaultSubjects = defaultSubjects,
                    UserCreatedSubjects = usersCreatedSubjects,

                };
                return View("GetAllSubjects", displayModel1);

            }


            var displayModel2 = new SubjectsAndQuestionsDisplayModel
            {
                DefaultSubjects = defaultSubjects
            };

            return View("GetAllSubjects", displayModel2);
        }


        [Authorize(Roles = "User")]
        public IActionResult CreateUserCreatedSubjectForm()
        {
            return View();
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> CreateUserCreatedSubject(string subjectName, string description)
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;

            var newUserCreatedSubject = _userCreatedSubjectsFactory.CreateUserCreatedSubject(userId, subjectName, description);

            _context.UserCreatedSubjects.Add(newUserCreatedSubject);
            _context.SaveChanges();


            return RedirectToAction("GetAllSubjects");

        }

        [Authorize(Roles = "User")]
        public IActionResult DeleteUserCreatedSubject(int subjectId)
        {
            var userCreatedSubject = _context.UserCreatedSubjects.FirstOrDefault(u => u.Id == subjectId);

            _context.UserCreatedSubjects.Remove(userCreatedSubject);
            _context.SaveChanges();

            var questionRelatedToSubject = _context.UserCreatedQuestions.Where(s => s.UserCreatedSubjectId == subjectId);
            foreach (var question in questionRelatedToSubject)
            {
                _context.UserCreatedQuestions.Remove(question);
                _context.SaveChanges();

            }
            _context.SaveChanges();

            return RedirectToAction("GetAllSubjects");
        }


        [Authorize(Roles = "User")]
        public IActionResult GetUserCreatedSubject(int subjectId)
        {
            var userCreatedSubject = _context.UserCreatedSubjects.FirstOrDefault(u => u.Id == subjectId);

            return View(userCreatedSubject);
        }

        public IActionResult UpdateUserCreatedSubject(int subjectId, string newSubjectName, string newDescription)
        {
            var userCreatedSubject = _context.UserCreatedSubjects.FirstOrDefault(u => u.Id == subjectId);
            if (userCreatedSubject.SubjectName != newSubjectName)
            {
                userCreatedSubject.SubjectName = newSubjectName;
                _context.Update(userCreatedSubject);
                _context.SaveChanges();
            }

            if (userCreatedSubject.Description != newDescription)
            {
                userCreatedSubject.Description = newDescription;
                _context.Update(userCreatedSubject);
                _context.SaveChanges();
            }

            return RedirectToAction("GetAllSubjects");
        }
    }
}