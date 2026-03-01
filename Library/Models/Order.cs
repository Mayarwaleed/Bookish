using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int? MemberId { get; set; }
        [ForeignKey("Shipping")]
        public int? ShippingId { get; set; }

        [ForeignKey("Payment")]
        public int PaymentId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string OrderStatus { get; set; }
        public virtual Member Member { get; set; }
        public virtual Shipping? Shipping { get; set; }
        public virtual Payment? Payment { get; set; }
       
    }

}
