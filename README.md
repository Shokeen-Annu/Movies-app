# Movies-app

Pre-Requisites:

Microsoft Visual Studio 2019
.Net Core 3.1

STEPS TO RUN THE APP:-

After cloning the repo, the api key is required for code to function. To provide api key, follow the steps:

1. Right click on WebjetMovies project under the solution. 
2. Click "Manage user secrets".
3. Step 2 will create a file. Replace the text in file with the following code:

{
    "WebjetAccessToken": "YOUR_API_X_ACCESS_TOKEN"
}

4. Replace "YOUR_API_X_ACCESS_TOKEN" with your x-access-token for the API.
5. Run the project.

NOTE: If API key is not provided, movies will not be fetched.
