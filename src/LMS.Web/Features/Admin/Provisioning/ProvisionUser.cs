using System.Threading.Tasks;
using LMS.Core.Domain.Management;
using LMS.Core.Extensions;
using LMS.Data;
using MediatR;

namespace LMS.Web.Features.Admin.Provisioning
{
    public class ProvisionUser : IAsyncRequest<Auth0AppMetaData>
    {
        public ProvisionUser(Auth0UserViewModel model)
        {
            AuthId = model.user_id;
        }

        public string AuthId { get; private set; }

        public class Handler : IAsyncRequestHandler<ProvisionUser, Auth0AppMetaData>
        {
            private readonly LmsContext _db;

            public Handler(LmsContext db)
            {
                _db = db;
            }

            public async Task<Auth0AppMetaData> Handle(ProvisionUser cmd)
            {
                var existingUser = await _db.Users
                    .FirstActiveAsync(x => x.AuthId == cmd.AuthId)
                    .ConfigureAwait(false);

                var metaData = new Auth0AppMetaData();
                if (existingUser != null)
                {
                    metaData.userId = existingUser.Id;
                    return metaData;
                }

                var user = User.Create(cmd.AuthId);
                _db.Users.Add(user);
                _db.BeginTransaction();
                await _db.CommitTransactionAsync().ConfigureAwait(false);

                metaData.userId = user.Id;
                return metaData;
            }
        }
    }
}
