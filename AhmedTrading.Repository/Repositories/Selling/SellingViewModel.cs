﻿using System;
using System.Collections.Generic;

namespace AhmedTrading.Repository
{
    public class SellingViewModel
    {
        public SellingViewModel()
        {
            ProductList = new HashSet<SellingProductListViewModel>();
        }
        public int SellingId { get; set; }
        public int RegistrationId { get; set; }
        public int? CustomerId { get; set; }
        public double SellingTotalPrice { get; set; }
        public double SellingDiscountAmount { get; set; }
        public double TransportationCost { get; set; }
        public double SellingPaidAmount { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime SellingDate { get; set; }
        public ICollection<SellingProductListViewModel> ProductList { get; set; }
    }

    public class SellingProductListViewModel
    {
        public int ProductId { get; set; }
        public double SellingQuantity { get; set; }
        public double SellingUnitPrice { get; set; }
    }

    public class SellingReceiptProductViewModel
    {
        public int ProductId { get; set; }
        public int ProductBrandId { get; set; }
        public string ProductName { get; set; }
        public string BrandName { get; set; }
        public double SellingQuantity { get; set; }
        public double SellingUnitPrice { get; set; }
        public double SellingPrice { get; set; }
    }

    public class SellingReceiptViewModel
    {
        public SellingReceiptViewModel()
        {
            this.Products = new HashSet<SellingReceiptProductViewModel>();
            this.Payments = new HashSet<SellingPaymentViewModel>();
        }
        public InstitutionVM InstitutionInfo { get; set; }
        public int SellingSn { get; set; }
        public int SellingId { get; set; }
        public double SellingTotalPrice { get; set; }
        public double SellingDiscountAmount { get; set; }
        public double TransportationCost { get; set; }
        public double SellingPaidAmount { get; set; }
        public double SellingDueAmount { get; set; }
        public DateTime SellingDate { get; set; }
        public ICollection<SellingReceiptProductViewModel> Products { get; set; }
        public ICollection<SellingPaymentViewModel> Payments { get; set; }
        public CustomerReceiptViewModel CustomerInfo { get; set; }
        public string SoildBy { get; set; }
    }



    public class SellingRecordViewModel
    {
        public int SellingId { get; set; }
        public int? CustomerId { get; set; }
        public int SellingSn { get; set; }
        public string CustomerName { get; set; }
        public double SellingAmount { get; set; }
        public double SellingPaidAmount { get; set; }
        public double SellingDiscountAmount { get; set; }
        public double TransportationCost { get; set; }
        public double SellingDueAmount { get; set; }
        public DateTime SellingDate { get; set; }
    }


    public class SellingProductReportModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string BrandName { get; set; }
        public double SellingQuantity { get; set; }
        public double SellingPrice { get; set; }
    }

}