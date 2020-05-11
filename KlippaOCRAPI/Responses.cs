using Newtonsoft.Json;

namespace Klippa.OCRAPI
{
    public partial class APIResponse
    {
        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public Data Data { get; set; }

        [JsonProperty("request_id")]
        public string RequestId { get; set; }

        [JsonProperty("result")]
        public string Result { get; set; }
    }

    public partial class Data
    {
        [JsonProperty("amount")]
        public long Amount { get; set; }

        [JsonProperty("amount_change")]
        public long AmountChange { get; set; }

        [JsonProperty("amountexvat")]
        public long Amountexvat { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("customer_address")]
        public string CustomerAddress { get; set; }

        [JsonProperty("customer_bank_account_number")]
        public string CustomerBankAccountNumber { get; set; }

        [JsonProperty("customer_bank_account_number_bic")]
        public string CustomerBankAccountNumberBic { get; set; }

        [JsonProperty("customer_city")]
        public string CustomerCity { get; set; }

        [JsonProperty("customer_coc_number")]
        public string CustomerCocNumber { get; set; }

        [JsonProperty("customer_country")]
        public string CustomerCountry { get; set; }

        [JsonProperty("customer_email")]
        public string CustomerEmail { get; set; }

        [JsonProperty("customer_municipality")]
        public string CustomerMunicipality { get; set; }

        [JsonProperty("customer_name")]
        public string CustomerName { get; set; }

        [JsonProperty("customer_number")]
        public string CustomerNumber { get; set; }

        [JsonProperty("customer_phone")]
        public string CustomerPhone { get; set; }

        [JsonProperty("customer_province")]
        public string CustomerProvince { get; set; }

        [JsonProperty("customer_reference")]
        public string CustomerReference { get; set; }

        [JsonProperty("customer_vat_number")]
        public string CustomerVatNumber { get; set; }

        [JsonProperty("customer_website")]
        public string CustomerWebsite { get; set; }

        [JsonProperty("customer_zipcode")]
        public string CustomerZipcode { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("document_subject")]
        public string DocumentSubject { get; set; }

        [JsonProperty("document_type")]
        public string DocumentType { get; set; }

        [JsonProperty("hash")]
        public string Hash { get; set; }

        [JsonProperty("hash_duplicate")]
        public bool HashDuplicate { get; set; }

        [JsonProperty("invoice_number")]
        public string InvoiceNumber { get; set; }

        [JsonProperty("invoice_type")]
        public string InvoiceType { get; set; }

        [JsonProperty("lines")]
        public Line[] Lines { get; set; }

        [JsonProperty("matched_keywords")]
        public MatchedKeyword[] MatchedKeywords { get; set; }

        [JsonProperty("matched_lineitems")]
        public MatchedLineitem[] MatchedLineitems { get; set; }

        [JsonProperty("merchant_address")]
        public string MerchantAddress { get; set; }

        [JsonProperty("merchant_bank_account_number")]
        public string MerchantBankAccountNumber { get; set; }

        [JsonProperty("merchant_bank_account_number_bic")]
        public string MerchantBankAccountNumberBic { get; set; }

        [JsonProperty("merchant_bank_domestic_account_number")]
        public string MerchantBankDomesticAccountNumber { get; set; }

        [JsonProperty("merchant_bank_domestic_bank_code")]
        public string MerchantBankDomesticBankCode { get; set; }

        [JsonProperty("merchant_city")]
        public string MerchantCity { get; set; }

        [JsonProperty("merchant_coc_number")]
        public string MerchantCocNumber { get; set; }

        [JsonProperty("merchant_country")]
        public string MerchantCountry { get; set; }

        [JsonProperty("merchant_country_code")]
        public string MerchantCountryCode { get; set; }

        [JsonProperty("merchant_email")]
        public string MerchantEmail { get; set; }

        [JsonProperty("merchant_id")]
        public string MerchantId { get; set; }

        [JsonProperty("merchant_main_activity_code")]
        public string MerchantMainActivityCode { get; set; }

        [JsonProperty("merchant_municipality")]
        public string MerchantMunicipality { get; set; }

