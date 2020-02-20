using Homework_mvc.DAL.Repositories;
using Homework_mvc.Infrastructure.Data;

namespace Homework_mvc.DAL
{
    public interface IUnitOfWork
    {
        ArticleRepository Articles { get; }
        ReviewRepository Reviews { get; }
        QuestionRepository Questions { get; }
        UserRepository Users { get; }
        TagRepository Tags { get; }
    }
}