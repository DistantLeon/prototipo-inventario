namespace InventoryCycle.Domain;

public record InventoryProduct
{
    public Guid Id { get; init; }
    public string PartNumber { get; init; } = string.Empty;
    public decimal AverageOutputRate { get; init; }
    public DateTime LastInventoryDate { get; init; }
    public ProductClass? AssignedClass { get; set; }
}
