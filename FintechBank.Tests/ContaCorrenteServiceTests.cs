using FintechBank.App;
using Xunit;

namespace FintechBank.Tests;

public class ContaCorrenteServiceTests
{
    private readonly ContaCorrenteService _service = new();

    [Theory]
    [InlineData(6000.00, false, 0.00)]
    [InlineData(500.00, true, 0.00)]
    [InlineData(2500.00, false, 12.50)]
    [InlineData(300.00, false, 25.00)]
    public void CalcularTaxaManutencao_DeveRetornarValorEsperado(
        double saldo,
        bool clienteEspecial,
        double taxaEsperada)
    {
        var resultado = _service.CalcularTaxaManutencao(
            (decimal)saldo, clienteEspecial);

        Assert.Equal((decimal)taxaEsperada, resultado);
    }
}