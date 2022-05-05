using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NoteApp.Common;

namespace NoteApp.Domain
{
    public class DomainDependencyModule : IDependencyModule
    {
        public void ResolveDependencies(IServiceCollection container)
        {
            container.AddSingleton<INotesManager, NotesManager>();
        }

        public IEnumerable<IDependencyModule> ResolveModules()
        {
            return null;
        }
    }
}
