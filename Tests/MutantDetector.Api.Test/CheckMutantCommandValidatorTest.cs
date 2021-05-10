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
using FluentValidation;

namespace MutantDetector.Api.Test
{
    public class CheckMutantCommandValidatorTest
    {
        
        public CheckMutantCommandValidatorTest()
        {
           
        }

        [Fact]
        public async void CheckEmptyDnas()
        {
            CheckMutantCommandValidator validator = new CheckMutantCommandValidator();
            CheckMutantCommand checkCommand = new CheckMutantCommand() { dna = new List<string>() { } };
            string expected = "NotEmptyValidator";

            var result = validator.Validate(checkCommand);
            var errors = result.Errors.Select(e => e.ErrorCode).ToList();

            Assert.False(result.IsValid);
            Assert.Contains(expected, errors);
        }

        [Theory]
        [MemberData(nameof(Data))]
        public async void CheckDnas(List<string> dnamock, string expected)
        {
            CheckMutantCommandValidator validator = new CheckMutantCommandValidator();
            CheckMutantCommand checkCommand = new CheckMutantCommand() { dna = dnamock };
            
            var result = validator.Validate(checkCommand);
            var errors = result.Errors.Select(e => e.ErrorMessage).ToList();

            Assert.False(result.IsValid);
            Assert.Contains(expected, errors);
        }
        public static IEnumerable<object[]> Data =>
       new List<object[]>
       {
            new object[] { new List<string>() { "AAAA", "AGTA", "ATAC", "AACS" }, "Sólo se acepta A - C - G -T." },
            new object[] { new List<string>() { "ACGT", "CGTA", "GTAC", "TA" }, "La matriz no es simétrica."},
       };
 
    }
}
