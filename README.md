# VAT Calculator API (.NET 8)

A clean and testable REST API for calculating **Net**, **VAT**, and **Gross** amounts based on Austrian VAT rates (10%, 13%, 20%).  
Developed as part of a coding challenge for **GLOBAL BLUE**.

---

## ✨ Features

- Calculates missing values when one of **NetAmount**, **GrossAmount**, or **VatAmount** is provided.
- Validates Austrian VAT rates (10%, 13%, 20%) and input constraints.
- Follows **SOLID principles** and uses **dependency injection**.
- Error messages are fully **localizable** via `.resx` files using `IStringLocalizer`.
- Unit tested with **xUnit** and **Moq**.
- Includes auto-generated **Swagger UI**.

---

## 🧱 Tech Stack

- .NET 8 Web API (ASP.NET Core)
- xUnit (unit testing)
- Moq (mocking for DI/localization)
- Swagger / Swashbuckle (OpenAPI)
- Optional localization (resx-based, extendable)

---

## 🚀 Getting Started

### ▶️ Run the API (Windows / Visual Studio / CLI)

```cmd
dotnet restore
dotnet build
dotnet run --project VatCalculatorApi
