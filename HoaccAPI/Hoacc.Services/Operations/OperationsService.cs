using HoaccCommon.Operations;
using HoaccIServices.Operations;
using HoaccIServices.Requests;

namespace HoaccServices.Operations;

public class OperationsService : IOperationsService
{
    private readonly IOperationsRepositoryDTO _operationsRepository;

    public OperationsService(IOperationsRepositoryDTO operationsRepository)
    {
        _operationsRepository = operationsRepository;
    }

    public Task<OperationsDTO> GetOperations(int operationsId)
    {
        return _operationsRepository.GetOperations(operationsId);
    }

    public async Task<IEnumerable<OperationsDTO>> GetOperationsByUser(int userId)
    {
        var operations = await _operationsRepository.GetOperationsByUser(userId);
        return operations;
    }

    public async Task<IEnumerable<OperationsDTO>> GetFilteredOperationsByUser
        (int userId, DateTime startDate, DateTime endDate, string type)
    {
        var operations = await _operationsRepository.GetOperationsByUser(userId);

        return operations.Where(op => op.UserId == userId &&
                                      op.Date >= startDate &&
                                      op.Date <= endDate &&
                                      op.Type == type);
    }

    public async Task<ProfitChartData> GetProfitDataForUser(int userId, DateTime startDate, DateTime endDate)
    {
        var operations = await _operationsRepository.GetOperationsByUserAndDateRange(userId, startDate, endDate);

        var totalIncome = operations
            .Where(op => op.UserId == userId && op.Date >= startDate && op.Date <= endDate && op.Type == "income")
            .Sum(op => (decimal) op.Amount);

        var totalExpense = operations
            .Where(op => op.UserId == userId && op.Date >= startDate && op.Date <= endDate && op.Type == "outcome")
            .Sum(op => (decimal) op.Amount);

        return new ProfitChartData
        {
            TotalIncome = totalIncome,
            TotalExpense = totalExpense
        };
    }

    public async Task<OperationsDTO> CreateOperations(CreateOperations createOperations)
    {
        var operations = new OperationsDTO(createOperations.Type,
            createOperations.Date, createOperations.Description, createOperations.Category,
            createOperations.Amount, createOperations.UserId);
        operations.OperationId = await _operationsRepository.CreateOperations(operations);
        return operations;
    }

    public async Task EditOperations(int operationId, EditOperations createOperations)
    {
        var operations = await _operationsRepository.GetOperations(operationId);
        operations.EditOperations(createOperations.Type,
            createOperations.Date, createOperations.Description, createOperations.Category,
            createOperations.Amount);
        await _operationsRepository.EditOperations(operationId, operations);
    }

    public async Task RemoveOperations(int operationId)
    {
        await _operationsRepository.RemoveOperations(operationId);
    }
}