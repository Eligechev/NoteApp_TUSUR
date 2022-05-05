using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace NoteApp.Common
{
    public static class ModuleInitializer
    {
        public static void InitializeModules(IDependencyModule entryPoint, IServiceCollection container)
        {
            entryPoint.ResolveDependencies(container);

            var dependentModules = entryPoint.ResolveModules()?.ToList();

            if (dependentModules == null)
            {
                return;
            }

            foreach (var module in dependentModules)
            {
                InitializeModules(module, container);
            }
        }
    }
}
