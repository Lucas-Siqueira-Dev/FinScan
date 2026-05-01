using FinScan.API.Services;

namespace FinScan.Tests;

public class ExtratorDadosTests
{
    [Fact]
    public void Deve_Extrair_Cnpj_Com_Sucesso()
    {
        // Arrange (Preparação)
        string textoBruto = "SUPERMERCADO TESTE\nCNPJ: 46.170.209/0001-25\nDATA: 19/02/2012";

        // Act (Ação)
        string resultado = ExtratorDados.ExtrairCnpj(textoBruto);

        // Assert (Validação)
        Assert.Equal("46.170.209/0001-25", resultado);
    }

    [Fact]
    public void Deve_Retornar_Nao_Encontrado_Quando_Sem_Cnpj()
    {
        string textoBruto = "CUPOM SEM IDENTIFICACAO FISCAL";
        string resultado = ExtratorDados.ExtrairCnpj(textoBruto);
        Assert.Equal("Não encontrado", resultado);
    }

    [Fact]
    public void Deve_Extrair_Valor_Com_Sucesso()
    {
        string textoBruto = "TOTAL A PAGAR R$ 1.500,00 OBRIGADO";
        string resultado = ExtratorDados.ExtrairValor(textoBruto);
        Assert.Equal("R$ 1.500,00", resultado);
    }

    [Fact]
    public void Deve_Extrair_Data_Com_Sucesso()
    {
        string textoBruto = "COMPRA REALIZADA EM 19/02/2012 PELO CLIENTE";
        string resultado = ExtratorDados.ExtrairData(textoBruto);
        Assert.Equal("19/02/2012", resultado);
    }
}