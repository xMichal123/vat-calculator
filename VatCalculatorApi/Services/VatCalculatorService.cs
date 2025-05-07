using Microsoft.Extensions.Localization;
using VatCalculatorApi.Models;

namespace VatCalculatorApi.Services;

public class VatCalculatorService : IVatCalculatorService
{
    private readonly IStringLocalizer _localizer;
    private readonly ILogger<VatCalculatorService> _logger;

    public VatCalculatorService(IStringLocalizer<VatCalculatorService> localizer, ILogger<VatCalculatorService> logger)
    {
        _localizer = localizer;
        _logger = logger;
    }

    public VatCalculationResponse Calculate(VatCalculationRequest request)
    {
        _logger.LogInformation("Calculating VAT using rate {VatRate}", request.VatRate);

        if (request.VatRate <= 0 || !new[] { 10m, 13m, 20m }.Contains(request.VatRate))
        {
            _logger.LogWarning("Invalid VAT rate: {VatRate}", request.VatRate);
            throw new ArgumentException(_localizer["Error_InvalidVatRate"]);
        }

        int nonNulls = new[] { request.NetAmount, request.GrossAmount, request.VatAmount }
                        .Count(x => x.HasValue);

        if (nonNulls != 1)
        {
            _logger.LogWarning("You must provide exactly one of NetAmount, GrossAmount, or VatAmount.");
            throw new ArgumentException(_localizer["Error_AmountMissing"]);
        }

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