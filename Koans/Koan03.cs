using DoctorWhoUniverse;
using DoctorWhoUniverse.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neo4jClient;
using System.Linq;

namespace Koans
{
    /**
     * In this Koan we learn the basics of the Cypher query language, focusing on the
     * MATCH clause to RETURN subgraphs of information about the Doctor Who
     * universe.
     */
    [TestClass]
    public class Koan03
    {
        private static DoctorWhoUniverseGenerator buildsUniverses;

        [ClassInitialize]
        public static void CreateDatabase(TestContext context)
        {
            buildsUniverses = new DoctorWhoUniverseGenerator();

            buildsUniverses.GenerateUniverse();
        }

        [TestMethod]
        public void ShouldFindAndReturnTheDoctor()
        {
            // YOUR CODE GOES HERE
            // SNIPPET_START

            //cql = "MATCH (doctor:Character {character: 'Doctor'}) RETURN doctor";
            var doctorQuery = DatabaseHelper.ConnectToDatabase().Cypher.
                                Match("(doctor:Character)").
                                Where<Character>(doctor => doctor.CharacterName == "Doctor").
                                Return<Character>("doctor");

            // SNIPPET_END

            Assert.IsTrue(new CharacterNameComparer().Equals(buildsUniverses.TheDoctor, doctorQuery.Results.First()), "Doctor Characters did not match.");
        }
        [ClassCleanup]
        public static void DestroyTheUniverseMwahHaHaHa()
        {}
    }
}
