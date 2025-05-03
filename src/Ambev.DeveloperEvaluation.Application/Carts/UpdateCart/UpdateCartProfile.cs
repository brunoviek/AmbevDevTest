using Ambev.DeveloperEvaluation.Application.Carts.Shared.Models;
using Ambev.DeveloperEvaluation.Domain.Entities.Carts;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart
{
    /// <summary>
    /// AutoMapper profile for mapping data between UpdateCartCommand and Cart entity.
    /// </summary>
    public class UpdateCartProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of <see cref="UpdateCartProfile"/> configuring the mappings.
        /// </summary>
        public UpdateCartProfile()
        {
            CreateMap<UpdateCartCommand, Cart>().ForMember(dest => dest.Id, opt => opt.Ignore());

        }
    }
}
