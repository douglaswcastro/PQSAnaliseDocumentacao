using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octokit;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var github = new GitHubClient(new ProductHeaderValue("Analise"));
            Console.WriteLine("Digite o usuário do GitHub");
            var usuario = Console.ReadLine();
            Console.WriteLine("digite a senha so GitHub");
            var senha = Console.ReadLine();
            var basicAuth = new Credentials(usuario, senha);
            github.Credentials = basicAuth;

            List<string> usuarios = new List<string>
            {
                "wongsyrone",
                "Noisyfox",
                "GangZhuo",
                "celeron533",
                "icylogic",
                "breakwa11",
                "wzxjohn",
                "chenshaoju",
                "everyx",
                "rwasef1830"
            };
            GetRepositorioStar(github, usuarios);
            Console.ReadKey();
        }

        public static async void GetRepositorioStar(GitHubClient gitHub, List<string> usuarios)
        {
            List<Usuario> users = new List<Usuario>();
            var repositorio = gitHub.Repository.Get("shadowsocks", "shadowsocks-windows");
            var commitrepo = await gitHub.Repository.Commit.GetAll(repositorio.Result.Id);


            int somaestrelas;
            int somaforks;
            int somacommit;
            foreach (var user in usuarios)
            {
                var usuarioclass = await gitHub.User.Get(user);
                var repositorios = await gitHub.Repository.GetAllForUser(user);

                somaestrelas = 0;
                somaforks = 0;
                somacommit = 0;
                foreach (var repo in repositorios)
                {
                    somaestrelas += repo.StargazersCount;
                    somaforks += repo.ForksCount;
                }

                foreach (var commit in commitrepo)
                {
                    if (commit.Author != null)
                    {
                        somacommit += commit.Author.Login == user ? 1 : 0;
                    }
                }
                users.Add(new Usuario
                {
                    Nome = user,
                    estrelas = somaestrelas,
                    forks = somaforks,
                    commits = somacommit
                   
                });
            }

            foreach (var usuario in users)
            {
                Console.WriteLine($"nome : {usuario.Nome} | stars : {usuario.estrelas} | forks: {usuario.forks} | commits : {usuario.commits}");
            }



        }
    }
}
