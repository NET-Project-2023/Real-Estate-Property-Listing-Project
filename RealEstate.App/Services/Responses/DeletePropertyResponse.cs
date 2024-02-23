namespace RealEstate.App.Services.Responses
{
    public class DeletePropertyResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<string>? ValidationsErrors { get; set; }
    }
}
