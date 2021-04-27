using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Security.Claims;

using Advertisement_WebApp.Models;
using Advertisement_WebApp.Data;

namespace Advertisement_WebApp.Controllers
{
    public class AdvertisementController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdvertisementController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Advertisements.FromSqlInterpolated($"getUserAdvertisements {User.FindFirstValue(ClaimTypes.NameIdentifier)}").ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Advertisements advertisements)
        {
            if ((ModelState.IsValid) &&
                (advertisements.Type == "Login" || advertisements.Type == "Balance" || advertisements.Type == "Deposit" || advertisements.Type == "Withdrawal") &&
                (advertisements.Condition == "Above" || advertisements.Condition == "Below" || advertisements.Condition == "NA") &&
                (advertisements.Value >= 0))
            {
                if ((advertisements.Type == "Deposit" || advertisements.Type == "Withdrawal") &&
                    (advertisements.Value <= 0))
                {
                    return View(advertisements);
                }

                if (advertisements.Type == "Login")
                {
                    advertisements.Condition = "NA";
                    advertisements.Value = 0;
                }

                if (advertisements.Message == null || advertisements.Message.ToString() == "")
                {
                    advertisements.Message = "NA";
                }

                var param = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@customer_id",
                            SqlDbType =  System.Data.SqlDbType.NVarChar,
                            Size = 450,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = User.FindFirstValue(ClaimTypes.NameIdentifier)
                        },
                        new SqlParameter() {
                            ParameterName = "@type",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 32,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = advertisements.Type
                        },
                        new SqlParameter() {
                            ParameterName = "@condition",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 32,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = advertisements.Condition
                        },
                        new SqlParameter() {
                            ParameterName = "@value",
                            SqlDbType =  System.Data.SqlDbType.Decimal,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = advertisements.Value
                        },
                        new SqlParameter() {
                            ParameterName = "@notify_text",
                            SqlDbType =  System.Data.SqlDbType.Bit,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = advertisements.Notify_Text
                        },
                        new SqlParameter() {
                            ParameterName = "@notify_email",
                            SqlDbType =  System.Data.SqlDbType.Bit,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = advertisements.Notify_Email
                        },
                        new SqlParameter() {
                            ParameterName = "@notify_web",
                            SqlDbType =  System.Data.SqlDbType.Bit,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = advertisements.Notify_Web
                        },
                        new SqlParameter() {
                            ParameterName = "@message",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 300,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = advertisements.Message
                        }};

                await _context.Database.ExecuteSqlRawAsync("[dbo].[AddNotificationRule] @customer_id, @type, @condition, @value, @notify_text, @notify_email, @notify_web, @message", param);
                await _context.SaveChangesAsync();
                return RedirectToAction("Manage");
            }

            return View(advertisements);
        }*/

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisements = await _context.Advertisements
                .FirstOrDefaultAsync(m => m.Advertisement_ID == id);
            if (advertisements == null)
            {
                return NotFound();
            }

            return View(advertisements);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var param = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@advertisement_ID",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = id
                        }
            };

            await _context.Database.ExecuteSqlRawAsync("[dbo].[delAdvertisement] @advertisement_ID", param);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
