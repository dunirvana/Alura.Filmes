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
            //ConsultarFilmes();
            //ConsultarElenco();
            //ElencoDeUmFilme();

            using (var contexto = new AluraFilmesContexto())
            {
                contexto.LogSQLToConsole();

                var categorias = contexto.Categorias
                    .Include(c => c.Filmes)
                    .ThenInclude(fc => fc.Filme);

                foreach (var c in categorias)
                {
                    Console.WriteLine("");
                    Console.WriteLine($"Filmes da categoria {c}:");
                    foreach (var fc in c.Filmes)
                    {
                        Console.WriteLine(fc.Filme);
                    }
                }

            }

            Console.ReadLine();
        }

        private static void ElencoDeUmFilme()
        {
            using (var contexto = new AluraFilmesContexto())
            {
                contexto.LogSQLToConsole();

                var filme = contexto.Filmes
                    .Include(f => f.Atores)
                    .ThenInclude(fa => fa.Ator)
                    .First();

                Console.WriteLine(filme);
                Console.WriteLine("Elenco");

                foreach (var ator in filme.Atores)
                {
                    Console.WriteLine(ator.Ator);
                }
            }
        }

        private static void ConsultarElenco()
        {
            using (var contexto = new AluraFilmesContexto())
            {
                contexto.LogSQLToConsole();

                foreach (var item in contexto.Elenco)
                {
                    var entidade = contexto.Entry(item);
                    var filmId = entidade.Property("film_id").CurrentValue;
                    var actorId = entidade.Property("actor_id").CurrentValue;
                    var lastUpd = entidade.Property("last_update").CurrentValue;
                    Console.WriteLine($"Filme {filmId}, Ator {actorId}, LastUpdate: {lastUpd}");
                }
            }
        }

        private static void ConsultarFilmes()
        {
            using (var contexto = new AluraFilmesContexto())
            {
                contexto.LogSQLToConsole();

                foreach (var filme in contexto.Filmes)
                {
                    Console.WriteLine(filme);
                }
            }
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