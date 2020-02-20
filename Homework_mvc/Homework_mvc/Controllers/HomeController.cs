using Homework_mvc.Entities;
using Homework_mvc.Model;
using Homework_mvc.Services;
using Homework_mvc.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Homework_mvc.Controllers
{
    public class HomeController : Controller
    {
        IMainService mainService;

        public HomeController(IMainService newMainService)
        {
            mainService = newMainService;
        }

        public ActionResult Index(List<int> tagsList, string tagsQuery, string searchPattern, int page = 1)
        {
            IEnumerable<Tag> tags = mainService.GetTags();
            IEnumerable<Article> articles = mainService.GetArticles();

            if (!String.IsNullOrEmpty(searchPattern))
            {
                searchPattern = searchPattern.ToLower();
                articles = articles.Where(article => article.Title.ToLower().Contains(searchPattern));
            }

            if (!String.IsNullOrEmpty(tagsQuery)) tagsList = tagsQuery.Split(',').Select(tag => Convert.ToInt32(tag)).ToList();

            if (tagsList != null)
            { 
                articles = articles.Where(a => tagsList.All(filteredTagId => a.Tags.Select(t => t.Id).Contains(filteredTagId))).ToList();
            }
            else
            {
                tagsList = new List<int>();
            }

            int pageSize = 2;
            IEnumerable<Article> articlesPerPages = articles.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = articles.Count() };
            IndexViewModel ivm = new IndexViewModel { CheckedTags = tagsList, PageInfo = pageInfo, Articles = articlesPerPages, Tags= tags, SearchPattern=searchPattern };
            return View(ivm);
        }


        [HttpGet]
        public ActionResult CreateArticle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateArticle(Article article, string TagsQuery)
        {
          

            if (ModelState.IsValid)
            {
                var arrayTags = TagsQuery.Split(',');
                var tags = mainService.GetTags();
                var articleTags = new List<Tag>();
                foreach (var at in arrayTags)
                {
                    Tag newTag;

                    newTag = tags.FirstOrDefault(tg => at == tg.Text);

                    if (newTag == null)
                    {
                        newTag = new Tag()
                        {
                            Text = at
                        };
                    }

                    articleTags.Add(newTag);
                }

                article.Tags = articleTags;
                mainService.CreateArticle(article);

                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult GetArticle(int id)
        {
            var article = mainService.GetArticle(id);
            return View(article);

        }
        public ActionResult DeleteArticle(int id)
        {
            mainService.DeleteArticle(id);
            mainService.DeletePassiveTags();

            return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult EditArticle(int id)
        {
            if (id == 0)
            {
                return HttpNotFound();
            }
            
            var article = mainService.GetArticle(id);
            var tags = mainService.GetTags();

            var articleVM = new ArticleViewModel
            {
                Id = article.Id,
                Title=article.Title,
                Text=article.Text,
                Date=article.Date
            };

            var tagsOfArticle = tags.Where(tg => article.Tags.Contains(tg)).Select(tag => tag.Text).ToList();

            articleVM.TagsQuery = String.Join(",", tagsOfArticle);

            if (article != null)
            {
                return View(articleVM);
            }
            return HttpNotFound();
            
        }
        [HttpPost]
        public ActionResult EditArticle(ArticleViewModel articleModel)
        {
            var article = mainService.GetArticle(articleModel.Id);
            if (article == null)
                return HttpNotFound();
            article.Title = articleModel.Title;
            article.Text = articleModel.Text;
            article.Date = articleModel.Date;

            var arrayTags=articleModel.TagsQuery.Split(',');
            var tags = mainService.GetTags();

            var articleTags = new List<Tag>();

            foreach(var at in arrayTags)
            {
                Tag newTag;

                newTag = tags.FirstOrDefault(tg => at == tg.Text);

                if(newTag == null)
                {
                    newTag = new Tag()
                    {
                        Text = at
                    };
                }

                articleTags.Add(newTag);
            }

            article.Tags.Clear();
            article.Tags = articleTags;

            mainService.UpdateArticle(article);
            mainService.DeletePassiveTags();

            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public ActionResult Guest()
        {
            ViewBag.Reviews = mainService.GetReviews();

            return View();
        }
        
        [HttpPost]
        public ActionResult Guest(Review review)
        {
            if (ModelState.IsValid)
            {
                mainService.CreateReview(review);

                return RedirectToAction("Guest");
            }

            return View();
            
        }

        [HttpGet]
        public ActionResult Profile()
        {
            ViewBag.Questions = mainService.GetQuestions();

            return View();            
        }
      
        [HttpPost]
        public ActionResult Profile(User user)
        {
            if (ModelState.IsValid)
            {
                mainService.CreateUser(user);

                return View("~/Views/Home/Result.cshtml");
            }
            return View();
        }        
    }
}