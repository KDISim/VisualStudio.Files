using VisualStudio.Files.Abstractions;
using IProjectInSolution = VisualStudio.Files.Core.Wrappers.IProjectInSolution;

namespace VisualStudio.Files.Core.Factories
{
    internal interface IProjectFactory
    {
        IProject Create(IProjectInSolution projectInSolution);
    }
}