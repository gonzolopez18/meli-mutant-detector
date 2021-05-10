using MediatR;
using Moq;
using MutantDetector.Api.Application.Commands;
using MutantDetector.Controllers;
using MutantDetector.Domain.DomainEvents;
using System.Collections.Generic;
using System.Threading;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System;
using MutantDetector.Api.Application.Queries;
using MutantDetector.Api.Application.Model;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace MutantDetector.Api.Test
{
    public class StatsControllerTest
    {
        private readonly statsController _sut;
        private readonly Mock<IMediator> _mediator = new Mock<IMediator>();

        public StatsControllerTest()
        {
            _sut = new statsController(
                _mediator.Object);
        }

        [Fact]
        public async void CheckStats()
        {
            string expected = "200";
            GetStatsQuery getquery = new GetStatsQuery();
            StatsView statitics = new StatsView( 1, 1, 1);

            _mediator
                .Setup(m => m.Send(It.IsAny<GetStatsQuery>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(statitics).Verifiable();

            ActionResult<StatsView> actionResult = await _sut.GetStats();

            var result = actionResult.Result as OkObjectResult;
            
            Assert.Equal(expected, result.StatusCode.ToString());
            _mediator.VerifyAll();
        }

        [Fact]
        public async void CheckStatsException()
        {
            string expected = "500";
            GetStatsQuery getquery = new GetStatsQuery();
            StatsView statitics = new StatsView(1, 1, 1);

            _mediator
                .Setup(m => m.Send(It.IsAny<GetStatsQuery>(), It.IsAny<CancellationToken>()))
                    .Throws(new Exception()).Verifiable();

            var result = await _sut.GetStats();

            IStatusCodeActionResult statusCodeResult = result.Result as IStatusCodeActionResult;
            
            Assert.Equal(expected, statusCodeResult.StatusCode.ToString());
            _mediator.VerifyAll();
        }

    }
}
