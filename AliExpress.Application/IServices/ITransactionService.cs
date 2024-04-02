using AliExpress.Dtos.Pagination;
using AliExpress.Dtos.Payment;
using AliExpress.Dtos.ViewResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Application.IServices
{
    public interface ITransactionService
    {
        Task<PaginationResult<TransactionDTO>> GetAllTansaction(string searchValue, int page, int pageSize);
        Task<ResultView<TransactionDTO>> Create(TransactionDTO transactionDTO);
        Task<ResultView<TransactionDTO>> Update(TransactionDTO transactionDTO);
        Task<ResultView<TransactionDTO>> Delete(int id);
        Task<ResultView<TransactionDTO>> GetOne(int Id);
        Task<int> countTransaction();
    }
}
