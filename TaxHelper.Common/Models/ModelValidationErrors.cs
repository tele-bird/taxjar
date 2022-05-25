namespace TaxHelper.Common.Models
{
    public static class ModelValidationErrors
    {
        public static readonly string ORDER_TOCOUNTRY_REQUIRED = "Order: To Country is required";
        public static readonly string ORDER_SHIPPING_REQUIRED = "Order: Shipping is required";
        public static readonly string ORDER_AMOUNT_OR_LINE_ITEMS_REQUIRED = "Order: Either Amount or Line Items are required";
        public static readonly string ORDER_TOZIP_REQUIRED_FOR_US = "Order: To Zip is required when To Country is US";
        public static readonly string ORDER_TOSTATE_REQUIRED_FOR_US_AND_CA = "Order: To State is required when To Country is US or CA";
        public static readonly string ORDER_AMOUNT_MISMATCH = "Order: Amount doesn't match the total of the line items";
    }
}
