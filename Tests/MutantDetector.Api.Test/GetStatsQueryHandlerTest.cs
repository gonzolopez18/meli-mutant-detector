using MutantDetector.Api.Application.Queries;
using MutantDetector.Domain.AggregatesModel.Stats;
using System;
using Xunit;
using Moq;
using MutantDetector.Api.Application.Model;

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

        [Fact]
        public async void Stats()
        {
            GetStatsQuery StatsQuery = GetQuery();
            Stats statMocked = new Stats(5, 10);
            StatsView view = new StatsView(5, 10, (decimal)0.5);
            _repository.Setup(x => x.GetStats()).ReturnsAsync(statMocked).Verifiable();

            var result = await _sut.Handle(StatsQuery, new System.Threading.CancellationToken());

            Assert.Equal(statMocked.ratio, result.ratio);
            _repository.VerifyAll();
        }

        private GetStatsQuery GetQuery()
        {
            return new GetStatsQuery();
        }
    }
}
