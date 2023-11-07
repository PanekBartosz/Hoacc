using HoaccIServices.Requests;

namespace HoaccIServices.Operations
{
    public interface IOperationsService
    {
        Task<HoaccCommon.Operations.OperationsDTO> GetOperations(int operationId);
        Task<int> CreateOperations(CreateOperations createOperations);
        Task EditOperations(int operationId, EditOperations editOperations);
        Task RemoveOperations(int operationId);
    }
}