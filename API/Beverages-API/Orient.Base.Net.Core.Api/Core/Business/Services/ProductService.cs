using Microsoft.EntityFrameworkCore;
using Orient.Base.Net.Core.Api.Core.Business.Models.Base;
using Orient.Base.Net.Core.Api.Core.Business.Models.Products;
using Orient.Base.Net.Core.Api.Core.Common.Constants;
using Orient.Base.Net.Core.Api.Core.Common.Reflections;
using Orient.Base.Net.Core.Api.Core.DataAccess.Repositories.Base;
using Orient.Base.Net.Core.Api.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orient.Base.Net.Core.Api.Core.Business.Services
{
    public interface IProductService
    {
        Task<PagedList<ProductViewModel>> ListProductAsync(ProductRequestListViewModel productRequestListViewModel);

        Task<List<ProductViewModel>> GetAllProductAsync(ProductRequestGetAllModel productRequestGetAllModel);

        Task<ResponseModel> CreateProductAsync(ProductManageModel productManagerModel);

        Task<ResponseModel> UpdateProductAsync(Guid id, ProductManageModel productManagerModel);

        Task<ResponseModel> GetProductByIdAsync(Guid? id);

        Task<ResponseModel> DeleteProductAsync(Guid id);
    }

    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productResponsitory;
        private readonly IRepository<ProductInCategory> _productInCategoryReponsitory;

        public ProductService(IRepository<Product> productRepository, IRepository<ProductInCategory> productInCategoryReponsitory)
        {
            _productResponsitory = productRepository;
            _productInCategoryReponsitory = productInCategoryReponsitory;
        }

        private IQueryable<Product> GetAll()
        {
            return _productResponsitory.GetAll()
                    .Include(x => x.ProductInCategories)
                        .ThenInclude(y => y.Category)
                     .Where(x => !x.RecordActive);
        }

        private List<string> GetAllProperyNameOfProductViewModel()
        {
            var productViewModel = new ProductViewModel();

            var type = productViewModel.GetType();

            return ReflectionUtilities.GetAllPropertyNamesOfType(type);
        }

        public async Task<PagedList<ProductViewModel>> ListProductAsync(ProductRequestListViewModel productRequestListViewModel)
        {
            var list = await GetAll()
                .Where(x => (!productRequestListViewModel.IsActive.HasValue || x.RecordActive == productRequestListViewModel.IsActive)
                    && (string.IsNullOrEmpty(productRequestListViewModel.Query)
                    || (x.Name.Contains(productRequestListViewModel.Query)
                 )))
            .Select(x => new ProductViewModel(x)).ToListAsync();

            var productViewModelProperties = GetAllProperyNameOfProductViewModel();

            var requestPropertyName = !string.IsNullOrEmpty(productRequestListViewModel.SortName) ? productRequestListViewModel.SortName.ToLower() : string.Empty;

            string matchedPropertyName = string.Empty;

            foreach (var productViewModelProperty in productViewModelProperties)
            {
                var lowerTypeViewModelProperty = productViewModelProperty.ToLower();
                if (lowerTypeViewModelProperty.Equals(requestPropertyName))
                {
                    matchedPropertyName = productViewModelProperty;
                    break;
                }
            }

            if (string.IsNullOrEmpty(matchedPropertyName))
            {
                matchedPropertyName = "Name";
            }

            var type = typeof(ProductViewModel);

            var sortProperty = type.GetProperty(matchedPropertyName);

            list = productRequestListViewModel.IsDesc ? list.OrderByDescending(x => sortProperty.GetValue(x, null)).ToList() : list.OrderBy(x => sortProperty.GetValue(x, null)).ToList();

            return new PagedList<ProductViewModel>(list, productRequestListViewModel.Skip ?? CommonConstants.Config.DEFAULT_SKIP, productRequestListViewModel.Take ?? CommonConstants.Config.DEFAULT_TAKE);
        }

        public async Task<List<ProductViewModel>> GetAllProductAsync(ProductRequestGetAllModel productRequestGetAllModel)
        {
            var list = await GetAll()
                .Where(x => (!productRequestGetAllModel.IsActive.HasValue || x.RecordActive == productRequestGetAllModel.IsActive)
                    && (string.IsNullOrEmpty(productRequestGetAllModel.Query)
                    || (x.Name.Contains(productRequestGetAllModel.Query)
                )))
            .Select(x => new ProductViewModel(x)).ToListAsync();

            return list;
        }

        public async Task<ResponseModel> CreateProductAsync(ProductManageModel productManageModel)
        {
            var product = await _productResponsitory.FetchFirstAsync(x => x.Name == productManageModel.Name);
            if (product != null)
            {
                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Message = MessageConstants.EXISTED_CREATED
                };
            }
            else
            {
                //create product
                product = AutoMapper.Mapper.Map<Product>(productManageModel);
                await _productResponsitory.InsertAsync(product);

                product = await GetAll().FirstOrDefaultAsync(x => x.Id == product.Id);

                //create productInCategory
                var productInCategories = new List<ProductInCategory>();
                if (productManageModel.CategoryIds != null)
                {
                    foreach (var categoryId in productManageModel.CategoryIds)
                    {
                        var productInCategory = new ProductInCategory();
                        productInCategory.ProductId = product.Id;
                        productInCategory.CategoryId = categoryId;
                        productInCategories.Add(productInCategory);
                    }
                }
                await _productInCategoryReponsitory.InsertAsync(productInCategories);
                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Data = new ProductViewModel(product),
                    Message = MessageConstants.CREATED_SUCCESSFULLY
                };
            }
        }

        public async Task<ResponseModel> UpdateProductAsync(Guid id, ProductManageModel productManageModel)
        {
            var product = await GetAll().FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = MessageConstants.NOT_FOUND
                };
            }
            else
            {
                var existedProduct = await _productResponsitory.FetchFirstAsync(x => x.Name == productManageModel.Name && x.Id != id);
                if (existedProduct != null)
                {
                    return new ResponseModel()
                    {
                        StatusCode = System.Net.HttpStatusCode.BadRequest,
                        Message = MessageConstants.EXISTED_CREATED
                    };
                }
                else
                {
                    //update Product
                    productManageModel.SetDataToModel(product);

                    

                    //update productInCategory
                    await _productInCategoryReponsitory.DeleteAsync(product.ProductInCategories);

                    var productInCategories = new List<ProductInCategory>();
                    foreach(var categoryId in productManageModel.CategoryIds)
                    {
                        var productInCategory = new ProductInCategory()
                        {
                            ProductId = product.Id,
                            CategoryId = categoryId
                        };
                        productInCategories.Add(productInCategory);
                    }

                    await _productInCategoryReponsitory.InsertAsync(productInCategories);

                    await _productResponsitory.UpdateAsync(product);

                    return new ResponseModel()
                    {
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Data = new ProductViewModel(product),
                        Message = MessageConstants.UPDATED_SUCCESSFULLY
                    };
                }
            }
        }

        public async Task<ResponseModel> GetProductByIdAsync(Guid? id)
        {
            var product = await GetAll().FirstOrDefaultAsync(x => x.Id == id);
            if (product != null)
            {
                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Data = new ProductViewModel(product)
                };
            }
            else
            {
                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = MessageConstants.NOT_FOUND
                };
            }
        }

        public async Task<ResponseModel> DeleteProductAsync(Guid id)
        {
            var product = await GetAll().FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = MessageConstants.NOT_FOUND
                };
            }
            else
            {
                //delete ProductInCategory
                await _productInCategoryReponsitory.DeleteAsync(product.ProductInCategories);

                //delete product
                await _productResponsitory.DeleteAsync(id);

                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Message = MessageConstants.DELETED_SUCCESSFULLY
                };
            }
        }
    }
}
