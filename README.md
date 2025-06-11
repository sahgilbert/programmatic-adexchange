# Programmatic AdExchange
# Simple Second Price Auction - RESTful Api
A simple C#.Net example of a Programmatic AdExchange, that simulates a second price auction.

### Tech Stack
- C#.Net 8.0.0
- Asp.Net MVC/Web Api
- XUnit
- CI/CD to Microsoft Azure Web App Service (via Github Actions)

### Running Locally

Endpoint: [https://localhost:7124/api/auction](https://localhost:7124/api/auction)

### Running in the Microsoft Azure Cloud

Endpoint: [https://programmaticadexchange.azurewebsites.net/api/auction](https://programmaticadexchange.azurewebsites.net/api/auction)

---

### Example POST Request

```
{
  "id": "req-123",
  "imp": [
    {
      "id": "1",
      "bidfloor": 0.50
    }
  ]
}
```

### Example POST Response

```
{
    "requestId": "req-123",
    "winningBid":
    {
        "bidder": "DSP-A",
        "impId": "1",
        "price": 0.90
    }
}
```