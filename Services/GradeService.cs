namespace ASP.NetCoreMVC_SchoolSystem.Services
{
    public class GradeService
    {
        private readonly SchoolDbContext _dbContext;

        public GradeService(SchoolDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
