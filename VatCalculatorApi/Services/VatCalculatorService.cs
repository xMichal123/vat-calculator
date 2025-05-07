using Microsoft.Extensions.Localization;
using VatCalculatorApi.Models;

namespace VatCalculatorApi.Services;

public class VatCalculatorService : IVatCalculatorService
{
    private readonly IStringLocalizer _localizer;

    public VatCalculatorService(IStringLocalizer<VatCalculatorService> localizer)
    {
        _localizer = localizer;
    }

    public VatCalculationResponse Calculate(VatCalculationRequest request)
    {
        if (request.VatRate <= 0 || !new[] { 10m, 13m, 20m }.Contains(request.VatRate))
            throw new ArgumentException(_localizer["Error_InvalidVatRate"]);

        int nonNulls = new[] { request.NetAmount, request.GrossAmount, request.VatAmount }
                        .Count(x => x.HasValue);

        if (nonNulls != 1)
            throw new ArgumentException(_localizer["Error_AmountMissing"]);

        var rate = request.VatRate / 100m;

        if (request.NetAmount.HasValue)
        {
            var vat = request.NetAmount.Value * rate;
            return new VatCalculationResponse
            {
                NetAmount = request.NetAmount.Value,
                VatAmount = vat,
                GrossAmount = request.NetAmount.Value + vat
            };
        }
        else if (request.GrossAmount.HasValue)
        {
            var net = request.GrossAmount.Value / (1 + rate);
            return new VatCalculationResponse
            {
                NetAmount = net,
                VatAmount = request.GrossAmount.Value - net,
                GrossAmount = request.GrossAmount.Value
            };
        }
        else if (request.VatAmount.HasValue)
        {
            var net = request.VatAmount.Value / rate;
            return new VatCalculationResponse
            {
                NetAmount = net,
                VatAmount = request.VatAmount.Value,
                GrossAmount = net + request.VatAmount.Value
            };
        }
        else
        {
            // This should never happen due to earlier validation
            throw new InvalidOperationException(_localizer["Error_UnexpectedState"]);
        }
    }
}