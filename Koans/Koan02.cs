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

        [TestCleanup]
        private void DeleteAllNodesAndRelationships()
        {
            graphClient.Cypher.Start(new { allNodes = All.Nodes }).OptionalMatch("()-[allEdges]-()").Delete("allNodes, allEdges").ExecuteWithoutResults();
        }
    }
}
