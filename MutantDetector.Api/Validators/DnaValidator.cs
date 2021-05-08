using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MutantDetector.Domain;
using MutantDetector.Domain.AggregatesModel;

namespace MutantDetector.Api.Validators
{
    public class DnaValidator : AbstractValidator<Dna>
    {
    }
}
