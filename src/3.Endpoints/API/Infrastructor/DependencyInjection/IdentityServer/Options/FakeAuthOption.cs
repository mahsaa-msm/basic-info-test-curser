namespace Vehicle.Insurance.Endpoints.API.Infrastructor.DependencyInjection.IdentityServer.Options
{
    public class FakeAuthOption
    {
        public bool Enabled { get; set; } = false;
        public string FakeToken { get; set; } = string.Empty;
        public string NationalCodeClaim { get; set; } = "4200000008";
        public string ZaminNationalCodeClaim { get; set; } = "4200000008";
        public string CustomerId { get; set; } = "1";
        public string CustomerTypeClaim { get; set; } = string.Empty;
    }
}

