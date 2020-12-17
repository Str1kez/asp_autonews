using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace asp_autonews.Domain.Entities
{
    public class Article : Base
    {
        [Required(ErrorMessage = "Введите название статьи")]
        [Display(Name = "Заголовок")]
        public override string Title { get; set; }

        [Display(Name = "Описание")]
        public override string Subtitle { get; set; }

        [Display(Name = "Текст")]
        public override string Text { get; set; }

        [Display(Name = "Картинка")]
        public override string Image { get; set; }
    }
}
