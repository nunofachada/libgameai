using Xunit;
using LibGameAI.NGrams;

namespace TestNGrams;

public class NGramsTestClass
{
    [Fact]
    public void TestAF()
    {
        ActionFrequency<string> af = new ActionFrequency<string>();
        Assert.Equal(0, af.Total);
    }
}