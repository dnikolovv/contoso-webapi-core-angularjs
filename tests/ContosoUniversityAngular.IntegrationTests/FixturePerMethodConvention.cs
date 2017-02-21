namespace ContosoUniversityAngular.IntegrationTests
{
    using Fixie;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class FixturePerMethodConvention : Convention
    {
        public FixturePerMethodConvention()
        {
            Classes
                .ClassNameIsBddStyleOrEndsWithTests()
                .ConstructorDoesntHaveArguments();

            ClassExecution
                .CreateInstancePerCase();

            Parameters.Add(
                methodInfo =>
                            (methodInfo.GetParameters().Length == 1) &&
                            (methodInfo.GetParameters()[0].ParameterType == typeof(SliceFixture))
                                ? new[] { new[] { new SliceFixture() } }
                                : null);

            FixtureExecution.Wrap<DeleteData>();
        }
    }
}
