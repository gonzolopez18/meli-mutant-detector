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

        [Fact]
        public async void ChechHuman()
        {
            CheckMutantCommand checkCommand = GetCommandHuman();
            bool IsMutant = false;

            _processor.Setup(x => x.isMutant(checkCommand.dna)).Returns(IsMutant);

            Dna dna = new Dna() { DnaSecuence = checkCommand.dna.ToString(), IsMutant = IsMutant };

            _repository.Setup(x => x.AddAsync(It.IsAny<Dna>())).ReturnsAsync(true);

            var result = await _sut.Handle(checkCommand, new System.Threading.CancellationToken());

            Assert.False(result);
            _repository.VerifyAll();
            _processor.VerifyAll();
        }

        [Fact]
        public async void ChechMutant()
        {
            CheckMutantCommand checkCommand = GetCommandMutant();
            bool IsMutant = true;

            _processor.Setup(x => x.isMutant(checkCommand.dna)).Returns(IsMutant);
            _mediator
                .Setup(m => m.Send(It.IsAny<DnaProcessedEvent>(), It.IsAny<CancellationToken>()))
                    .Verifiable("Notification was not sent.");

            Dna dna = new Dna() { DnaSecuence = string.Join("|", checkCommand.dna.ToArray()), IsMutant = IsMutant };

            _repository.Setup(x => x.AddAsync(It.IsAny<Dna>())).ReturnsAsync(true);

            var result = await _sut.Handle(checkCommand, new System.Threading.CancellationToken());

            Assert.True(result);
            _repository.VerifyAll();
            _processor.VerifyAll();
        }

        private CheckMutantCommand GetCommandHuman()
        {
            List<string> dnamock = new List<string>() { "ACGT", "CGTA", "GTAC", "TACG" };

            return new CheckMutantCommand() {  dna = dnamock};
        }
        private CheckMutantCommand GetCommandMutant()
        {
            List<string> dnamock = new List<string>() { "AAAA", "AGTA", "ATAC", "AACG" };

            return new CheckMutantCommand() { dna = dnamock };
        }
    }
}
