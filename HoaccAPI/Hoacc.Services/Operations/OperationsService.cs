using HoaccIData.Operations;
using HoaccIServices.Operations;
using HoaccIServices.Requests;

namespace HoaccServices.Operations
{
    public class OperationsService: IOperationsService
    {
        private readonly IOperationsRepository _operationsRepository;

        public OperationsService(IOperationsRepository operationsRepository)
        {
            _operationsRepository = operationsRepository;
        }

        public Task<HoaccCommon.Operations.OperationsDTO> GetOperations(int operationsId)
        {
            return _operationsRepository.GetOperations(operationsId);
        }

        public async Task<HoaccCommon.Operations.OperationsDTO> CreateOperations(CreateOperations createOperations)
        {
            var operations = new HoaccCommon.Operations.OperationsDTO(createOperations.Type,
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
    
}