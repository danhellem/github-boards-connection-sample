# GitHub + Boards Connection sample

Sample code for using connection APIs to connection GitHub Repos to Azure Boards.

### 🌟 Getting started

1) Install the Azure Boards App for GitHub. Follow the instructions [in this article](https://learn.microsoft.com/en-us/azure/devops/boards/github/install-github-app). Note, these new API’s do not facilitate the connection. You must create the connection first by installing the app and connecting a single repository. Once that connection is made, you can use the new API’s to connect more GitHub repositories.

   > 🗒️ When you setup the connection to a repository, make sure you give the connection access to all repositories in that organization.

2) You will need to set up an [Azure DevOps Personal Access Token](https://learn.microsoft.com/en-us/azure/devops/organizations/accounts/use-personal-access-tokens-to-authenticate). Use the “GitHub Connections” scope that is required when scoping your PAT.

3) If you are interested in these APIs, then you are mostly likely going to be creating some tooling to fetch the repos from GitHub via code. To do so, you must [create a GitHub PAT token](https://docs.github.com/en/authentication/keeping-your-account-and-data-secure/creating-a-personal-access-token) as well. 

### 🚫 Limitations

- You are limited to link up to 2,000 repos per connection.
- Batch is limited to 200 repos per request.
- Subject to general API rate limits.

### ⚒️ Suggested work flow

This workflow suggests that any new repos that come online in GitHub should be added to the existing connection. You will want to tailor this workflow to better fit your needs if you are selective of the repos that should be added.

1)	Install Azure Boards App for GitHub.
2)	Connect one GitHub repository to project.
3)	Call Get Connections to get list of available connections.
4)	Call Get list of GitHub repositories.
5)	Call Get Connected Repos to get list of repos already connected.
6)	Compare list for #4 to #5 to get list of repos that are not yet connected.
7)	Generate body and call ReposBatch endpoint to add repos
8)	Optional: Any items in #4 that are not in #5 can be removed using same ReposBatch endpoint.

### 📝 Notes

- We have provided this sample code to help you with the onboard process. The code has been simplified to facilitate the learning of the APIs. The code is not ready for production and should not be used as such.

  **For example:** You are limited to 200 repos per batch request. The sample code does not check for that.

- The identity using the APIs must have project admin rights.
