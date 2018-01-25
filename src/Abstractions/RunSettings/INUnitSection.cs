namespace VisualStudio.Files.Abstractions.RunSettings
{
    /// <summary>
    /// NUnit specific section in a .runsettings file.
    /// </summary>
    public interface INUnitSection
    {
        /// <summary>
        /// Indicates whether a section <NUnit>...</NUnit> exists in the .runsettings file.
        /// </summary>
        bool Exists { get; }
        
        /// <summary>
        /// Integer value in milliseconds for the default timeout value
        /// for test cases.
        /// </summary>
        int? DefaultTimeOut { get; }
    }
}