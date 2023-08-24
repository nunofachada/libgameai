using Xunit;
using System.Collections.Generic;
using LibGameAI.NaiveBayes;

namespace TestNaiveBayes;

public class TestAttrib
{
    [Fact]
    public void TestAnAttrib()
    {
        Attrib a = new Attrib("temp", new List<string>());
    }
}