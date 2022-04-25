namespace EKS.ProcessMaps.Models
{
    public class EmailNotificationModel
    {
        public string From { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Host { get; set; }

        public string Port { get; set; }

        public string Body { get; set; }

        public string Sender { get; set; }

        public string Recipients { get; set; }

        public string Subject { get; set; }

        public string CC { get; set; }

        public string EnableSsl { get; set; }

        public string TargetName { get; set; }
    }
}
