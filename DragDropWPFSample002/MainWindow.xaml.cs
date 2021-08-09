using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DragDropWPFSample002
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
public partial class MainWindow : Window
{
    private TextBlock _shadow;
    public MainWindow()
    {
        InitializeComponent();
    }       

    private void source_MouseDown(object sender, MouseButtonEventArgs e)
    {
        CreateShadowTextBlock();
        DragDrop.DoDragDrop(source, source.Text, DragDropEffects.Copy);
    }

    private void CreateShadowTextBlock()
    {
        _shadow = new TextBlock { Text = source.Text };
        _shadow.RenderTransform = new TranslateTransform();
        _shadow.FontSize = source.FontSize;
        _shadow.Foreground = new SolidColorBrush(Colors.Gray);
        canvas.Children.Add(_shadow);
        Canvas.SetTop(_shadow, Canvas.GetTop(source));
        Canvas.SetLeft(_shadow, Canvas.GetLeft(source));
    }

    private void canvas_DragOver(object sender, DragEventArgs e)
    {            
        var position = e.GetPosition(canvas);
        var transform = (TranslateTransform)_shadow.RenderTransform;
        transform.X = position.X;
        transform.Y = position.Y;
    }

    private void canvas_Drop(object sender, DragEventArgs e)
    {
        _shadow.Foreground = source.Foreground;
    }
}
}
