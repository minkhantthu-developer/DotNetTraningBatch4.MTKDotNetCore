using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKTDotNetCore.PizzaApi.Db
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        }

        public DbSet<PizzaModel> pizza { get; set; }
        public DbSet<PizzaExtraModel> pizzaExtra { get; set; }
        public DbSet<PizzaOrderModel> pizzaOrder { get; set; }
        public DbSet<PizzaOrderDetailModel> pizzaDetail { get; set; }
    }

    [Table("Tbl_Pizza")]
    public class PizzaModel
    {
        [Key]
        [Column("PizzaId")]
        public int Id { get; set; }

        [Column("Pizza")]
        public string? Name { get; set; }

        [Column("Price")]
        public decimal Price { get; set; }

        [NotMapped]
        public string PriceStr { get { return "$" + Price; } }
    }

    [Table("Tbl_PizzaExtra")]
    public class PizzaExtraModel
    {
        [Key]
        [Column("PizzaExtraId")]
        public int Id { get; set; }

        [Column("PizzaExtra")]
        public string? Name { get; set; }

        [Column("Price")]
        public decimal Price { get; set; }

        [NotMapped]
        public string PizzaStr { get { return "$" + Price; } }
    }

    [Table("Tbl_PizzaOrder")]
    public class PizzaOrderModel
    {
        [Key] public int PizzaOrderId { get; set; }
        public string? PizzaOrderInvoiceCode { get; set; }
        public int PizzaId { get; set; }
        public decimal Total { get; set; }
    }

    public class OrderRequest
    {
        public int PizzaId { get; set; }
        public int[]? Extra { get; set; }
    }

    [Table("Tbl_PizzaOrderDetail")]
    public class PizzaOrderDetailModel
    {
        [Key] public int PizzaOrderDetailId { get; set; }
        public string? PizzaOrderInvoiceCode { get; set; }
        public int PizzaExtraId { get; set; }
    }

    public class OrderResponse
    {
        public string? InvoiceNo { get; set; }
        public string? messaage { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class OrderDetailResponseModel
    {
        public PizzaOrderHeadModel? pizzaOrder { get; set; }
        public List<PizzaOrderDetail>? lstPizzaOrderDetail { get; set; }
    }

    public class PizzaOrderHeadModel
    {
        public int PizzaOrderId { get; set; }
        public string? PizzaOrderInvoiceCode { get; set; }
        public decimal Total { get; set; }
        public int PizzaId { get; set; }
        public string? Pizza { get; set; }
        public decimal Price { get; set; }
    }

    public class PizzaOrderDetail
    {
        public int PizzaOrderDetailId { get; set; }
        public string? PizzaOrderInvoiceCode { get; set; }
        public int PizzaExtraId { get; set; }
        public string? PizzaExtra { get; set; }
        public decimal Price { get; set; }
    }

}
