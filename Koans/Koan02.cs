using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neo4jClient;
using Neo4jClient.Cypher;
using System;
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