        [JsonProperty("merchant_name")]
        public string MerchantName { get; set; }

        [JsonProperty("merchant_phone")]
        public string MerchantPhone { get; set; }

        [JsonProperty("merchant_province")]
        public string MerchantProvince { get; set; }

        [JsonProperty("merchant_vat_number")]
        public string MerchantVatNumber { get; set; }

        [JsonProperty("merchant_website")]
        public string MerchantWebsite { get; set; }

        [JsonProperty("merchant_zipcode")]
        public string MerchantZipcode { get; set; }

        [JsonProperty("order_number")]
        public string OrderNumber { get; set; }

        [JsonProperty("package_number")]
        public string PackageNumber { get; set; }

        [JsonProperty("payment_auth_code")]
        public string PaymentAuthCode { get; set; }

        [JsonProperty("payment_card_account_number")]
        public string PaymentCardAccountNumber { get; set; }

        [JsonProperty("payment_card_bank")]
        public string PaymentCardBank { get; set; }

        [JsonProperty("payment_card_issuer")]
        public string PaymentCardIssuer { get; set; }

        [JsonProperty("payment_card_number")]
        public string PaymentCardNumber { get; set; }

        [JsonProperty("payment_due_date")]
        public string PaymentDueDate { get; set; }

        [JsonProperty("payment_slip_code")]
        public string PaymentSlipCode { get; set; }

        [JsonProperty("payment_slip_customer_number")]
        public string PaymentSlipCustomerNumber { get; set; }

        [JsonProperty("payment_slip_reference_number")]
        public string PaymentSlipReferenceNumber { get; set; }

        [JsonProperty("paymentmethod")]
        public string Paymentmethod { get; set; }

        [JsonProperty("purchasedate")]
        public string Purchasedate { get; set; }

        [JsonProperty("purchasetime")]
        public string Purchasetime { get; set; }

        [JsonProperty("raw_text")]
        public string RawText { get; set; }

        [JsonProperty("receipt_number")]
        public string ReceiptNumber { get; set; }

        [JsonProperty("server")]
        public string Server { get; set; }

        [JsonProperty("shop_number")]
        public string ShopNumber { get; set; }

        [JsonProperty("table_group")]
        public string TableGroup { get; set; }

        [JsonProperty("table_number")]
        public string TableNumber { get; set; }

        [JsonProperty("terminal_number")]
        public string TerminalNumber { get; set; }

        [JsonProperty("transaction_number")]
        public string TransactionNumber { get; set; }

        [JsonProperty("transaction_reference")]
        public string TransactionReference { get; set; }

        [JsonProperty("vatamount")]
        public long Vatamount { get; set; }

        [JsonProperty("vatitems")]
        public Vatitem[] Vatitems { get; set; }
    }

    public partial class Line
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("lineitems")]
        public Lineitem[] Lineitems { get; set; }
    }

    public partial class Lineitem
    {
        [JsonProperty("amount")]
        public long Amount { get; set; }

        [JsonProperty("amount_each")]
        public long AmountEach { get; set; }

        [JsonProperty("amount_ex_vat")]
        public long AmountExVat { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("quantity")]
        public long Quantity { get; set; }

        [JsonProperty("sku")]
        public string Sku { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("vat_amount")]
        public long VatAmount { get; set; }

        [JsonProperty("vat_code")]
        public string VatCode { get; set; }

        [JsonProperty("vat_percentage")]
        public long VatPercentage { get; set; }
    }

    public partial class MatchedKeyword
    {
        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public partial class MatchedLineitem
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("lineitems")]
        public Lineitem[] Lineitems { get; set; }
    }

    public partial class Vatitem
    {
        [JsonProperty("amount")]
        public long Amount { get; set; }

        [JsonProperty("amount_excl_vat")]
        public long AmountExclVat { get; set; }

        [JsonProperty("amount_incl_excl_vat_estimated")]
        public bool AmountInclExclVatEstimated { get; set; }

        [JsonProperty("amount_incl_vat")]
        public long AmountInclVat { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("percentage")]
        public long Percentage { get; set; }
    }
}
