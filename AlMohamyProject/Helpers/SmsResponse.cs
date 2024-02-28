namespace AlMohamyProject.Helpers
{
    public class SmsResponse
    {

        public int statusCode { get; set; }
        public string messageId { get; set; }
        public double cost { get; set; }
        public string currency { get; set; }
        public int totalCount { get; set; }
        public int msgLength { get; set; }
        public string accepted { get; set; }
        public string rejected { get; set; }
    }
}
