using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace NoteApp.Common
{
    /// <summary>
    /// Интерфейс ресолвера зависимостей для сборки.
    /// </summary>
    public interface IDependencyModule
    {
        /// <summary>
        /// Регистрация необходимых зависимостей в контейнере.
        /// </summary>
        /// <param name="container">DI-контейнер.</param>
        public void ResolveDependencies(IServiceCollection container);

        /// <summary>
        /// Регистрация зависимых модулей.
        /// </summary>
        public IEnumerable<IDependencyModule> ResolveModules();
    }
}
