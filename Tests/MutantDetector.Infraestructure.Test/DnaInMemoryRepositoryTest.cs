using MutantDetector.Domain.AggregatesModel;
using MutantDetector.Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MutantDetector.Infraestructure.Test
{
    public class DnaInMemoryRepositoryTest
    {
        private readonly DnaInMemoryRepository _repository;

        public DnaInMemoryRepositoryTest()
        {
            _repository = new DnaInMemoryRepository();

        }

        [Fact]
        public async void AddAsync()
        {
            bool expected = true;

            Dna dna = new Dna();

            var result = await _repository.AddAsync(dna);

            Assert.True(expected);
        }



    public static IEnumerable<object[]> Data =>
        new List<object[]>
        {
                        new object[] { true, 1, 0 },
                        new object[] { false, 0, 1 }
        };
}
}
