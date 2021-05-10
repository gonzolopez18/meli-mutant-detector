using MutantDetector.Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MutantDetector.Infraestructure.Test
{
    public class StatsInMemoryRepositoryTest
    {
        private readonly StatsInMemoryRepository _repository;

        public StatsInMemoryRepositoryTest()
        {
            _repository = new StatsInMemoryRepository();

        }

        [Theory]
        [MemberData(nameof(System.Data))]
        public async void AddStatAsync(bool isMutant, int expectedMutants, int expectedHumans)
        {
            await _repository.AddStatAsync(isMutant);
            var result = await _repository.GetStatsAsync();

            Assert.Equal(expectedMutants, result.count_mutant_dna);
            Assert.Equal(expectedHumans, result.count_human_dna);
        }



    public static IEnumerable<object[]> Data =>
        new List<object[]>
        {
                        new object[] { true, 1, 0 },
                        new object[] { false, 0, 1 }
        };
}
}
