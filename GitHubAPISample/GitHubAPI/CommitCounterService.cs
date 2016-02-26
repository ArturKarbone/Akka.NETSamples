using Octokit;
using Octokit.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubAPI
{
    public class CommitCounterService
    {
        const string GitHubToken = "SomeToken"; 

        public async Task<int> CountCommits()
        {
            var github = new GitHubClient(new ProductHeaderValue("AkkaCounter"), new InMemoryCredentialStore(new Credentials(GitHubToken)));

            var repos = await github.Repository.GetAllForUser("michal-franc");

            var countCommits = 0;

            foreach (var repository in repos)
            {
                var commits = await github.Repository.Commits.GetAll(repository.Owner.Login, repository.Name);
                countCommits = countCommits + commits.Count;
            }

            return countCommits;
        }

        public async Task<int> CountCommitsOptimized()
        {
            var github = new GitHubClient(new ProductHeaderValue("AkkaCounter"), new InMemoryCredentialStore(new Credentials(GitHubToken)));

            var repos = await github.Repository.GetAllForUser("michal-franc");

            var countCommits = 0;

            var commitsResults = repos
                .Select(repo => (github.Repository.Commits.GetAll(repo.Owner.Login, repo.Name)))
                .ToList();
            //.ForEach(async commits => countCommits += (await commits).Count);     

            foreach (var commitResult in commitsResults)
            {
                countCommits += (await commitResult).Count;
            }


            return countCommits;
        }
    }
}
