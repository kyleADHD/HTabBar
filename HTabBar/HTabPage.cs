using System;
using Xamarin.Forms;

namespace HTabBar
{
    public class HTabPage
    {
        internal ContentPage m_content;
        internal ContentPage m_contentPage;
        internal HTabControl HTabParent;
        private HTabHeader m_header;

        public HTabPage()
        {
            m_content = new ContentPage();
            Header = new HTabHeader();
        }
        public View Content
        {
            get => m_content.Content;
            set
            {
                try
                {
                    m_content.Content = value;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error setting content for HTabPage: {ex.Message}");
                }
            }
        }
        public ContentPage CustomContentPage
        {
            get => m_content;
            set => m_content = value;
        }
        public HTabHeader Header
        {
            get => m_header;
            set
            {
                m_header = value;
                InitHeaderEvent();
            }
        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                OnTabClickedEventArgs args = new OnTabClickedEventArgs(this)
                {
                    SelectedIndex = HTabParent.HTabPages.IndexOf(this)
                };
                HTabParent.OnTabClicked(args);
                HTabParent.SelectTabPage(this);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error selecting tab page: {ex.Message}");
            }
        }
        internal void InitHeaderEvent()
        {
            try
            {
                var tapGestureRecognizer = new TapGestureRecognizer();
                m_header.GestureRecognizers.Add(tapGestureRecognizer);
                tapGestureRecognizer.Tapped -= TapGestureRecognizer_Tapped;
                tapGestureRecognizer.Tapped += TapGestureRecognizer_Tapped;
            }
            catch (Exception ex)
            {
                // handle exception here
                Console.WriteLine($"Error initializing header event: {ex.Message}");
            }
        }
    }
}