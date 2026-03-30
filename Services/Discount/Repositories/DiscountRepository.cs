using Dapper;
using Discount.Entities;
using Npgsql;

namespace Discount.Repositories;

public sealed class DiscountRepository : IDiscountRepository
{
    private readonly string? _connectionString;
    public DiscountRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetValue<string>("DatabaseSettings:ConnectionString")
            ?? throw new ArgumentNullException(nameof(configuration), "Connection string cannot be null");
    }
    public async Task<bool> CreateDiscount(Coupon coupon)
    {
        await using var connection = new NpgsqlConnection(_connectionString);

        // ✅ RETURNING Id eklendi
        var sql = @"
        INSERT INTO Coupon (ProductName, Description, Amount) 
        VALUES (@ProductName, @Description, @Amount) 
        RETURNING Id";

        // ✅ QuerySingleAsync ile Id'yi al
        var newId = await connection.QuerySingleAsync<int>(sql, new
        {
            coupon.ProductName,
            coupon.Description,
            coupon.Amount
        });

        // ✅ Entity'nin Id'sini güncelle
        coupon.Id = newId;

        return newId > 0;
    }

    public async Task<bool> DeleteDiscount(string productName)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        var affected = await connection.ExecuteAsync(
            "DELETE FROM Coupon WHERE ProductName = @ProductName",
             new { ProductName = productName });
        return affected > 0;
    }

    public async Task<Coupon?> GetDiscount(string productName) // ✅ Nullable döndürmeli
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>(
            "SELECT * FROM Coupon WHERE ProductName=@ProductName",
            new { ProductName = productName });

        // ❌ SAHTE OBJE SİLİNDİ
        // return coupon ?? new Coupon { ProductName = "No Discount" ... };

        // ✅ DOĞRUSU
        return coupon;
    }

    public async Task<bool> UpdateDiscount(Coupon coupon)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        var affected = await connection.ExecuteAsync(
            "UPDATE Coupon SET ProductName=@ProductName, Description=@Description, Amount=@Amount WHERE Id=@Id",
            new { coupon.Id, coupon.ProductName, coupon.Description, coupon.Amount });
        return affected > 0;
    }

}
