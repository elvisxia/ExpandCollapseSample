using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// The Templated Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234235

namespace ExpandCollapseSample
{
    public sealed class ExpandPanel : ContentControl
    {
        public ExpandPanel()
        {
            this.DefaultStyleKey = typeof(ExpandPanel);
        }

        private bool _useTransitions = true;
        private VisualState _collapsedState;
        public Windows.UI.Xaml.Controls.Primitives.ToggleButton toggleExpander;
        private FrameworkElement contentElement;

        public event EventHandler ExpandStateChanged;


        public static readonly DependencyProperty HeaderContentProperty =
        DependencyProperty.Register("HeaderContent", typeof(object),
        typeof(ExpandPanel), null);

        public static readonly DependencyProperty IsExpandedProperty =
        DependencyProperty.Register("IsExpanded", typeof(bool),
        typeof(ExpandPanel), new PropertyMetadata(true, IsExpanded_Changed));

        public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register("CornerRadius", typeof(CornerRadius),
        typeof(ExpandPanel), null);

        public object HeaderContent
        {
            get { return GetValue(HeaderContentProperty); }
            set { SetValue(HeaderContentProperty, value); }
        }

        public bool IsExpanded
        {
            get { return (bool)GetValue(IsExpandedProperty); }
            set
            {
                SetValue(IsExpandedProperty, value);
                if (ExpandStateChanged != null)
                {
                    ExpandStateChanged(this,null);
                }
            }
        }

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        private static void IsExpanded_Changed(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            var panel = (ExpandPanel)sender;
            panel.changeVisualState(false);
        }

        public void changeVisualState(bool useTransitions)
        {
            if (IsExpanded)
            {
                if (contentElement != null)
                {
                    contentElement.Visibility = Visibility.Visible;
                }
                VisualStateManager.GoToState(this, "Expanded", useTransitions);
            }
            else
            {
                VisualStateManager.GoToState(this, "Collapsed", useTransitions);
                _collapsedState = (VisualState)GetTemplateChild("Collapsed");
                if (_collapsedState == null)
                {
                    if (contentElement != null)
                    {
                        contentElement.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            toggleExpander = (Windows.UI.Xaml.Controls.Primitives.ToggleButton)
                GetTemplateChild("ExpandCollapseButton");
            if (toggleExpander != null)
            {
                toggleExpander.Click += (object sender, RoutedEventArgs e) =>
                {
                    IsExpanded = !IsExpanded;
                    toggleExpander.IsChecked = IsExpanded;
                    changeVisualState(_useTransitions);
                };
            }
            contentElement = (FrameworkElement)GetTemplateChild("Content");
            if (contentElement != null)
            {
                _collapsedState = (VisualState)GetTemplateChild("Collapsed");
                if ((_collapsedState != null) && (_collapsedState.Storyboard != null))
                {
                    _collapsedState.Storyboard.Completed += (object sender, object e) =>
                    {
                        contentElement.Visibility = Visibility.Collapsed;
                    };
                }
            }
            changeVisualState(false);
        }
    }
}
