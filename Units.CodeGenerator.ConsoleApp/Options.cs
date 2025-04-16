namespace Units.CodeGenerator.ConsoleApp;

[Flags]
public enum Options
{
    None = 0,
    ImplementsBaseValue = 1,
    WithAddition = 2,
    WithSubtraction = 3,
    WithMultiplicationByNumber = 8,
    WithDivisionByNumber = 16,
    WithDivisionBySameValue = 32,
    WithZero = 64,
}