﻿using System;
using System.Collections.Generic;

namespace AhmedTrading.Data
{
    public class PurchasePayment
    {
        public PurchasePayment()
        {
            PurchasePaymentList = new HashSet<PurchasePaymentList>();
        }

        public int PurchasePaymentId { get; set; }
        public int RegistrationId { get; set; }
        public int VendorId { get; set; }
        public int ReceiptSn { get; set; }
        public double PaidAmount { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime PaidDate { get; set; }
        public DateTime InsertDate { get; set; }

        public virtual Registration Registration { get; set; }
        public virtual Vendor Vendor { get; set; }
        public virtual ICollection<PurchasePaymentList> PurchasePaymentList { get; set; }
    }
}
