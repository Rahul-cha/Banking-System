using AdminCheckWeb.Data;
using AdminCheckWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace AdminCheckWeb.Controllers
{

    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;


        public CategoryController(ApplicationDbContext db)
        {
            _db = db;

        }
        public IActionResult Index()
        {
            var objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //EDIT
        public IActionResult Edit(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var categoryFromDb = _db.Categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View();
        }



        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.Description)
            {
                ModelState.AddModelError("CustomError", "Name and Description cannot match");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }



        //[HttpGet]

        //public IActionResult TransferMoney()
        //{
        //    var TDetails = _db.UserDetails.ToList();
        //    return View(TDetails);
        //}

        public IActionResult Admin()
        {
            var allUserDetails = _db.UserDetails.ToList();
            return View(allUserDetails);
        }


        public IActionResult UserD()
        {
            var allUserDetails = _db.UserDetails.ToList();
            return View(allUserDetails);
        }

        public IActionResult FinancialForecast()
        {

            List<FinancialForecast> financialForecasts = new List<FinancialForecast>();

            var allUserDetails = _db.UserDetails.ToList();

            foreach (var item in allUserDetails)
            {
                FinancialForecast forecast = new FinancialForecast();
                var name = _db.logins.Select(y=>y.username).FirstOrDefault();


                forecast.Name = _db.logins.Where(y => y.Id == item.LoginId).Select(y => y.username).SingleOrDefault();

                forecast.Account_Id = item.Account_Id;

                forecast.Interest = item.Interest;

                forecast.Account_Type = item.Account_Type;

                forecast.Balance = item.Balance;

                forecast.BalanceAfterAYear = item.Balance + (item.Balance * 1 * item.Interest) / 100;

                forecast.TotalBalance = forecast.BalanceAfterAYear + item.Balance;

                financialForecasts.Add(forecast);
            }



            //  forecast.BalanceAfterAYear = allUserDetails.Balance
            return View(financialForecasts); //, forecast.TotalBalance,forecast.BalanceAfterAYear);
        } 

        //GET
        public new IActionResult User(int uid)
        {


            var objUserDetails = _db.UserDetails.Where(y => y.LoginId == uid).ToList();

            ViewBag.receiversid = objUserDetails;
            ViewBag.userid = uid;



            return View(objUserDetails);
        }

        //POST
        [HttpPost]
        public new IActionResult User(float DepositAmt, float WithdrawAmt, int receiversid, int receiversaccountid)
        {


            if (DepositAmt != 0) { 
  
                var depositaccount = _db.UserDetails.Where(y => y.LoginId == receiversid && y.Account_Id == receiversaccountid).SingleOrDefault();

                depositaccount.Balance = depositaccount.Balance + DepositAmt;
            }

            if (WithdrawAmt != 0)
            {
                var withdrawaccount = _db.UserDetails.Where(y => y.LoginId == receiversid && y.Account_Id == receiversaccountid).SingleOrDefault();
                withdrawaccount.Balance -= WithdrawAmt;

            }

            _db.SaveChanges();

            return RedirectToAction(nameof(User), new { uid = receiversid });

            //return View(User(uid));

        }

        //POST
        //[HttpPost]
        //public new IActionResult Withdraw(float WithdrawAmt, int receiversid, int receiversaccountid)
        //{
        //    var withdrawaccount = _db.UserDetails.Where(y => y.LoginId == receiversid && y.Account_Id == receiversaccountid).SingleOrDefault();
        //    withdrawaccount.Balance -= WithdrawAmt;

        //    _db.SaveChanges();

        //    return RedirectToAction(nameof(User), new { uid = receiversid });
        //}

        //GET
        [HttpGet]
        public IActionResult AccountSelect(int sendersId, int receiversId)
        {
            var sender = _db.logins.Where(y => y.Id == sendersId).SingleOrDefault();
            var reciever = _db.logins.Where(y => y.Id == receiversId).SingleOrDefault();

            ViewBag.sendersid = sender.Id;
            ViewBag.receiversid = reciever.Id;


            var senders_accounts = _db.UserDetails.Where(y => y.LoginId == sendersId).ToList();
            var receivers_accounts = _db.UserDetails.Where(y => y.LoginId == receiversId).ToList();

            ViewBag.senders_accounts = senders_accounts;
            ViewBag.receivers_accounts = receivers_accounts;


            return View();


        }


        [HttpPost]
        public IActionResult AccountSelect(UserDetails UserDetails, int sendersId, int receiversId, int senderaccountid, int receiversaccountid)
        {

            float transferablebalance = _db.UserDetails.Where(y => y.LoginId == sendersId && y.Account_Id == senderaccountid).Select(y => y.Balance).SingleOrDefault();
            if (transferablebalance < UserDetails.Balance)
            {
                return BadRequest();
            }
            var senderaccount = _db.UserDetails.Where(y => y.LoginId == sendersId && y.Account_Id == senderaccountid).SingleOrDefault();
            senderaccount.Balance = senderaccount.Balance - UserDetails.Balance;

            var reciveraccount = _db.UserDetails.Where(y => y.LoginId == receiversId && y.Account_Id == receiversaccountid).SingleOrDefault();

            reciveraccount.Balance = reciveraccount.Balance + UserDetails.Balance;

            _db.SaveChanges();

            return RedirectToAction(nameof(User), new { uid = sendersId });
        }



        ////POST
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult CTransfer(Category obj, int receiversId, float amount)
        //{
        //    return View(); 
        //}



    }
}


////GET
//[HttpGet]
//public new IActionResult Deposit(int receiversId)
//{
//    var reciever = _db.logins.Where(y => y.Id == receiversId).Single();

//    ViewBag.receiversid = reciever.Id;

//    var receivers_accounts = _db.UserDetails.Where(y => y.LoginId == receiversId).ToList();
//    ViewBag.receivers_accounts = receivers_accounts;
//    return View();
//}

////POST
//[HttpPost]
//public IActionResult Deposit(UserDetails UserDetails,int receiversId, int receiversaccountid)
//{



//    var reciveraccount = _db.UserDetails.Where(y => y.LoginId == receiversId && y.Account_Id == receiversaccountid).SingleOrDefault();

//    reciveraccount.Balance = reciveraccount.Balance + UserDetails.Balance;

//    _db.SaveChanges();

//    return RedirectToAction(nameof(User), new { uid = receiversId });
//}



