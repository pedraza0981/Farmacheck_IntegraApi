﻿using Farmacheck.Application.Models.Brands;

namespace Farmacheck.Application.Interfaces
{
    public interface IBrandApiClient
    {
        Task<List<BrandResponse>> GetBrandsAsync();
        Task<List<BrandResponse>> GetBrandsByPageAsync(int page, int items);
        Task<List<BrandResponse>> GetBrandsByBusinessUnitAsync(int businessUnitId);
        Task<BrandResponse?> GetBrandAsync(int id);
        Task<int> CreateAsync(BrandRequest request);
        Task<bool> UpdateAsync(UpdateBrandRequest request);
        Task DeleteAsync(int id);


    }
}
