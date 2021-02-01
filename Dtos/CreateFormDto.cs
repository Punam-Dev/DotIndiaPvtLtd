using DotIndiaPvtLtd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotIndiaPvtLtd.Dtos
{
    public class CreateFormDto : Forms
    {
        public List<FormQueryDto> formQueryDtos { get; set; }
    }
}
