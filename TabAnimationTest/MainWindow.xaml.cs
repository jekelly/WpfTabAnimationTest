using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace TabAnimationTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabItem o = e.RemovedItems.Cast<TabItem>().FirstOrDefault();
            TabItem n = e.AddedItems.Cast<TabItem>().FirstOrDefault();

            if (o is object && n is object)
            {
                var t = o.TransformToVisual(n);
                var d = t.Transform(new Point(0, 0));

                var nh = (Visual)n.Template.FindName("selectionHint", n);

                var xAnim = new DoubleAnimation();
                Storyboard.SetTarget(xAnim, nh);
                Storyboard.SetTargetProperty(xAnim, new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.X)"));
                xAnim.Duration = TimeSpan.FromMilliseconds(100);
                xAnim.From = d.X;
                xAnim.To = 0;

                var yanim = new DoubleAnimation();
                Storyboard.SetTarget(yanim, nh);
                Storyboard.SetTargetProperty(yanim, new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.Y)"));
                yanim.Duration = TimeSpan.FromMilliseconds(100);
                yanim.From = d.Y+2;
                yanim.To = 0;

                Storyboard s = new Storyboard();
                s.Children.Add(yanim);
                s.Children.Add(xAnim);
                s.Begin();
            }
        }
    }
}
