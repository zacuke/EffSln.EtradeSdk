using EffSln.EtradeSdk;
using EffSln.EtradeSdk.Accounts.ListAccounts;
using EffSln.EtradeSdk.Accounts.GetAccountBalances;
using EffSln.EtradeSdk.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Threading;
using System.Text.Json;
using System;

namespace EffSln.EtradeSdkTest;

public class Worker : MenuConsoleApp
{
    private readonly EtradeSdkOptions _etradeSdkOptions;
    private readonly AuthorizationClient _authorizationClient;
    private readonly ListAccountsClient _accountsClient;
    private readonly GetAccountBalancesClient _getAccountBalancesClient;
    private readonly IConfiguration _configuration;
    private CancellationToken _cancellationToken;

    public Worker(AuthorizationClient authorizationClient, 
                    ListAccountsClient accountsClient, 
                    GetAccountBalancesClient getAccountBalancesClient,
                    IOptions<EtradeSdkOptions> etradeSdkOptions,
                    IConfiguration configuration)
    {
        _authorizationClient = authorizationClient;
        _accountsClient = accountsClient;
        _etradeSdkOptions = etradeSdkOptions.Value;
        _getAccountBalancesClient = getAccountBalancesClient;
        _configuration = configuration;
    }

    private AccountListResponse _accountListResponse;
    public async override Task StartAsync(CancellationToken cancellationToken)
    {
        _cancellationToken = cancellationToken;
        OAuthGetAccessTokenResponse oAuthGetAccessTokenResponse;

        //todo: add expiration method, for now manually refresh token saved in secrets.json
        if (string.IsNullOrEmpty(_configuration.GetValue<string>("accessToken")))
        {
            //first get access token
            var requestToken = await _authorizationClient.GetRequestTokenAsync(cancellationToken);
            var authUrl = EtradeSdkExtensions.GetAuthorizeApplicationUrl(requestToken.Oauth_token);
            oAuthGetAccessTokenResponse = await GetTokenFromUser(authUrl, requestToken.Oauth_token, requestToken.Oauth_token_secret, cancellationToken);
            Console.WriteLine("New access token:");
            Console.WriteLine($"\"accessToken\": \"{oAuthGetAccessTokenResponse.Oauth_token}\", ");
            Console.WriteLine($"\"accessTokenSecret\": \"{oAuthGetAccessTokenResponse.Oauth_token_secret}\"");
            
  
        }
        else
        {
            oAuthGetAccessTokenResponse = new OAuthGetAccessTokenResponse()
            {
                Oauth_token = _configuration.GetValue<string>("accessToken"),
                Oauth_token_secret = _configuration.GetValue<string>("accessTokenSecret") 
            };
        }

        //based on etrade docs these will expire at end of day
        _etradeSdkOptions.AccessOauth_token = oAuthGetAccessTokenResponse.Oauth_token;
        _etradeSdkOptions.AccessOauth_token_secret = oAuthGetAccessTokenResponse.Oauth_token_secret;

        //now we can start calling the normal API methods
        _accountListResponse = await _accountsClient.ListAccountsAsync(cancellationToken);

        await MainMenu();
    }
    async Task MainMenu()
    {
        //builds a menu based on list of acconts
        var mainMenuOptions = BuildMenuOptions(
            _accountListResponse.Accounts.Account,
            account => (account.AccountDesc).ToString() + $" ({account.AccountIdKey})",
            account => async () => await AccountMenu(account.AccountIdKey)
        );

        // Add a common "Exit" option
        mainMenuOptions.Add(0, ("Exit", () =>
        {
            Environment.Exit(0); 
            return Task.CompletedTask;  
        }));


        await HandleMenu(mainMenuOptions, "Main Menu: List of Accounts");
    }
    async Task AccountMenu(string AccountIdKey)
    {
        // Define the account-specific menu as a dictionary
        var accountMenuOptions = new Dictionary<int, (string, Func<Task>)>
            {
                { 1, ("Get Account Balances", async () => await GetAccountBalances(AccountIdKey))},
                { 2, ("List Transactions", async () => ListTransactions(AccountIdKey)) },
                { 3, ("View Portfolio", async () => ViewPortfolio(AccountIdKey)) },
                { 0, ("Back to Main Menu", async () => await MainMenu()) }
            };

        await HandleMenu(accountMenuOptions, $"Menu for {AccountIdKey}");
    }
    async Task GetAccountBalances(string accountIdKey)
    {
        var accountBalances = await _getAccountBalancesClient.GetAccountBalancesAsync(accountIdKey, InstType.BROKERAGE, null, null, _cancellationToken);
        PrettyPrint(accountBalances);
        //Console.WriteLine("Press Enter to continue...");
    }
    void ListTransactions(string accountIdKey)
    {

    }
    void ViewPortfolio(string accountIdKey)
    {

    }
 
    /// <summary>
    /// Example method to open browser where user logs into etrade and gets a token. Etrade has an optional Callback URL that can be implemented instead to avoid copy pasting the token, but it still requires user interaction to sign into Etrade and click Agree.
    /// </summary>
    /// <param name="authUrl"></param>
    /// <param name="oauth_token"></param>
    /// <param name="oauth_token_secret"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    async Task<OAuthGetAccessTokenResponse> GetTokenFromUser(string authUrl,string oauth_token, string oauth_token_secret, CancellationToken cancellationToken)
    {
        //etrade requires user interaction once a day
        var launchBrowser = new ProcessStartInfo(authUrl) { UseShellExecute = true };
        Process.Start(launchBrowser);

        Console.Write("Please enter the verification code from the browser: ");
        var oauth_verifier = Console.ReadLine();
        var accessTokenResponse = await _authorizationClient.GetAccessTokenAsync(oauth_token, oauth_token_secret, oauth_verifier, cancellationToken);
        return accessTokenResponse;

    }

    void PrettyPrint(object person)
    {
        // Specify JSON options for pretty printing
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        // Serialize the object to a JSON string
        string jsonString = JsonSerializer.Serialize(person, options);
 
        // Output to console
        Console.WriteLine(jsonString);
    }

}
