using Alura.Filmes.App.Dados;
using Alura.Filmes.App.Extensions;
using Alura.Filmes.App.Negocio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Alura.Filmes.App
{
    class Program
    {
        static void Main(string[] args)
        {
            //IncluirAtor();
            //ConsultarAtores();
            //ListarOs10AtoresModificadosRecentemente();

            using (var contexto = new AluraFilmesContexto())
            {
                contexto.LogSQLToConsole();

                foreach(var filme in contexto.Filmes)
                {
                    Console.WriteLine(filme);
                }
            }

            Console.ReadLine();
        }

        private static void ListarOs10AtoresModificadosRecentemente()
        {
            using (var contexto = new AluraFilmesContexto())
            {
                contexto.LogSQLToConsole();

                //listar os 10 atores modificados recentemente 
                var atores = contexto.Atores
                    .OrderByDescending(a => EF.Property<DateTime>(a, "last_update"))
                    .Take(10);

                foreach (var ator in atores)
                {
                    Console.WriteLine(ator + " - " + contexto.Entry(ator).Property("last_update").CurrentValue);
                }
            }
        }

        private static void IncluirAtor()
        {
            using (var contexto = new AluraFilmesContexto())
            {
                contexto.LogSQLToConsole();

                var ator = new Ator();
                ator.PrimeiroNome = "Tom";
                ator.UltimoNome = "Hanks";
                //contexto.Entry(ator).Property("last_update").CurrentValue = DateTime.Now;

                contexto.Atores.Add(ator);

                contexto.SaveChanges();

            }
        }

        private static void ConsultarAtores()
        {
            //select * from actor

            using (var contexto = new AluraFilmesContexto())
            {
                contexto.LogSQLToConsole();

                foreach (var ator in contexto.Atores)
                {
                    System.Console.WriteLine(ator);
                }
            }
        }
    }
}