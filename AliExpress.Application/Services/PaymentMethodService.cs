using AliExpress.Application.Contract;
using AliExpress.Application.IServices;
using AliExpress.Dtos.Pagination;
using AliExpress.Dtos.Payment;
using AliExpress.Dtos.Product;
using AliExpress.Dtos.ViewResult;
using AliExpress.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Application.Services
{
    public class PaymentMethodService : IPaymentMethodService
    {
        private readonly IPaymentRepository _paymentMethod;
        private readonly IMapper _mapper;

        public PaymentMethodService(IPaymentRepository paymentMethod,
            IMapper mapper)
        {
            _paymentMethod = paymentMethod;
            _mapper = mapper;
        }


        public async Task<int> CoutPayemnt()
        {
            return await _paymentMethod.CoutPayemnt();
        }

        public async Task<ResultView<PaymentMethodDTO>> Create(PaymentMethodDTO paymentMethodDTO)
        {
            try
            {
                var payment = _mapper.Map<PaymentMethodDTO, PaymentMethod>(paymentMethodDTO);

                var createPayment = await _paymentMethod.CreateAsync(payment);
                var createPaymentDTO = _mapper.Map<PaymentMethod, PaymentMethodDTO>(createPayment);
                return new ResultView<PaymentMethodDTO> { Entity = createPaymentDTO, IsSuccess = true, Message = "Create success" };
            }
            catch (Exception ex)
            {
                return new ResultView<PaymentMethodDTO> { Entity = null, IsSuccess = false, Message = ex.Message };
            }

        }

        public async Task<ResultView<PaymentMethodDTO>> Delete(int id)
        {

            var pay=await _paymentMethod.GetByIdAsync(id);
            if(pay == null)
            {
                return new ResultView<PaymentMethodDTO> { IsSuccess = false, Message = "product not found" };
            }
            await _paymentMethod.DeleteAsync(pay);
            return new ResultView<PaymentMethodDTO> { IsSuccess = true, Message = "product deleted successfully" };
        }

        public Task<PaginationResult<PaymentMethodDTO>> GetAllPayment(string searchValue, int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<ResultView<PaymentMethodDTO>> GetOne(int Id)
        {
            var pay=await _paymentMethod.GetByIdAsync(Id);
            if(pay == null)
            {
                return new ResultView<PaymentMethodDTO> { IsSuccess = false, Message = "payment  not found" };
            }
            else
            {
                var payDto=_mapper.Map<PaymentMethod,PaymentMethodDTO>(pay);

                return new ResultView<PaymentMethodDTO> { Entity = payDto, IsSuccess = true, Message = "payment upaded successfully" };
            }

        }
           
        public async Task<ResultView<PaymentMethodDTO>> Update(PaymentMethodDTO paymentMethodDTO)
        {
            var existingPayment = await _paymentMethod.GetByIdAsync(paymentMethodDTO.Id);
            if (existingPayment == null)
            {
                return new ResultView<PaymentMethodDTO> { IsSuccess = false, Message = "payment not found" };
            }
            else
            {
                var pay = _mapper.Map<PaymentMethodDTO, PaymentMethod>(paymentMethodDTO);
                await _paymentMethod.UpdateAsync(pay);
                return new ResultView<PaymentMethodDTO> {Entity=paymentMethodDTO, IsSuccess = true, Message = "payment updated successfully" };
            }
            

        }
    }
}
