using MutantDetector.Infraestructure.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace MutantDetector.Infraestructure.Api
{
    public class DnaProcessorTest
    {
        private readonly DnaProcessor _processor;

        public DnaProcessorTest()
        {
            _processor = new DnaProcessor();
        }

        [Theory]
        [MemberData(nameof(Data))]
        public async void TestIsMutant(List<string> dna, bool expected)
        {
            var result = _processor.isMutant(dna);

            Assert.Equal(expected, result);

        }

        public static IEnumerable<object[]> Data =>
            new List<object[]>
            {
                        new object[] { new List<string>() { "CAAAC", "GAAGA", "AACAT", "TTAAA", "TACAG" }, true },
                        new object[] { new List<string>() { "GGGG", "GGGG", "GGGG", "GGGG", "GGGG" }, true},
                        new object[] { new List<string>() { "GGGG", "GAAGA", "GACAT", "GTAAA", "TACAG" }, true},
                        new object[] { new List<string>() { "CAAAC", "GAAGA", "AGCGT", "TTAAA", "TACAG" }, false},
                        new object[] { new List<string>() { "CAATC", "GAAGA", "AACAT", "TTGAA", "TACAG" }, false},
            };
        }
}
