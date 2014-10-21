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

        public void GenerateUniverse()
        {
            AddUniquenessConstraints();
        }


        public Models.Character TheDoctor { get; set; }

        private void AddUniquenessConstraints()
        {
            db.Cypher.CreateUniqueConstraint("a:Actor", "a.ActorName").ExecuteWithoutResults();
            db.Cypher.CreateUniqueConstraint("c:Character", "c.CharacterName").ExecuteWithoutResults();
            db.Cypher.CreateUniqueConstraint("e:Episode", "e.Title").ExecuteWithoutResults();
            db.Cypher.CreateUniqueConstraint("p:Planet", "p.PlanetName").ExecuteWithoutResults();
            db.Cypher.CreateUniqueConstraint("s:Species", "s.SpeciesName").ExecuteWithoutResults();
        }
    }
}
