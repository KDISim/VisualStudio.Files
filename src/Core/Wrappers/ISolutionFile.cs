using System.Collections.Generic;

namespace VisualStudio.Files.Core.Wrappers
{
    public interface ISolutionFile
    {
        /// <summary>
        /// All projects in this solution, in the order they appeared in the solution file
        /// </summary>
        IReadOnlyList<Core.Wrappers.IProjectInSolution> ProjectsInOrder { get; }

        /// <summary>
        /// The collection of projects in this solution, accessible by their guids as a
        /// string in "{XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX}" form
        /// </summary>
        IReadOnlyDictionary<string, Core.Wrappers.IProjectInSolution> ProjectsByGuid { get; }

        /// <summary>
        /// The list of all full solution configurations (configuration + platform) in this solution
        /// </summary>
        IReadOnlyList<Core.Wrappers.ISolutionConfigurationInSolution> SolutionConfigurations { get; }

        /// <summary>
        /// Gets the default configuration name for this solution. Usually it's Debug, unless it's not present
        /// in which case it's the first configuration name we find.
        /// </summary>
        string GetDefaultConfigurationName();

        /// <summary>
        /// Gets the default platform name for this solution. Usually it's Mixed Platforms, unless it's not present
        /// in which case it's the first platform name we find.
        /// </summary>
        string GetDefaultPlatformName();
    }
}