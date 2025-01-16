using EffSln.EtradeSdk;
using EffSln.EtradeSdk.Accounts;
using EffSln.EtradeSdk.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Threading;

namespace EffSln.EtradeSdkTest;

public class Worker : MenuConsoleApp
{
    private readonly EtradeSdkOptions _etradeSdkOptions;
    private readonly AuthorizationClient _authorizationClient;
    private readonly AccountsClient _accountsClient;    
    public Worker(AuthorizationClient authorizationClient, AccountsClient accountsClient, IOptions<EtradeSdkOptions> etradeSdkOptions)
    {
        _authorizationClient = authorizationClient;
        _accountsClient = accountsClient;
        _etradeSdkOptions = etradeSdkOptions.Value;
    }

    private AccountListResponse _accountListResponse;
    public async override Task StartAsync(CancellationToken cancellationToken)
    {        
 
        //first get access token
        var requestToken = await _authorizationClient.GetRequestTokenAsync(cancellationToken);
        var authUrl = EtradeSdkExtensions.GetAuthorizeApplicationUrl(requestToken.Oauth_token);
        var accessTokenResponse = await GetTokenFromUser(authUrl, requestToken.Oauth_token, requestToken.Oauth_token_secret, cancellationToken);

        //based on etrade docs these will expire at end of day
        _etradeSdkOptions.AccessOauth_token = accessTokenResponse.Oauth_token;
        _etradeSdkOptions.AccessOauth_token_secret = accessTokenResponse.Oauth_token_secret;

        //now we can start calling the normal API methods
        _accountListResponse = await _accountsClient.ListAccountsAsync(cancellationToken);

        MainMenu();
    }
    void MainMenu()
    {

        var mainMenuOptions = BuildMenuOptions(
            _accountListResponse.Accounts.Account,
            account => account.AccountName.ToString() + $" ({account.AccountIdKey})",
            account => () => AccountMenu(account.AccountIdKey)
        );

        // Add a common "Exit" option
        mainMenuOptions.Add(0, ("Exit", () => Console.WriteLine("Exiting program...")));



        HandleMenu(mainMenuOptions, "Main Menu: List of Accounts");
    }
    void AccountMenu(string AccountIdKey)
    {
        // Define the account-specific menu as a dictionary
        var accountMenuOptions = new Dictionary<int, (string, Action)>
            {
                { 1, ("Get Account Balances", () => GetAccountBalances(AccountIdKey))},
                { 2, ("List Transactions", () => ListTransactions(AccountIdKey)) },
                { 3, ("View Portfolio", () => ViewPortfolio(AccountIdKey)) },
                { 0, ("Back to Main Menu", MainMenu) }
            };

        HandleMenu(accountMenuOptions, $"Menu for {AccountIdKey}");
    }
    void GetAccountBalances(string accountIdKey)
    {
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

}
