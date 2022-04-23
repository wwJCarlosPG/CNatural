using CNaturalApi.Models;

namespace CNaturalApi.Repository.IServices
{
    
    public interface ISaleService
    {
        public Task<Sale> GetSale(int saleId);
        public Task<IEnumerable<Sale>> GetAllSales();
        public Task<Sale> AddSale(Sale sale);
        public Task<Sale> UpdateSale(int saleId, Sale sale);
        public Task<bool> DeleteSale(int saleId);
        //Solo puedo borrar una venta si y solo si no han pasado 24 horas de realizada.
    }
}
