using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyMap.CustomViews;
using MyMap.Model;

namespace MyMap.IRepository
{
    public interface IMapObjectRepository
    {
        Task<IEnumerable<CustomPin>> GetPaymentAsync();

        Task<CustomPin> GetPaymentByIdAsync(int id);

        Task<bool> AddProductAsync(CustomPin payment);
        Task<bool> UpdateProductAsync(CustomPin payment);
        Task<bool> RemoceProductAsync(int id);
    }
   
}
