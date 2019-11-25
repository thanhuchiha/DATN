using Microsoft.EntityFrameworkCore;
using Orient.Base.Net.Core.Api.Core.Business.Models.Base;
using Orient.Base.Net.Core.Api.Core.Business.Models.Shops;
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
    public interface IShopService
    {
        Task<PagedList<ShopViewModel>> ListShopAsync(ShopRequestListViewModel shopRequestListViewModel);

        Task<List<ShopViewModel>> GetAllShopAsync(ShopRequestGetAllModel shopRequestGetAllModel);

        Task<ResponseModel> CreateShopAsync(ShopManageModel shopManagerModel);

        Task<ResponseModel> UpdateShopAsync(Guid id, ShopManageModel shopManagerModel);

        Task<ResponseModel> GetShopByIdAsync(Guid? id);

        Task<ResponseModel> DeleteShopAsync(Guid id);
    }

    public class ShopService : IShopService
    {
        private readonly IRepository<Shop> _shopResponsitory;
        private readonly IRepository<UserInShop> _userInShopRepository;

        public ShopService(IRepository<Shop> shopRepository, IRepository<UserInShop> userInShopRepository)
        {
            _shopResponsitory = shopRepository;
            _userInShopRepository = userInShopRepository;
        }

        private IQueryable<Shop> GetAll()
        {
            return _shopResponsitory.GetAll()
                .Include(x => x.UserInShops)
                .ThenInclude(y => y.User)
                    .ThenInclude(y => y.UserInRoles)
                        .ThenInclude(z => z.Role)
                     .Where(x => !x.RecordActive);
        }

        private List<string> GetAllProperyNameOfShopViewModel()
        {
            var shopViewModel = new ShopViewModel();

            var type = shopViewModel.GetType();

            return ReflectionUtilities.GetAllPropertyNamesOfType(type);
        }

        public async Task<PagedList<ShopViewModel>> ListShopAsync(ShopRequestListViewModel shopRequestListViewModel)
        {
            var list = await GetAll()
                .Where(x => (!shopRequestListViewModel.IsActive.HasValue || x.RecordActive == shopRequestListViewModel.IsActive)
                    && (string.IsNullOrEmpty(shopRequestListViewModel.Query)
                    || (x.Name.Contains(shopRequestListViewModel.Query)
                 )))
            .Select(x => new ShopViewModel(x)).ToListAsync();

            var shopViewModelProperties = GetAllProperyNameOfShopViewModel();

            var requestPropertyName = !string.IsNullOrEmpty(shopRequestListViewModel.SortName) ? shopRequestListViewModel.SortName.ToLower() : string.Empty;

            string matchedPropertyName = string.Empty;

            foreach (var shopViewModelProperty in shopViewModelProperties)
            {
                var lowerTypeViewModelProperty = shopViewModelProperty.ToLower();
                if (lowerTypeViewModelProperty.Equals(requestPropertyName))
                {
                    matchedPropertyName = shopViewModelProperty;
                    break;
                }
            }

            if (string.IsNullOrEmpty(matchedPropertyName))
            {
                matchedPropertyName = "Name";
            }

            var type = typeof(ShopViewModel);

            var sortProperty = type.GetProperty(matchedPropertyName);

            list = shopRequestListViewModel.IsDesc ? list.OrderByDescending(x => sortProperty.GetValue(x, null)).ToList() : list.OrderBy(x => sortProperty.GetValue(x, null)).ToList();

            return new PagedList<ShopViewModel>(list, shopRequestListViewModel.Skip ?? CommonConstants.Config.DEFAULT_SKIP, shopRequestListViewModel.Take ?? CommonConstants.Config.DEFAULT_TAKE);
        }

        public async Task<List<ShopViewModel>> GetAllShopAsync(ShopRequestGetAllModel shopRequestGetAllModel)
        {
            var list = await GetAll()
                .Where(x => (!shopRequestGetAllModel.IsActive.HasValue || x.RecordActive == shopRequestGetAllModel.IsActive)
                    && (string.IsNullOrEmpty(shopRequestGetAllModel.Query)
                    || (x.Name.Contains(shopRequestGetAllModel.Query)
                )))
            .Select(x => new ShopViewModel(x)).ToListAsync();

            return list;
        }

        public async Task<ResponseModel> CreateShopAsync(ShopManageModel shopManagerModel)
        {
            var shop = await _shopResponsitory.FetchFirstAsync(x => x.Name == shopManagerModel.Name);
            if (shop != null)
            {
                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Message = MessageConstants.EXISTED_CREATED
                };
            }
            else
            {
                //create Shop
                shop = AutoMapper.Mapper.Map<Shop>(shopManagerModel);
                await _shopResponsitory.InsertAsync(shop);

                //create UserInShop
                var userInShops = new List<UserInShop>();
                if (shopManagerModel.UserIds != null)
                {
                    foreach (var userId in shopManagerModel.UserIds)
                    {
                        var userInShop = new UserInShop();
                        userInShop.ShopId = shop.Id;
                        userInShop.UserId = userId;
                        userInShops.Add(userInShop);
                    }
                }
                await _userInShopRepository.InsertAsync(userInShops);

                shop = await GetAll().FirstOrDefaultAsync(x => x.Id == shop.Id);
                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Data = new ShopViewModel(shop),
                    Message = MessageConstants.CREATED_SUCCESSFULLY
                };
            }
        }

        public async Task<ResponseModel> DeleteShopAsync(Guid id)
        {
            var shop = await GetAll().FirstOrDefaultAsync(x => x.Id == id);
            if (shop == null)
            {
                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = MessageConstants.NOT_FOUND
                };
            }
            else
            {
                //delete UserInShop
                await _userInShopRepository.DeleteAsync(shop.UserInShops);

                await _shopResponsitory.DeleteAsync(id);
                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Message = MessageConstants.DELETED_SUCCESSFULLY
                };
            }
        }



        public async Task<ResponseModel> GetShopByIdAsync(Guid? id)
        {
            var shop = await GetAll().FirstOrDefaultAsync(x => x.Id == id);
            if (shop != null)
            {
                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Data = new ShopViewModel(shop)
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



        public async Task<ResponseModel> UpdateShopAsync(Guid id, ShopManageModel shopManageModel)
        {
            var shop = await GetAll().FirstOrDefaultAsync(x => x.Id == id);
            if (shop == null)
            {
                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = MessageConstants.NOT_FOUND
                };
            }
            else
            {
                var existedShop = await _shopResponsitory.FetchFirstAsync(x => x.Name == shopManageModel.Name && x.Id != id);
                if (existedShop != null)
                {
                    return new ResponseModel()
                    {
                        StatusCode = System.Net.HttpStatusCode.BadRequest,
                        Message = MessageConstants.EXISTED_CREATED
                    };
                }
                else
                {
                    shopManageModel.SetDataToModel(shop);

                    await _userInShopRepository.DeleteAsync(shop.UserInShops);

                    //update UserInShop
                    var userInShops = new List<UserInShop>();
                    foreach (var userId in shopManageModel.UserIds)
                    {
                        var userInShop = new UserInShop();
                        userInShop.ShopId = shop.Id;
                        userInShop.UserId = userId;
                        userInShops.Add(userInShop);
                    }

                    await _userInShopRepository.InsertAsync(userInShops);

                    //updateshop
                    await _shopResponsitory.UpdateAsync(shop);

                    return new ResponseModel()
                    {
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Data = new ShopViewModel(shop),
                        Message = MessageConstants.UPDATED_SUCCESSFULLY
                    };
                }
            }
        }
    }
}
