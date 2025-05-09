﻿using POS.Api.DTOs;
using POS.Api.Entities;

namespace POS.Api.Interfaces
{
    public interface IPesananRepository
    {
        Task<IEnumerable<Pesanan>> GetAllAsync();
        Task<Pesanan?> GetByIdAsync(int id);
        Task<Pesanan> CreateAsync(Pesanan pesanan);
        Task<Pesanan?> CheckoutAsync(int pesananId, string metodePembayaran);
        Task<Pesanan?> CancelAsync(int pesananId);
        Task<IEnumerable<Pesanan>> GetHistoryAsync(string? status = null, DateTime? from = null, DateTime? to = null);
    }
}
