﻿using HoaccCommon.Goals;
using HoaccIServices.Requests;

namespace HoaccIServices.Goals;

public interface IGoalsService
{
    Task<GoalsDTO> GetGoals(int goalsId);
    Task<IEnumerable<GoalsDTO>> GetGoalsByUser(int userId);
    Task<GoalsDTO> CreateGoals(CreateGoals createGoals);
    Task EditGoals(int goalsId, EditGoals editGoals);
    Task RemoveGoals(int goalsId);
}