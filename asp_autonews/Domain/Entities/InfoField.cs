using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace asp_autonews.Domain.Entities
{
    public class InfoField : Base
    {
        [Required]
        public string Key { get; set; }

        [Display(Name = "Заголовок")]
        public override string Title { get; set; } = "Страница с информацией о сайте";

        [Display(Name = "Текст")]
        public override string Text { get; set; } = "Ведите себя хорошо!";

    }
}
