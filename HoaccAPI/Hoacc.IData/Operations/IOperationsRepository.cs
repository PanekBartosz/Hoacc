namespace HoaccIData.Operations
{
    public interface IOperationsRepository
    {
        Task<HoaccDomain.Operations.Operations> GetOperations(int operationId);
        Task<int> CreateUser(HoaccDomain.Operations.Operations operations);
        Task EditOperations(int operationId, HoaccDomain.Operations.Operations operations);
        Task RemoveOperations(int operationId);
    }
}