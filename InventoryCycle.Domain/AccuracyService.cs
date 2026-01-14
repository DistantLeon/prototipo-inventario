namespace InventoryCycle.Domain;

/// <summary>
/// Serviço para cálculos de acuracidade e variação de inventário.
/// </summary>
public static class AccuracyService
{
    /// <summary>
    /// Calcula os resultados de uma contagem de inventário comparando o sistema (snapshot) com o físico (counted).
    /// </summary>
    /// <param name="snapshotQty">Quantidade registrada no sistema no momento do corte.</param>
    /// <param name="countedQty">Quantidade física contada.</param>
    /// <returns>Objeto CountResult com métricas de acuracidade.</returns>
    public static CountResult Calculate(decimal snapshotQty, decimal countedQty)
    {
        // Diferença absoluta (pode ser negativa se faltar item)
        decimal variance = countedQty - snapshotQty;
        decimal accuracyPercentage;

        // Cálculo da porcentagem de acuracidade
        if (snapshotQty == 0)
        {
            // Se o sistema diz 0 e contamos 0, está 100% certo. Se contamos algo, erro total (0%).
            accuracyPercentage = countedQty == 0 ? 100m : 0m;
        }
        else
        {
            // Fórmula: (1 - (|Diferença| / QtdSistema)) * 100
            accuracyPercentage = (1 - (Math.Abs(variance) / snapshotQty)) * 100;
        }

        return new CountResult
        {
            SnapshotQty = snapshotQty,
            CountedQty = countedQty,
            Variance = variance,
            AccuracyPercentage = accuracyPercentage,
            // Ajuste é necessário se houver qualquer divergência
            IsAdjustmentNeeded = variance != 0
        };
    }
}