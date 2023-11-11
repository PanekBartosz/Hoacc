namespace HoaccCommon.Operations;

public interface IOperationsRepositoryDTO
{
    Task<OperationsDTO> GetOperations(int operationId);
    Task<int> CreateOperations(OperationsDTO operations);
    Task EditOperations(int operationId, OperationsDTO operations);
    Task RemoveOperations(int operationId);
}