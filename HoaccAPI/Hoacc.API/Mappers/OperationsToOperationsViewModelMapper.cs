using HoaccAPI.ViewModels;
using HoaccCommon.Operations;

namespace HoaccAPI.Mappers;

public class OperationsToOperationsViewModelMapper
{
    public static OperationsViewModel OperationsToOperationsViewModel(OperationsDTO operations)
    {
        var operationsViewModel = new OperationsViewModel
        {
            OperationId = operations.OperationId,
            Type = operations.Type,
            Date = operations.Date,
            Description = operations.Description,
            Amount = operations.Amount,
            UserId = operations.UserId
        };

        // Map the CategoryType enum to the corresponding view model enum
        operationsViewModel.Category = MapCategoryTypeToViewModel(operations.Category);
        return operationsViewModel;
    }

    private static OperationsViewModel.CategoryTypeViewModel MapCategoryTypeToViewModel(
        OperationsDTO.CategoryType domainCategoryType)
    {
        switch (domainCategoryType)
        {
            case OperationsDTO.CategoryType.Other:
                return OperationsViewModel.CategoryTypeViewModel.Other;
            case OperationsDTO.CategoryType.Bills:
                return OperationsViewModel.CategoryTypeViewModel.Bills;
            case OperationsDTO.CategoryType.Food:
                return OperationsViewModel.CategoryTypeViewModel.Food;
            case OperationsDTO.CategoryType.Education:
                return OperationsViewModel.CategoryTypeViewModel.Education;
            case OperationsDTO.CategoryType.Entertainment:
                return OperationsViewModel.CategoryTypeViewModel.Entertainment;
            default:
                // Handle any unknown values
                return OperationsViewModel.CategoryTypeViewModel.Unknown;
        }
    }
}