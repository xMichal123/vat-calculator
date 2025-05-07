# VAT Calculator

A clean and extensible VAT calculation solution supporting Austrian VAT rates (10%, 13%, 20%).  
Designed to be modular — starting with a .NET 8 REST API, with the potential to expand into CLI tools, frontends, or cloud services.

> Originally developed as part of a technical challenge for **GLOBAL BLUE**.

---

## ✨ Features

- Calculates missing values (Net, VAT, or Gross) from a single provided amount.
- Validates VAT rates (10%, 13%, 20%) and enforces strict input rules.
- Clean architecture with dependency injection and SOLID principles.
- Localizable error messages using `.resx` and `IStringLocalizer`.
- Unit tested using **xUnit** and **Moq**.
- Includes built-in Swagger UI for easy exploration.

---

## 📦 Current Structure

```
vat-calculator/
├── VatCalculator.Api/          → .NET 8 Web API
│   ├── Controllers/
│   ├── Models/
│   ├── Resources/              → Localizable error messages (.resx)
│   ├── Services/
│   └── Program.cs
│
├── VatCalculator.Tests/        → xUnit test project with Moq
├── VatCalculator.sln           → Solution file
└── README.md                   → This file
```

Planned future modules:
- `VatCalculator.Frontend/` → Angular/Blazor frontend
- `VatCalculator.Cli/`      → Console interface
- `VatCalculator.Core/`     → Shared business logic

---

## 🧰 Tech Stack

- .NET 8 Web API (ASP.NET Core)
- xUnit + Moq for unit testing
- IStringLocalizer for localization
- Swashbuckle/Swagger for OpenAPI support

---

## 🚀 Running the Project

### ▶️ Run the API

```cmd
dotnet restore
dotnet build
dotnet run --project VatCalculatorApi
```

Visit: http://localhost:5245/swagger/index.html

---

### 🧪 Run the Tests

```cmd
dotnet test
```

Covers:
- VAT calculation for Net, Gross, and VAT inputs
- Validation for input combinations and VAT rates
- Error message fallback

---

## 🔌 API Overview

### Endpoint

`POST /api/vatcalculator/calculate`

### Request Example

```json
{
  "netAmount": 100,
  "vatRate": 20
}
```

### Response

```json
{
  "netAmount": 100,
  "vatAmount": 20,
  "grossAmount": 120
}
```

---

## 🌍 Localization

The application supports localizable error messages using `.resx` resource files. Supported UI cultures:

- `en` (English) — default
- `de` (German)
- `sk` (Slovak)

### Available Messages

| Key                  | English                                      | German (de)                                              | Slovak (sk)                                                     |
|-----------------------|----------------------------------------------|-----------------------------------------------------------|------------------------------------------------------------------|
| `Error_InvalidVatRate`  | Invalid VAT rate. Allowed values are 10, 13, or 20. | Ungültiger Mehrwertsteuersatz. Erlaubte Werte sind 10, 13 oder 20. | Neplatná sadzba DPH. Povolené hodnoty sú 10, 13 alebo 20.         |
| `Error_AmountMissing`   | You must provide exactly one of NetAmount, GrossAmount, or VatAmount. | Sie müssen genau einen Wert von NetAmount, GrossAmount oder VatAmount angeben. | Musíte zadať presne jednu z hodnôt: NetAmount, GrossAmount alebo VatAmount. |
| `Error_UnexpectedState` | Unexpected null state in VAT calculation logic. | Unerwarteter Nullzustand in der Mehrwertsteuerberechnung. | Neočakávaný prázdny stav v logike výpočtu DPH.                   |

The appropriate message is returned based on the `Accept-Language` header or default culture.

---

## 🛠️ Requirements

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- Visual Studio 2022 v17.8+ or VS Code with C# extension

---

## 💡 Future Ideas

- Add Docker support for containerized deployment
- Angular or Blazor client for VAT calculation UI
- Azure deployment sample
- Redis-based caching for frequently requested rates
- Multi-country VAT rules engine

---

## 📝 License

This project is provided for demonstration and evaluation purposes.

---

## 🙋‍♂️ Author

Maintained by Michal Mihálik
Feel free to connect or fork the repo for further experimentation.
