# Contexto
Atue como um Engenheiro de Software Sênior em .NET. Sua tarefa é criar um módulo de **Lógica de Domínio** (Domain Logic) para um sistema de Inventário Rotativo (Cycle Counting).
O código deve ser "Agnóstico de Infraestrutura", focado puramente em algoritmos de classificação e agendamento.

# Stack Tecnológica
- Linguagem: C# (.NET 8.0)
- Tipo de Projeto: Class Library (`InventoryCycle.Domain`)
- Testes: xUnit (`InventoryCycle.Tests`)

# Estrutura de Classes Solicitada

## 1. Entidades de Dados (DTOs/Records)
Crie `InventoryProduct.cs`:
- `Id` (Guid)
- `PartNumber` (string)
- `AverageOutputRate` (decimal) - Média de saída/venda.
- `LastInventoryDate` (DateTime)
- `AssignedClass` (Enum: A, B, C) - Inicialmente pode ser nulo ou default.

Crie `CountResult.cs`:
- `SnapshotQty` (decimal)
- `CountedQty` (decimal)
- `AccuracyPercentage` (decimal)
- `Variance` (decimal)
- `IsAdjustmentNeeded` (bool)

## 2. Serviços de Cálculo (Core Logic)

### 2.1 Classificação ABC (`AbcClassifier.cs`)
Implemente um método estático `List<InventoryProduct> RecalculateClasses(List<InventoryProduct> products)`.
**Regra de Negócio (Baseada em Volume de Itens):**
1. Ordenar a lista por `AverageOutputRate` (Decrescente).
2. Calcular os índices de corte baseados no total de itens da lista:
   - **Classe A:** Top 20% (0.20).
   - **Classe B:** Próximos 30% (0.30).
   - **Classe C:** Restantes 50% (0.50).
3. Atribuir a propriedade `AssignedClass` para cada item e retornar a lista atualizada.
*Nota: Use `Math.Ceiling` para arredondar os índices de corte.*

### 2.2 Agendamento (`SchedulerService.cs`)
Implemente um método estático `bool IsDueForCount(InventoryProduct product, DateTime referenceDate)`.
**Regra de Frequência:**
- **Classe A:** A cada 30 dias.
- **Classe B:** A cada 90 dias.
- **Classe C:** A cada 90 dias.
*Lógica:* Se `(LastInventoryDate + Frequencia) <= referenceDate`, retorna `true`.

### 2.3 Cálculo de Acuracidade (`AccuracyService.cs`)
Implemente `CountResult Calculate(decimal snapshotQty, decimal countedQty)`.
**Fórmulas:**
- `Variance` = `CountedQty` - `SnapshotQty`.
- `AccuracyPercentage`:
    - Se `SnapshotQty` == 0 e `CountedQty` == 0 -> 100%.
    - Se `SnapshotQty` == 0 e `CountedQty` != 0 -> 0%.
    - Caso contrário: `(1 - (Math.Abs(Variance) / SnapshotQty)) * 100`.
- `IsAdjustmentNeeded`: Se `Variance` != 0, retorna `true`.

## 3. Testes Unitários (Essencial)
Crie `CycleCountTests.cs` cobrindo:

### Cenário A: Classificação ABC
- Crie uma lista mock de 10 produtos com `AverageOutputRate` de 10 a 100.
- Execute o `AbcClassifier`.
- Verifique se os top 2 (20%) viraram 'A', os próximos 3 (30%) viraram 'B' e o resto 'C'.

### Cenário B: Agendamento (Scheduler)
- Crie um produto Classe A contado há 25 dias. `IsDueForCount` deve ser `false`.
- Crie um produto Classe A contado há 31 dias. `IsDueForCount` deve ser `true`.
- Repita para Classe B (limite de 90 dias).

### Cenário C: Acuracidade
- Teste Snapshot=100, Counted=95. Variance deve ser -5 e Accuracy deve ser 95%.
- Teste Snapshot=0, Counted=5. Accuracy deve ser 0%.

# Restrições de Saída
1. Forneça o código completo, arquivo por arquivo.
2. Não use bibliotecas externas além do xUnit.
3. Use `decimal` para cálculos de quantidade.