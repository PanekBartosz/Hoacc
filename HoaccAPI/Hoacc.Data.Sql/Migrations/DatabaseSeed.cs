﻿
using Org.BouncyCastle.Tls;

namespace HoaccDataSql.Migrations
{
    // the class responsible for filling the database with test data
     public class DatabaseSeed
     {
         private readonly HoaccDbContext _context;
         
         //Injecting an instance of the HoaccDbContext class via the constructor.
         public DatabaseSeed(HoaccDbContext context)
         {
             _context = context;
         }
         
         public void Seed()
         {
             #region CreateUsers
             var userList = BuildUserList();
             _context.User.AddRange(userList);
             _context.SaveChanges();
             #endregion
             
             #region CreateGoals
             var goalsList = BuildGoalsList(userList);
             _context.Goals.AddRange(goalsList);
             _context.SaveChanges();
             #endregion
             
             #region CreateNotification
             var notificationList = BuildNotificationList(userList);
             _context.Notification.AddRange(notificationList);
             _context.SaveChanges();
             #endregion
             
             #region CreateOperations
             var operationsList = BuildOperationsList(userList);
             _context.Operations.AddRange(operationsList);
             _context.SaveChanges();
             #endregion
         }
    
         private IEnumerable<DAO.User> BuildUserList()
         {
             var userList = new List<DAO.User>();
             var user = new DAO.User()
             {
                 Email = "1xyz@gmail.com",
                 Password = "passwd",
             };
             userList.Add(user);
    
             var user2 = new DAO.User()
             {
                 Email = "2xyz@gmail.com",
                 Password = "passwd",
             };
             userList.Add(user2);
             
             for (int i = 3; i <= 20; i++)
             {
                 var user3 = new DAO.User
                 {
                     Email = i + "xyz@gmail.com",
                     Password = "passwd",
                 };
                 userList.Add(user3);
             }
    
             return userList;
         }
    
         private IEnumerable<DAO.Goals> BuildGoalsList(IEnumerable<DAO.User> userList)
         {
             var goalsList = new List<DAO.Goals>();
             var rand = new Random();
             var userCount = userList.Count();
             for (int i = 1; i <= 5; i++)
             {
                 var goals = new DAO.Goals
                 {
                     Name = "goal" + i,
                     GoalAmount = 1000,
                     CurrentAmount = i * 100,
                     UserId = userList.ToList()[rand.Next(userCount)].UserId
                 };
                 goalsList.Add(goals);
             }
             
             return goalsList;
         }
         
         private IEnumerable<DAO.Notification> BuildNotificationList(IEnumerable<DAO.User> userList)
         {
             var notificationList = new List<DAO.Notification>();
             var rand = new Random();
             var userCount = userList.Count();
             for (int i = 1; i <= 5; i++)
             {
                 var notification = new DAO.Notification
                 {
                     Name = "notification" + i,
                     Date = new DateTime(1970, i, 7),
                     Amount = i * 100,
                     UserId = userList.ToList()[rand.Next(userCount)].UserId
                 };
                 notificationList.Add(notification);
             }
             
             return notificationList;
         }
         
         private IEnumerable<DAO.Operations> BuildOperationsList(IEnumerable<DAO.User> userList)
         {
             var operationsList = new List<DAO.Operations>();
             var userCount = userList.Count();
             for (int i = 1; i <= 25; i++)
             {
                 Random random = new Random();

                 // Get all values from CategoryType enum
                 HoaccDomain.Operations.Operations.CategoryType[] allCategories = (
                     HoaccDomain.Operations.Operations.CategoryType[])Enum
                     .GetValues(typeof(HoaccDomain.Operations.Operations.CategoryType));
                 
                 // Generate a random index to select a category
                 int randomIndex = random.Next(0, allCategories.Length);
                 var operation = new DAO.Operations
                 {
                     Type = "operation" + i,
                     Date = new DateTime(1970, 1, i),
                     Description = "description" + i,
                     Category = allCategories[randomIndex],
                     Amount = i * 10,
                     UserId = userList.ToList()[random.Next(userCount)].UserId
                 };
                 operationsList.Add(operation);
             }
             
             return operationsList;
         }
     }
}