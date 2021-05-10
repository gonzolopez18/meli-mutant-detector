using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MutantDetector.Api.Application.Commands;
using MutantDetector.Controllers;
using System;
using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace MutantDetector.Api.Test
{
    public class MutantControllerTest
    {
        private readonly mutantController _sut;
        private readonly Mock<IMediator> _mediator = new Mock<IMediator>();

        public MutantControllerTest()
        {
            _sut = new mutantController(
                _mediator.Object);
        }

        [Theory]
        [MemberData(nameof(Data))]
        public async void CheckDnas(List<string> dnamock, bool IsMutant, string expected)
        {
            CheckMutantCommand checkCommand = new CheckMutantCommand() { dna = dnamock };

            _mediator
                .Setup(m => m.Send(It.IsAny<CheckMutantCommand>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(IsMutant).Verifiable();

            var result = await _sut.Post(checkCommand);
            StatusCodeResult statusCodeResult = result as StatusCodeResult;

            Assert.Equal(expected, statusCodeResult.StatusCode.ToString());
            _mediator.VerifyAll();
        }

        [Fact]
        public async void CheckException()
        {
            CheckMutantCommand checkCommand = new CheckMutantCommand() { dna = new List<string>() { "AAAA", "AGTA", "ATAC", "AACG" } };
            string expected = "403";

            _mediator
                .Setup(m => m.Send(It.IsAny<CheckMutantCommand>(), It.IsAny<CancellationToken>()))
                    .Throws(new Exception()).Verifiable();

            var result = await _sut.Post(checkCommand);
            StatusCodeResult statusCodeResult = result as StatusCodeResult;

            Assert.Equal(expected, statusCodeResult.StatusCode.ToString());
            _mediator.VerifyAll();
        }

        public static IEnumerable<object[]> Data =>
       new List<object[]>
       {
            new object[] { new List<string>() { "AAAA", "AGTA", "ATAC", "AACG" }, true, "200" },
            new object[] { new List<string>() { "ACGT", "CGTA", "GTAC", "TACG" }, false, "403" },
       };

    }
}
