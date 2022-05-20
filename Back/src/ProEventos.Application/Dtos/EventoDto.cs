using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProEventos.Application.Dtos
{
    public class EventoDto
    {
         public int Id { get; set; }
        public string Local { get; set; }
        public string DataEvento { get; set; }
        [Required(ErrorMessage ="O campo {0} é obrigatório"),
        StringLength(50, MinimumLength =3, ErrorMessage ="Intervalo de 3 a 50 caracteres")]
        public string Tema { get; set; }
        [Display(Name ="Qtde de pessoas")]
        [Range(1,120000, ErrorMessage ="{0} não pode ser menor que 1 e maior q 120k")]
        public int QtdePessoas { get; set; }
       [RegularExpression(@".*\.(gif|jpe?g|bmp|png)$", ErrorMessage ="Não é uma imagem válida ")]
        public string ImagemURL { get; set; }
        [Required(ErrorMessage = "{0} é obrigatório")]
        public string Telefone { get; set; }

        [Display(Name ="E-mail")]
        [EmailAddress(ErrorMessage = "É necessário ser um {0} válido")]
        public string Email { get; set; }

        public IEnumerable<LoteDto> Lotes { get; set; }
        public IEnumerable<RedeSocialDto> RedesSociais { get; set; }
        public IEnumerable<PalestranteDto> Palestrantes { get; set; }
    }
}