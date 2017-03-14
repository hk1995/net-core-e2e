using LMS.Core.Domain.Management;

namespace LMS.Core.Domain.Enrollment
{
    public class CurriculumUser : Entity
    {
        private CurriculumUser() { }

        public CurriculumUser(long curriculumId, long userId)
        {
            CurriculumId = curriculumId;
            UserId = userId;
        }

        public CurriculumUser(Curriculum curriculum, User user)
        {
            Curriculum = curriculum;
            CurriculumId = curriculum.Id;
            User = user;
            UserId = user.Id;
        }

        public long CurriculumId { get; private set; }
        public long UserId { get; private set; }
        public Curriculum Curriculum { get; private set; }
        public virtual User User { get; private set; }
    }
}
