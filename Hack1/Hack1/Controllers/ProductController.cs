using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hack1.Context;
using Hack1.Models;
using System.Diagnostics;

namespace Hack1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductContext _context;

        public ProductController(ProductContext context)
        {
            _context = context;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct(string SKUS)
        {
            Dictionary<char, int> itemCounts = new Dictionary<char, int>();

            foreach (char skus in SKUS)
            {
                if (!itemCounts.ContainsKey(skus))
                    itemCounts[skus] = 0;

                itemCounts[skus]++;
            }

            var products = await _context.Product.ToListAsync();

            int totalPrize = 0;
            
            foreach (var items in itemCounts )
            {
                char item = items.Key;
                int count = items.Value;
                bool flag = false;
                int prize = 0;
                string offer = "";

                foreach (var prods in products) 
                {
                    if(prods.Item == item)
                    {
                        prize = prods.Price;
                        if(prods.Offer != null)
                            offer = prods.Offer;
                        else
                        {
                            totalPrize += prize * count;
                            flag = true;
                            continue;
                        }
                        break;
                    }
                }

                if (flag) { continue; }

                int priceWithOffer = 0;

                string[] offerValues = offer.Split(" ");

                int offerCount = Int32.Parse(offerValues[0]);
                int offervalue = Int32.Parse(offerValues[2]);

                priceWithOffer += (count / offerCount) * (offervalue);
                priceWithOffer += (count % offerCount) * prize;

                totalPrize += priceWithOffer;
            }

            return Ok(totalPrize);
        }
    }
}


//if (_context.Product == null)
//{
//    return NotFound();
//}
//return await _context.Product.ToListAsync();
