namespace InventoryCycle.Domain;

/// <summary>
/// Resultado do cálculo de uma contagem de inventário.
/// </summary>
public record CountResult
{
    /// <summary>
    /// Quantidade esperada (sistema).
    /// </summary>
    public decimal SnapshotQty { get; init; }

    /// <summary>
    /// Quantidade efetivamente contada.
    /// </summary>
    public decimal CountedQty { get; init; }

    /// <summary>
    /// Percentual de acuracidade da contagem.
    /// </summary>
    public decimal AccuracyPercentage { get; init; }

    /// <summary>
    /// Diferença absoluta entre contagem e sistema (Counted - Snapshot).
    /// </summary>
    public decimal Variance { get; init; }

    /// <summary>
    /// Indica se é necessário realizar ajuste de estoque.
    /// </summary>
    public bool IsAdjustmentNeeded { get; init; }
}