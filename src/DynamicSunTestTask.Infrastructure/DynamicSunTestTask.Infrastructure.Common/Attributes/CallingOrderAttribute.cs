namespace DynamicSunTestTask.Infrastructure.Common.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class CallingOrderAttribute : Attribute
{
    public int Index { get; set; }

    public CallingOrderAttribute(int index)
    {
        Index = index;
    }
}