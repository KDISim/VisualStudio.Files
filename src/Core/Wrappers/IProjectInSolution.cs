using System.Collections.Generic;

namespace VisualStudio.Files.Core.Wrappers
{
    public interface IProjectInSolution
    {
        /// <summary>This project's name</summary>
        string ProjectName { get; }

        /// <summary>
        /// The path to this project file, relative to the solution location
        /// </summary>
        string RelativePath { get; }

        /// <summary>Returns the absolute path for this project</summary>
        string AbsolutePath { get; }

        /// <summary>
        /// The unique guid associated with this project, in "{XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX}" form
        /// </summary>
        string ProjectGuid { get; }

        /// <summary>
        /// The guid, in "{XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX}" form, of this project's
        /// parent project, if any.
        /// </summary>
        string ParentProjectGuid { get; }

        /// <summary>
        /// List of guids, in "{XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX}" form, mapping to projects
        /// that this project has a build order dependency on, as defined in the solution file.
        /// </summary>
        IReadOnlyList<string> Dependencies { get; }

        /// <summary>
        /// Configurations for this project, keyed off the configuration's full name, e.g. "Debug|x86"
        /// </summary>
        IReadOnlyDictionary<string, IProjectConfigurationInSolution> ProjectConfigurations { get; }
    }
}