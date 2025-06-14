namespace ASP.NetCoreMVC_SchoolSystem.Services
{
    public class TeacherService
    {
        private readonly SchoolDbContext _dbContext;
        public TeacherService(SchoolDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
