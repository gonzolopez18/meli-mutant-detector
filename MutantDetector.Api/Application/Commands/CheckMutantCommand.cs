using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MutantDetector.Api.Application.Commands
{
    public class CheckMutantCommand : IRequest<bool>
    {
        public IEnumerable<string> dna { get; set; }

    }
    public class DnaValidator : AbstractValidator<CheckMutantCommand>
    {
        public DnaValidator()
        {
            RuleFor(m => m.dna)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Must(d => d.All(x => x.ToUpper().ToCharArray()
                        .All(c => "ACGT".Contains(c))))
                    .WithMessage("Sólo se acepta A - C - G -T.");

            RuleFor(m => m.dna)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .Must(d => d.All(x => x.Length == d.Count()))
                    .WithMessage("La matriz no es simétrica.");
        }
    }
}
