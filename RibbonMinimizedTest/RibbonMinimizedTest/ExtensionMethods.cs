using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Xml;

namespace RibbonMinimizedTest
{
    public static class ExtensionMethods
    {
        public static bool TryCast<T>(this object from, out T to) where T : class
        {
            to = from as T;
            return to != null;
        }

        //public static T XamlClone<T>(this T original)
        //  where T : class
        //{
        //    if (original == null)
        //        return null;

        //    object clone;
        //    using (var stream = new MemoryStream())
        //    {
        //        var writer = XmlWriter.Create(stream, new XmlWriterSettings()
        //        {
        //            Indent = true,
        //            ConformanceLevel = ConformanceLevel.Fragment,
        //            OmitXmlDeclaration = true,
        //            NamespaceHandling = NamespaceHandling.OmitDuplicates,
        //        });

        //        var xamlManager = new XamlDesignerSerializationManager(writer)
        //        {
        //            XamlWriterMode = XamlWriterMode.Expression
        //        };

        //        XamlWriter.Save(original, xamlManager);
        //        stream.Seek(0, SeekOrigin.Begin);

        //        StreamReader reader = new StreamReader(stream, Encoding.UTF8);
        //        var s= reader.ReadToEnd();

        //        stream.Seek(0, SeekOrigin.Begin);
        //        clone = XamlReader.Load(stream);
        //    }

        //    if (clone is T)
        //        return (T)clone;
        //    else
        //        return null;
        //}
    }
}
