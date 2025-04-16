namespace Units.CodeGenerator.ConsoleApp;

public record UnitOperation(
    Operator Operator,
    string OutputType,
    string OutputDefaultUnit,
    string SecondInputType,
    string SecondInputDefaultUnit);

public enum Operator
{
    Multiplication,
    Division,
}