using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcSalesApp.Web.DependencyResolution
{


  using StructureMap;

  public static class IoC
  {
    public static IContainer Initialize() {
      return new Container(c => c.AddRegistry<DefaultRegistry>());
    }
  }
}
