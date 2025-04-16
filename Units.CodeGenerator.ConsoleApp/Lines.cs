using System.Collections.Immutable;

namespace Units.CodeGenerator.ConsoleApp;

internal class Lines
{
    private readonly string _indent;
    List<string> _lines = new List<string>();

    public Lines(string indent)
    {
        _indent = indent;
    }

    public Lines Add(string line)
    {
        _lines.Add(line);
        return this;
    }

    public Lines AddEmptyLine(bool when = true)
    {
        if (!when)
        {
            return this;
        }

        _lines.Add(string.Empty);
        return this;
    }

    public Lines Add(ushort indentCount, string line, bool when = true)
    {
        if (!when)
        {
            return this;
        }

        var indents = string.Join(string.Empty, Enumerable.Range(1, (int)indentCount).Select(_ => _indent));
        _lines.Add($"{indents}{line}");
        return this;
    }

    public Lines AddRange(ushort indentCount, IImmutableList<string> lines)
    {
        foreach (var line in lines)
        {
            Add(indentCount, line);
        }

        return this;
    }

    public IImmutableList<string> ToIImmutableList()
    {
        return _lines.ToImmutableList();
    }

    public bool Any()
    {
        return _lines.Any();
    }
}