using VisualStudio.Files.Abstractions;

namespace VisualStudio.Files.Core
{
    public class Property : IProperty
    {
        private readonly string _name;
        private readonly string _value;
        
        internal Property(string name, string value)
        {
            _name = name;
            _value = value;
        }

        public string Name => _name;
        public string Value => _value;
    }
}