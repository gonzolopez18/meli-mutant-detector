using MutantDetector.Api.Application.Queries;
using MutantDetector.Domain.AggregatesModel.Stats;
using System;
using Xunit;
using Moq;
using MutantDetector.Api.Application.Model;
using System.Collections.Generic;

namespace MutantDetector.Api.Test
{
    public class GetStatsQueryHandlerTest
    {
        private readonly GetStatsQueryHandler _sut;
        private readonly Mock<IStatsRepository> _repository = new Mock<IStatsRepository>();

        public GetStatsQueryHandlerTest()
        {
            _sut = new GetStatsQueryHandler(_repository.Object);
        }

        [Theory]
        [MemberData(nameof(System.Data))]
        public async void Stats( int mutants, int humans, decimal expectedRatio)
        {
            GetStatsQuery StatsQuery = new GetStatsQuery();

            Stats statMocked = new Stats() {  count_mutant_dna = mutants, count_human_dna = humans };
            _repository.Setup(x => x.GetStatsAsync()).ReturnsAsync(statMocked).Verifiable();

            var result = await _sut.Handle(StatsQuery, new System.Threading.CancellationToken());

            Assert.Equal(expectedRatio, result.ratio);
            _repository.VerifyAll();
        }


        public static IEnumerable<object[]> Data =>
    new List<object[]>
    {
                        new object[] { 5, 10, 0.5 },
                        new object[] { 10, 0, 0 },
                        new object[] { 10, 5, 2 }
    };
    }
}
