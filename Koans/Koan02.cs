using DoctorWhoUniverse.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neo4jClient;
using Neo4jClient.Cypher;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Koans
{
    [TestClass]
    public class Koan02
    {
        private GraphClient graphClient;

        [ClassInitialize]
        public static void MakeSureDatabaseIsEmptyBeforeWeStart(TestContext context)
        {
            var graphClient = new GraphClient(new Uri("http://localhost:7474/db/data"));

            graphClient.Connect();

            DeleteAllNodesAndRelationshipsFromGraphClient(graphClient);
        }

        [TestInitialize]
        public void CreateANewClient()
        {
            graphClient = new GraphClient(new Uri("http://localhost:7474/db/data"));

            graphClient.Connect();
        }

        [TestMethod]
        public void ShouldCreateASingleNode()
        {
            // YOUR CODE GOES HERE
            // SNIPPET_START

            graphClient.Cypher.Create("n").ExecuteWithoutResults();

            // SNIPPET_END

            var getAllNodes = graphClient.Cypher.Start(new { allNodes = All.Nodes }).Return<object>("allNodes");

            Assert.AreEqual(1, getAllNodes.Results.Count());
        }

        [TestMethod]
        public void ShouldCreateASingleNodeWithSomeProperties()
        {
        // YOUR CODE GOES HERE
        // SNIPPET_START

        graphClient.Cypher.Create("(n { firstname : 'Tom', lastname : 'Baker' })").ExecuteWithoutResults();

        // SNIPPET_END

        var matchedNodes = graphClient.Cypher.Match("(n {firstname: 'Tom', lastname: 'Baker'})").Return<object>("n");

        /* Geek question: if you've read ahead, what's the Big O cost of this match? */

        Assert.AreEqual(1, matchedNodes.Results.Count());
        }

        [TestMethod]
        public void ShouldCreateASimpleConnectedGraph()
        {
        // YOUR CODE GOES HERE
        // SNIPPET_START

            string doctorCharacterName = "Doctor";
            string masterCharacterName = "Master";

            var theDoctor = new Character{CharacterName = doctorCharacterName };
            var theMaster = new Character{CharacterName = masterCharacterName };


            var cypherParameters = new Dictionary<string, object>();

            cypherParameters.Add("doctor", theDoctor);
            cypherParameters.Add("master", theMaster);

            graphClient.Cypher.
                Create("(doctor:Character{doctor})<-[:ENEMY_OF]-(master:Character{master})").
                WithParams(cypherParameters).
                ExecuteWithoutResults();
        // SNIPPET_END


            var queryResult = graphClient.Cypher.Match("(doctor:Character)<-[:ENEMY_OF]-(master:Character)").
                Where<Character>(doctor => doctor.CharacterName == doctorCharacterName).
                AndWhere((Character master) => master.CharacterName == "Master").
                Return((doctor, master) => new { Doctor = doctor.As<Character>(), Master = master.As<Character>() }).
                Results;
        /* Same geek question: if you've read ahead, what's the Big O cost of this match? */

            Assert.AreEqual(1, queryResult.Count());
            Assert.AreEqual(doctorCharacterName, queryResult.First().Doctor.CharacterName);
            Assert.AreEqual(masterCharacterName, queryResult.First().Master.CharacterName);
        }

        [TestCleanup]
        public void DeleteAllNodesAndRelationships()
        {
            DeleteAllNodesAndRelationshipsFromGraphClient(graphClient);
        }

        private static void DeleteAllNodesAndRelationshipsFromGraphClient(GraphClient deletionTarget)
        {
            deletionTarget.Cypher.Start(new { allNodes = All.Nodes }).OptionalMatch("()-[allEdges]-()").Delete("allNodes, allEdges").ExecuteWithoutResults();
        }
    }
}
