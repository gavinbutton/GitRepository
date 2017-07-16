using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace WpfLayoutTest.Behavior
{
    public class RichTextBoxBehavior : DependencyObject
    {
        public static readonly DependencyProperty TextProperty =
          DependencyProperty.RegisterAttached("Text", typeof(string),
          typeof(RichTextBoxBehavior), new UIPropertyMetadata(null, OnValueChanged));

        public static string GetText(RichTextBox o) { return (string)o.GetValue(TextProperty); }

        public static void SetText(RichTextBox o, string value) { o.SetValue(TextProperty, value); }

        private static void OnValueChanged(DependencyObject dependencyObject,
          DependencyPropertyChangedEventArgs e)
        {
            var richTextBox = (RichTextBox)dependencyObject;
            var text = (e.NewValue ?? string.Empty).ToString();
            LoadRTF(text, richTextBox);
        }

        private static void LoadRTF(string rtf, RichTextBox richTextBox)
        {

            if (string.IsNullOrEmpty(rtf))
            {
                throw new ArgumentNullException();
            }

            TextRange textRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);

            //Create a MemoryStream of the Rtf content

            using (MemoryStream rtfMemoryStream = new MemoryStream())
            {
                using (StreamWriter rtfStreamWriter = new StreamWriter(rtfMemoryStream))
                {
                    rtfStreamWriter.Write(rtf);
                    rtfStreamWriter.Flush();
                    rtfMemoryStream.Seek(0, SeekOrigin.Begin);

                    //Load the MemoryStream into TextRange ranging from start to end of RichTextBox.
                    textRange.Load(rtfMemoryStream, DataFormats.Rtf);
                }
            }

            HyperlinksSubscriptions(richTextBox.Document);
        }

        private static void HyperlinksSubscriptions(FlowDocument flowDocument)
        {
            if (flowDocument == null) return;
            GetVisualChildren(flowDocument).OfType<Hyperlink>().ToList()
                     .ForEach(i => i.RequestNavigate += HyperlinkNavigate);
        }

        private static IEnumerable<DependencyObject> GetVisualChildren(DependencyObject root)
        {
            foreach (var child in LogicalTreeHelper.GetChildren(root).OfType<DependencyObject>())
            {
                yield return child;
                foreach (var descendants in GetVisualChildren(child)) yield return descendants;
            }
        }

        private static void HyperlinkNavigate(object sender,
         System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }
}
