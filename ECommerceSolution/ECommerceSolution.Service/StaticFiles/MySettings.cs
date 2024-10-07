namespace ECommerceSolution.Service.StaticModels
{
    public static class MySettings
    {
        public static string DbConnection { get; }= "Server=DESKTOP-4AQFFCU;Database=Ecommerce_Db;User=sa;Password=123456;MultipleActiveResultSets=true;TrustServerCertificate=true";
        public static string PayDbConnection { get; set; }
        public static string PaymentApiURL { get; set; }
        public static string GoDaddyApiURL { get; set; }
        public static string GoDaddyKey { get; set; }
        public static string GoDaddySecret { get; set; }
        public static string GoDaddyDomainName { get; set; }
        public static string GoDaddyShopperID { get; set; }
        public static string GoDaddySubDomainIP { get; set; }
        public static string GoDaddyAppendDomain { get; set; }
        public static string GoDaddyDomainType { get; set; }
        public static string FromMailJetEmail { get; set; }
        public static string FromEmail { get; set; }
        public static string GoogleLocationAPIKey { get; set; }
        public static string HostURL { get; set; }
        public static string URL { get; set; }
        public static string OptionalURL { get; set; }
        public static string RequestType { get; set; }
        public static string SquareToken { get; set; }
        public static string SumUpToken { get; set; }
        public static string BraintreeToken { get; set; }
        public static string MerchantApplicationToken { get; set; }
        public static string MerchantPaymentToken { get; set; }
        public static string ImagePath { get; set; }
        public static bool LocalLogin { get; set; }
        public static string TitleOnEveryPage { get; set; }
        public static long TimeSheetAppID { get; set; }
        public static long PayCardAppID { get; set; }
        public static long AccessControlAppID { get; set; }
        public static long SaasAdminAppID { get; set; }
        public static long OrgSettingsAppID { get; set; }
        public static long OverViewFeatureID { get; set; }
        public static string GoogleURL { get; set; }
        public static string FacebookURL { get; set; }
        public static string SupportEmail { get; set; }
        public static string ServerPath { get; set; }
        public static string EncryptionKeyPassword { get; set; }
        public static string CompanyName { get; set; }
        public static string Logo { get; set; }
        public static string Icon { get; set; }
        public static string Address { get; set; }
        public static string Language { get; set; } = "En";

    }
}
