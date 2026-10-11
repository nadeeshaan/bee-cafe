# Bee Cafe

A small ASP.NET Core (.NET 10) minimal API for placing coffee orders. It prices the order, applies a 10% student discount, and simulates printing a receipt, saving the order to a database, and emailing the customer (all three are written to the console).

## Project structure

Each class has a single reason to change, and each folder has its own namespace:

| Folder | Namespace | Contents |
| --- | --- | --- |
| `Domain/` | `BeeCafe.Domain` | `MenuItem`, `Menu`, `Order`: the core model, with no I/O |
| `Pricing/` | `BeeCafe.Pricing` | `PriceCalculator`: prices and discounts |
| `Receipts/` | `BeeCafe.Receipts` | `ReceiptPrinter`: what a receipt shows |
| `Persistence/` | `BeeCafe.Persistence` | `OrderRepository`: how orders are stored (simulated) |
| `Notifications/` | `BeeCafe.Notifications` | `EmailNotifier`: how customers are contacted (simulated) |
| `Api/` | `BeeCafe.Api` | `OrderEndpoints` and request/response contracts |

`Program.cs` configures OpenAPI and Swagger UI, and maps the endpoints.

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [Docker](https://docs.docker.com/get-docker/) (only needed for the container steps)

## Build and run

```bash
dotnet restore
dotnet build
dotnet run --launch-profile http
```

The API listens on `http://localhost:5230`.

To also serve HTTPS on `https://localhost:7291`, use the `https` profile. The first time, you may need to trust the .NET development certificate:

```bash
dotnet dev-certs https --trust
dotnet run --launch-profile https
```

## API documentation (Swagger UI)

When the app runs in the `Development` environment (both launch profiles set this), you can open:

| What | URL |
| --- | --- |
| Swagger UI | http://localhost:5230/swagger |
| OpenAPI document (JSON) | http://localhost:5230/openapi/v1.json |

The OpenAPI document comes from the built-in `Microsoft.AspNetCore.OpenApi` package, and `Swashbuckle.AspNetCore.SwaggerUI` displays it. Neither one is enabled outside `Development`.

In Swagger UI, expand **POST /orders**, click **Try it out**, edit the request body, and click **Execute**.

You can also send the sample requests in [BeeCafe.http](BeeCafe.http) from Visual Studio, Rider, or VS Code (with the REST Client extension).

## API reference

### `POST /orders`

Places an order and returns the items with the total price.

**Request body**

| Field | Type | Description |
| --- | --- | --- |
| `customerEmail` | string | Required. Must not be blank. |
| `isStudent` | boolean | If `true`, a 10% discount is applied. |
| `items` | string[] | Required. At least one item. |

**Menu**

| Item | Price |
| --- | --- |
| `latte` | $5.50 |
| `flat white` | $5.00 |
| `long black` | $4.50 |

### Examples

**Successful order**

```bash
curl -X POST http://localhost:5230/orders \
  -H "Content-Type: application/json" \
  -d '{
        "customerEmail": "jane@example.com",
        "isStudent": true,
        "items": ["latte", "flat white"]
      }'
```

`200 OK`

```json
{
  "items": ["latte", "flat white"],
  "total": 9.450,
  "gst": 1.23
}
```

The total is ($5.50 + $5.00) × 0.9 for the student discount. Prices include NZ GST at 15%, so `gst` is the GST share of the total (total × 3/23, rounded to 2 decimal places).

**Unknown item**

```json
{ "customerEmail": "jane@example.com", "isStudent": false, "items": ["mocha"] }
```

`400 Bad Request`

```json
{ "error": "Unknown item: mocha" }
```

**Validation errors** (`400 Bad Request`)

| Cause | Response |
| --- | --- |
| `customerEmail` is missing or blank | `{ "error": "CustomerEmail is required." }` |
| `items` is missing or empty | `{ "error": "At least one item is required." }` |

## Docker

### Build the image

```bash
docker build -t beecafe .
```

You can also run `docker compose build`, which uses [compose.yaml](compose.yaml) to build the same `beecafe` image.

### Run the container

Inside the container, the app listens on port `8080` over HTTP. Map that port to a port on your machine:

```bash
docker run --rm -p 8080:8080 beecafe
```

Then call the API at `http://localhost:8080/orders`.

The container runs in the `Production` environment by default, so Swagger UI is turned off. To turn it on, set the environment to `Development`:

```bash
docker run --rm -p 8080:8080 -e ASPNETCORE_ENVIRONMENT=Development beecafe
```

Swagger UI is then at http://localhost:8080/swagger.

## License

Copyright 2026 nadeeshaan

Licensed under the [Apache License, Version 2.0](LICENSE).
