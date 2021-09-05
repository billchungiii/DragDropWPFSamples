using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DragDropWPFSample004
{
    public class DragAndDropHelper
    {
        public static readonly DependencyProperty DropDataFormatProperty
            = DependencyProperty.RegisterAttached("DropDataFormat", typeof(string), typeof(DragAndDropHelper), new FrameworkPropertyMetadata("Text"));

        public static void SetDropDataFormat(UIElement element, string value)
        {
            element.SetValue(DropDataFormatProperty, value);
        }

        public static string GetDropDataFormat(UIElement element)
        {
            return (string)element.GetValue(DropDataFormatProperty);
        }


        public static readonly DependencyProperty DragSourceProperty
            = DependencyProperty.RegisterAttached("DragSource", typeof(DependencyProperty), typeof(DragAndDropHelper), new FrameworkPropertyMetadata(null));

        public static void SetDragSource(UIElement element, DependencyProperty value)
        {
            element.SetValue(DragSourceProperty, value);
        }

        public static DependencyProperty GetDragSource(UIElement element)
        {
            return (DependencyProperty)element.GetValue(DragSourceProperty);
        }


        public static readonly DependencyProperty DropTargetProperty
           = DependencyProperty.RegisterAttached("DropTarget", typeof(DependencyProperty), typeof(DragAndDropHelper), new FrameworkPropertyMetadata(null));

        public static void SetDropTarget(UIElement element, DependencyProperty value)
        {
            element.SetValue(DropTargetProperty, value);
        }

        public static DependencyProperty GetDropTarget(UIElement element)
        {
            return (DependencyProperty)element.GetValue(DropTargetProperty);
        }

        public static readonly DependencyProperty DragAndDropEnabledProperty
            = DependencyProperty.RegisterAttached("DragAndDropEnabled", typeof(bool), typeof(DragAndDropHelper), new FrameworkPropertyMetadata(OnDragAndDropEnableChanged));

        public static void SetDragAndDropEnabled(UIElement element, bool value)
        {
            element.SetValue(DragAndDropEnabledProperty, value);
        }

        public static bool GetDragAndDropEnabled(UIElement element)
        {
            return (bool)element.GetValue(DragAndDropEnabledProperty);
        }

        private static void OnDragAndDropEnableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = (UIElement)d;
            var newValue = (bool)e.NewValue;
            var oldValue = (bool)e.OldValue;
            if (newValue != oldValue)
            {
                if (newValue)
                {
                    Enable(element);
                }
                else
                {
                    Disable(element);
                }
            }
        }

        private static void Enable(UIElement element)
        {
            element.AllowDrop = true;
            element.PreviewMouseDown += Element_PreviewMouseDown;
            element.Drop += Element_Drop;
        }

        private static void Disable(UIElement element)
        {
            element.AllowDrop = false;
            element.PreviewMouseDown -= Element_PreviewMouseDown;
            element.Drop -= Element_Drop;
        }

        private static void Element_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.Source is UIElement element)
            {
                var property = DragAndDropHelper.GetDragSource(element);
                if (property == null) return;
                var data = element.GetValue(property);
                DragDrop.DoDragDrop(element, data, DragDropEffects.Copy);
            }
        }

        private static void Element_Drop(object sender, DragEventArgs e)
        {
            if (e.Source is UIElement element)
            {
                var property = DragAndDropHelper.GetDropTarget(element);
                if (property == null) return;
                var format = GetDropDataFormat(element);
                if (format == null) return;
                if (e.Data.GetDataPresent(format))
                {
                    element.SetValue(property, e.Data.GetData(format));
                }
            };
        }
    }
}
