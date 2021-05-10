using MutantDetector.Api.Application.Commands;
using MutantDetector.Domain.AggregatesModel;
using System;
using Xunit;
using Moq;
using MutantDetector.Api.Application.Model;
using System.Collections.Generic;
using MediatR;
using MutantDetector.Domain.DomainEvents;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace MutantDetector.Api.Test
{
    public class CheckMutantCommandHandlerTest
    {
        private readonly CheckMutantCommandHandler _sut;
        private readonly Mock<IDnaRepository> _repository = new Mock<IDnaRepository>();
        private readonly Mock<IDnaProcessor> _processor = new Mock<IDnaProcessor>();
        private readonly Mock<IMediator> _mediator = new Mock<IMediator>();

        public CheckMutantCommandHandlerTest()
        {
            _sut = new CheckMutantCommandHandler(_repository.Object,
                _processor.Object, 
                _mediator.Object);
        }

        [Theory]
        [MemberData(nameof(Data))]
        public async void CheckDnas(List<string> dnamock, bool expected)
        {
            CheckMutantCommand checkCommand = new CheckMutantCommand() { dna = dnamock };
            bool IsMutant = expected;

            _processor.Setup(x => x.isMutant(checkCommand.dna)).Returns(IsMutant);
            _mediator
                .Setup(m => m.Publish(It.IsAny<DnaProcessedEvent>(), It.IsAny<CancellationToken>()))
                    .Verifiable();

            Dna dna = new Dna() { DnaSecuence = string.Join("|", checkCommand.dna.ToArray()), IsMutant = IsMutant };

            _repository.Setup(x => x.AddAsync(It.IsAny<Dna>())).ReturnsAsync(true);

            var result = await _sut.Handle(checkCommand, new System.Threading.CancellationToken());

            Assert.Equal(IsMutant, result);
            _repository.VerifyAll();
            _processor.VerifyAll();
            _mediator.VerifyAll();
        }
        public static IEnumerable<object[]> Data =>
       new List<object[]>
       {
            new object[] { new List<string>() { "AAAA", "AGTA", "ATAC", "AACG" }, true },
            new object[] { new List<string>() { "ACGT", "CGTA", "GTAC", "TACG" }, false },
       };
 
    }
}
