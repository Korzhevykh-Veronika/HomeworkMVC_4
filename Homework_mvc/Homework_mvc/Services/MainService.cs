using Homework_mvc.DAL;
using Homework_mvc.Entities;
using Homework_mvc.Infrastructure.Data;
using Homework_mvc.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Homework_mvc.Services
{
    public class MainService: IMainService
    {
        IUnitOfWork unitOfWork;

        public MainService(IUnitOfWork newUnitOfWork)
        {
            unitOfWork = newUnitOfWork;
        }

        public IEnumerable<Entities.Article> GetArticles()
        {            
           var articles= unitOfWork.Articles.GetAll();
            foreach(var a in articles)
            {
                var maxCount = a.Text.Length < 200 ? a.Text.Length : 200;

                a.Text = a.Text.Substring(0, maxCount) +"...";
            }
            return articles;

        }
        public Article GetArticle(int id)
        {
            return unitOfWork.Articles.Get(id);
        }
        public void DeleteArticle(int id)
        {
            unitOfWork.Articles.Delete(id);
        }

        public void DeletePassiveTags()
        {
            var allTags = unitOfWork.Tags.GetAll();

            foreach(var tag in allTags)
            {
                if(tag.Articles.Count == 0)
                {
                    unitOfWork.Tags.Delete(tag.Id);
                }
            }
        }
        public IEnumerable<Entities.Review> GetReviews()
        {
            return unitOfWork.Reviews.GetAll();
        }
        public IEnumerable<Entities.Tag> GetTags()
        {
            return unitOfWork.Tags.GetAll();
        }

        public IEnumerable<Question> GetQuestions()
        {
            return unitOfWork.Questions.GetAll();
        }

        public void CreateReview(Entities.Review review)
        {
            unitOfWork.Reviews.Create(review);
        }
        public void CreateArticle(Entities.Article article)
        {
            unitOfWork.Articles.Create(article);
        }
        public void UpdateArticle(Article article)
        {
            unitOfWork.Articles.Update(article);
        }
        public void CreateUser(User user)
        {
            for (int i = 0; i < user.Profiles.Count; i++)
            {
                user.Profiles[i].QuestionId = user.Questions[i].Id;
            }

            user.Profiles = user.Profiles.Where(profile => profile.Text != null).ToList();
            user.Questions.Clear();

            unitOfWork.Users.Create(user);
        }
    }
}