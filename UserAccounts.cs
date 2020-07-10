using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Globalization;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;

namespace WebApplication2
{
    public class UserAccounts
    {
        //private DateTimeOffset _PaymentDueDate = DateTimeOffset.MinValue;

        [JsonPropertyName("Id")]
        public int Id { get; set; }
        
        [JsonPropertyName("FirstName")] 
        public string FirstName { get; set; }
        
        [JsonPropertyName("LastName")] 
        public string LastName { get; set; }
        
        [JsonPropertyName("Email")] 
        public string Email { get; set; }

        private string _PhoneNumber = null;
        [JsonPropertyName("PhoneNumber")] 
        public string PhoneNumber {
            get
            {
                //Since we assume USD, I will assume US phone numbers
                return Regex.Replace(_PhoneNumber, @"(\d{3})(\d{3})(\d{4})", "(($1)-$2-$3)"); //((###)-###-####)
            }
            set
            {
                //error checking
                _PhoneNumber = value;
            }
        }

        [JsonPropertyName("AmountDue")]
        public decimal AmountDue { get; set; }

        public string FormattedAmountDue
        {
            get
            {
                return AmountDue.ToString("$##0.00");
            }
        }
        private string _PaymentDueDateIn = null;
        [JsonPropertyName("PaymentDueDate")]
        public String PaymentDueDateIn 
        {
            get
            {
                return _PaymentDueDateIn;
            }
            set
            {
               DateTimeOffset ConvertedDTPaymentDueDate;
               DateTimeOffset.TryParse(value, null as IFormatProvider, DateTimeStyles.AssumeUniversal, out ConvertedDTPaymentDueDate);
               DTPaymentDueDate = ConvertedDTPaymentDueDate;
               _PaymentDueDateIn = value;
            }
        }
       

        public DateTimeOffset DTPaymentDueDate { get; set; }

        public string FormattedPaymentDueDate
        {
            get
            {
                string DateFormat = "MM/dd/yyyy";
                return "(" + DTPaymentDueDate.ToString(DateFormat) + ")";
            }
        }
         
        [JsonPropertyName("AccountStatusId")] 
        public int AccountStatusId { get; set; } //error checking

    }


    public enum AccountStatuses
    {
        Active,
        Inactive,
        Overdue
    }


}

