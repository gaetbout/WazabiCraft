using Wazabi.Model;

namespace Wazabi.Client
{
    public class CarteClient
    {
        public CarteClient()
        {
        }

        public CarteClient(Carte carte)
        {
            Id = carte.Id;
            Effet = carte.Effet;
            Description = carte.Description;
            Image = carte.ImageRef;
            Cout = carte.Cout;
            CodeEffet = carte.CodeEffet;
        }

        public int Id { get; set; }

        public string Effet { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public int Cout { get; set; }

        public string CodeEffet { get; set; }
    }
}