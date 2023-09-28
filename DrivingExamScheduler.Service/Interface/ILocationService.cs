using DrivingExamScheduler.Domain.Models.Domain;

namespace DrivingExamScheduler.Service.Interface
{
    public interface ILocationService
    {
        public List<Location> ListAllLocations();

        public Location GetLocation(Guid? id);

        public Location CreateNewLocation(Location location);

        public Location EditLocation(Location location);

        public Location DeleteLocation(Guid id);
    }
}
