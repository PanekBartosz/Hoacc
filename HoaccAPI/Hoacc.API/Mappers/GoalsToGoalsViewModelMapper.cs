﻿using HoaccAPI.ViewModels;

namespace HoaccAPI.Mappers
{
    public class GoalsToGoalsViewModelMapper
    {
        public static GoalsViewModel 
            GoalsToGoalsViewModel(HoaccDomain.Goals.Goals goals)
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
}