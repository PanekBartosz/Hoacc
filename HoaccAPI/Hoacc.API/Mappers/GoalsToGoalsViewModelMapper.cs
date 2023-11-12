using HoaccAPI.ViewModels;
using HoaccCommon.Goals;

namespace HoaccAPI.Mappers;

public class GoalsToGoalsViewModelMapper
{
    public static GoalsViewModel
        GoalsToGoalsViewModel(GoalsDTO goals)
    {
        var goalsViewModel = new GoalsViewModel
        {
            GoalsId = goals.GoalsId,
            Name = goals.Name,
            GoalAmount = goals.GoalAmount,
            CurrentAmount = goals.CurrentAmount,
            UserId = goals.UserId
        };
        return goalsViewModel;
    }
}