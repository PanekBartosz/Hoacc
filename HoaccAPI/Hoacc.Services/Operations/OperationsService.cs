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