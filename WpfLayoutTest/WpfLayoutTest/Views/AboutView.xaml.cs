using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfLayoutTest.Views
{
    /// <summary>
    /// Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class AboutView : Window
    {
        public AboutView()
        {
            InitializeComponent();
            LoadRTF(Properties.Resources.TestLicense, licenseBox);
        }

        private void LoadRTF(string rtf, RichTextBox richTextBox)
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

        private void HyperlinksSubscriptions(FlowDocument flowDocument)
        {
            if (flowDocument == null) return;
            this.GetVisualChildren(flowDocument).OfType<Hyperlink>().ToList()
                     .ForEach(i => i.RequestNavigate += HyperlinkNavigate);
        }

        private IEnumerable<DependencyObject> GetVisualChildren(DependencyObject root)
        {
            foreach (var child in LogicalTreeHelper.GetChildren(root).OfType<DependencyObject>())
            {
                yield return child;
                foreach (var descendants in GetVisualChildren(child)) yield return descendants;
            }
        }

        private void HyperlinkNavigate(object sender,
         System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }
}
