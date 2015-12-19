using Wazabi.Model;

namespace Wazabi.Client
{
    public class DeClient
    {
        public DeClient()
        {
        }

        public DeClient(De de)
        {
            Id = de.Id;
            Value = de.Valeur;
            Image = de.ImageRef;
        }

        public int Id { get; set; }

        public string Value { get; set; }

        public string Image { get; set; }
    }
}