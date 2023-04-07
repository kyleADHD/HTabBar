using System;
using Xamarin.Forms;

namespace HTabBar
{
    public class HTabHeader : Frame
    {
        internal BoxView Selector;
        internal HTabPage HParentTabPage;

        public HTabHeader()
        {
            CornerRadius = 0;
            Padding = 0;
            Margin = 0;
        }

        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(nameof(value), "Title cannot be null or empty.");
                }

                _title = value;
                HeaderLabel = new Label
                {
                    Text = _title
                };
                HeaderLabel.Opacity = 0.8;
                Content = HeaderLabel;
            }
        }

        internal Label HeaderLabel { get; set; }
    }
}
