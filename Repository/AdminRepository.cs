﻿using Ecommerce_Api.Model;
using Ecommerce_Api.ViewModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Api.Repository
{
    public class AdminRepository:IAdminRepository
    {
        private readonly EcommercedemoContext _context;
        public AdminRepository(EcommercedemoContext context)
        {
            _context = context;   
        }

        //brand
        public async Task<Brand> CreateBrand(Brand brand)
        {
            try
            {
                if (_context != null)
                {
                    await _context.Brands.AddAsync(brand);
                    await _context.SaveChangesAsync();
                       
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> DeleteBrand(int brand_id)
        {
            try
            {
                if (_context != null)
                {
                    var brand = await _context.Brands.FindAsync(brand_id);
                    if (brand != null)
                    {
                        _context.Brands.Remove(brand);
                        _context.SaveChanges();
                        return "Brand deleted successfully";
                    }
                    return "Brand not found";
                }
                return null;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Brand> UpdateBrand(TotalViewModel TVM)
        {
            try
            {
                if(_context != null)
                {
                    var item = _context.Brands.FirstOrDefault(x => x.BrandId == TVM.BrandId);

                    if (item != null)
                    {
                        item.BrandName= TVM.BrandName;

                        _context.Brands.Update(item);
                        await _context.SaveChangesAsync();
                        return item;
                    }
                        
                    
                    return null;
                }
                return null;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Brand>> GetAllBrands()
        {
            try
            {
                if( _context != null)
                {
                    List<Brand> brands=new List<Brand>();

                    brands = _context.Brands.ToList();
                    return brands;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Brand> GetBrandById(int brand_id)
        {
            try
            {
                if(_context != null)
                {
                    var brand = await _context.Brands.FirstOrDefaultAsync(x => x.BrandId == brand_id);
                    return brand;
                }
                return null;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }



        //Product
        public async Task<Product> CreateProduct(ProductViewModel APVM)
        {
            try
            {
                if (_context != null)
                {
                    var product = new Product()
                    {
                        CategoryId = APVM.CategoryId,
                        BrandId = APVM.BrandId,
                        ProductName = APVM.ProductName,
                        StockQuantity = APVM.StockQuantity,
                        Price = APVM.Price,
                        Weight = APVM.Weight,
                        Unit = APVM.Unit,
                        ImageUrl = APVM.ImageUrl,
                        IsAvailable = APVM.IsAvailable,
                        ExpiryDate = APVM.ExpiryDate,
                        ManufactureDate = APVM.ManufactureDate,
                        DiscountId = APVM.DiscountId,
                        Description = APVM.Description,
                    };
                    await _context.Products.AddAsync(product);
                    await _context.SaveChangesAsync();
                }
                return null;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> DeleteProduct(int product_id)
        {
            try
            {
                if (_context != null)
                {
                    var product = await _context.Products.FindAsync(product_id);
                    if (product != null)
                    {
                        _context.Products.Remove(product);
                        _context.SaveChanges();
                        return "Product deleted successfully";
                    }
                    return "Product not found";
                }
                return null;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Product> UpdateProduct(ProductViewModel UPVM)
        {
            try
            {
                if (_context != null)
                {
                    var item = _context.Products.FirstOrDefault(x => x.ProductId == UPVM.ProductId);
                    if(item!= null)
                    {
                        item.CategoryId = UPVM.CategoryId;
                        item.BrandId = UPVM.BrandId;
                        item.ProductName = UPVM.ProductName;
                        item.StockQuantity = UPVM.StockQuantity;
                        item.Price = UPVM.Price;
                        item.Weight = UPVM.Weight;
                        item.Unit = UPVM.Unit;
                        item.ImageUrl = UPVM.ImageUrl;
                        item.IsAvailable = UPVM.IsAvailable;
                        item.ExpiryDate = UPVM.ExpiryDate;
                        item.ManufactureDate = UPVM.ManufactureDate;
                        item.DiscountId = UPVM.DiscountId;
                        item.Description = UPVM.Description;

                        _context.Products.Update(item);
                       await _context.SaveChangesAsync();
                        return item;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Product>> GetAllProducts()
        {
            try
            {
                if (_context != null)
                {
                    List<Product> products = new List<Product>();

                    products =await _context.Products.ToListAsync();
                    return products;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Product>> GetProductById(List<int> product_id)
        {
            try
            {
                if (_context != null && product_id!=null && product_id.Any())
                {
                    var product = await _context.Products.Where(x => product_id.Contains(x.ProductId)).ToListAsync();
                    return product;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
