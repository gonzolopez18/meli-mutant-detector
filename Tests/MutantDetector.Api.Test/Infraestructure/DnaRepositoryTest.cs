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
        private readonly Mock<ISqlConnectionFactory> _connectionFactory = new Mock<ISqlConnectionFactory>();
        private readonly DnaRepository _sut;
        public DnaRepositoryTest()
        {
            DnaRepository _sut = new DnaRepository( _connectionFactory.Object);
        }

        //[Fact]
        //public async void AddAsync()
        //{

        //    var dbConnection = new Mock<IDbConnection>();
        //   _connectionFactory.Setup(x => x.GetOpenConnection())
        //        .Returns(dbConnection.Object);
        //    dbConnection.SetupDapper(x => x.ExecuteAsync(It.IsAny<string>(), null, null, null, null))
        //        .ReturnsAsync(1);
                
 

        //    bool expected = true;

        //    Dna dna = new Dna();

        //    var result = await _sut.AddAsync(dna);

        //    Assert.True(result);
        //}



    public static IEnumerable<object[]> Data =>
        new List<object[]>
        {
                        new object[] { true, 1, 0 },
                        new object[] { false, 0, 1 }
        };
}
}
