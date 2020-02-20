using Homework_mvc.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_mvc.Services.Interfaces
{
    public interface IMainService
    {
        IEnumerable<Entities.Article> GetArticles();
        Article GetArticle(int id);
        void DeleteArticle(int id);

        void DeletePassiveTags();
        IEnumerable<Entities.Review> GetReviews();

        IEnumerable<Entities.Tag> GetTags();

        IEnumerable<Question> GetQuestions();

        void CreateReview(Entities.Review review);
        void CreateArticle(Entities.Article article);
        void UpdateArticle(Article article);
        void CreateUser(User user);
    }
}
