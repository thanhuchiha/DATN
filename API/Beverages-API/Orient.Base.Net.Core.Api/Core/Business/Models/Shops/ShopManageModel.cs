using Orient.Base.Net.Core.Api.Core.Business.IoC;
using Orient.Base.Net.Core.Api.Core.DataAccess.Repositories.Base;
using Orient.Base.Net.Core.Api.Core.Entities;
using Orient.Base.Net.Core.Api.Core.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Orient.Base.Net.Core.Api.Core.Business.Models.Shops
{
    public class ShopManageModel : IValidatableObject
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public string FeeShip { get; set; }

        public ShopStatusEnums.Status Status { get; set; }

        public string AvatarUrl { get; set; }

        public Guid[] UserIds { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var userRepository = IoCHelper.GetInstance<IRepository<User>>();

            if (UserIds == null || UserIds.Length == 0)
            {
                yield return new ValidationResult("User not be null", new string[] { "UserIds" });
            }

            if (!UserIds.All(x => userRepository.GetAll().Select(y => y.Id).Contains(x)))
            {
                yield return new ValidationResult("Invalid User", new string[] { "UserIds" });
            }
        }

        public void SetDataToModel(Shop shop)
        {
            shop.Name = Name;
            shop.Address = Address;
            shop.Description = Description;
            shop.FeeShip = FeeShip;
            shop.Status = Status;
            shop.AvatarUrl = AvatarUrl;
        }
    }
}
