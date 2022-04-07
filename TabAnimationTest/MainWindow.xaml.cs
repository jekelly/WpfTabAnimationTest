using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
                var oh = (Visual)o.Template.FindName("selectionHint", o);
                var nh = (Visual)n.Template.FindName("selectionHint", n);

                //var t = oh.TransformToVisual(nh);
                var d = t.Transform(new Point(0, 0));

                var anim = new DoubleAnimation();
                Storyboard.SetTarget(anim, nh);
                Storyboard.SetTargetProperty(anim, new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.X)"));
                anim.Duration = TimeSpan.FromMilliseconds(100);
                anim.From = d.X;
                anim.To = 0;

                var yanim = new DoubleAnimation();
                Storyboard.SetTarget(yanim, nh);
                Storyboard.SetTargetProperty(yanim, new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.Y)"));
                yanim.Duration = TimeSpan.FromMilliseconds(100);
                yanim.From = d.Y+2;
                yanim.To = 0;

                Storyboard s = new Storyboard();
                s.Children.Add(yanim);
                s.Children.Add(anim);
                s.Begin();
            }
        }
    }
}
