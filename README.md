# FintechBank

Aplicação bancária de exemplo desenvolvida em **.NET 10** para a lista de exercícios da disciplina **Garantia da Qualidade de Software / Gestão e Qualidade de Software**.

O projeto aplica boas práticas de qualidade de software: regras de negócio isoladas em classes de serviço e cobertas por **testes unitários parametrizados com xUnit**.

---

## 📚 Visão geral

A aplicação simula operações bancárias em dois domínios:

| Domínio | Responsabilidade |
|---|---|
| **Conta corrente** | Cálculo da taxa de manutenção e validação de saque (com cheque especial) |
| **Empréstimo** | Cálculo da parcela conforme o score de crédito e aprovação por margem consignável |

---

## 🗂️ Estrutura do repositório

```
fintech-bank-xunit/
├── FintechBank.App/                    # Regras de negócio
│   ├── ContaCorrenteService.cs
│   ├── EmprestimoService.cs
│   └── FintechBank.App.csproj
├── FintechBank.Tests/                  # Testes unitários (xUnit)
│   ├── ContaCorrenteServiceTests.cs
│   ├── EmprestimoServiceTests.cs
│   └── FintechBank.Tests.csproj
├── FintechBank.slnx
├── .gitignore
├── LICENSE
└── README.md
```

---

## ✅ Requisitos

- [.NET SDK 10.0](https://dotnet.microsoft.com/download) ou superior.
- Verifique a instalação com:

```bash
dotnet --version
```

---

## ⚙️ Como compilar a solução

```bash
# 1. Clonar o repositório
git clone https://github.com/arthurmdiniz/fintech-bank-xunit.git
cd fintech-bank-xunit

# 2. Compilar a solução (as dependências são restauradas automaticamente)
dotnet build
```

---

## 🧪 Como executar os testes unitários

```bash
dotnet test
```

O comando restaura, compila e executa todas as suítes de teste do projeto `FintechBank.Tests`.

---

## 🧩 Regras de negócio (classes de serviço)

### `ContaCorrenteService`

| Método | Regra implementada |
|---|---|
| `CalcularTaxaManutencao(decimal saldo, bool clienteEspecial)` | Saldo `>= R$ 5.000,00` **ou** `clienteEspecial == true` → `0,00` (isento); saldo entre `R$ 1.000,00` e `R$ 4.999,99` (não especial) → `12,50`; saldo `< R$ 1.000,00` (não especial) → `25,00` |
| `PodeRealizarSaque(decimal saldo, decimal limiteChequeEspecial, decimal valorSaque)` | `true` apenas quando `valorSaque <= saldo + limiteChequeEspecial` |

### `EmprestimoService`

| Método | Regra implementada |
|---|---|
| `CalcularValorParcela(decimal valorTotal, int parcelas, int scoreCredito)` | `scoreCredito >= 700` → juros de 5% (`valorTotal * 1.05m`); entre `500` e `699` → 12% (`1.12m`); `< 500` → 20% (`1.20m`). Retorna o valor da parcela: `valorComJuros / parcelas` |
| `AprovarEmprestimo(decimal rendaMensal, decimal valorParcela)` | `true` apenas quando a parcela compromete **no máximo 30%** da renda: `valorParcela <= rendaMensal * 0.30m` |

---

## 🧪 Testes parametrizados (`[Theory]` / `[InlineData]`)

Os testes utilizam `[Theory]` combinado com `[InlineData]`, permitindo validar vários cenários em um único método de teste. Cada linha de `[InlineData]` representa um caso de entrada e o resultado esperado.

```csharp
[Theory]
[InlineData(6000.00, false, 0.00)]   // Saldo alto -> isento
[InlineData(500.00,  true,  0.00)]   // Cliente especial -> isento
[InlineData(2500.00, false, 12.50)]  // Saldo intermediário -> R$ 12,50
[InlineData(300.00,  false, 25.00)]  // Saldo baixo -> R$ 25,00
public void CalcularTaxaManutencao_DeveRetornarValorEsperado(
    double saldo, bool clienteEspecial, double taxaEsperada)
{
    var service = new ContaCorrenteService();

    var resultado = service.CalcularTaxaManutencao((decimal)saldo, clienteEspecial);

    Assert.Equal((decimal)taxaEsperada, resultado);
}
```

Confira abaixo o que cada suíte cobre:

- **`ContaCorrenteServiceTests`** — taxa de manutenção e validação de saque.
- **`EmprestimoServiceTests`** — cálculo da parcela por score e aprovação por margem consignável.

---

## 📄 Licença

Distribuído sob a **Licença MIT**. Consulte o arquivo [LICENSE](LICENSE) para mais detalhes.
