using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LMS.Core.ValueObjects;

namespace LMS.Core.Domain.Enrollment
{
    public class Curriculum : Entity
    {
        private ICollection<CurriculumUser> _curriculumUsers;

        private Curriculum () { }

        private Curriculum(string title, string description, Recurrence recurrence)
        {
            Title = title;
            Description = description;
            Recurrence = recurrence;
        }

        [MaxLength(100)]
        public string Title { get; private set; }
        [MaxLength(1000)]
        public string Description { get; private set; }
        public Recurrence Recurrence { get; private set; }
        public bool IsApproved { get; private set; }

        public ICollection<CurriculumUser> CurriculumUsers
        {
            get { return _curriculumUsers ?? (_curriculumUsers = new List<CurriculumUser>()); }
            protected set { _curriculumUsers = value; }
        }

        public static Curriculum Create(string title, string description, Recurrence recurrence)
        {
            return new Curriculum(title, description, recurrence);
        }

        public void Approve()
        {
            IsApproved = true;
        }

        public void Revoke()
        {
            IsApproved = false;
        }

        public void UpdateRecurrence(Recurrence recurrence)
        {
            Recurrence = recurrence;
        }

        /// <summary>
        /// Returns a new collection of CurriculumUsers that are NOT part of this CurriculumUsers collection
        /// </summary>
        /// <param name="userIds">User entity ids</param>
        /// <returns>ICollection CurriculumUser</returns>
        public ICollection<CurriculumUser> AddUsers(IEnumerable<long> userIds)
        {
            if (userIds == null)
                return CurriculumUsers;

            var newUsers = new List<CurriculumUser>();
            foreach (var userId in userIds)
            {
                if (CurriculumUsers.All(x => x.UserId != userId))
                    newUsers.Add(new CurriculumUser(Id, userId));
            }

            return newUsers;
        }

        /// <summary>
        /// Updates this collection of CurriculumUsers that are NOT part of this CurriculumUsers collection
        /// </summary>
        /// <param name="curriculumUsers">ICollection CurriculumUser</param>
        public void UpdateUserCollection(ICollection<CurriculumUser> curriculumUsers)
        {
            if (curriculumUsers == null || !curriculumUsers.Any())
                return;

            foreach (var curriculumUser in curriculumUsers)
            {
                if (CurriculumUsers.All(x => x.UserId != curriculumUser.UserId))
                    CurriculumUsers.Add(curriculumUser);
            }
        }

        /// <summary>
        /// Returns a collection of this CurriculumUsers that are NOT part of the inputted user ids
        /// </summary>
        /// <param name="userIds">User entity ids</param>
        /// <returns>ICollection CurriculumUser</returns>
        public ICollection<CurriculumUser> RemoveUsers(IEnumerable<long> userIds)
        {
            if (userIds == null || CurriculumUsers.Count < 1)
                return null;

            var catIds = userIds.ToList();
            var deleteUsers = new List<CurriculumUser>();
            foreach (var vc in CurriculumUsers)
            {
                if (catIds.All(x => x != vc.UserId))
                    deleteUsers.Add(vc);
            }

            return deleteUsers;
        }
    }
}
