﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Sales.Domain.Purchases
{
    public enum PurchaseStatus
    {
        Created,
        Paid,
        ReadyForDelivery,
        OnTheWay,
        Delivered
    }
}
