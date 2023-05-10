using DataLayer.Repositories;

namespace DataLayer
{
    public class UnitOfWork
    {
        public UsersRepository Users { get; }
        public RolesRepository Roles { get; }
        public TripsRepository Trips { get; }

        private readonly AppDbContext _dbContext;

        public UnitOfWork
        (
            AppDbContext dbContext,
            UsersRepository users,
            RolesRepository roles,
            TripsRepository trips

        )
        {
            _dbContext = dbContext;
            Users = users;
            Roles = roles;
            Trips = trips;
        }

        public void SaveChanges()
        {
            try
            {
                _dbContext.SaveChanges();
            }
            catch(Exception exception)
            {
                var errorMessage = "Error when saving to the database: "
                    + $"{exception.Message}\n\n"
                    + $"{exception.InnerException}\n\n"
                    + $"{exception.StackTrace}\n\n";

                Console.WriteLine(errorMessage);
            }
        }
    }
}
