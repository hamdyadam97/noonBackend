using AliExpress.Application.Contract;
using AliExpress.Application.IServices;
using AliExpress.Dtos.Pagination;
using AliExpress.Dtos.Payment;
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
    public class TransactionService: ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;

        public TransactionService(ITransactionRepository transactionRepository,IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        public async Task<int> countTransaction()
        {
            return await _transactionRepository.CoutTransaction();
        }

        public async Task<ResultView<TransactionDTO>> Create(TransactionDTO transactionDTO)
        {

            try
            {
                var payment = _mapper.Map<TransactionDTO, Transaction>(transactionDTO);
                var createPayment = await _transactionRepository.CreateAsync(payment);
                var createPaymentDTO = _mapper.Map<Transaction, TransactionDTO>(createPayment);
                return new ResultView<TransactionDTO> { Entity = createPaymentDTO, IsSuccess = true, Message = "Create success" };
            }
            catch (Exception ex)
            {
                return new ResultView<TransactionDTO> { Entity = null, IsSuccess = false, Message = ex.Message };
            }
        }

        public async Task<ResultView<TransactionDTO>> Delete(int id)
        {
            var pay = await _transactionRepository.GetByIdAsync(id);
            if (pay == null)
            {
                return new ResultView<TransactionDTO> { IsSuccess = false, Message = "product not found" };
            }
            await _transactionRepository.DeleteAsync(pay);
            return new ResultView<TransactionDTO> { IsSuccess = true, Message = "product deleted successfully" };
        }

        public Task<PaginationResult<TransactionDTO>> GetAllTansaction(string searchValue, int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<ResultView<TransactionDTO>> GetOne(int Id)
        {
            var pay = await _transactionRepository.GetByIdAsync(Id);
            if (pay == null)
            {
                return new ResultView<TransactionDTO> { IsSuccess = false, Message = "payment  not found" };
            }
            else
            {
                var payDto = _mapper.Map<Transaction, TransactionDTO>(pay);

                return new ResultView<TransactionDTO> { Entity = payDto, IsSuccess = true, Message = "payment upaded successfully" };
            }
        }

        public  async Task<ResultView<TransactionDTO>> Update(TransactionDTO transactionDTO)
        {
            var existingPayment = await _transactionRepository.GetByIdAsync(transactionDTO.Id);
            if (existingPayment == null)
            {
                return new ResultView<TransactionDTO> { IsSuccess = false, Message = "payment not found" };
            }
            else
            {
                var pay = _mapper.Map<TransactionDTO, Transaction>(transactionDTO);
                await _transactionRepository.UpdateAsync(pay);
                return new ResultView<TransactionDTO> { Entity = transactionDTO, IsSuccess = false, Message = "payment updated successfully" };
            }

        }
    }
}
