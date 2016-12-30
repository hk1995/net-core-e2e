using System.ComponentModel.DataAnnotations;

namespace LMS.Core.Domain.Management
{
    public class User : Entity
    {
        private User(string authId)
        {
            AuthId = authId;
        }

        //Needed for EF
        private User () { }

        [MaxLength(100)]
        public string AuthId { get; private set; }

        public User Create(string authId)
        {
            return new User(authId);
        }
    }
}
