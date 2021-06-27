# ac5.msal.ccg

[Asp.net Core 5 using MSAL.NET and Client Credentials Grant](https://github.com/Azure-Samples/active-directory-dotnetcore-daemon-v2/tree/master/2-Call-OwnApi).

## Azure AD Configuration

The following has been added to the manifest in addition to creating the application registration.

``` json
"appRoles": [
    {
        "allowedMemberTypes": [
            "User",
            "Application"
        ],
        "description": "Read messages",
        "displayName": "Read",
        "id": "1ac8563b-4cb8-4910-bfb5-cab6afa36f55",
        "isEnabled": true,
        "lang": null,
        "origin": "Application",
        "value": "Read"
    }
]
```

Also, the API has been exposed and given the `api://<some_guid>` convention.

## Configuration

This type of thing has been set in the local configuration file:

``` json
  "AzureAd": {
    "Instance": "https://login.microsoftonline.com/",
    "ClientId": "???",
    "Domain": "???.onmicrosoft.com",
    "TenantId": "???"
  }  
```

You can also put it in your VSCode `launch.json`:

``` json
"env": {
    "AzureAd__Instance": "https://login.microsoftonline.com/",
    "AzureAd__ClientId": "???",
    "AzureAd__Domain": "???.onmicrosoft.com",
    "AzureAd__TenantId": "???"
}
```

## Testing

To populate the `http` rest extension run the secret for the client through the following command:

`[System.Web.HTTPUtility]::UrlEncode("your_password")`

## Deploy

To deploy to the cloud you will need:

- Azure CLI
- An Azure Subscription

Login to your subscription:

`az login --use-device-code`

Create a resource group:

`az group create -n rg-ccg-dev-we-01 -l westeurope --debug`

Run the following command in the `src/arm` folder to provision the resources in Azure (this command uses `pwsh`):

``` pwsh
az deployment group create --name $(New-Guid) `
--resource-group rg-ccg-dev-we-01 `
--template-file ./api.template.json `
--parameters `
"appServicePlanName=plan-ccg-dev-we-01" `
"appName=api-ccg-dev-we-01" `
"clientId=<your client id>" `
"tenantId=<your tenant id>" `
"domainId=<your domain id>" `
--debug
```

Publish your application from the `src/msalccgapi` folder:

`dotnet publish -c Release -o d:/api/`

Then deploy it:

`az webapp deployment source config-zip -g rg-ccg-dev-we-01 -n api-ccg-dev-we-01 --src d:/api/api.zip --debug`
