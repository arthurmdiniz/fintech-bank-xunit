namespace FintechBank.App;

public class EmprestimoService
{
    public decimal CalcularValorParcela(
        decimal valorTotal,
        int parcelas,
        int scoreCredito)
    {
        decimal multiplicador;

        if (scoreCredito >= 700)
        {
            multiplicador = 1.05m;
        }
        else if (scoreCredito >= 500)
        {
            multiplicador = 1.12m;
        }
        else
        {
            multiplicador = 1.20m;
        }

        return valorTotal * multiplicador / parcelas;
    }

    public bool AprovarEmprestimo(decimal rendaMensal, decimal valorParcela)
    {
        return valorParcela <= rendaMensal * 0.30m;
    }
}