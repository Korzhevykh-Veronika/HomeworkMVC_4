using Homework_mvc.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Homework_mvc.Model
{
    public class ArticleViewModel
    {
        
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = "Название статьи")]
        [Required(ErrorMessage = "Поле должно быть установлено")]
        [DataType(DataType.Text)]
        public string Title { get; set; }
        
        [Display(Name = "Текст статьи")]
        [Required(ErrorMessage = "Поле должно быть установлено")]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }

        [Display(Name = "Дата публикации")]
        public DateTime Date { get; set; }
        public string TagsQuery { get; set; }
    }
}