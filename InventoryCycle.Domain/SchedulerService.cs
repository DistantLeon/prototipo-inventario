namespace InventoryCycle.Domain;

/// <summary>
/// Serviço responsável por determinar o agendamento de inventários.
/// </summary>
public static class SchedulerService
{
    /// <summary>
    /// Verifica se um produto deve ser contado na data de referência.
    /// </summary>
    /// <param name="product">O produto a ser verificado.</param>
    /// <param name="referenceDate">A data de referência para a verificação (geralmente 'hoje').</param>
    /// <returns>True se o prazo de contagem venceu, False caso contrário.</returns>
    /// <remarks>
    /// Frequência de contagem:
    /// - Classe A: A cada 30 dias.
    /// - Classe B e C: A cada 90 dias.
    /// </remarks>
    public static bool IsDueForCount(InventoryProduct product, DateTime referenceDate)
    {
        // Se o produto não tem classe, não pode ser agendado
        if (product.AssignedClass == null)
            return false;

        // Determina a frequência em dias baseada na classe
        int frequencyDays = product.AssignedClass switch
        {
            ProductClass.A => 30,
            ProductClass.B => 90,
            ProductClass.C => 90,
            _ => 90
        };

        // Verifica se a data da última contagem + frequência é anterior ou igual à data de referência
        return product.LastInventoryDate.AddDays(frequencyDays) <= referenceDate;
    }
}