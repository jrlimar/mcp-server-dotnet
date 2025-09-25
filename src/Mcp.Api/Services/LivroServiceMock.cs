using Mcp.Api.Entities;
using Mcp.Api.Interfaces;

namespace Mcp.Api.Services
{
    public class LivroServiceMock : ILivroService
    {
        private readonly List<Categoria> _categorias;
        private readonly List<Livro> _livros;

        public LivroServiceMock()
        {
            _categorias = new List<Categoria>
            {
                new Categoria(1, "Tecnologia"),
                new Categoria(2, "Ficção"),
                new Categoria(3, "História")
            };

            _livros = new List<Livro>
            {
                new Livro("Clean Code", "Robert C. Martin", 1, new DateTime(2008, 8, 1), 120m)
                {
                    Id = 1,
                    Categoria = _categorias[0]
                },
                new Livro("O Senhor dos Anéis", "J.R.R. Tolkien", 2, new DateTime(1954, 7, 29), 90m)
                {
                    Id = 2,
                    Categoria = _categorias[1]
                },
                new Livro("Sapiens", "Yuval Noah Harari", 3, new DateTime(2011, 1, 1), 70m)
                {
                    Id = 3,
                    Categoria = _categorias[2]
                }
            };
        }

        public List<Livro> ObterLivros(string titulo)
        {
            return _livros
                .Where(p => string.IsNullOrWhiteSpace(titulo) || p.Titulo.Contains(titulo, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public List<Categoria> ObterCategoriasDeLivros()
        {
            return _categorias;
        }

        public Livro? ObterPorId(int id)
        {
            return _livros.FirstOrDefault(p => p.Id == id);
        }

        public int CriarLivro(Livro livro)
        {
            livro.Id = _livros.Max(l => l.Id) + 1;
            livro.Categoria = _categorias.FirstOrDefault(c => c.Id == livro.CategoriaId);
            _livros.Add(livro);
            return livro.Id;
        }

        public void AtualizarLivro(Livro livroOriginal, Livro livroAlteracoes)
        {
            livroOriginal.AlterarDados(livroAlteracoes.Titulo,
                                       livroAlteracoes.Autor,
                                       livroAlteracoes.CategoriaId,
                                       livroAlteracoes.DataPublicacao,
                                       livroAlteracoes.Preco);

            livroOriginal.Categoria = _categorias.FirstOrDefault(c => c.Id == livroAlteracoes.CategoriaId);
        }

        public void AtualizarPrecoLivro(Livro livroOriginal, decimal preco)
        {
            livroOriginal.AlterarPrecoLivro(preco);
        }

        public void RemoverLivro(int id)
        {
            var livro = ObterPorId(id);
            if (livro == null)
                throw new ArgumentException("O livro com o identificador informado não existe", "id");

            _livros.Remove(livro);
        }
    }
}
