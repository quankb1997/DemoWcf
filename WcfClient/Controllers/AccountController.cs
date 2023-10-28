using Microsoft.AspNetCore.Mvc;
using WcfClient.Models;
using WcfClient.Service;

namespace WcfClient.Controllers
{
    public class AccountController : Controller
    {
        IAccountService service;
        public AccountController(IAccountService service)
        {
            this.service = service;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Login(string email, string password)
        {
            AccountModels? acc = await service.Login(email, password);  
            if(acc == null)
            {
                return View();
            }
            return RedirectToAction("ListAccount");
        }
        public async Task<IActionResult> ListAccount()
        {
            var accounts = await service.GetAccounts();
            return View(accounts);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AccountModels acc)
        {
            if (ModelState.IsValid)
            {
                await service.Create(acc);
                return RedirectToAction("ListAccount");
            }

            return View(acc);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var book = await service.GetAccount(id);
            if (book != null)
            {
                return View(book);
            }
            return RedirectToAction("ListAccount");
        }
        [HttpPost]
        public async Task<IActionResult> Edit( int id, AccountModels acc)
        {
            if (ModelState.IsValid)
            {
                await service.Edit(acc, id);
                return RedirectToAction("ListAccount");
            }

            return View(acc);
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                await service.Delete(id);
                return RedirectToAction("ListAccount");
            }
            return View();
        }
    }
}
