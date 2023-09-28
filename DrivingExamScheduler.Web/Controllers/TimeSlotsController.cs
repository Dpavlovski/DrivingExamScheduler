using DrivingExamScheduler.Domain.Models.Domain;
using DrivingExamScheduler.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DrivingExamScheduler.Web.Controllers
{
    public class TimeSlotsController : Controller
    {
        private readonly ITimeSlotService _timeSlotService;
        private readonly ILocationService _locationService;

        public TimeSlotsController(ITimeSlotService timeSlotService, ILocationService locationService)
        {
            _timeSlotService = timeSlotService;
            _locationService = locationService;
        }

        public IActionResult Index()
        {
            return View(_timeSlotService.ListAllTimeSlots());
        }

        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeSlot = _timeSlotService.GetTimeSlot(id);

            return View(timeSlot);
        }

        public IActionResult Create()
        {
            ViewData["LocationId"] = new SelectList(_locationService.ListAllLocations(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TimeSlot timeSlot)
        {
            _timeSlotService.CreateNewTimeSlot(timeSlot);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeSlot = _timeSlotService.GetTimeSlot(id);
            ViewData["LocationId"] = new SelectList(_timeSlotService.ListAllTimeSlots(), "Id", "Name", timeSlot.LocationId);
            return View(timeSlot);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, TimeSlot timeSlot)
        {
            if (id != timeSlot.Id)
            {
                return NotFound();
            }


            try
            {
                _timeSlotService.EditTimeSlot(timeSlot);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TimeSlotExists(timeSlot.Id))
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

        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeSlot = _timeSlotService.GetTimeSlot(id);

            return View(timeSlot);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var timeSlot = _timeSlotService.GetTimeSlot(id);
            if (timeSlot != null)
            {
                _timeSlotService.DeleteTimeSlot(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool TimeSlotExists(Guid id)
        {
            return _timeSlotService.GetTimeSlot(id) != null ? true : false;
        }
    }
}
