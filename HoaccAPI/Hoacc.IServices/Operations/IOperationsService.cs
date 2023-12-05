using HoaccCommon.Operations;
using HoaccIServices.Requests;

namespace HoaccIServices.Operations;

public interface IOperationsService
{
    Task<OperationsDTO> GetOperations(int operationId);
    Task<IEnumerable<OperationsDTO>> GetOperationsByUser(int userId);

    Task<IEnumerable<OperationsDTO>> GetFilteredOperationsByUser
        (int userId, DateTime startDate, DateTime endDate, string type);

    Task<ProfitChartData> GetProfitDataForUser
        (int userId, DateTime startDate, DateTime endDate);

    Task<OperationsDTO> CreateOperations(CreateOperations createOperations);
    Task EditOperations(int operationId, EditOperations editOperations);
    Task RemoveOperations(int operationId);
}

public class ProfitChartData
{
    public decimal TotalIncome { get; set; }
    public decimal TotalExpense { get; set; }
}