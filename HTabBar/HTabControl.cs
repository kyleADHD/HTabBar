using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace HTabBar
{
    public delegate void OnTabClickEventHandler(object sender, OnTabClickedEventArgs args);
    public class HTabControl : Frame
    {
        public static readonly BindableProperty SelectedIndexProperty = BindableProperty.Create(nameof(SelectedIndex), typeof(int), typeof(HTabControl), 0, BindingMode.TwoWay, propertyChanged: IndexChanged);
        public static BindableProperty TabbedPagesProperty = BindableProperty.Create(nameof(HTabPages), typeof(ObservableCollection<HTabPage>), typeof(HTabControl), null, BindingMode.TwoWay);
        public static readonly BindableProperty IsPanEnabledProperty = BindableProperty.Create(nameof(IsPanEnabled), typeof(bool), typeof(HTabControl), true);
        public static readonly BindableProperty HeaderFontFamilyProperty = BindableProperty.Create(nameof(HeaderFontFamily), typeof(string), typeof(HTabControl), default(string));
        public static readonly BindableProperty HeaderFontSizeProperty = BindableProperty.Create(nameof(HeaderFontSize), typeof(double), typeof(HTabControl), default(double));
        public static readonly BindableProperty HeaderTextColorProperty = BindableProperty.Create(nameof(HeaderTextColor), typeof(Color), typeof(HTabControl), default(Color));
        public static readonly BindableProperty TabBarHeightProperty = BindableProperty.Create(nameof(TabBarHeight), typeof(double), typeof(HTabControl), default(double));
        public bool IsPanEnabled
        {
            get { return (bool)GetValue(IsPanEnabledProperty); }
            set { SetValue(IsPanEnabledProperty, value); }
        }
        public string HeaderFontFamily
        {
            get => (string)GetValue(HeaderFontFamilyProperty);
            set => SetValue(HeaderFontFamilyProperty, value);
        }
        public double HeaderFontSize
        {
            get => (double)GetValue(HeaderFontSizeProperty);
            set => SetValue(HeaderFontSizeProperty, value);
        }
        public Color HeaderTextColor
        {
            get => (Color)GetValue(HeaderTextColorProperty);
            set => SetValue(HeaderTextColorProperty, value);
        }
        public double TabBarHeight
        {
            get => (double)GetValue(TabBarHeightProperty);
            set => SetValue(TabBarHeightProperty, value);
        }
        internal Grid m_Parent;
        internal HTabPage SelectedPage;
        private double _SwipeStartX = 0, _SwipeStartY = 0;
        private Grid m_Header;
        private Color m_headerColor;
        private Grid m_Selection;
        internal Grid HTabBody { get; set; }
        public Font HeaderFont { get; set; }
        public string FontFamily { get; set; }
        public Color HeaderColor
        {
            get => m_headerColor;
            set
            {
                m_headerColor = value;
                m_Header.BackgroundColor = value;
                m_Selection.BackgroundColor = value;
            }
        }
        public int HeaderHeight { get; set; } = 38;
        public Position Position { get; set; }
        public int SelectedIndex { get => (int)GetValue(SelectedIndexProperty); set => SetValue(SelectedIndexProperty, value); }
        public Color SelectionColor { get; set; } = Color.FromRgb(102, 153, 255);
        public int SelectorHeight { get; set; } = 2;
        public ObservableCollection<HTabPage> HTabPages
        {
            get => (ObservableCollection<HTabPage>)GetValue(TabbedPagesProperty); set => SetValue(TabbedPagesProperty, value);
        }
        public HTabControl()
        {
            Padding = 0;
            Margin = 0;
            init();
            HTabPages = new ObservableCollection<HTabPage>();
            HTabPages.CollectionChanged += HTabPages_CollectionChanged;
        }
        public event OnTabClickEventHandler TabClicked;
        private void addTabPageContent(HTabPage tabPage)
        {
            try
            {
                tabPage.HTabParent = this;
                tabPage.Header.HParentTabPage = tabPage;
                tabPage.Header.BackgroundColor = HeaderColor;
                tabPage.Header.Selector = new BoxView
                {
                    BackgroundColor = HeaderColor
                };
                if (tabPage.Header.IsVisible)
                {
                    m_Header.Children.Add(tabPage.Header, m_Header.Children.Count, 0);
                    m_Selection.Children.Add(tabPage.Header.Selector, m_Selection.Children.Count, 0);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding tab page content: {ex.Message}");
            }
        }
        private static void IndexChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is HTabControl control))
            {
                Console.WriteLine("Invalid bindable object type in IndexChanged method.");
                return;
            }

            if (!(newValue is int index))
            {
                Console.WriteLine("Invalid new value type in IndexChanged method.");
                return;
            }

            control.SelectTabPage(index);
        }
        private void init()
        {
            m_Parent = new Grid { RowSpacing = 0, ColumnSpacing = 0 };
            HTabBody = new Grid { RowSpacing = 0, ColumnSpacing = 0 };
            m_Header = new Grid { RowSpacing = 0, ColumnSpacing = 0 };
            m_Selection = new Grid { RowSpacing = 0, ColumnSpacing = 0 };
        }
        private void panGesture_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            try
            {
                if (!IsPanEnabled)
                    return;

                switch (e.StatusType)
                {
                    case GestureStatus.Running:
                        _SwipeStartX = e.TotalX;
                        _SwipeStartY = e.TotalY;
                        break;
                    case GestureStatus.Completed:
                        var xdiff = _SwipeStartX;
                        var ydiff = _SwipeStartY;

                        if (Math.Abs(xdiff) > Math.Abs(ydiff))
                        {
                            if (xdiff < 0 && (SelectedIndex + 1) < m_Header.Children.Count)
                            {
                                SelectedIndex = SelectedIndex + 1;
                            }
                            else if (xdiff > 0 && (SelectedIndex - 1) > -1)
                            {
                                SelectedIndex = SelectedIndex - 1;
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in panGesture_PanUpdated: {ex.Message}");
            }
        }
        private void TabLayout()
        {
            if (Position == Position.Top)
            {
                m_Parent.RowDefinitions = new RowDefinitionCollection
        {
            new RowDefinition { Height = new GridLength(HeaderHeight, GridUnitType.Absolute) },
            new RowDefinition { Height = new GridLength(8, GridUnitType.Absolute) },
            new RowDefinition { Height = GridLength.Star}
        };
                m_Parent.Children.Add(m_Header, 0, 0);
                m_Parent.Children.Add(m_Selection, 0, 1);
                m_Parent.Children.Add(HTabBody, 0, 2);
            }
            else
            {
                m_Parent.RowDefinitions = new RowDefinitionCollection
        {
            new RowDefinition { Height = GridLength.Star},
            new RowDefinition { Height = new GridLength(8, GridUnitType.Absolute)},
            new RowDefinition { Height = new GridLength(HeaderHeight, GridUnitType.Absolute) }
        };
                m_Parent.Children.Add(HTabBody, 0, 0);
                m_Parent.Children.Add(m_Selection, 0, 1);
                m_Parent.Children.Add(m_Header, 0, 2);
            }

            m_Parent.BackgroundColor = BackgroundColor;
            if (HeaderFontFamily != null)
            {
                foreach (var page in HTabPages)
                {
                    if (page.Header.HeaderLabel != null)
                    {
                        page.Header.HeaderLabel.FontFamily = HeaderFontFamily;
                    }
                }
            }
            if (HeaderFontSize > 0)
            {
                foreach (var page in HTabPages)
                {
                    if (page.Header.HeaderLabel != null)
                    {
                        page.Header.HeaderLabel.FontSize = HeaderFontSize;
                    }
                }
            }
            SetHeaderColor();
            m_Selection.BackgroundColor = SelectionColor;
            if (TabBarHeight > 0)
            {
                m_Selection.HeightRequest = TabBarHeight;
            }
        }
        public void AddTab(HTabPage tabPage)
        {
            HTabPages.Add(tabPage);
            m_Header.Children.Add(tabPage.Header, HTabPages.Count - 1, 0);
            m_Selection.Children.Add(tabPage.Header.Selector, HTabPages.Count - 1, 0);
            tabPage.Header.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    var index = HTabPages.IndexOf(tabPage);
                    SelectTabPage(index);
                })
            });
            addTabPageContent(tabPage);
        }
        public void RemoveTab(HTabPage tabPage)
        {
            int index = HTabPages.IndexOf(tabPage);
            HTabPages.Remove(tabPage);
            m_Header.Children.RemoveAt(index);
            m_Selection.Children.RemoveAt(index);
            tabPage.Header.GestureRecognizers.Clear();
            if (SelectedPage == tabPage)
            {
                SelectedIndex = 0;
                SelectTabPage(0);
            }
            for (int i = index; i < HTabPages.Count; i++)
            {
                m_Header.Children[i].SetValue(Grid.ColumnProperty, i);
                m_Selection.Children[i].SetValue(Grid.ColumnProperty, i);
            }
        }
        private void HTabPages_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            try
            {
                switch (e.Action)
                {
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                        var page = HTabPages[e.NewStartingIndex];
                        addTabPageContent(page);
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in HTabPages_CollectionChanged: {ex.Message}");
            }
        }
        public void AddPage(HTabPage tabPage)
        {
            try
            {
                HTabPages.Add(tabPage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding a tab page: {ex.Message}");
                throw;
            }
        }
        public void RemovePage(HTabPage tabPage)
        {
            if (HTabPages.Contains(tabPage))
            {
                var index = HTabPages.IndexOf(tabPage);
                HTabPages.Remove(tabPage);
                m_Header.Children.RemoveAt(index);
                m_Selection.Children.RemoveAt(index);
                TabLayout();
                SelectTabPage(SelectedIndex);
            }
        }
        public void SelectTabPage(int index)
        {
            try
            {
                if (index > -1 && m_Header.Children.Count > index)
                {
                    var page = (m_Header.Children[index] as HTabHeader)?.HParentTabPage;
                    if (page != null)
                    {
                        SelectTabPage(page);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while selecting a tab page: {ex.Message}");
                throw;
            }
        }
        public void SelectTabPage(HTabPage page)
        {
            try
            {
                if (SelectedPage != null)
                {
                    SelectedPage.Header.Selector.BackgroundColor = HeaderColor;
                    SelectedPage.Header.Opacity = 0.8;
                }
                page.Header.Selector.BackgroundColor = SelectionColor;
                page.Header.Opacity = 1;
                SelectedPage = page;
                HTabBody.Children.Clear();
                HTabBody.Children.Add(page.Content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while selecting a tab page: {ex.Message}");
                throw;
            }
        }
        protected override void OnParentSet()
        {
            try
            {
                var panGesture = new PanGestureRecognizer();
                HTabBody.GestureRecognizers.Add(panGesture);
                panGesture.PanUpdated += panGesture_PanUpdated;
                TabLayout();
                Content = m_Parent;
                SelectTabPage(SelectedIndex);
                SetHeaderColor();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while setting the parent of the control: {ex.Message}");
                throw;
            }
        }
        internal void SetHeaderColor()
        {
            try
            {
                foreach (var page in HTabPages)
                {
                    if (page.Header.HeaderLabel != null)
                    {
                        page.Header.HeaderLabel.TextColor = HeaderTextColor;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while setting the header color: {ex.Message}");
                throw;
            }
        }
        internal void OnTabClicked(OnTabClickedEventArgs e)
        {
            try
            {
                TabClicked?.Invoke(this, e);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while handling the tab click event: {ex.Message}");
                throw;
            }
        }
    }
}