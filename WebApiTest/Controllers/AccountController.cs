using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
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
            if (_db.CardHolders.Any())
            {
                _db.CardHolders.Add(new Cardholder { Count100 = 1, Count200 = 2, Count1000 = 0, Count5000 = 0 });
                _db.CardHolders.Add(new Cardholder { Count100 = 5, Count200 = 42, Count1000 = 200, Count5000 = 228 });
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
            var account = _db.Accounts.FirstOrDefault(x => x.Id == id);
            if (account == null)
            {
                return NotFound();
            }
            return new ObjectResult(account);
        }

        [HttpPut("[controller]")]
        public IActionResult PutAccount([FromBody]int cardholderId)
        {
            if (_db.Accounts.Any(x => x.CardholderId == cardholderId))
            {
                return BadRequest();
            }

            var newAccount = new Account { CardholderId = cardholderId, Amount = 0 };
            _db.Accounts.Add(newAccount);
            _db.SaveChanges();
            return Ok(newAccount);
        }

        [HttpPost("[controller]/{id}/withdraw/{AtmId}")]
        public IActionResult Withdraw(int id, int AtmId, [FromBody]int value)
        {
            var account = _db.Accounts.FirstOrDefault(x => x.Id == id);
            var cardhodler = _db.CardHolders.FirstOrDefault(x => x.Id == account.CardholderId);
            var atm = _db.Atms.FirstOrDefault(x => x.Id == AtmId);
            if (atm == null)
            {
                return BadRequest();
            }
            if (account == null)
            {
                return BadRequest();
            }
            if (value > account.Amount)
            {
                return new ObjectResult("You haven't enough money on your account");
            }

            if (!atm.TryWithdraw(value, cardhodler))
            {
                return new ObjectResult("Atm haven't enough money");
            }
            return Ok("Operation succesful");

        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
