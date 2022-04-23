using CNaturalApi.Models;

namespace CNaturalApi.Repository.IServices
{
    public interface IBuyerService
    {
        public Task<Buyer> AddBuyer(Buyer buyer);
        public Task<Buyer> UpdateBuyer(int buyerId, Buyer buyer);
        public Task<Buyer> GetBuyer(int buyerId);
        public Task<IEnumerable<Buyer>> GetBuyers();
        public Task<bool> DeleteBuyer(int buyerId);
        public Task<bool> BuyerAlreadyExists(Buyer buyer);

    }
}
