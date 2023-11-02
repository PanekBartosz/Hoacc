using HoaccIData.Operations;
using HoaccIData.User;
using Microsoft.EntityFrameworkCore;

namespace HoaccDataSql.Operations
{
    public class OperationsRepository: IOperationsRepository
    {
        private readonly HoaccDbContext _context;
        private IOperationsRepository _operationsRepositoryImplementation;

        public OperationsRepository(HoaccDbContext context)
        {
            _context = context;
        }
        public async Task<HoaccDomain.Operations.Operations> GetOperations(int operationId)
        {
            var operation = await _context.Operations.FirstOrDefaultAsync(x=>x.OperationId == operationId);
            return new HoaccDomain.Operations.Operations(operation.OperationId,
                operation.Type,
                operation.Date,
                operation.Description,
                operation.Category,
                operation.Amount,
                operation.UserId);
        }
        
        public async Task<int> CreateOperations(HoaccDomain.Operations.Operations operations)
        {
            var operationsDAO =  new DAO.Operations { 
                OperationId = operations.OperationId,
                Type = operations.Type,
                Date = operations.Date,
                Description = operations.Description,
                Category = operations.Category,
                Amount = operations.Amount,
                UserId = operations.UserId
            };
            await _context.AddAsync(operationsDAO);
            await _context.SaveChangesAsync();
            return operationsDAO.OperationId;
        }

        public async Task EditOperations(int operationId, HoaccDomain.Operations.Operations operations)
        {
            var editOperations = await _context.Operations.FirstOrDefaultAsync(
                x=>x.OperationId == operations.OperationId);
            editOperations.Type = operations.Type;
            editOperations.Date = operations.Date;
            editOperations.Description = operations.Description;
            editOperations.Category = operations.Category;
            editOperations.Amount = operations.Amount;
            await _context.SaveChangesAsync();
        }
        
        public async Task RemoveOperations(int operationId)
        {
            var operation = await _context.Operations.FirstOrDefaultAsync(
                x => x.OperationId == operationId);

            if (operation == null)
            {
                throw new Exception("Operation not found");
            }
            _context.Operations.Remove(operation);
            await _context.SaveChangesAsync();
        }

    }

}