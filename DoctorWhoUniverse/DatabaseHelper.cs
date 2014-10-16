using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Neo4jClient;
using Neo4jClient.Gremlin;

namespace DoctorWhoUniverse
{
    public class DatabaseHelper
    {
        private static GraphClient db;

        static DatabaseHelper()
        {
            db = new GraphClient(new Uri("http://localhost:7474/db/data"));
            db.Connect();
        }

        public static GraphClient ConnectToDatabase()
        {
            return db;
        }

    }
}
