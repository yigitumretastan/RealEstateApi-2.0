using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealEstateApiRepositories.Contacts;
using RealEstateApiEntity.Models;

namespace RealEstateApiServices.Contacts
{
    public interface ITokenService
    {
        Task<GenerateTokenResponse> GenerateToken(User user);
    }


}