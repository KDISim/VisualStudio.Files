namespace VisualStudio.Files.Core.Wrappers 
{
    public interface IProjectConfigurationInSolution
    {
        /// <summary>
        /// The configuration part of this configuration - e.g. "Debug", "Release"
        /// </summary>
        string ConfigurationName { get; }

        /// <summary>
        /// The platform part of this configuration - e.g. "Any CPU", "Win32"
        /// </summary>
        string PlatformName { get; }

        /// <summary>
        /// The full name of this configuration - e.g. "Debug|Any CPU"
        /// </summary>
        string FullName { get; }

        /// <summary>
        /// True if this project configuration should be built as part of its parent solution configuration
        /// </summary>
        bool IncludeInBuild { get; }
    }
}