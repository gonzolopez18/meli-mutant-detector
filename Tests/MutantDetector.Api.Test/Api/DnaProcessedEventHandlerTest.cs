using MutantDetector.Api.Application.Queries;
using MutantDetector.Domain.AggregatesModel.Stats;
using System;
using Xunit;
using Moq;
using MutantDetector.Api.Application.Model;
using MutantDetector.Api.Application.DomainEventHandlers;
using MutantDetector.Domain.DomainEvents;

namespace MutantDetector.Api.Test
{
    //public class DnaProcessedEventHandlerTest
    //{
    //    private readonly DnaProcessedEventHandler _sut;
    //    private readonly Mock<IStatsRepository> _repository = new Mock<IStatsRepository>();

    //    public DnaProcessedEventHandlerTest()
    //    {
    //        _sut = new DnaProcessedEventHandler(_repository.Object);
    //    }

    //    [Fact]
    //    public async void Stats()
    //    {
    //        bool isMutant = false;
    //        DnaProcessedEvent notification = new DnaProcessedEvent(isMutant);
    //        _repository.Setup(x => x.AddStatAsync(isMutant))
    //            .Verifiable();

    //        await _sut.Handle(notification, new System.Threading.CancellationToken());

    //        _repository.VerifyAll();
    //    }

    //}
}
