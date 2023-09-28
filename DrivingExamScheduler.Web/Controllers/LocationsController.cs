using DrivingExamScheduler.Domain.Models.Domain;
using DrivingExamScheduler.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DrivingExamScheduler.Web.Controllers
{
    public class LocationsController : Controller
    {
        private readonly ILocationService _locationService;

        public LocationsController(ILocationService locationService)
        {
            _locationService = locationService;
        }


        public IActionResult Index()
        {
            return View(_locationService.ListAllLocations());
        }

        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = _locationService.GetLocation(id);

            return View(location);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Location location)
        {
            _locationService.CreateNewLocation(location);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = _locationService.GetLocation(id);
            return View(location);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, Location location)
        {
            if (id != location.Id)
            {
                return NotFound();
            }

            try
            {
                _locationService.EditLocation(location);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationExists(location.Id))
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

            var location = _locationService.GetLocation(id);

            return View(location);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var location = _locationService.GetLocation(id);
            if (location != null)
            {
                _locationService.DeleteLocation(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool LocationExists(Guid id)
        {
            return _locationService.GetLocation(id) != null ? true : false;
        }
    }
}
