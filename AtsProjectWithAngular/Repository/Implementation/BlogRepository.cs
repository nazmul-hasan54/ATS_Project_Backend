//using AtsProjectWithAngular.Domain;
//using AtsProjectWithAngular.Domain.Context;
//using AtsProjectWithAngular.Repository.Abastraction;

//namespace AtsProjectWithAngular.Repository.Implementation
//{
//    public class BlogRepository : IBlogInterface
//    {
//        private readonly BlogDbContext _blogDbContext;
//        public BlogRepository(BlogDbContext blogDbContext)
//        {
//            _blogDbContext = blogDbContext;
//        }
//        public bool AddBlog(Blog blog)
//        {
//            try
//            {
//                _blogDbContext.Add(blog);
//                _blogDbContext.SaveChanges();
//                return true;
//            }
//            catch (Exception)
//            {
//                return false;
//            }
//        }
//    }
//}
