using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xZoneAPI.Data;
using xZoneAPI.Models.Accounts;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace xZoneAPI.Repositories.AccountRepo
{
    public class AccountRepository : IAccountRepo
    {
        ApplicationDBContext db;
        IFriendRepository friendRepository;
        private readonly AppSettings appSettings;
        public AccountRepository(ApplicationDBContext _db, IOptions<AppSettings> _appSettings, IFriendRepository friendRepository)
        {
            db = _db;
            appSettings = _appSettings.Value;
            this.friendRepository = friendRepository;
        }
        public Account register(Account account)
        {
            db.Accounts.Add(account);
            account.bio = "";
            Save();
            return account;
        }
        public Account AuthenticateUser(string email, string password)
        {
            var account = db.Accounts.Include(u => u.Tasks).Include(u=>u.Projects)
                .Include("Projects.Sections")
                .Include("Projects.Sections.ProjectTasks")
                .Include(u=>u.Roadmaps)
                .Include(u=>u.Zones)
                .Include(u=>u.Skills)
                .Include(u=>u.Badges)
                .Include(u=>u.ZoneTasks)
                .ThenInclude(u=>u.ZoneTask)
                .FirstOrDefault(x => x.Email == email && x.Password == password);

            if (account == null)
                return null;
            var TokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, account.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = TokenHandler.CreateToken(tokenDescriptor);
            account.Token = TokenHandler.WriteToken(token);
           return account;
        }

        public Account FindAccountByEmail(string email)
        {
           Account account =db.Accounts.SingleOrDefault(x => x.Email == email);
            return account;
        }
        public Account FindAccountById(int id)
        {
            Account account = db.Accounts.SingleOrDefault(x => x.Id == id);
            return account;
        }
        public ICollection<Account> GetAllAccounts()
        {
            
            return db.Accounts.ToList();
        }
        public bool DeleteAccount(Account account)
        {
            db.Accounts.Remove(account);
            return Save();
        }
        public bool UpdateAccount(Account acount)
        {
            db.Accounts.Update(acount);
            return Save();
        }
        public Account getProfile(int AccountId)
        {
            Account account = db.Accounts
                .Include(u => u.Roadmaps)
                .Include(u => u.Zones)
                .ThenInclude(u=>u.Zone)
                .Include(u => u.Skills)
                .Include(u => u.Badges)
                .Include(u => u.Skills)
                .ThenInclude(u=>u.Skill)
                .FirstOrDefault(x => x.Id == AccountId);
            return account;
        }
        public bool Save()
        {
            return db.SaveChanges() >= 0;
        }

        public Account GetAccountWithItsBadges(int accountId)
        {
            Account account = db.Accounts.Include(u => u.Badges).FirstOrDefault(x => x.Id == accountId);
            return account;
        }
        public List<Account> FindAccountByName(string name)
        {
            return db.Accounts.Where(a => a.UserName.Contains(name)).ToList();
        }
    }
}
