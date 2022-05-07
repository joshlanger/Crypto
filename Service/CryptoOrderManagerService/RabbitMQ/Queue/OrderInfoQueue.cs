using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel.Queue
{
    public class OrderInfoQueue : Queue
    {
        public const string ConfigurationNode = "OrderInfoQueue";
        public OrderInfoQueue() : base() { }
    }
}
