using Moq;
using MutantDetector.Domain.AggregatesModel;
using MutantDetector.Infraestructure.Dapper;
using MutantDetector.Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using Xunit;
using Moq.Dapper;
using System.Data;
using Dapper;

namespace MutantDetector.Infraestructure.Api
{
    public class DnaRepositoryTest
    {
        private readonly string _mockedConnString = @"Server=SERVERNAME;Database=TESTDB;Integrated Security=true;";
        private readonly Mock<IDapperWrapper> mockDapper = new Mock<IDapperWrapper>();
        private readonly DnaRepository _sut;
        public DnaRepositoryTest()
        {
            _sut = new DnaRepository(_mockedConnString, mockDapper.Object);
        }

        [Fact]
        public async void AddAsync()
        {

            Dna dna = new Dna() { DnaSecuence = "AAAA", IsMutant = true };

            var dictionary = new Dictionary<string, object>
                        {
                            { "@DnaSecuence", dna.DnaSecuence },
                            { "@IsMutant", dna.IsMutant },
                        };

            string sqlQuery = @"INSERT INTO [dbo].[Dna]
                                            (ID, [DnaSecuence],[IsMutant])
                                        VALUES(NEWID(), @DnaSecuence , @IsMutant)";

            mockDapper.Setup(t => t.ExecuteAsync(It.Is<IDbConnection>(db => 
                        db.ConnectionString == _mockedConnString), sqlQuery, dictionary))
                        .ReturnsAsync(1);

            var result = await _sut.AddAsync(dna);


            Assert.True(result);
        }

        [Fact]
        public async void AddAsyncThrowsError()
        {

            Dna dna = new Dna() { DnaSecuence = "AAAA", IsMutant = true };

            var dictionary = new Dictionary<string, object>
                        {
                            { "@DnaSecuence", dna.DnaSecuence },
                            { "@IsMutant", dna.IsMutant },
                        };

            string sqlQuery = @"INSERT INTO [dbo].[Dna]
                                            (ID, [DnaSecuence],[IsMutant])
                                        VALUES(NEWID(), @DnaSecuence , @IsMutant)";

            mockDapper.Setup(t => t.ExecuteAsync(It.Is<IDbConnection>(db =>
                        db.ConnectionString == _mockedConnString), sqlQuery, dictionary))
                        .ThrowsAsync(new Exception()).Verifiable();

            await Assert.ThrowsAsync<Exception>(async () => await _sut.AddAsync(dna));

        }

    }
}
