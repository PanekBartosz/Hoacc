using HoaccAPI.ViewModels;
using HoaccDomain.Operations;

namespace HoaccAPI.Mappers
{
    public class OperationsToOperationsViewModelMapper
    {
        public static OperationsViewModel OperationsToOperationsViewModel(HoaccDomain.Operations.Operations operations)
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
        
        private static OperationsViewModel.CategoryTypeViewModel MapCategoryTypeToViewModel(Operations.CategoryType domainCategoryType)
        {
            switch (domainCategoryType)
            {
                case Operations.CategoryType.Bills:
                    return OperationsViewModel.CategoryTypeViewModel.Bills;
                case Operations.CategoryType.Food:
                    return OperationsViewModel.CategoryTypeViewModel.Food;
                case Operations.CategoryType.Education:
                    return OperationsViewModel.CategoryTypeViewModel.Education;
                case Operations.CategoryType.Entertainment:
                    return OperationsViewModel.CategoryTypeViewModel.Entertainment;
                default:
                    // Handle any unknown values
                    return OperationsViewModel.CategoryTypeViewModel.Unknown;
            }
        }

    }
}