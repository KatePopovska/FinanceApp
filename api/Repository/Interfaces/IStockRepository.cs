using api.DTOs;
using api.DTOs.Request;
using api.Helpers;
using api.Models;

namespace api.Repository.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync(QueryObject query);

        Task<Stock?> GetByIdAsync(int id);

        Task<Stock> CreateAsync(Stock stock);

        Task<Stock?> UpdateAsync(int id, UpdateStockRequest stock);

        Task<bool> DeleteAsync(int id);

        Task<bool> StockExist(int id);       
    }
}
