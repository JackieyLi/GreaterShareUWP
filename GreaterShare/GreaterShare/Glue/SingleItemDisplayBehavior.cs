﻿using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace GreaterShare.Glue
{

    public class SingleItemDisplayBehavior : DependencyObject, IBehavior
    {

        public SingleItemDisplayBehavior()
        {

        }




        public DependencyObject AssociatedObject
        {
            get { return (DependencyObject)GetValue(AssociatedObjectProperty); }
            private set { SetValue(AssociatedObjectProperty, value); }
        }



        // Using a DependencyProperty as the backing store for AssociatedObject.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AssociatedObjectProperty =
            DependencyProperty.Register(nameof(AssociatedObject), typeof(DependencyObject), typeof(MinItemSizeBehavior), new PropertyMetadata(null));


        public ItemsWrapGrid AssociatedItemsWrapGrid
        {
            get
            {
                return AssociatedObject as ItemsWrapGrid;
            }
        }


        public void Attach(DependencyObject associatedObject)
        {
            AssociatedObject = associatedObject;
            if (AssociatedItemsWrapGrid != null)
            {
                AssociatedItemsWrapGrid.SizeChanged += AssociatedItemsWrapGrid_SizeChanged;

                var b = new Binding();
                b.Source = AssociatedItemsWrapGrid;
                b.Path = new PropertyPath(nameof(AssociatedItemsWrapGrid.Orientation));
                BindingOperations.SetBinding(this, GridOrientationProperty, b);
            }


        }



        public void Detach()
        {
            if (AssociatedItemsWrapGrid != null)
            {
                AssociatedItemsWrapGrid.SizeChanged -= AssociatedItemsWrapGrid_SizeChanged;
            }

        }



        private void ResizeItem()
        {
            if (AssociatedItemsWrapGrid != null)
            {
                switch (GridOrientation)
                {
                    case Orientation.Vertical:
                        {
                            double actualItemHeight = AssociatedItemsWrapGrid.ActualHeight ;
                            AssociatedItemsWrapGrid.ItemHeight = actualItemHeight - 8;
                            var ip = VisualTreeHelper.GetParent(AssociatedItemsWrapGrid);
							if (ip==null)
							{
								return;
							}
                            var sv = VisualTreeHelper.GetParent(ip) as FrameworkElement;
                            AssociatedItemsWrapGrid.ItemWidth = sv.ActualWidth-72;
                            break;
                        }
                    case Orientation.Horizontal:
                        {
                            double actualItemWidth = AssociatedItemsWrapGrid.ActualWidth ;
                            AssociatedItemsWrapGrid.ItemWidth = actualItemWidth - 8;
                            var ip = VisualTreeHelper.GetParent(AssociatedItemsWrapGrid);
							if (ip == null)
							{
								return;
							}
							var sv = VisualTreeHelper.GetParent(ip) as FrameworkElement;
                            AssociatedItemsWrapGrid.ItemHeight = sv.ActualHeight-72;

                  
                            break;
                        }
                    default:
                        break;
                }
            }
        }

        static IEnumerable<DependencyObject> FindVisualParent(DependencyObject source)
        {
            DependencyObject p=source;
            for (;;)
            {
                p = VisualTreeHelper.GetParent(p);
                if (p == null)
                {
                    yield break;
                }
                yield return p;
            }
        }

        private Orientation GridOrientation
        {
            get { return (Orientation)GetValue(GridOrientationProperty); }
            set { SetValue(GridOrientationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GridOrientation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GridOrientationProperty =
            DependencyProperty.Register(nameof(GridOrientation), typeof(Orientation), typeof(MinItemSizeBehavior), new PropertyMetadata(
                      Orientation.Horizontal,
                     (o, e) => (o as SingleItemDisplayBehavior)?.ResizeItem()
                ));



        private void AssociatedItemsWrapGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ResizeItem();
        }






        //public Double ItemMinWidth
        //{
        //	get { return (Double)GetValue(ItemMinWidthProperty); }
        //	set { SetValue(ItemMinWidthProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for ItemMinWidth.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty ItemMinWidthProperty =
        //	DependencyProperty.Register(nameof(ItemMinWidth), typeof(Double), typeof(MinItemSizeBehavior), new PropertyMetadata(50));




        //public double ItemMinHeight
        //{
        //	get { return (double)GetValue(ItemMinHeightProperty); }
        //	set { SetValue(ItemMinHeightProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for ItemMinHeight.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty ItemMinHeightProperty =
        //	DependencyProperty.Register(nameof(ItemMinHeight), typeof(double), typeof(MinItemSizeBehavior), new PropertyMetadata(50));










    }
}
