using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RPSAcademy.Constants;
using RPSAcademy.Data;
using RPSAcademy.Models.DTOs;
using RPSAcademy.Models;
using RPSAcademy.Repository;
using Microsoft.EntityFrameworkCore;
using RPSAcademy.Services;
using Microsoft.AspNetCore.Identity;
using RPSAcademy.Factories;
using System.ComponentModel;

namespace RPSAcademy.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPlayRepository _playRepository;
        private readonly ApplicationDbContext _context;
        private readonly IFileService _fileService;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IDefaultSubjectsFactory _defaultSubjectsFactory;
        private readonly IDefaultQuestionFactory _defaultQuestionFactory;
        private readonly IQuestionRepository _questionRepository;

        public AdminController
            (IPlayRepository playRepository, 
            ApplicationDbContext context, 
            IFileService fileService,   
            ISubjectRepository subjectRepository,
            UserManager<ApplicationUser> userManager, 
            IDefaultSubjectsFactory defaultSubjectsFactory,
            IDefaultQuestionFactory defaultQuestionFactory,
            IQuestionRepository questionRepository)
        {
            _playRepository = playRepository;
            _context = context;
            _fileService = fileService;
            _subjectRepository = subjectRepository;
            _userManager = userManager;
            _defaultSubjectsFactory = defaultSubjectsFactory;
            _defaultQuestionFactory = defaultQuestionFactory;
            _questionRepository = questionRepository;
        }

        public async Task<IActionResult> GetAllOpponents()
        {
            var opponents = await _playRepository.GetOpponents();

            var displayModel = new AdminDisplayModel
            {
                Opponents = opponents,
            };
            return View(displayModel);
        }
        public IActionResult GetOpponent(int opponentId)
        {
            var opponent = _context.Opponents.FirstOrDefault(u => u.id == opponentId);

            return View(opponent);
        }
        public IActionResult UpdateOpponent(int opponentId, string newName, string newDescription, IFormFile newOpponentImageFile)
        {
            var opponent = _context.Opponents.FirstOrDefault(u => u.id == opponentId);
            if (opponent.Name !=  newName)
            {
                opponent.Name = newName;
                _context.Update(opponent);
                _context.SaveChanges();
            }

            if (opponent.Description != newDescription)
            {
                opponent.Description = newDescription;
                _context.Update(opponent);
                _context.SaveChanges();
            }

            if (newOpponentImageFile != null)
            {
                var result = _fileService.SaveImageAdmin(newOpponentImageFile);
                if (result.Item1 == 1)
                {
                    var oldImage = opponent.ImageUrl;
                    opponent.ImageUrl = result.Item2;
                    _context.Update(opponent);
                    _context.SaveChanges();

                    if (oldImage is not null)
                    {
                        var deleteResult = _fileService.DeleteImageAdmin(oldImage);

                    }
                }
            }

            return RedirectToAction("GetOpponent", new { opponentId = opponentId });
        }
        public async Task<IActionResult> GetAllDefaultSubjects()
        {
            var defaultSubjects = await _subjectRepository.GetDefaultSubjects();

            var displayModel = new AdminDisplayModel
            {
                DefaultSubjects = defaultSubjects,
            };
            return View(displayModel);
        }
        public IActionResult CreateDefaultSubjectForm()
        {
            return View();
        }
        public async Task<IActionResult> CreateDefaultSubject(string subjectName, string description)
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;

            var newDeafultSubject = _defaultSubjectsFactory.CreateDefaultSubject(userId, subjectName, description);

            _context.DefaultSubjects.Add(newDeafultSubject);
            _context.SaveChanges();

            var defaultSubjects = await _subjectRepository.GetDefaultSubjects();

            var displayModel = new AdminDisplayModel
            {
                DefaultSubjects = defaultSubjects,
            };

            return View("GetAllDefaultSubjects", displayModel);
        }
        public IActionResult GetDefaultSubject(int subjectId)
        {
            var defaultSubject = _context.DefaultSubjects.FirstOrDefault(u => u.id == subjectId);

            return View(defaultSubject);
        }
        public async Task<IActionResult> UpdateDefaultSubject(int subjectId, string newSubjectName, string newDescription)
        {
            var defaultSubject = _context.DefaultSubjects.FirstOrDefault(u => u.id == subjectId);
            if (defaultSubject.SubjectName != newSubjectName)
            {
                defaultSubject.SubjectName = newSubjectName;
                _context.Update(defaultSubject);
                _context.SaveChanges();
            }

            if (defaultSubject.Description != newDescription)
            {
                defaultSubject.Description = newDescription;
                _context.Update(defaultSubject);
                _context.SaveChanges();
            }

            var defaultSubjectsList = await _subjectRepository.GetDefaultSubjects();

            var displayModel = new AdminDisplayModel
            {
                DefaultSubjects = defaultSubjectsList,
            };

            return View("GetAllDefaultSubjects", displayModel);
        }
        public async Task<IActionResult> DeleteDefaultSubject(int subjectId)
        {
            var defaultsubject = _context.DefaultSubjects.FirstOrDefault(s => s.id == subjectId);

            _context.DefaultSubjects.Remove(defaultsubject);
            _context.SaveChanges();
            var questionRelatedToSubject = _context.DefaultQuestions.Where(s => s.DefaultSubjectId == subjectId);
            foreach(var question in questionRelatedToSubject)
            {
                _context.DefaultQuestions.Remove(question);
                _context.SaveChanges();
            }
            _context.SaveChanges();
            var defaultSubjectsList = await _subjectRepository.GetDefaultSubjects();

            var displayModel = new AdminDisplayModel
            {
                DefaultSubjects = defaultSubjectsList,
            };

            return View("GetAllDefaultSubjects", displayModel);

        }
        public async Task<IActionResult> SelectDefaultSubjectToAddTo() 
        {
            var defaultSubjects = await _subjectRepository.GetDefaultSubjects();

            var displayModel = new AdminDisplayModel
            {
                DefaultSubjects = defaultSubjects,
            };

            return View(displayModel);
        }
        public IActionResult CreateDefaultQuestionForm(int subjectId)
        {
            var defaultSubject = _context.DefaultSubjects.FirstOrDefault(u => u.id == subjectId);

            return View(defaultSubject);
        }
        public IActionResult CreateDefaultQuestion(string question, string answerA, string answerB, string answerC, string answerD, int correctAnswer, int defaultSubjectId)
        {

            var newDefaultQuestion = _defaultQuestionFactory.CreateDefaultQuestion(question, answerA, answerB, answerC, answerD, correctAnswer, defaultSubjectId);

            _context.DefaultQuestions.Add(newDefaultQuestion);
            _context.SaveChanges();


            TempData["SuccessMessage"] = "Question was added successfully. Feel free to add More";

            var defaultSubject = _context.DefaultSubjects.FirstOrDefault(u => u.id == defaultSubjectId);


            return View("CreateDefaultQuestionForm", defaultSubject);
        }
        public async Task<IActionResult> ViewDefaultSubjects()
        {
            var defaultSubjects = await _subjectRepository.GetDefaultSubjects();

            var displayModel = new AdminDisplayModel
            {
                DefaultSubjects = defaultSubjects,
            };

            return View(displayModel);
        }
        public async Task<IActionResult> ViewDefaultQuestions(int subjectId)
        {
            var defaultQuestions = await _questionRepository.GetDefaultQuestions(subjectId);

            var defaultSubject = _context.DefaultSubjects.FirstOrDefault(u => u.id == subjectId);

            var displayModel = new AdminDisplayModel
            {
                DefaultQuestions = defaultQuestions,
                DefaultSubject = defaultSubject
            };

            return View(displayModel);
        }
        public async Task<IActionResult> DeleteDefaultQuestion(int questionId, int subjectId)
        {
            var defaultQuestion = _context.DefaultQuestions.FirstOrDefault(s => s.Id == questionId);

            _context.DefaultQuestions.Remove(defaultQuestion);
            _context.SaveChanges();

            var defaultSubject = _context.DefaultSubjects.FirstOrDefault(u => u.id == subjectId);

            var defaultQuestions = await _questionRepository.GetDefaultQuestions(subjectId);

            var displayModel = new AdminDisplayModel
            {
                DefaultQuestions = defaultQuestions,
                DefaultSubject = defaultSubject
            };

            return View("ViewDefaultQuestions", displayModel);
        }
        public IActionResult GetDefaultQuestion(int questionId)
        {
            var defaultQuestion = _context.DefaultQuestions.FirstOrDefault(u => u.Id == questionId);

            return View(defaultQuestion);
        }
        public async Task<IActionResult> UpdateDefaultQuestion(string newQuestion, string newAnswerA, string newAnswerB, string newAnswerC, string newAnswerD, int newCorrectAnswer, int questionId, int subjectId)
        {
            var defaultQuestion = _context.DefaultQuestions.FirstOrDefault(u => u.Id == questionId);
            if (defaultQuestion.Question != newQuestion)
            {
                defaultQuestion.Question = newQuestion;
                _context.Update(defaultQuestion);
                _context.SaveChanges();
            }

            if (defaultQuestion.AnswerA != newAnswerA)
            {
                defaultQuestion.AnswerA = newAnswerA;
                _context.Update(defaultQuestion);
                _context.SaveChanges();
            }
            if (defaultQuestion.AnswerB != newAnswerB)
            {
                defaultQuestion.AnswerB = newAnswerB;
                _context.Update(defaultQuestion);
                _context.SaveChanges();
            }
            if (defaultQuestion.AnswerC != newAnswerC)
            {
                defaultQuestion.AnswerC = newAnswerC;
                _context.Update(defaultQuestion);
                _context.SaveChanges();
            }
            if (defaultQuestion.AnswerD != newAnswerD)
            {
                defaultQuestion.AnswerD = newAnswerD;
                _context.Update(defaultQuestion);
                _context.SaveChanges();
            }
            switch (newCorrectAnswer)
            {
                case 1:
                    defaultQuestion.CorrectAnswerText = newAnswerA;
                    _context.Update(defaultQuestion);
                    _context.SaveChanges();

                    break;
                case 2:
                    defaultQuestion.CorrectAnswerText = newAnswerB;
                    _context.Update(defaultQuestion);
                    _context.SaveChanges();

                    break;
                case 3:
                    defaultQuestion.CorrectAnswerText = newAnswerC;
                    _context.Update(defaultQuestion);
                    _context.SaveChanges();

                    break;
                case 4:
                    defaultQuestion.CorrectAnswerText = newAnswerD;
                    _context.Update(defaultQuestion);
                    _context.SaveChanges();

                    break;
            }

            var defaultQuestions = await _questionRepository.GetDefaultQuestions(subjectId);

            var defaultSubject = _context.DefaultSubjects.FirstOrDefault(u => u.id == subjectId);

            var displayModel = new AdminDisplayModel
            {
                DefaultQuestions = defaultQuestions,
                DefaultSubject = defaultSubject
            };

            return RedirectToAction("ViewDefaultQuestions", new { subjectId });
        }
    }
}