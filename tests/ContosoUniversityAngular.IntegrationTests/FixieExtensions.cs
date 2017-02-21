namespace ContosoUniversityAngular.IntegrationTests
{
    using Fixie.Conventions;
    using System.Linq;
    using System.Reflection;

    public static class FixieExtensions
    {
        public static ClassExpression ClassNameIsBddStyleOrEndsWithTests(this ClassExpression expression)
        {
            return expression.Where(type => type.Name.EndsWith("Tests") || type.Name.StartsWith("When"));
        }

        public static ClassExpression ConstructorHasArguments(this ClassExpression expression)
        {
            return expression.Where(type => type.GetConstructors(BindingFlags.Public | BindingFlags.Instance)
                                                .Any(x => x.GetParameters().Any()));
        }

        public static ClassExpression ConstructorDoesntHaveArguments(this ClassExpression expression)
        {
            return expression.Where(type => type.GetConstructors(BindingFlags.Public | BindingFlags.Instance)
                                                .All(x => x.GetParameters().Length == 0));
        }
    }
}
