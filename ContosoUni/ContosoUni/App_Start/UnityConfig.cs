using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using ServiceLayer.Interfaces;
using ServiceLayer.Services;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Repositories;

namespace ContosoUni
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
           container.RegisterType<IStudentService, StudentService>();
            container.RegisterType<IStudentRepo, StudentRepository>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}