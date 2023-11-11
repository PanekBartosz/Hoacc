using HoaccIServices.Requests;

namespace HoaccIServices.Operations
{
    public interface IOperationsService
    {
        Task<HoaccCommon.Operations.OperationsDTO> GetOperations(int operationId);
        Task<HoaccCommon.Operations.OperationsDTO> CreateOperations(CreateOperations createOperations);
        Task EditOperations(int operationId, EditOperations editOperations);
        Task RemoveOperations(int operationId);
    }
}