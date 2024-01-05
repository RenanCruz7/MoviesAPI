using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Data.Dtos;

/*
DTOs nos ajudam a não deixar nosso modelo de banco de dados exposto.
Como fazer conversões práticas entre diferentes tipos através do AutoMapper.
 */

public class CreateMovieDto
{
    [Required(ErrorMessage = "O Título é obrigatorio")]
    public string Title { get; set; }
    [Required(ErrorMessage = "O Gênero é obrigatorio")]
    [StringLength(50, ErrorMessage = "O tamanho do gênero nao pode exceder 50 caracteres")]
    public string Genre { get; set; }
    [Required(ErrorMessage = "A duração é obrigatorio")]
    [Range(70, 600, ErrorMessage = "A duração deve ter entre 70 e 600 minutos")]
    public int Duration { get; set; }
}
