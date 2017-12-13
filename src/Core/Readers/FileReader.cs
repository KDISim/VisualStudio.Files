using System;
using System.IO;
using DotNet.Globbing;
using Microsoft.Extensions.DependencyInjection;
using WrapThat.SystemIO;

namespace VisualStudio.Files.Core.Readers
{
    public abstract class FileReader<TResult>
    {
        private readonly Glob _fileGlob;
        
        private readonly ISystemIO _io;
        
        protected abstract TResult ReadFromFileInfo(FileInfo fileInfo);
        
        protected FileReader(ISystemIO io, string fileNameGlob)
        {
            _fileGlob = Glob.Parse(fileNameGlob);
            _io = io ?? throw new ArgumentNullException(nameof(io));
        }

        public TResult ReadFromFile(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException($"Path cannot be null, empty or contain only whitespaces ({path})", nameof(path));
            }
            
            if (!_io.File.Exists(path))
            {
                throw new FileNotFoundException("Could not find file.", path);
            }
            
            var fileInfo = new FileInfo(path);

            if (!_fileGlob.IsMatch(fileInfo.Name))
            {
                throw new ArgumentException($"File exists but file name does match expected glob pattern \"{_fileGlob}\" (fileName=\"{fileInfo.Name}\", path=\"{path}\")", nameof(path));
            }

            return ReadFromFileInfo(fileInfo);
        }
    }
}