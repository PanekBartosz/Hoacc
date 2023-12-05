using HoaccCommon.Operations;
using Microsoft.EntityFrameworkCore;

namespace HoaccDataSql.Operations;

public class OperationsRepository : IOperationsRepositoryDTO
{
    private readonly HoaccDbContext _context;
    private IOperationsRepositoryDTO _operationsRepositoryImplementation;

    public OperationsRepository(HoaccDbContext context)
    {
        _context = context;
    }

    public async Task<OperationsDTO> GetOperations(int operationId)
    {
        var operation = await _context.Operations.FirstOrDefaultAsync(x => x.OperationId == operationId);
        return new OperationsDTO(operation.OperationId,
            operation.Type,
            operation.Date,
            operation.Description,
            operation.Category,
            operation.Amount,
            operation.UserId);
    }

    public async Task<IEnumerable<OperationsDTO>> GetOperationsByUser(int userId)
    {
        var operations = await _context.Operations
            .Where(x => x.UserId == userId)
            .ToListAsync();

        return operations.Select(MapOperationsToDTO);
    }

    public async Task<int> CreateOperations(OperationsDTO operations)
    {
        var operationsDAO = new DAO.Operations
        {
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

    public async Task EditOperations(int operationId, OperationsDTO operations)
    {
        var editOperations = await _context.Operations.FirstOrDefaultAsync(
            x => x.OperationId == operations.OperationId);
        editOperations.Type = operations.Type;
        editOperations.Date = operations.Date;
        editOperations.Description = operations.Description;
        editOperations.Category = operations.Category;
        editOperations.Amount = operations.Amount;
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<OperationsDTO>> GetOperationsByUserAndDateRange(int userId, DateTime startDate,
        DateTime endDate)
    {
        var operations = await _context.Operations
            .Where(op => op.UserId == userId && op.Date >= startDate && op.Date <= endDate)
            .ToListAsync();

        return operations.Select(MapOperationsToDTO);
    }

    public async Task RemoveOperations(int operationId)
    {
        var operation = await _context.Operations.FirstOrDefaultAsync(
            x => x.OperationId == operationId);

        if (operation == null) throw new Exception("Operation not found");
        _context.Operations.Remove(operation);
        await _context.SaveChangesAsync();
    }

    private OperationsDTO MapOperationsToDTO(DAO.Operations operation)
    {
        return new OperationsDTO(
            operation.OperationId,
            operation.Type,
            operation.Date,
            operation.Description,
            operation.Category,
            operation.Amount,
            operation.UserId
        );
    }
}