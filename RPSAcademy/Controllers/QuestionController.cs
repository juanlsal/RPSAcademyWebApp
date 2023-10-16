using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RPSAcademy.Constants;
using RPSAcademy.Data;
using RPSAcademy.Factories;
using RPSAcademy.Models;
using RPSAcademy.Models.DTOs;
using RPSAcademy.Repository;

namespace RPSAcademy.Controllers
{
    public class QuestionController : Controller
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly ApplicationDbContext _context;
        private readonly IUserCreatedQuestionsFactory _userCreatedQuestionsFactory;

        public QuestionController(IQuestionRepository questionRepository, ISubjectRepository subjectRepository, ApplicationDbContext context, IUserCreatedQuestionsFactory userCreatedQuestionsFactory )
        {
            _questionRepository = questionRepository;
            _subjectRepository = subjectRepository;
            _context = context;
            _userCreatedQuestionsFactory = userCreatedQuestionsFactory;
        }

        public async Task<IActionResult> GetRelatedDefaultQuestions(int subjectId)
        {
            var relatedDefaultQuestions = await _questionRepository.GetDefaultQuestions(subjectId);
            var relatedDefaultSubject = _subjectRepository.GetRelatedDefaultSubject(subjectId);

            var displayModel = new SubjectsAndQuestionsDisplayModel
            {
                DefaultQuestions = relatedDefaultQuestions,
                DefaultSubject = relatedDefaultSubject
            };

            return View(displayModel);
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetRelatedUserCreatedQuestions(int subjectId)
        {
            var relatedUserCreatedQuestions = await _questionRepository.GetUserCreatedQuestions(subjectId);
            var relatedUserCreatedSubject = _subjectRepository.GetRelatedUserCreatedSubject(subjectId);


            

            if(relatedUserCreatedQuestions.Count() == 0)
            {

                return View("CreateUserCreatedQuestionForm", relatedUserCreatedSubject);
            }

            var displayModel = new SubjectsAndQuestionsDisplayModel
            {
                UserCreatedQuestions = relatedUserCreatedQuestions,
                UserCreatedSubject = relatedUserCreatedSubject
            };

            return View(displayModel);
        }



        [Authorize(Roles = "User")]
        public IActionResult CreateUserCreatedQuestionForm(int subjectId)
        {
            var relatedUserCreatedSubject = _subjectRepository.GetRelatedUserCreatedSubject(subjectId);

            return View(relatedUserCreatedSubject);
        }

        [Authorize(Roles = "User")]
        public IActionResult CreateUserCreatedQuestion(string question, string answerA, string answerB, string answerC, string answerD, int correctAnswer, int userCreatedSubjectId)
        {

            var newUserCreatedSubject = _userCreatedQuestionsFactory.CreateUserCreatedQuestion(question, answerA, answerB, answerC, answerD, correctAnswer, userCreatedSubjectId);

            _context.UserCreatedQuestions.Add(newUserCreatedSubject);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Question was added successfully. Feel free to add More";

            var userCreatedSubject = _context.UserCreatedSubjects.FirstOrDefault(u => u.Id == userCreatedSubjectId);


            return View("CreateUserCreatedQuestionForm", userCreatedSubject);


        }

        public IActionResult DeleteUserCreatedQuestion(int questionId, int subjectId)
        {
            var userCreatedQuestion = _context.UserCreatedQuestions.FirstOrDefault(u => u.Id == questionId);

            _context.UserCreatedQuestions.Remove(userCreatedQuestion);
            _context.SaveChanges();


            return RedirectToAction("GetRelatedUserCreatedQuestions", new {subjectId = subjectId});
        }
        
        public IActionResult GetUserCreatedQuestion(int questionId)
        {
            var userCreatedQuestion = _context.UserCreatedQuestions.FirstOrDefault(u => u.Id == questionId);


            return View(userCreatedQuestion);
        }

        public IActionResult UpdateUserCreatedQuestion(string newQuestion, string newAnswerA, string newAnswerB, string newAnswerC, string newAnswerD, int newCorrectAnswer, int questionId, int subjectId)
        {
            var userCreatedQuestion = _context.UserCreatedQuestions.FirstOrDefault(u => u.Id == questionId);
            if (userCreatedQuestion.Question != newQuestion)
            {
                userCreatedQuestion.Question = newQuestion;
                _context.Update(userCreatedQuestion);
                _context.SaveChanges();
            }
            if (userCreatedQuestion.AnswerA != newAnswerA)
            {
                userCreatedQuestion.AnswerA = newAnswerA;
                _context.Update(userCreatedQuestion);
                _context.SaveChanges();
            }
            if (userCreatedQuestion.AnswerB != newAnswerB)
            {
                userCreatedQuestion.AnswerB = newAnswerB;
                _context.Update(userCreatedQuestion);
                _context.SaveChanges();
            }
            if (userCreatedQuestion.AnswerC != newAnswerC)
            {
                userCreatedQuestion.AnswerC = newAnswerC;
                _context.Update(userCreatedQuestion);
                _context.SaveChanges();
            }
            if (userCreatedQuestion.AnswerD != newAnswerD)
            {
                userCreatedQuestion.AnswerD = newAnswerD;
                _context.Update(userCreatedQuestion);
                _context.SaveChanges();
            }
            switch (newCorrectAnswer)
            {
                case 1:
                    userCreatedQuestion.CorrectAnswerText = newAnswerA;
                    _context.Update(userCreatedQuestion);
                    _context.SaveChanges();

                    break;
                case 2:
                    userCreatedQuestion.CorrectAnswerText = newAnswerB;
                    _context.Update(userCreatedQuestion);
                    _context.SaveChanges();

                    break;
                case 3:
                    userCreatedQuestion.CorrectAnswerText = newAnswerC;
                    _context.Update(userCreatedQuestion);
                    _context.SaveChanges();

                    break;
                case 4:
                    userCreatedQuestion.CorrectAnswerText = newAnswerD;
                    _context.Update(userCreatedQuestion);
                    _context.SaveChanges();

                    break;
            }

            return RedirectToAction("GetRelatedUserCreatedQuestions", new { subjectId = subjectId });
        }
    }
}