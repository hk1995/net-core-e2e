using Fixie;

namespace LMS.Integration.Tests
{
    public class FullSliceConvention : Convention
    {
        public FullSliceConvention()
        {
            var env = Options<CliArgs>();

            Classes
                .IsBddStyleClassNameOrEndsWithTests()
                .HasNoClassAttributes();

            ClassExecution
                .CreateInstancePerCase();

            Methods
                .Where(method => method.IsPublic);

            Parameters.Add(
                mi =>
                    (mi.GetParameters().Length == 1) &&
                    (mi.GetParameters()[0].ParameterType == typeof(FullSliceFixture))
                        ? new[] { new[] { new FullSliceFixture(env.Env) } }
                        : null);

            FixtureExecution
                .Wrap<DeleteData>();
        }
    }
}
