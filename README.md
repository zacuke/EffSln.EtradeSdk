### To regenerate http clients

```
dotnet tool install --global NSwag.ConsoleCore
powershell .\generate_csharp_client.ps1
```
#### Note
> E-Trade's [Renew](https://apisb.etrade.com/docs/api/authorization/renew_access_token.html) and [Revoke](https://apisb.etrade.com/docs/api/authorization/revoke_access_token.html) access token endpoints don't seem to be functional.