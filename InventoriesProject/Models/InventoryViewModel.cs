﻿namespace InventoriesProject.Models
{
    public class InventoryViewModel
    {
        public Guid Id { get; set; }

        public int Quantity { get; set; }

        public Guid ProductId { get; set; }
    }
}
