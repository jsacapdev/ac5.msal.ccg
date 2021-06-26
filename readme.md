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
