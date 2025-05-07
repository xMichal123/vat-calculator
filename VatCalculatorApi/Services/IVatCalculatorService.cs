using VatCalculatorApi.Models;

namespace VatCalculatorApi.Services;

public interface IVatCalculatorService
{
    VatCalculationResponse Calculate(VatCalculationRequest request);
}