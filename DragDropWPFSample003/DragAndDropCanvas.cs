using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DragDropWPFSample003
{
    public class DragAndDropCanvas : Canvas
    {
        public static readonly DependencyProperty DropDataFormatProperty
            = DependencyProperty.RegisterAttached("DropDataFormat", typeof(string), typeof(DragAndDropCanvas), new FrameworkPropertyMetadata("Text"));

        public static void SetDropDataFormat(UIElement element, string value)
        {
            element.SetValue(DropDataFormatProperty, value);
        }

        public static string GetDropDataFormat(UIElement element)
        {
            return (string)element.GetValue(DropDataFormatProperty);
        }


        public static readonly DependencyProperty DragSourceProperty
            = DependencyProperty.RegisterAttached("DragSource", typeof(DependencyProperty), typeof(DragAndDropCanvas), new FrameworkPropertyMetadata(null));

        public static void SetDragSource(UIElement element, DependencyProperty value)
        {
            element.SetValue(DragSourceProperty, value);
        }

        public static DependencyProperty GetDragSource(UIElement element)
        {
            return (DependencyProperty)element.GetValue(DragSourceProperty);
        }


        public static readonly DependencyProperty DropTargetProperty
           = DependencyProperty.RegisterAttached("DropTarget", typeof(DependencyProperty), typeof(DragAndDropCanvas), new FrameworkPropertyMetadata(null));

        public static void SetDropTarget(UIElement element, DependencyProperty value)
        {
            element.SetValue(DropTargetProperty, value);
        }

        public static DependencyProperty GetDropTarget(UIElement element)
        {
            return (DependencyProperty)element.GetValue(DropTargetProperty);
        }

        public DragAndDropCanvas()
        {
            AllowDrop = true;
        }

        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            if (e.Source is UIElement element)
            {
                var property = DragAndDropCanvas.GetDragSource(element);
                if (property == null) return;
                var data = element.GetValue(property);
                DragDrop.DoDragDrop(element, data, DragDropEffects.Copy);
            }
        }

        protected override void OnDrop(DragEventArgs e)
        {
            if (e.Source is UIElement element)
            {
                var property = DragAndDropCanvas.GetDropTarget(element);
                if (property == null) return;
                var format = GetDropDataFormat(element);
                if (format == null) return;
                if (e.Data.GetDataPresent(format))
                {
                    element.SetValue(property, e.Data.GetData(format));
                }
            }
        }
    }
}
