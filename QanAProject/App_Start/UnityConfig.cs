using System.Web.Http;
using Unity;
using Unity.WebApi;
using Unity.Mvc5;
using QandAServiceLayer;
using System.Web.Mvc;

namespace QanAProject
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<IQuestionsService, QuestionsService>();
            container.RegisterType<IUsersService, UserService>();
            container.RegisterType<ICategoriesService, CategoriesService>();
            container.RegisterType<IAnswersService, AnswersService>();
            // For Mvc
            DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));

            // For WebApi
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi .UnityDependencyResolver(container);
        }
    }
}