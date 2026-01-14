namespace InventoryCycle.Domain;

public record CountResult
{
    public decimal SnapshotQty { get; init; }
    public decimal CountedQty { get; init; }
    public decimal AccuracyPercentage { get; init; }
    public decimal Variance { get; init; }
    public bool IsAdjustmentNeeded { get; init; }
}
