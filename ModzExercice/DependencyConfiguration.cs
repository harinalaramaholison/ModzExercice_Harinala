

namespace ModzExercice.WebApp
{
    public class DependencyConfiguration
    {
        public IServiceCollection _services { get; }

        public DependencyConfiguration(IServiceCollection services)
        {
            _services = services;
        }

        public void Register()
        {
            // Enregistrer ici les services de l'application
            
        }
     }


}
