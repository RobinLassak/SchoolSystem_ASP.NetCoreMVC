namespace ASP.NetCoreMVC_SchoolSystem.Services
{
    public class StudentService
    {
        private readonly SchoolDbContext _dbcontext;
        public StudentService(SchoolDbContext dbContext)
        {
            _dbcontext = dbContext;
        }
    }
}
