using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NoteApp.Common;
using NoteApp.Domain;

namespace NoteApp
{
    public class ApiDependencyModule : IDependencyModule
    {
        public void ResolveDependencies(IServiceCollection container)
        {
        }

        public IEnumerable<IDependencyModule> ResolveModules()
        {
            yield return new DomainDependencyModule();
        }
    }
}
