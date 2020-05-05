
# Coding exercise

You can choose whatever technology for the example application, the only requirements is that the working result is published on Azure (preferred) or any other cloud provider of your choice (today many cloud providers offer free tiers of their services or free trials) so it can be tested and the code repository provided for review (a Git repository is preferred, GitHub personal account with private repositories can be used).

## First Tier

Create an Invoice REST API. This API has to offer an endpoint to register Invoices, get Invoices, get a specific Invoice and edit an existing invoice.

The structure of an Invoice is as follow:

```json
{
    "invoiceId": "5e3e0b21-e98a-4480-bfb7-49e8dc61f551",
    "suplier": "",
    "dateIssued": "2019-10-10 13:30:01 T+0210",
    "currency": "EUR",
    "amount": 1000.00,
    "description": "New projector for confenrece room"
}
```

When retrieving an Invoice, an optional parameter `currency` can be passed to choose the currency for the amount. For example: if the Invoice of the example was registered (currency: EUR; amount: 1000), when it is retrieved with optinal parameter `currency=USD`, the amount returned must be 1097.38, with an exchange rate of 1.00 EUR : 1.097388 USD at the time the Invoice was registered.

> TIP: to get exchange rate values you can use free APIs like [ExchangeRate-API](https://www.exchangerate-api.com/): <https://api.exchangerate-api.com/v4/latest/EUR>, or any other of your choice)

## Second Tier

Once the API is in place, a Web App is created to register, show and edit the Invoices.

## Optional Tier

The API and the Web App are secured using OAuth (ideally both components, but it is valuable to ahve at least one of them). You can use any social identity provider of your choice (Twitter, GitHub, etc) or create one for your own (Azure Active Directory).
