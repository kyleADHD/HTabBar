using System;
namespace HTabBar
{
    public enum Position
    {
        Top,
        Bottom
    }
    public class OnTabClickedEventArgs
    {
        public OnTabClickedEventArgs(HTabPage Item)
        {
            this.Item = Item;
        }
        public HTabPage Item { get; }
        public int SelectedIndex { get; set; }
    }
}