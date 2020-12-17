using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace asp_autonews.Domain.Entities
{
    //Сделаем абстрактным, так как объекты не нужны
    public abstract class Base
    {
        //Дата создания задается сразу
        protected Base() => DateCreate = DateTime.UtcNow;

        [Required]  // обязательно
        public Guid Id { get; set; }

        // виртуальные, чтобы можно было переопределять в дочерних
        [Display(Name = "Заголовок")]
        public virtual string Title { get; set; }
        
        [Display(Name = "Описание")]
        public virtual string Subtitle { get; set; }
        
        [Display(Name = "Текст")]
        public virtual string Text{ get; set; }
        
        [Display(Name = "Картинка")]
        public virtual string Image{ get; set; }
        
        [Display(Name = "мета-заголовок")]
        public string MetaTitle{ get; set; }
        
        [Display(Name = "мета-описание")]
        public string MetaDescription{ get; set; }
        
        [Display(Name = "мета-ключи")]
        public string MetaKeywords{ get; set; }
        
        //Дата создания
        [DataType(DataType.Time)]
        public DateTime DateCreate{ get; set; }

    }
}
