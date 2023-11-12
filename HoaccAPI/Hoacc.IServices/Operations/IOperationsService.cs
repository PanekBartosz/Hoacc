using HoaccCommon.Operations;
using HoaccIServices.Requests;

namespace HoaccIServices.Operations;

public interface IOperationsService
{
    Task<OperationsDTO> GetOperations(int operationId);
    Task<OperationsDTO> CreateOperations(CreateOperations createOperations);
    Task EditOperations(int operationId, EditOperations editOperations);
    Task RemoveOperations(int operationId);
}