# EtradeSdk

EtradeSdk is a .NET client SDK for the E*TRADE API.

The E*TRADE API requires daily reauthentication and isn't suitable for fully automated trading.

This is currently a work in progress and doesn't yet support all API endpoints.

> **Note**: This project is not affiliated with or endorsed by E*TRADE.


## Features

- **OpenAPI**: Leverages OpenAPI spec for API definitions. These definitions are custom and not provided by E*TRADE.
- **nswag-Generated Clients**: Automatically generates strongly-typed clients for interaction with the API.
- **Non-JSON Response Handling**: Custom `nswag` template for converting non-JSON API responses to JSON.
- **Example Console App**: A menu-driven console application to test API calls and explore SDK capabilities.

## Implemented methods:

### Authorization  

- [x] Get Request Token  
- [x] Authorize Application  
- [x] Get Access Token  
- [ ] Renew Access Token  
- [ ] Revoke Access Token  
     

### Accounts  

- [x] List Accounts  
- [x] Get Account Balances  
- [ ] List Transactions  
- [ ] List Transaction Details  
- [ ] View Portfolio  
     

### Alerts  

- [ ] List Alerts  
- [ ] List Alert Details  
- [ ] Delete Alert  
     

### Market  

- [ ] Get Quotes  
- [ ] Look Up Product  
- [ ] Get Option Chains  
- [ ] Get Option Expire Dates  
     

### Order  

- [ ] List Orders  
- [ ] Preview Order  
- [ ] Place Order  
- [ ] Cancel Order  
- [ ] Change Previewed Order  
- [ ] Place Changed Order  

### To regenerate http clients after modifying openapi yaml

```
dotnet tool install --global NSwag.ConsoleCore
powershell .\generate_csharp_client.ps1
```
 