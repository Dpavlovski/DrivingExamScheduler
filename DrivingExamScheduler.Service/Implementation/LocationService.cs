using DrivingExamScheduler.Domain.Models.Domain;
using DrivingExamScheduler.Repository.Interface;
using DrivingExamScheduler.Service.Interface;

namespace DrivingExamScheduler.Service.Implementation
{
    public class LocationService : ILocationService
    {
        private readonly IRepository<Location> _locationRepository;
        public LocationService(IRepository<Location> locationRepository)
        {
            _locationRepository = locationRepository;
        }
        public Location CreateNewLocation(Location location)
        {
            location.Id = Guid.NewGuid();
            return _locationRepository.Insert(location);
        }

        public Location DeleteLocation(Guid id)
        {
            var location = _locationRepository.Get(id);
            return _locationRepository.Delete(location);
        }

        public Location EditLocation(Location location)
        {

            return _locationRepository.Update(location);
        }

        public Location GetLocation(Guid? id)
        {
            return _locationRepository.Get(id);
        }

        public List<Location> ListAllLocations()
        {
            return _locationRepository.GetAll().ToList();
        }
    }

}
