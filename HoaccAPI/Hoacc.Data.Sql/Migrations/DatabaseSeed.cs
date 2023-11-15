using HoaccCommon.Operations;

namespace HoaccDataSql.Migrations;

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
        var user = new DAO.User
        {
            Email = "1xyz@gmail.com",
            Password = "passwd"
        };
        userList.Add(user);

        var user2 = new DAO.User
        {
            Email = "2xyz@gmail.com",
            Password = "passwd"
        };
        userList.Add(user2);

        for (var i = 3; i <= 20; i++)
        {
            var user3 = new DAO.User
            {
                Email = i + "xyz@gmail.com",
                Password = "passwd"
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
        for (var i = 1; i <= 5; i++)
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
        for (var i = 1; i <= 5; i++)
        {
            var notification = new DAO.Notification
            {
                Name = "notification" + i,
                Date = new DateTime(2023, 11, i),
                Amount = i * 100,
                UserId = 1
            };
            notificationList.Add(notification);
        }

        return notificationList;
    }

    private IEnumerable<DAO.Operations> BuildOperationsList(IEnumerable<DAO.User> userList)
    {
        var operationsList = new List<DAO.Operations>();
        var userCount = userList.Count();
        for (var i = 1; i <= 25; i++)
        {
            var random = new Random();

            // Get all values from CategoryType enum
            var allCategories = (
                OperationsDTO.CategoryType[]) Enum
                .GetValues(typeof(OperationsDTO.CategoryType));

            // Generate a random index to select a category
            var randomIndex = random.Next(0, allCategories.Length);
            
            // Generate a random boolean to determine the type
            var isIncome = random.Next(0, 2) == 0;
            var type = isIncome ? "income" : "outcome";
            
            var operation = new DAO.Operations
            {
                Type = type,
                Date = new DateTime(2023, 11, i),
                Description = "description" + i,
                Category = allCategories[randomIndex],
                Amount = i * 10,
                UserId = 1
            };
            operationsList.Add(operation);
        }

        return operationsList;
    }
}