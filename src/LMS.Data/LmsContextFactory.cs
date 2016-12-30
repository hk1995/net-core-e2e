using System.Data.Entity.Infrastructure;

namespace LMS.Data
{
    public class LmsContextFactory : IDbContextFactory<LmsContext>
    {
        public LmsContext Create()
        {
            return new LmsContext("Data Source=(localdb)\\MSSQLLocalDB;Database=LMS;Integrated Security=True");
        }
    }
}
