namespace LMS.Core.Enums
{
    public class RecurrenceType : Enumeration
    {
        public static readonly RecurrenceType FixedDateAnnualRecurring = new RecurrenceType(1, "FixedDateAnnualRecurring");
        public static readonly  RecurrenceType DynamicDateRecurring = new RecurrenceType(2, "DynamicDateRecurring");
        public static readonly RecurrenceType HireDateRecurring = new RecurrenceType(3, "HireDateRecurring");
        public static readonly RecurrenceType BirthDateRecurring = new RecurrenceType(4, "BirthDateRecurring");
        public static readonly RecurrenceType NewHireNonRecurring = new RecurrenceType(5, "NewHireNonRecurring");
        public static readonly RecurrenceType FixedNonRecurring = new RecurrenceType(6, "FixedNonRecurring");
        public static readonly RecurrenceType OneTimeOnly = new RecurrenceType(7, "OneTimeOnly");
        public static readonly RecurrenceType Custom = new RecurrenceType(8, "Custom");

        private RecurrenceType(int value, string displayName) : base(value, displayName)
        {            
        }
    }
}
