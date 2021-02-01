using DotIndiaPvtLtd.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotIndiaPvtLtd.Controllers
{
    public class UsersController : Controller
    {
        private readonly IAccountRepository accountRepository;
        public UsersController(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await accountRepository.GetUsers();
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            accountRepository.DeleteUser(id);

            var users = await accountRepository.GetUsers();
            return View("Index",users);
        }
    }
}
