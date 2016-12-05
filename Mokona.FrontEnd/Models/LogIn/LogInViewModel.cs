namespace Mokona.FrontEnd.Models
{
    public class LogInViewModel
    {
        public string Name { get; set; }

        public string Password { get; set; }

        public string CompanyDomain { get; set; }

        public bool RememberMe { get; set; }

        public override string ToString()
        {
            return string.Format("{0} - {1}", this.Name, this.CompanyDomain);
        }
    }
}
