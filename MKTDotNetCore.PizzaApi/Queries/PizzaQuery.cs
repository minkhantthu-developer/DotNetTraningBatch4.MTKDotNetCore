namespace MKTDotNetCore.PizzaApi.Queries
{
    public class PizzaQuery
    {
        public static string PizzaOrderQuery { get; } =
            @"select po.[PizzaOrderId],po.[PizzaOrderInvoiceCode],po.[Total],po.[PizzaId]
              ,p.[Pizza],p.[Price] from [dbo].[Tbl_PizzaOrder] po inner join Tbl_Pizza p 
               on p.PizzaId=po.PizzaId where PizzaOrderInvoiceCode=@PizzaOrderInvoiceCode";

        public static string PizzaOrderDetailQuery { get; } =
            @"select pod.[PizzaOrderDetailId],pod.[PizzaOrderInvoiceCode],pod.[PizzaExtraId],
              pe.[PizzaExtra],pe.[Price] from [dbo].[Tbl_PizzaOrderDetail] pod     
              inner join [dbo].[Tbl_PizzaExtra] pe on pe.PizzaExtraId=pod.PizzaExtraId
                where PizzaOrderInvoiceCode=@PizzaOrderInvoiceCode";
    }         
}
