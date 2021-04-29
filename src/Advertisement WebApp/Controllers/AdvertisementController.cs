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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Advertisements advertisement)
        {
            if (ModelState.IsValid)
            {
                var param = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@advTitle",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 35,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = advertisement.AdvTitle
                        },
                        new SqlParameter() {
                            ParameterName = "@advDetails",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 100,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = advertisement.AdvDetails
                        },
                        new SqlParameter() {
                            ParameterName = "@price",
                            SqlDbType =  System.Data.SqlDbType.Decimal,
                            Precision = 18,
                            Scale = 2,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = advertisement.Price
                        },
                        new SqlParameter() {
                            ParameterName = "@category_ID",
                            SqlDbType =  System.Data.SqlDbType.Char,
                            Size = 3,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = advertisement.Category_ID
                        },
                        new SqlParameter() {
                            ParameterName = "@user_ID",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 25,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = User.Identity.Name
                        }};

                await _context.Database.ExecuteSqlRawAsync("[dbo].[addAdvertisement] @advTitle, @advDetails, @price, @category_ID, @user_ID", param);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(advertisement);
        }

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
