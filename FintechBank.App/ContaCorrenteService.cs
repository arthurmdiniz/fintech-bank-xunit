namespace FintechBank.App;

public class ContaCorrenteService
{
    public decimal CalcularTaxaManutencao(decimal saldo, bool clienteEspecial)
    {
        if (clienteEspecial || saldo >= 5000)
        {
            return 0.00m;
        }

        if (saldo >= 1000)
        {
            return 12.50m;
        }

        return 25.00m;
    }

    public bool PodeRealizarSaque(
        decimal saldo,
        decimal limiteChequeEspecial,
        decimal valorSaque)
    {
        return valorSaque <= saldo + limiteChequeEspecial;
    }
}