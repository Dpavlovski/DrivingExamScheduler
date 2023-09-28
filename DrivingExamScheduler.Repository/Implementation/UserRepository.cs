using DrivingExamScheduler.Domain.Models.Identity;
using DrivingExamScheduler.Repository.Data;
using DrivingExamScheduler.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace DrivingExamScheduler.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Candidate> entities;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
            entities = _context.Set<Candidate>();
        }
        public Candidate Get(string id)
        {
            return entities
                .Include(z => z.CurrentCategory)
                .Include(z => z.Appointment)
                .Include(z => z.Documents)
                .SingleOrDefault(z => z.Id == id);

        }

        public IEnumerable<Candidate> GetAll()
        {
            return entities.Include(z => z.CurrentCategory)
                .Include(z => z.Appointment)
                .Include(z => z.Documents)
                .AsEnumerable();
        }
    }
}