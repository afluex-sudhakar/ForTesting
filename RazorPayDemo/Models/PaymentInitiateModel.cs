using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RazorPayDemo.Models
{
    public class PaymentInitiateModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public int Amount { get; set; }
        public string Pk_UserId { get; set; }
        public string TransactionType { get; set; }
        public string orderId { get; set; }
        public string Type { get; set; }
        
        public string PaymentId { get; set; }
        public string entity { get; set; }
        public string currency { get; set; }
        public string status { get; set; }
        public string invoice_id { get; set; }
        public string international { get; set; }
        public string method { get; set; }
        public string amount_refunded { get; set; }
        public string refund_status { get; set; }
        
        public string captured { get; set; }
        public string description { get; set; }
        public string card_id { get; set; }
        public string bank { get; set; }
        public string wallet { get; set; }
        public string vpa { get; set; }
        public string fee { get; set; }
        public string tax { get; set; }
        public string error_code { get; set; }
        
        public string error_description { get; set; }
        public string error_source { get; set; }
        public string error_step { get; set; }
        public string error_reason { get; set; }
        public string created_at { get; set; }
  
        public DataSet SaveOrderDetails()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@amount", Amount),
                                      new SqlParameter("@Pk_UserId", Pk_UserId),
                                      new SqlParameter("@Type", Type),
                                      new SqlParameter("@TransactionType", TransactionType),
                                      new SqlParameter("@OrderId", orderId)
                                  };
            DataSet ds = Connection.ExecuteQuery("SaveOrderDetails", para);
            return ds;
        }


        public DataSet SaveFetchPaymentResponse()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@PaymentId", PaymentId),
                                      new SqlParameter("@entity", entity),
                                      new SqlParameter("@amount", Amount),
                                      new SqlParameter("@currency", currency),
                                      new SqlParameter("@OrderId", orderId),
                                      new SqlParameter("@status", status),
                                      new SqlParameter("@invoice_id", invoice_id),
                                      new SqlParameter("@international", international),
                                      new SqlParameter("@method", method),
                                      new SqlParameter("@amount_refunded", amount_refunded),
                                      new SqlParameter("@refund_status", refund_status),
                                      new SqlParameter("@captured", captured),
                                      new SqlParameter("@description", description),
                                      new SqlParameter("@card_id", card_id),
                                      new SqlParameter("@bank", bank),
                                      new SqlParameter("@wallet", wallet),
                                      new SqlParameter("@vpa", vpa),
                                      new SqlParameter("@email", Email),
                                      new SqlParameter("@contact", Mobile),
                                      new SqlParameter("@fee", fee),
                                      new SqlParameter("@tax", tax),
                                      new SqlParameter("@error_code", error_code),
                                      new SqlParameter("@error_description", error_description),
                                      new SqlParameter("@error_source", error_source),
                                      new SqlParameter("@error_step", error_step),
                                      new SqlParameter("@error_reason", error_reason),
                                      new SqlParameter("@created_at", created_at),
                                      new SqlParameter("@Pk_UserId", Pk_UserId),

                                  };
            DataSet ds = Connection.ExecuteQuery("SaveFetchPaymentResponse", para);
            return ds;
        }
    }

    //public class RazorPayOrder
    //{
    //    public string OrderId { get; set; }
    //    public string RazorPayAPIKey { get; set; }
    //    public int Amount { get; set; }
    //    public string Currency { get; set; }
    //    public string Name { get; set; }
    //    public string Email { get; set; }
    //}

    public class RazorPayOrder
    {
        public string OrderId { get; set; }
        public string RazorPayAPIKey { get; set; }
        public int Amount { get; set; }
        public string Currency { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
    }



}