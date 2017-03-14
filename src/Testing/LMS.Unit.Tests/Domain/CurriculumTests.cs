using System.Collections.Generic;
using System.Linq;
using LMS.Core.Domain.Enrollment;
using LMS.Core.Domain.Management;
using LMS.Core.ValueObjects;
using Shouldly;

namespace LMS.Unit.Tests.Domain
{
    public class CurriculumTests
    {
        public void Curriculum_should_return_correct_CurriculumUsers_on_UpdateUserCollection_when_current_curriculum_is_null()
        {
            //Arrange
            var recurrence = Recurrence.CreateFixedNonRecurring();
            var curriculum = Curriculum.Create("title", "description", recurrence);

            var user1 = User.Create("auth0-1");
            var user2 = User.Create("auth0-2");
            var user3 = User.Create("auth0-3");
            user1.Id = 1;
            user2.Id = 2;
            user3.Id = 3;
            var curriculumUsers = new List<CurriculumUser>
            {
                new CurriculumUser(curriculum, user1),
                new CurriculumUser(curriculum, user2),
                new CurriculumUser(curriculum, user3)
            };

            //Act
            curriculum.UpdateUserCollection(curriculumUsers);

            //Assert
            curriculum.CurriculumUsers.Count.ShouldBe(3);
            curriculum.CurriculumUsers.Any(x => x.UserId == 1).ShouldBeTrue();
        }

        public void Curriculum_should_return_correct_CurriculumUsers_on_UpdateUserCollection_when_current_curriculum_is_NOT_null()
        {
            //Arrange
            var recurrence = Recurrence.CreateFixedNonRecurring();
            var curriculum = Curriculum.Create("title", "description", recurrence);

            var user1 = User.Create("auth0-1");
            var user2 = User.Create("auth0-2");
            var user3 = User.Create("auth0-3");
            user1.Id = 1;
            user2.Id = 2;
            user3.Id = 3;
            var curriculumUsers = new List<CurriculumUser>
            {
                new CurriculumUser(curriculum, user1),
                new CurriculumUser(curriculum, user2),
                new CurriculumUser(curriculum, user3)
            };
            curriculum.UpdateUserCollection(curriculumUsers);

            //Act
            var user4 = User.Create("auth0-4");
            user4.Id = 4;
            var curriculumUsers2 = new List<CurriculumUser>
            {
                new CurriculumUser(curriculum, user1),
                new CurriculumUser(curriculum, user2),
                new CurriculumUser(curriculum, user4)
            };
            curriculum.UpdateUserCollection(curriculumUsers2);

            //Assert
            curriculum.CurriculumUsers.Count.ShouldBe(4);
            curriculum.CurriculumUsers.Any(x => x.UserId == 1).ShouldBeTrue();
            curriculum.CurriculumUsers.Any(x => x.UserId == 4).ShouldBeTrue();
        }

        public void Curriculum_should_return_correct_CurriculumUsers_on_AddUsers_when_current_curriculum_is_null()
        {
            //Arrange
            var recurrence = Recurrence.CreateFixedNonRecurring();
            var curriculum = Curriculum.Create("title", "description", recurrence);
            var userIds = new long[] { 1, 2, 3 };

            //Act
            var newUsers = curriculum.AddUsers(userIds);

            //Assert
            newUsers.Count.ShouldBe(3);
        }

        public void Curriculum_should_return_correct_CurriculumUsers_on_AddUsers_when_current_curriculum_is__NOT_null()
        {
            //Arrange
            var recurrence = Recurrence.CreateFixedNonRecurring();
            var curriculum = Curriculum.Create("title", "description", recurrence);

            var user1 = User.Create("auth0-1");
            var user2 = User.Create("auth0-2");
            var user3 = User.Create("auth0-3");
            user1.Id = 1;
            user2.Id = 2;
            user3.Id = 3;
            var curriculumUsers = new List<CurriculumUser>
            {
                new CurriculumUser(curriculum, user1),
                new CurriculumUser(curriculum, user2),
                new CurriculumUser(curriculum, user3)
            };
            curriculum.UpdateUserCollection(curriculumUsers);

            //Act
            var useregoryIds = new long[] { 1, 2, 4 };
            var newUsers = curriculum.AddUsers(useregoryIds);

            //Assert
            newUsers.Count.ShouldBe(1);
            newUsers.Any(x => x.UserId == 1).ShouldBeFalse();
            newUsers.Any(x => x.UserId == 4).ShouldBeTrue();
            newUsers.Any(x => x.UserId == 9).ShouldBeFalse();
        }

        public void Curriculum_should_return_correct_CurriculumUsers_on_RemoveUsers_when_current_curriculum_is_null()
        {
            //Arrange
            var recurrence = Recurrence.CreateFixedNonRecurring();
            var curriculum = Curriculum.Create("title", "description", recurrence);
            var useregoryIds = new long[] { 1, 2, 3 };

            //Act
            var deleteUsers = curriculum.RemoveUsers(useregoryIds);

            //Assert
            deleteUsers.ShouldBeNull();
        }

        public void Curriculum_should_return_correct_CurriculumUsers_on_RemoveUsers_when_current_curriculum_is_NOT_null()
        {
            //Arrange
            var recurrence = Recurrence.CreateFixedNonRecurring();
            var curriculum = Curriculum.Create("title", "description", recurrence);

            var user1 = User.Create("auth0-1");
            var user2 = User.Create("auth0-2");
            var user3 = User.Create("auth0-3");
            user1.Id = 1;
            user2.Id = 2;
            user3.Id = 3;
            var curriculumUsers = new List<CurriculumUser>
            {
                new CurriculumUser(curriculum, user1),
                new CurriculumUser(curriculum, user2),
                new CurriculumUser(curriculum, user3)
            };
            curriculum.UpdateUserCollection(curriculumUsers);

            //Act
            var useregoryIds = new long[] { 1, 3 };
            var deleteUsers = curriculum.RemoveUsers(useregoryIds);

            //Assert
            deleteUsers.ShouldNotBeNull();
            deleteUsers.Count.ShouldBe(1);
            deleteUsers.Any(x => x.UserId == 1).ShouldBeFalse();
            deleteUsers.Any(x => x.UserId == 2).ShouldBeTrue();
        }
    }
}
