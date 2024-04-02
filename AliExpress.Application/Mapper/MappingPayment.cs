using AliExpress.Dtos.Payment;
using AliExpress.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Application.Mapper
{
    public class MappingPayment : Profile
    {


        public MappingPayment()
        {
            CreateMap<PaymentMethod,PaymentMethodDTO>().ForMember(pDTO=> pDTO.Transactions,pDistantion=> pDistantion.MapFrom(p=>p.Transactions))
                .ReverseMap();
            CreateMap<Transaction,TransactionDTO>().ReverseMap();




            //.ForMember(d => d.Images, o => o.MapFrom(s => s.Images.Select(img => img.Url))).ReverseMap();

        }
    }
}

