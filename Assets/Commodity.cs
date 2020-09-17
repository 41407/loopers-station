public interface ICommodity
{
    string Name { get; }
    int Value { get; }
}

public class Commodity : ICommodity
{
    public string Name { get; }
    public int Value { get; }

    public Commodity(string name, int value)
    {
        Name = name;
        Value = value;
    }

    public override bool Equals(object obj) => (obj as ICommodity)?.Name == Name;

    public override int GetHashCode()
    {
        return Name.GetHashCode() + Value;
    }
}
