using Moq;
using MutantDetector.Domain.AggregatesModel.Stats;
using MutantDetector.Infraestructure.Dapper;
using MutantDetector.Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using Xunit;

namespace MutantDetector.Infraestructure.Api
{
    public class StatsRepositoryTest
    {
        private readonly string _mockedConnString = @"Server=SERVERNAME;Database=TESTDB;Integrated Security=true;";
        private readonly Mock<IDapperWrapper> mockDapper = new Mock<IDapperWrapper>();
        private readonly StatsRepository _sut;
        public StatsRepositoryTest()
        {
            _sut = new StatsRepository(_mockedConnString, mockDapper.Object);
        }

        [Theory]
        [MemberData(nameof(Data))]
        public async void AddAsync(bool isMutant, Stats stat)
        {
            string sqlQueryGet = "select count_human_dna , count_mutant_dna from Stats";

            mockDapper.Setup(t => t.QuerySingle<Stats>(It.Is<IDbConnection>(db =>
                      db.ConnectionString == _mockedConnString), sqlQueryGet))
                      .ReturnsAsync(stat);

            string insertQuery = "INSERT INTO Stats (Id, count_human_dna, count_mutant_dna)" +
                            " VALUES ('00000000-0000-0000-0000-000000000000', 0, 0)";

            mockDapper.Setup(t => t.ExecuteAsync(It.Is<IDbConnection>(db =>
                     db.ConnectionString == _mockedConnString), insertQuery))
                     .ReturnsAsync(1);

            var dictionary = new Dictionary<string, object>
                        {
                            { "@humans", isMutant ? 0 : 1 },
                            { "@mutants", isMutant ? 1 : 0 },
                        };
            string updateQuery = "UPDATE Stats SET count_human_dna = @humans, " +
                        "count_mutant_dna = @mutants WHERE Id = '00000000-0000-0000-0000-000000000000'";


            mockDapper.Setup(t => t.ExecuteAsync(It.Is<IDbConnection>(db =>
                        db.ConnectionString == _mockedConnString), updateQuery, dictionary))
                        .ReturnsAsync(1);

            await _sut.AddStatAsync(isMutant);

            mockDapper.Verify(t => t.QuerySingle<Stats>(It.IsAny<IDbConnection>(),
                    It.IsAny<string>()), Moq.Times.Exactly(1) ) ;
            if (stat == null)
                mockDapper.Verify(t => t.ExecuteAsync(It.IsAny<IDbConnection>(),
                    It.IsAny<string>()), Moq.Times.Exactly(1));
            mockDapper.Verify(t => t.ExecuteAsync(It.IsAny<IDbConnection>(),
                    It.IsAny<string>(), It.IsAny<Dictionary<string, object>>()), Moq.Times.Exactly(1));
        }

        [Fact]
        public async void AddAsync_ThrowsException()
        {
            string sqlQueryGet = "select count_human_dna , count_mutant_dna from Stats";

            mockDapper.Setup(t => t.QuerySingle<Stats>(It.Is<IDbConnection>(db =>
                      db.ConnectionString == _mockedConnString), sqlQueryGet))
                      .ThrowsAsync(new Exception()).Verifiable();

            await Assert.ThrowsAsync<Exception>(async () => await _sut.AddStatAsync(true));

        }

        [Fact]
        public async void GetStatsAsync( )
        {
            Stats stats = new Stats() { count_human_dna = 10, count_mutant_dna = 20 };

            string sqlQueryGet = "select count_human_dna , count_mutant_dna " +
                        "from Stats where Id = '00000000-0000-0000-0000-000000000000'";

            mockDapper.Setup(t => t.QuerySingle<Stats>(It.Is<IDbConnection>(db =>
                      db.ConnectionString == _mockedConnString), sqlQueryGet))
                      .ReturnsAsync(stats);


            var result = await _sut.GetStatsAsync();

            Assert.Equal<Stats>(stats, result);

            mockDapper.Verify(t => t.QuerySingle<Stats>(It.IsAny<IDbConnection>(),
                    It.IsAny<string>()), Moq.Times.Exactly(1));
            }

        [Fact]
        public async void GetStatsAsync_null()
        {
            Stats mockedStats = null;
            Stats expetedStats = new Stats();

            string sqlQueryGet = "select count_human_dna , count_mutant_dna " +
                        "from Stats where Id = '00000000-0000-0000-0000-000000000000'";

            mockDapper.Setup(t => t.QuerySingle<Stats>(It.Is<IDbConnection>(db =>
                      db.ConnectionString == _mockedConnString), sqlQueryGet))
                      .ReturnsAsync(mockedStats);

            var result = await _sut.GetStatsAsync();

            Assert.Equal(expetedStats.Id, result.Id);

            mockDapper.Verify(t => t.QuerySingle<Stats>(It.IsAny<IDbConnection>(),
                    It.IsAny<string>()), Moq.Times.Exactly(1));
        }

        [Fact]
        public async void GetStatsAsync_ThrowsException()
        {
            Stats expetedStats = new Stats();

            string sqlQueryGet = "select count_human_dna , count_mutant_dna " +
                        "from Stats where Id = '00000000-0000-0000-0000-000000000000'";

            mockDapper.Setup(t => t.QuerySingle<Stats>(It.Is<IDbConnection>(db =>
                      db.ConnectionString == _mockedConnString), sqlQueryGet))
                      .ThrowsAsync(new Exception()).Verifiable();

            await Assert.ThrowsAsync<Exception>(async () => await _sut.GetStatsAsync());
        }

        public static IEnumerable<object[]> Data =>
       new List<object[]>
       {
                        new object[] { true, null },
                        new object[] { false, null},
                        new object[] { true, new Stats() { count_human_dna = 0, count_mutant_dna = 0 } },
                        new object[] { false, new Stats() { count_human_dna = 0, count_mutant_dna = 0 } }
       };
    }
}
