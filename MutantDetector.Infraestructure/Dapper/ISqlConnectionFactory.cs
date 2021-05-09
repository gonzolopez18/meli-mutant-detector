using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace MutantDetector.Infraestructure.Dapper
{
    public interface ISqlConnectionFactory
    {
        SqlConnection GetOpenConnection();
    }
}
