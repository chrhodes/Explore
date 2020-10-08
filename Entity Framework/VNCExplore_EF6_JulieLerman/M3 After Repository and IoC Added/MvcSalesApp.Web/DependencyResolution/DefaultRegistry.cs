namespace MvcSalesApp.Web.DependencyResolution
{
    using System.Data.Entity;

    using Data;

    using StructureMap;

    public class DefaultRegistry : Registry
  {
    #region Constructors and Destructors

    public DefaultRegistry() {
      Scan(
          scan =>
          {
            scan.TheCallingAssembly();
            scan.WithDefaultConventions();
            scan.With(new ControllerConvention());
          });
      //remember that Transient is the default. Left it here as a reminder
      For<DbContext>().Use<OrderSystemContext>().Transient();

      //Alternate
      //For(typeof(GenericRepository<>))
      //  .Use(typeof(GenericRepository<>))
      //  .Ctor<DbContext>().Is(new OrderSystemContext());
    }

    #endregion Constructors and Destructors
  }
}