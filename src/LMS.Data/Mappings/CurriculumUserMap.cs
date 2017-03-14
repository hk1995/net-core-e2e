using System.Data.Entity.ModelConfiguration;
using LMS.Core.Domain.Enrollment;

namespace LMS.Data.Mappings
{
    public class CurriculumUserMap : EntityTypeConfiguration<CurriculumUser>
    {
        public CurriculumUserMap()
        {
            HasRequired(e => e.Curriculum)
                .WithMany(c => c.CurriculumUsers)
                .HasForeignKey(e => e.CurriculumId);

            HasRequired(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId);
        }
    }
}
