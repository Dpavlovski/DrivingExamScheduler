using DrivingExamScheduler.Domain.Models.Domain;
using DrivingExamScheduler.Domain.Models.Enum;
using DrivingExamScheduler.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DrivingExamScheduler.Web.Controllers
{
    public class ExamsController : Controller
    {
        private readonly IExamService _examService;
        private readonly ITimeSlotService _timeSlotService;
        private readonly ICategoryService _categoryService;

        public ExamsController(IExamService examService, ITimeSlotService timeSlotService, ICategoryService categoryService)
        {
            _examService = examService;
            _timeSlotService = timeSlotService;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            return View(_examService.ListAllExams());
        }

        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exam = _examService.GetExam(id);

            return View(exam);
        }

        public IActionResult Create()
        {
            ViewData["ExamTypes"] = new SelectList(Enum.GetValues(typeof(ExamType)));
            ViewData["TimeSlots"] = _timeSlotService.ListAllTimeSlots();
            ViewData["CategoryId"] = new SelectList(_categoryService.ListAllCategories(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Exam exam)
        {
            _examService.CreateNewExam(exam);
            return RedirectToAction(nameof(Index));

        }

        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exam = _examService.GetExam(id);
            ViewData["ExamTypes"] = new SelectList(Enum.GetValues(typeof(ExamType)));
            ViewData["TimeSlots"] = _timeSlotService.ListAllTimeSlots();
            ViewData["CategoryId"] = new SelectList(_categoryService.ListAllCategories(), "Id", "Name");
            return View(exam);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Exam exam)
        {
            if (id != exam.Id)
            {
                return NotFound();
            }

            try
            {
                _examService.EditExam(exam);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamExists(exam.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exam = _examService.GetExam(id);

            return View(exam);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {

            _examService.DeleteExam(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ExamExists(Guid id)
        {
            return _examService.GetExam(id) != null ? true : false;
        }



    }
}
