using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Transport.Libuv.Internal.Networking;
using WebApiTest.Models;

namespace WebApiTest.Controllers
{


    [Route("api")]
    public class
        AccountController : Controller
    {
        private DataContext _db;

        public AccountController(DataContext context)
        {
            _db = context;
            if (!_db.CardHolders.Any())
            {
                _db.CardHolders.Add(new Cardholder { Count100 = 1, Count200 = 2, Count1000 = 0, Count5000 = 0 });
                _db.CardHolders.Add(new Cardholder { Count100 = 5, Count200 = 42, Count1000 = 200, Count5000 = 228 });
                _db.SaveChanges();
            }
            
            if (!_db.Atms.Any())
            {
                _db.Atms.Add(new Atm {Count100 = 1000, Count200 = 1000, Count1000 = 1000, Count5000 = 1000});
                _db.Atms.Add(new Atm {Count100 =  1,Count200 = 22,Count1000 = 23,Count5000 = 2});
                _db.SaveChanges();
            }

            if (_db.Accounts.Any()) return;
            _db.Accounts.Add(new Account { CardholderId = 1, Amount = 2228 });
            _db.Accounts.Add(new Account { Amount = 0, CardholderId = 2 });
            _db.SaveChanges();
            
        }

        [HttpGet("Cardholder/{id}")]
        public IActionResult GetCardholder(int id)
        {
            var cardholder = _db.CardHolders.FirstOrDefault(x => x.Id == id);
            if (cardholder == null)
                return NotFound();
            return new ObjectResult(cardholder);

        }
    
        [HttpGet("Cardholder")]
        public IEnumerable<Cardholder> GetCardholders()
        {
            return _db.CardHolders.ToList();
        }

        [HttpPost("Cardholder")]
        public IActionResult PostCardholder([FromBody]Cardholder cardholder)
        {
            if (cardholder == null)
            {
                return BadRequest();
            }

            _db.CardHolders.Add(cardholder);
            _db.SaveChanges();
            return Ok(cardholder);
        }

        [HttpGet("[controller]")]
        public IEnumerable<Account> GetAccounts()
        {
            return _db.Accounts.ToList();
        }

        [HttpGet("[controller]/{id}")]
        public IActionResult GetAccount(int id)
        {
            var account = _db
                .Accounts.FirstOrDefault(x => x.Id == id);
            if (account == null)
            {
                return NotFound();
            }
            return new ObjectResult(account);
        }

        [HttpPost("[controller]")]
        public IActionResult PostAccount([FromBody]int cardholderId)
        {
            if (_db.Accounts.Any(x => x.CardholderId == cardholderId))
            {
                return BadRequest();
            }

            if (cardholderId == 0)
            {
                return BadRequest();
            }

            var newAccount = new Account { CardholderId = cardholderId, Amount = 0 };
            _db.Accounts.Add(newAccount);
            _db.SaveChanges();
            return Ok(newAccount);
        }

        [HttpGet("atm")]
        public IEnumerable<Atm> GetAtms()
        {
            return _db.Atms.ToList();
        }

        [HttpGet("atm/{id}")]
        public IActionResult GetAtm(int id)
        {
            var atm = _db.Atms.FirstOrDefault(x => x.Id == id);
            if (atm == null)
                return NotFound();
            return Ok(atm);
        }

        [HttpPut("[controller]/{id}/withdraw/{atmId}")]
        public IActionResult Withdraw(int id, int atmId, [FromBody]int value)
        {
            var account = _db.Accounts.FirstOrDefault(x => x.Id == id);
            if (account == null)
            {
                return BadRequest();
            }
            var atm = _db.Atms.FirstOrDefault(x => x.Id == atmId);
            if (atm == null)
            {
                return BadRequest();
            }
            var cardholder = _db.CardHolders.FirstOrDefault(x => x.Id == account.CardholderId);
            if (value > account.Amount)
            {
                return new ObjectResult("You haven't enough money on your account");
            }

            if (!atm.TryWithdraw(value, cardholder))
            {
                return new ObjectResult("Atm haven't enough money");
            }
            return Ok("Operation successful");
        }
    }
}
