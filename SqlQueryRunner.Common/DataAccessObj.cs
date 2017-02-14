using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlQueryRunner.Common
{
    public class DataAccessObj:IDisposable
    {
        public SqlConnection Connection { get; private set; }
        public SqlCommand Command { get; private set; }
        public SqlDataAdapter Adapter { get; private set; }

        public DataAccessObj()
        {
            Connection = new SqlConnection(Options.connstr);
            Command = new SqlCommand();
            Command.Connection = Connection;
            Command.CommandText = Options.query;
            Adapter = new SqlDataAdapter(Command);
        }

        public void Dispose()
        {
            Adapter.Dispose();
            Command.Dispose();
            Connection.Dispose();
        }
    }
}
