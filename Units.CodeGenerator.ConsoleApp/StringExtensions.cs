namespace Units.CodeGenerator.ConsoleApp;

internal static class StringExtensions
{
    public static string ParameterName(this string str) => char.ToLower(str[0]) + str.Substring(1);

    public static string ClassName(this string str) => char.ToUpper(str[0]) + str.Substring(1);

    public static string PropertyName(this string str) => char.ToUpper(str[0]) + str.Substring(1);
}