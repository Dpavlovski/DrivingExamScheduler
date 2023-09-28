using DrivingExamScheduler.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DrivingExamScheduler.Web.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly IAppointmentService _appointmentService;


        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        public IActionResult Index()
        {

            return View(_appointmentService.ListAllAppointments());
        }

        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = _appointmentService.GetAppointment(id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        [HttpPost]
        public IActionResult Create(Guid examId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!_appointmentService.UserMeetsRequirements(userId, examId))
            {
                return RedirectToAction("CandidateProfile", "Account");
            }
            _appointmentService.CreateNewAppointment(userId, examId);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult CheckRequirements()
        {
            return View(_appointmentService.ListAllAppointments());
        }

        [HttpPost]
        public IActionResult Approve(Guid? id)
        {
            _appointmentService.ApproveAppointment(id);
            return RedirectToAction("CheckRequirements");
        }


        [HttpPost]
        public IActionResult Reject(Guid? id, string rejectionReason)
        {
            _appointmentService.RejectAppointment(id, rejectionReason);
            return RedirectToAction("CheckRequirements");
        }



        private bool AppointmentExists(Guid id)
        {
            return _appointmentService.GetAppointment(id) != null ? true : false;
        }


    }
}
