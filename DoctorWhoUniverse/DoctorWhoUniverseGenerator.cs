using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Neo4jClient;
using DoctorWhoUniverse.Services;
using DoctorWhoUniverse.Relationships;

namespace DoctorWhoUniverse
{
    public class DoctorWhoUniverseGenerator
    {
        private GraphClient db;
        public DoctorWhoUniverseGenerator()
        {
            db = DatabaseHelper.ConnectToDatabase();
        }

        public Universe GenerateUniverse()
        {
            return new Universe();
        }


        public Models.Character TheDoctor { get; set; }
    }
}
