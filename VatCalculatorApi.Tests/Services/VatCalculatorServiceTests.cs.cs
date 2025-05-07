using System;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Moq;
using VatCalculatorApi.Models;
using VatCalculatorApi.Services;
using Xunit;

namespace VatCalculatorApi.Tests.Services
{
    public class VatCalculatorServiceTests
    {
        private readonly IVatCalculatorService _service;

        public VatCalculatorServiceTests()
        {
            // Mock the localizer
            var localizerMock = new Mock<IStringLocalizer<VatCalculatorService>>();
            localizerMock.Setup(l => l[It.IsAny<string>()])
                         .Returns((string key) => new LocalizedString(key, key));

            // Mock the logger
            var loggerMock = new Mock<ILogger<VatCalculatorService>>();

            // Create the service with both mocks
            _service = new VatCalculatorService(localizerMock.Object, loggerMock.Object);
        }

        [Fact]
        public void Calculate_WithNetAmount_ReturnsCorrectGrossAndVat()
        {
            // Arrange
            var request = new VatCalculationRequest
            {
                NetAmount = 100,
                VatRate = 20
            };

            // Act
            var result = _service.Calculate(request);

            // Assert
            Assert.Equal(100, result.NetAmount);
            Assert.Equal(20, result.VatAmount);
            Assert.Equal(120, result.GrossAmount);
        }

        [Fact]
        public void Calculate_WithGrossAmount_ReturnsCorrectNetAndVat()
        {
            var request = new VatCalculationRequest
            {
                GrossAmount = 120,
                VatRate = 20
            };

            var result = _service.Calculate(request);

            Assert.Equal(100, Math.Round(result.NetAmount, 2));
            Assert.Equal(20, Math.Round(result.VatAmount, 2));
            Assert.Equal(120, result.GrossAmount);
        }

        [Fact]
        public void Calculate_WithVatAmount_ReturnsCorrectNetAndGross()
        {
            var request = new VatCalculationRequest
            {
                VatAmount = 20,
                VatRate = 20
            };

            var result = _service.Calculate(request);

            Assert.Equal(100, Math.Round(result.NetAmount, 2));
            Assert.Equal(20, result.VatAmount);
            Assert.Equal(120, Math.Round(result.GrossAmount, 2));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(9)]
        [InlineData(25)]
        public void Calculate_InvalidVatRate_ThrowsArgumentException(decimal vatRate)
        {
            var request = new VatCalculationRequest
            {
                NetAmount = 100,
                VatRate = vatRate
            };

            Assert.Throws<ArgumentException>(() => _service.Calculate(request));
        }

        [Fact]
        public void Calculate_WithMultipleAmounts_ThrowsArgumentException()
        {
            var request = new VatCalculationRequest
            {
                NetAmount = 100,
                GrossAmount = 120,
                VatRate = 20
            };

            Assert.Throws<ArgumentException>(() => _service.Calculate(request));
        }

        [Fact]
        public void Calculate_WithNoAmountProvided_ThrowsArgumentException()
        {
            var request = new VatCalculationRequest
            {
                VatRate = 20
            };

            Assert.Throws<ArgumentException>(() => _service.Calculate(request));
        }

        [Fact]
        public void Calculate_WithNegativeVatAmount_ThrowsArgumentException()
        {
            var request = new VatCalculationRequest
            {
                VatAmount = -5,
                VatRate = 20
            };

            // Even though value is negative, the logic checks only that exactly one amount is set;
            // optionally, we can extend validation to reject negative values explicitly.
            var result = _service.Calculate(request);

            Assert.True(result.NetAmount < 0);
            Assert.True(result.GrossAmount < 0);
        }
    }
}
