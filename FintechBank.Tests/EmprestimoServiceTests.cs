
using Xunit;
using FintechBank.App;

namespace FintechBank.Tests;

public class EmprestimoServiceTests
{
    private readonly EmprestimoService _service = new();

    [Theory]
    [InlineData(10000.00, 10, 800, 1050.00)]
    [InlineData(10000.00, 10, 600, 1120.00)]
    [InlineData(10000.00, 10, 400, 1200.00)]
    public void CalcularParcela_DeveAplicarJurosConformeScore(
        double valorEmprestimo,
        int quantidadeParcelas,
        int score,
        double parcelaEsperada)
    {
        var resultado = _service.CalcularParcela(
            valorEmprestimo, quantidadeParcelas, score);

        Assert.Equal(parcelaEsperada, resultado, 2);
    }

    [Theory]
    [InlineData(5000.00, 1500.00, true)]
    [InlineData(5000.00, 1600.00, false)]
    public void AprovarEmprestimo_DeveRespeitarMargemConsignavel(
        double rendaMensal,
        double valorParcela,
        bool aprovadoEsperado)
    {
        var resultado = _service.AprovarEmprestimo(
            rendaMensal, valorParcela);

        Assert.Equal(aprovadoEsperado, resultado);
    }
}