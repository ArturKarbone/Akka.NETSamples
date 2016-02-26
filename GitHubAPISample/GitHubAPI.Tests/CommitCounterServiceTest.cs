using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Threading.Tasks;

namespace GitHubAPI.Tests
{
    [TestClass]
    public class CommitCounterServiceTest
    {
        [TestMethod]
        public  async Task Test()
        {

        }

        [TestMethod]
        public async Task CountCommitsSimpleTest()
        {
            Debug.WriteLine("Test Started");
            var sw = Stopwatch.StartNew();

            var service = new CommitCounterService();
            var commitsCount = await service.CountCommits();

            sw.Stop();

            Console.WriteLine("Time it took to get the data - " + sw.ElapsedMilliseconds);
            Console.WriteLine(commitsCount);

            Assert.AreEqual(8921, commitsCount);
        }

        [TestMethod]
        public async Task CountCommitsOptimzedTest()
        {
            Debug.WriteLine("Test Started");
            var sw = Stopwatch.StartNew();

            var service = new CommitCounterService();
            var commitsCount = await service.CountCommitsOptimized();

            sw.Stop();

            Console.WriteLine("Time it took to get the data - " + sw.ElapsedMilliseconds);
            Console.WriteLine(commitsCount);

            Assert.AreEqual(8921, commitsCount);
        }
    }
}