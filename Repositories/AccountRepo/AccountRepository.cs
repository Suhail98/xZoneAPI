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
        private readonly AppSettings appSettings;
        public AccountRepository(ApplicationDBContext _db, IOptions<AppSettings> _appSettings)
        {
            db = _db;
            appSettings = _appSettings.Value;
        }
        public Account register(Account account)
        {
            db.Accounts.Add(account);
            Save();
            return account;
        }
        public Account AuthenticateUser(string email, string password)
        {
            var account = db.Accounts.FirstOrDefault(x => x.Email == email && x.Password == password);
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
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
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

        public bool Save()
        {
            return db.SaveChanges() >= 0;
        }


    }
}
