using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LMS.Core.Domain.Management;
using LMS.Core.Enums;

namespace LMS.Core.ValueObjects
{
    public class Recurrence : ValueObject<Recurrence>
    {
        private Recurrence () { }

        private Recurrence(string type, DateTime startDate, int daysAfterDueDateToRepeat, bool isRecurring)
        {
            Type = type;
            StartDate = startDate;
            DaysAfterDueDateToRepeat = daysAfterDueDateToRepeat;
            IsRecurring = isRecurring;
        }

        [MaxLength(100)]
        public string Type { get; private set; }
        [Column(TypeName = "datetime2")]
        public DateTime StartDate { get; private set; }
        public int DaysAfterDueDateToRepeat { get; private set; }
        public bool IsRecurring { get; private set; }

        public static Recurrence CreateSpecificRecurringDate(RecurrenceType recurrenceType, DateTime startDate)
        {
            if(!recurrenceType.Equals(RecurrenceType.FixedDateAnnualRecurring) || !recurrenceType.Equals(RecurrenceType.DynamicDateRecurring))
                throw new ArgumentException("Incorrect recurrence type for this method", nameof(recurrenceType));

            return new Recurrence(recurrenceType.DisplayName, startDate, 0, true);
        }

        public static Recurrence CreateUserSpecificRecurringDate(User user, RecurrenceType recurrenceType, DateTime startDate)
        {
           if(recurrenceType.Equals(RecurrenceType.HireDateRecurring))
                return new Recurrence(recurrenceType.DisplayName, user.HireDate, 0, true);
           if(recurrenceType.Equals(RecurrenceType.BirthDateRecurring))
                return new Recurrence(recurrenceType.DisplayName, user.DOB, 0, true);

            throw new ArgumentException("Incorrect recurrence type for this method", nameof(recurrenceType));
        }

        public static Recurrence CreateUserSpecificNonRecurringDate(User user, RecurrenceType recurrenceType, DateTime startDate)
        {
            if (recurrenceType.Equals(RecurrenceType.HireDateRecurring))
                return new Recurrence(recurrenceType.DisplayName, user.HireDate, 0, false);
            if (recurrenceType.Equals(RecurrenceType.BirthDateRecurring))
                return new Recurrence(recurrenceType.DisplayName, user.DOB, 0, false);

            throw new ArgumentException("Incorrect recurrence type for this method", nameof(recurrenceType));
        }

        public static Recurrence CreateFixedNonRecurring()
        {
            return new Recurrence(RecurrenceType.FixedNonRecurring.DisplayName, DateTime.UtcNow, 0, false);
        }

        public static Recurrence CreateCustom(DateTime startDate, int daysAfterDueDateToRepeat)
        {
            return new Recurrence(RecurrenceType.Custom.DisplayName, startDate, daysAfterDueDateToRepeat, true);
        }
    }
}
