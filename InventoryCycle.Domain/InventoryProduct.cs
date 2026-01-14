namespace InventoryCycle.Domain;

/// <summary>
/// Representa um produto no contexto de inventário.
/// </summary>
public record InventoryProduct
{
    /// <summary>
    /// Identificador único do produto.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Código ou número da peça (SKU).
    /// </summary>
    public string PartNumber { get; init; } = string.Empty;

    /// <summary>
    /// Taxa média de saída/venda do produto. Usado para classificação ABC.
    /// </summary>
    public decimal AverageOutputRate { get; init; }

    /// <summary>
    /// Data da última contagem de inventário realizada.
    /// </summary>
    public DateTime LastInventoryDate { get; init; }

    /// <summary>
    /// Classificação ABC atribuída (A, B ou C). Pode ser nulo se ainda não classificado.
    /// </summary>
    public ProductClass? AssignedClass { get; set; }
}