using System.Data.Entity;
using System.Threading.Tasks;
using LMS.Core.Domain.Management;
using LMS.Web.Features.Admin.Provisioning;
using Shouldly;

namespace LMS.Integration.Tests.Features.Admin
{
    public class ProvisionUserTests
    {
        public async Task Should_provision_new_user(FullSliceFixture fixture)
        {
            //Arrange
            var model = new Auth0UserViewModel {user_id = "abc-123"};
            var command = new ProvisionUser(model);

            //Act
            var metadata = await fixture.SendAsync(command);
            var created = await fixture.FindAsync<User>(metadata.userId);

            //Assert
            created.ShouldNotBeNull();
        }

        public async Task Should_provision_existing_user(FullSliceFixture fixture)
        {
            //Arrange
            var user = User.Create("abc-123");
            await fixture.InsertAsync(user);
            var created = await fixture.FindAsync<User>(user.Id);

            var model = new Auth0UserViewModel { user_id = "abc-123" };
            var command = new ProvisionUser(model);

            //Act
            var metadata = await fixture.SendAsync(command);
            var users = await fixture.ExecuteDbContextAsync(db => db.Users.ToListAsync());

            //Assert
            metadata.userId.ShouldBe(created.Id);
            users.Count.ShouldBe(1);
        }
    }
}
