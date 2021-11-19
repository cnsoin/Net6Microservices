using Discount.Grpc.Data;
using Discount.Grpc.Entities;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        protected readonly DiscountContext _dbContext;

        public DiscountRepository(DiscountContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            var result = await _dbContext.Coupons.Where(c => c.ProductName == productName).FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            var result = await _dbContext.AddAsync(coupon);

            if (result == null)
            {
                return false;
            }

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            var result = _dbContext.Entry(coupon);
            if (result == null)
                return false;

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            var coupon = _dbContext.Coupons.SingleOrDefaultAsync(c => c.ProductName == productName);
            var result = _dbContext.Remove(coupon);

            if (result == null)
            {
                return false;
            }

            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}