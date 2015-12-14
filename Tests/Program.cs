using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.ServiceReference;

namespace Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            GestionJoueurClient test = new GestionJoueurClient();

            JoueurClient j1 = new JoueurClient();
            j1.Pseudo = "PW";
            j1.Mdp = "Test";
            j1.ConfirmPassword = "Test";

            JoueurClient j2 = new JoueurClient();
            j2.Pseudo = "GA";
            j2.Mdp = "Test";
            j2.ConfirmPassword = "Test";

            JoueurClient j3 = new JoueurClient();
            j3.Pseudo = "QA";
            j3.Mdp = "Test";
            j3.ConfirmPassword = "Test";

            test.Inscription(j1);
            Console.WriteLine("Inscription 1: OK");
            test.Inscription(j2);
            Console.WriteLine("Inscription 2: OK");
            test.Inscription(j3);
            Console.WriteLine("Inscription 3: OK");
            try
            {
                test.Inscription(j1);
                Console.ReadLine();
                return;
            }
            catch (Exception)
            {
                Console.WriteLine("Inscription: OK");
            }

            JoueurClient jC = new JoueurClient();
            jC.Pseudo = "PW";
            jC.Mdp = "toto";
            jC.ConfirmPassword = "toto";

            if (test.Connexion(jC) != null)
            {
                Console.ReadLine();
                return;
            }

            Console.WriteLine("Connexion 1: OK");


            jC.Pseudo = "PW";
            jC.Mdp = "test";
            jC.ConfirmPassword = "test";
            if (test.Connexion(jC) != null)
            {
                Console.ReadLine();
                return;
            }

            Console.WriteLine("Connexion 2: OK");

            jC.Pseudo = "PW";
            jC.Mdp = "Test";
            jC.ConfirmPassword = "Test";

            Console.WriteLine(test.Connexion(jC).Id);
            Console.ReadLine();
        }
    }
}
