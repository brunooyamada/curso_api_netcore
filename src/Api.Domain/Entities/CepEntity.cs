﻿using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class CepEntity : BaseEntity
    {
        [Required]
        [MaxLength(10)]
        public string Cep { get; set; }

        [Required]
        [MaxLength(60)]
        public string Logradouro { get; set; }

        [MaxLength(10)]
        public string Numero { get; set; }

        [Required]
        public long MunicipioId { get; set; }

        public MunicipioEntity Municipio { get; set; }

    }
}
