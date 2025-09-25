namespace Mcp.Api.Entities
{
    public class Livro
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public int CategoriaId { get; set; }
        public DateTime DataPublicacao { get; set; }
        public decimal Preco { get; set; }

        public Categoria? Categoria { get; set; }

        public Livro(string titulo, string autor, int categoriaId, DateTime dataPublicacao, decimal preco)
        {
            Titulo = titulo;
            Autor = autor;
            CategoriaId = categoriaId;
            DataPublicacao = dataPublicacao;
            Preco = preco;
        }

        public void AlterarDados(string titulo, string autor, int categoriaId, DateTime dataPublicacao, decimal preco)
        {
            Titulo = titulo;
            Autor = autor;
            CategoriaId = categoriaId;
            DataPublicacao = dataPublicacao;
            Preco = preco;
        }

        public void AlterarPrecoLivro(decimal preco)
        {
            Preco = preco;
        }
    }
}