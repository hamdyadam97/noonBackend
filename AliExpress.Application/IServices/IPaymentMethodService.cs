using AliExpress.Dtos.Pagination;
using AliExpress.Dtos.Payment;
using AliExpress.Dtos.Product;
using AliExpress.Dtos.ViewResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Application.IServices
{
    public interface IPaymentMethodService
    {
        Task<PaginationResult<PaymentMethodDTO>> GetAllPayment(string searchValue, int page, int pageSize);
        Task<ResultView<PaymentMethodDTO>> Create(PaymentMethodDTO paymentMethodDTO);
        Task<ResultView<PaymentMethodDTO>> Update(PaymentMethodDTO paymentMethodDTO);
        Task<ResultView<PaymentMethodDTO>> Delete(int id);
        Task<ResultView<PaymentMethodDTO>> GetOne(int Id);
        Task<int> CoutPayemnt();


    }
}
