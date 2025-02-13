﻿using System;

namespace AhmedTrading.Repository
{
    public class PurchasePaymentViewModel
    {

    }

    public class PurchasePaymentListViewModel
    {
        public string PaymentMethod { get; set; }
        public double PaidAmount { get; set; }
        public DateTime PaidDate { get; set; }
    }

    public class PurchaseDuePaySingleModel
    {
        public int PurchaseId { get; set; }
        public int VendorId { get; set; }
        public int RegistrationId { get; set; }
        public string PaymentMethod { get; set; }
        public double PaidAmount { get; set; }
        public DateTime PaidDate { get; set; }
    }
}