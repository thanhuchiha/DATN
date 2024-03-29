﻿using Orient.Base.Net.Core.Api.Core.Common.Constants;
using Orient.Base.Net.Core.Api.Core.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orient.Base.Net.Core.Api.Core.Entities
{
    [Table("User")]
    public class User : BaseEntity
    {
        #region Constructors
        public User() : base()
        {
            Color = UserConstants.Color;
        }

        public User(string email)
            : this()
        {
            Email = email;
        }

        #endregion

        #region Properties

        public int? Age
        {
            get
            {
                if (DateOfBirth.HasValue)
                {
                    return (int)(DateTime.Now - DateOfBirth.Value).TotalDays / 365;
                }
                return null;
            }
        }

        #region Base Properties

        [StringLength(255)]
        public string Email { get; set; }

        [StringLength(512)]
        [Required]
        public string Password { get; set; }

        [StringLength(512)]
        [Required]
        public string PasswordSalt { get; set; }

        [StringLength(512)]
        [Required]
        public string Name { get; set; }

        [StringLength(50)]
        public string Mobile { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public UserEnums.Gender Gender { get; set; }

        public string About { get; set; }

        [StringLength(512)]
        public string AvatarUrl { get; set; }

        [StringLength(1024)]
        public string Address { get; set; }

        [StringLength(512)]
        public string Facebook { get; set; }

        [StringLength(512)]
        public string Twitter { get; set; }

        [StringLength(512)]
        public string LinkedIn { get; set; }

        [StringLength(512)]
        public string Google { get; set; }

        public string ResetPasswordCode { get; set; }

        public DateTime? ResetPasswordExpiryDate { get; set; }

        public string Color { get; set; }

        #endregion

        public List<UserInRole> UserInRoles { get; set; }

        public List<Comment> Comments { get; set; }

        public List<UserInShop> UserInShops { get; set; }
        #endregion
    }
}