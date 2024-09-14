using LabPreTest.Shared.Enums;
using LabPreTest.Shared.Interfaces;
using LabPreTest.Shared.Messages;
using System.ComponentModel.DataAnnotations;

namespace LabPreTest.Shared.Entities
{
    public class Order : IEntityWithId, IOrderEntity
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }

        public OrderStatus Status { get; set; }

        public User? User { get; set; }
        public string? UserId { get; set; }

        public ICollection<OrderDetail>? Details { get; set; }
        public int DetailsNumber => Details == null || Details.Count == 0 ? 0 : Details.Count;
    }
}