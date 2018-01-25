using System;
using System.Linq;
using System.Xml.Linq;
using VisualStudio.Files.Abstractions.RunSettings;

namespace VisualStudio.Files.Core.RunSettings
{
    public class NUnitSection : INUnitSection
    {
        private const string SectionRootName = "NUnit";
        private const string DefaultTimeoutName = "DefaultTimeout";
        
        public bool Exists { get; }
        public int? DefaultTimeOut { get; }

        internal NUnitSection(XElement root)
        {
            var nunitElement = root.Descendants().SingleOrDefault(x => x.Name.LocalName.Equals(SectionRootName));

            Exists = nunitElement != null;

            if (!Exists) return;
            
            var defaultTimeOutElement = nunitElement.Descendants()
                .SingleOrDefault(x => x.Name.LocalName.Equals(DefaultTimeoutName));

            if (defaultTimeOutElement != null)
            {
                DefaultTimeOut = Int32.Parse(defaultTimeOutElement.Value);
            }
            
        }
    }
}