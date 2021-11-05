﻿using Alura.Filmes.App.Dados;
using Alura.Filmes.App.Extensions;
using Alura.Filmes.App.Negocio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.SqlClient;
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
            //FilmesDeUmaCategoria();
            //Consultaridiomas();
            //ConsultarFilmesEIdiomas();
            //TestandoChaveUnica();
            //TestandoRestricaoCheck();
            //TestandoExtensionEnumClassificacaoIndicativa();
            //NovoTesteRestricaoCheck();
            //ConsultarClientesEFuncionarios();
            //ConsultarAtoresQueMaisAtuaram();
            //ConsultarAtoresQueMaisAtuaramComSqlCustomizado();
            //ConsultarAtoresQueMaisAtuaramComSqlCustomizadoEmUmaView();
            //ConsultarTotalDeAtoresPorCategoriaViaStoredProcedure();

            IncluirERemoverRegistrosEscrevendoComandoSql();

            Console.ReadLine();
        }

        private static void IncluirERemoverRegistrosEscrevendoComandoSql()
        {
            using (var contexto = new AluraFilmesContexto())
            {
                contexto.LogSQLToConsole();

                var sql = "INSERT INTO language (name) VALUES ('Teste 1'), ('Teste 2'), ('Teste 3')";

                var registros = contexto.Database.ExecuteSqlCommand(sql);
                System.Console.WriteLine($"O total de registros afetados é {registros}.");

                var deleteSql = "DELETE FROM language WHERE name LIKE 'Teste%'";
                registros = contexto.Database.ExecuteSqlCommand(deleteSql);
                System.Console.WriteLine($"O total de registros afetados é {registros}.");
            }
        }

        private static void ConsultarTotalDeAtoresPorCategoriaViaStoredProcedure()
        {
            using (var contexto = new AluraFilmesContexto())
            {
                contexto.LogSQLToConsole();

                var categ = "Action"; //36

                var paramCateg = new SqlParameter("category_name", categ);

                var paramTotal = new SqlParameter
                {
                    ParameterName = "@total_actors",
                    Size = 4,
                    Direction = System.Data.ParameterDirection.Output
                };

                contexto.Database
                    .ExecuteSqlCommand("total_actors_from_given_category @category_name, @total_actors OUT", paramCateg, paramTotal);

                System.Console.WriteLine($"O total de atores na categoria {categ} é de {paramTotal.Value}.");

            }
        }

        private static void ConsultarAtoresQueMaisAtuaramComSqlCustomizadoEmUmaView()
        {
            using (var contexto = new AluraFilmesContexto())
            {
                contexto.LogSQLToConsole();

                var sql = @"select a.* from actor a
                            inner join top5_most_starred_actors filmes on filmes.actor_id = a.actor_id";

                var atoresMaisAtuantes = contexto.Atores
                    .FromSql(sql)
                    .Include(a => a.Filmografia);

                foreach (var ator in atoresMaisAtuantes)
                {
                    System.Console.WriteLine($"O ator {ator.PrimeiroNome} {ator.UltimoNome} atuou em {ator.Filmografia.Count} filmes.");
                }

            }
        }

        private static void ConsultarAtoresQueMaisAtuaramComSqlCustomizado()
        {
            using (var contexto = new AluraFilmesContexto())
            {
                contexto.LogSQLToConsole();

                var sql = @"select a.*
                        from actor a
                          inner join
                        (select top 5 a.actor_id, count(*) total
                        from actor a
                          inner join film_actor fa on fa.actor_id = a.actor_id
                        group by a.actor_id
                        order by total desc) filmes on filmes.actor_id = a.actor_id";

                var atoresMaisAtuantes = contexto.Atores
                    .FromSql(sql)
                    .Include(a => a.Filmografia);

                foreach (var ator in atoresMaisAtuantes)
                {
                    System.Console.WriteLine($"O ator {ator.PrimeiroNome} {ator.UltimoNome} atuou em {ator.Filmografia.Count} filmes.");
                }

            }
        }

        private static void ConsultarAtoresQueMaisAtuaram()
        {
            using (var contexto = new AluraFilmesContexto())
            {
                contexto.LogSQLToConsole();

                var atoresMaisAtuantes = contexto.Atores
                    .Include(a => a.Filmografia)
                    .OrderByDescending(a => a.Filmografia.Count)
                    .Take(5);

                foreach (var ator in atoresMaisAtuantes)
                {
                    System.Console.WriteLine($"O ator {ator.PrimeiroNome} {ator.UltimoNome} atuou em {ator.Filmografia.Count} filmes");
                }
            }
        }

        private static void ConsultarClientesEFuncionarios()
        {
            using (var contexto = new AluraFilmesContexto())
            {
                contexto.LogSQLToConsole();

                Console.WriteLine("Clientes:");
                foreach (var cliente in contexto.Clientes)
                {
                    Console.WriteLine(cliente);
                }

                Console.WriteLine("\nFuncionários");
                foreach (var func in contexto.Funcionarios)
                {
                    Console.WriteLine(func);
                }
            }
        }

        private static void NovoTesteRestricaoCheck()
        {
            using (var contexto = new AluraFilmesContexto())
            {
                contexto.LogSQLToConsole();

                var filme = new Filme();
                filme.Titulo = "Cassino Royale";
                filme.Duracao = 120;
                filme.AnoLancamento = "2000";
                filme.Classificacao = ClassificacaoIndicativa.MaioresQue14;
                filme.IdiomaFalado = contexto.Idiomas.First();
                contexto.Entry(filme).Property("last_update").CurrentValue = DateTime.Now;

                contexto.Filmes.Add(filme);
                contexto.SaveChanges();

                var filmeInserido = contexto.Filmes.First(f => f.Titulo == "Cassino Royale");
                Console.WriteLine(filmeInserido.Classificacao);
            }
        }

        private static void TestandoExtensionEnumClassificacaoIndicativa()
        {
            var m10 = ClassificacaoIndicativa.MaioresQue18;
            Console.WriteLine(m10.ParaString());
            Console.WriteLine("G".ParaValor());
        }

        private static void TestandoRestricaoCheck()
        {
            using (var contexto = new AluraFilmesContexto())
            {
                contexto.LogSQLToConsole();

                var idioma = new Idioma { Nome = "English" };

                var filme = new Filme();
                filme.Titulo = "Senhor dos Aneis";
                filme.Duracao = 120;
                filme.AnoLancamento = "2000";
                filme.Classificacao = ClassificacaoIndicativa.Livre;
                filme.IdiomaFalado = idioma;
                contexto.Entry(filme).Property("last_update").CurrentValue = DateTime.Now;

                contexto.Filmes.Add(filme);
                contexto.SaveChanges();
            }
        }

        private static void TestandoChaveUnica()
        {
            using (var contexto = new AluraFilmesContexto())
            {

                contexto.LogSQLToConsole();

                var ator1 = new Ator { PrimeiroNome = "Emma", UltimoNome = "Watson" };
                var ator2 = new Ator { PrimeiroNome = "Emma", UltimoNome = "Watson" };
                contexto.Atores.AddRange(ator1, ator2);
                contexto.SaveChanges();

                var emmaWatson = contexto.Atores
                    .Where(a => a.PrimeiroNome == "Emma" && a.UltimoNome == "Watson");
                Console.WriteLine($"Total de atores encontrados: {emmaWatson.Count()}.");
            }
        }

        private static void ConsultarFilmesEIdiomas()
        {
            using (var contexto = new AluraFilmesContexto())
            {
                contexto.LogSQLToConsole();

                var idiomas = contexto.Idiomas
                    .Include(i => i.FilmesFalados);

                foreach (var idioma in idiomas)
                {
                    Console.WriteLine(idioma);

                    foreach (var filme in idioma.FilmesFalados)
                    {
                        Console.WriteLine(filme);
                    }
                    Console.WriteLine("\n");
                }

            }
        }

        private static void Consultaridiomas()
        {
            using (var contexto = new AluraFilmesContexto())
            {
                contexto.LogSQLToConsole();

                foreach (var idioma in contexto.Idiomas)
                {
                    Console.WriteLine(idioma);
                }
            }
        }

        private static void FilmesDeUmaCategoria()
        {
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