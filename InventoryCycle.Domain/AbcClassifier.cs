namespace InventoryCycle.Domain;

/// <summary>
/// Responsável pela classificação ABC dos produtos.
/// </summary>
public static class AbcClassifier
{
    /// <summary>
    /// Recalcula as classes (A, B, C) de uma lista de produtos baseada no volume de saída (AverageOutputRate).
    /// </summary>
    /// <param name="products">Lista de produtos para classificar.</param>
    /// <returns>A lista de produtos com a propriedade AssignedClass atualizada.</returns>
    /// <remarks>
    /// Regra de Classificação:
    /// - Classe A: Top 20% dos itens com maior movimentação.
    /// - Classe B: Próximos 30% dos itens.
    /// - Classe C: Restantes 50% dos itens.
    /// </remarks>
    public static List<InventoryProduct> RecalculateClasses(List<InventoryProduct> products)
    {
        if (products == null || products.Count == 0)
            return products ?? new List<InventoryProduct>();

        // Ordena os produtos do maior volume de saída para o menor
        var sortedProducts = products.OrderByDescending(p => p.AverageOutputRate).ToList();
        int totalCount = sortedProducts.Count;

        // Define os pontos de corte para cada classe usando arredondamento para cima
        int aLimit = (int)Math.Ceiling(totalCount * 0.20);
        int bLimit = (int)Math.Ceiling(totalCount * 0.50); // 20% + 30% = 50% acumulado

        for (int i = 0; i < totalCount; i++)
        {
            if (i < aLimit)
            {
                sortedProducts[i].AssignedClass = ProductClass.A;
            }
            else if (i < bLimit)
            {
                sortedProducts[i].AssignedClass = ProductClass.B;
            }
            else
            {
                sortedProducts[i].AssignedClass = ProductClass.C;
            }
        }

        return sortedProducts;
    }
}