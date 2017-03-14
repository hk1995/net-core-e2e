using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [Column(TypeName = "datetime2")]
        // ReSharper disable once InconsistentNaming
        public DateTime DOB { get; private set; }

        [Column(TypeName = "datetime2")]
        public DateTime HireDate { get; private set; }

        public static User Create(string authId)
        {
            return new User(authId);
        }

        public void EditUser(DateTime dob, DateTime hireDate)
        {
            DOB = dob;
            HireDate = hireDate;
        }
    }
}
