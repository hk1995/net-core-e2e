using System;
using Fixie;

namespace LMS.Integration.Tests
{
    public class DeleteData : FixtureBehavior, ClassBehavior
    {
        public void Execute(Class context, Action next)
        {
            BaseSliceFixture.ResetCheckpoint();
            next();
        }

        public void Execute(Fixture context, Action next)
        {
            BaseSliceFixture.ResetCheckpoint();
            next();
        }
    }
}
