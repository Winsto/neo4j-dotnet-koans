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

        private void AddUniquenessConstraints()
        {
            db.Cypher.
                CreateUniqueConstraint("a:Actor", "a.ActorName").
                CreateUniqueConstraint("c:Character", "c.CharacterName").
                CreateUniqueConstraint("e:Episode", "e.Title").
                CreateUniqueConstraint("p:Planet", "p.PlanetName").
                CreateUniqueConstraint("s:Species", "s.SpeciesName").
                ExecuteWithoutResults();
        }
    }
}
