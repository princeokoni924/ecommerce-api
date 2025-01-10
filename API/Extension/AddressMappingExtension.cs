using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using Core.Entities;

namespace API.Extension
{
    public static class AddressMappingExtension
    {
        public static AddressDto? ToDto(this Address? address)
        {
          if(address == null){
            return null;
          }else{
            return new AddressDto{
                City = address.City,
                Country = address.Country,
                State = address.State,
                LandMark = address.LandMark,
                NearestBustop = address.NearestBustop,
                Street = address.Street,
                PostalCode = address.PostalCode,

            };
          }
          }
          
        public static Address ToEntity(this AddressDto addressDto)
        {
            if(addressDto == null){
                throw new ArgumentNullException(nameof(addressDto));
            }else{
                return new Address{
                 City = addressDto.City,
                 Country = addressDto.Country,
                 Street = addressDto.Street,
                 State = addressDto.State,
                 LandMark = addressDto.LandMark,
                 NearestBustop = addressDto.NearestBustop
                };
            }

        }

        public static void UpdateFromDto(this Address address, AddressDto addressDto)
        {
            if(address == null){
                throw new ArgumentNullException(nameof(address));
            }else if(addressDto == null){
                throw new ArgumentNullException(nameof(addressDto));
            }else{
                address.City = addressDto.City;
                address.Country = addressDto.Country;
                address.LandMark = addressDto.LandMark;
                address.NearestBustop = addressDto.NearestBustop;
                address.State = addressDto.State;
                address.Street = addressDto.Street;
                address.PostalCode = addressDto.PostalCode;
            }
        }
    }
}
