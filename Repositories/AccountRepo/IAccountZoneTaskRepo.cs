using System;
using System.Collections.Generic;
using xZoneAPI.Models.Accounts;

namespace xZoneAPI.Repositories.AccountRepo
{
    public interface IAccountZoneTaskRepo
    {
        AccountZoneTask AddTask(AccountZoneTask NewTak);
        bool DeleteTask(AccountZoneTask task);
        bool Save();
        bool UpdateTask(AccountZoneTask task);
        public ICollection<AccountZoneTask> GetAllAccountZoneTasks();
        ICollection<AccountZoneTask> GetAllAccountZoneTasksForAccount(int id);
        ICollection<AccountZoneTask> GetAllAccountZoneTasks(int accountID, int zoneTaskID);
        public ICollection<DateTime> GetListOfActiveDays(int userId);
        public AccountZoneTask GetAccountZoneTask(int accountId, int taskId);
        public int GetFinishedTasks(int userId);
    }
}