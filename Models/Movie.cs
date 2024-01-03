using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Models;

public class Movie
{
    public int Id { get; set; }
    [Required(ErrorMessage ="O Título é obrigatorio")]
    public string Title { get; set; }
    [Required(ErrorMessage = "O Gênero é obrigatorio")]
    [MaxLength(50,ErrorMessage ="O tamanho do gênero nao pode exceder 50 caracteres")]
    public string Genre { get; set; }
    [Required(ErrorMessage = "A duração é obrigatorio")]
    [Range(70,600, ErrorMessage ="A duração deve ter entre 70 e 600 minutos")]
    public int  Duration { get; set; }
}
