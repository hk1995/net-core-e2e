using System;

namespace LMS.Data
{
    public static class DbInitialize
    {
        private static DateTime _now;

        public static void Seed(string connectionString = null)
        {
            _now = DateTime.UtcNow;

            using (var db = new LmsContext(connectionString))
            {
                
            }
        }
    }
}
