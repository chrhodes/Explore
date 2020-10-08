using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;

namespace Injection.Messaging
{
    public class MessagingFilterProvider : IFilterProvider
    {
        private readonly IKernel _kernel;

        public MessagingFilterProvider(IKernel kernel)
        {
            _kernel = kernel;
        }

        public IEnumerable<Filter> GetFilters(
            ControllerContext controllerContext, 
            ActionDescriptor actionDescriptor)
        {
            yield return new Filter(
                    _kernel.Get<MessageFilter>(),
                    FilterScope.Global,
                    order: null
                );
        }
    }
}