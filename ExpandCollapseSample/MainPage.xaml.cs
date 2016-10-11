using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ExpandCollapseSample
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        List<ExpandPanel> panels;
        public MainPage()
        {
            this.InitializeComponent();
        }
        

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            panels = new List<ExpandPanel>();
            weekAstrology.Text = "weekAstrology's Text";
            monthlyAstrology.Text = "monthlyAstrology's Text";
            yearlyAstrology.Text = "yearlyAstrology's Text";
            panels.Add(weekExpandPanel);
            panels.Add(monthExpandPanel);
            panels.Add(yearExpandPanel);
            base.OnNavigatedTo(e);
        }

        private void btnClick_Click(object sender, RoutedEventArgs e)
        {
            weekExpandPanel.IsExpanded = false;
        }

        private void ExpandStateChangedHandler(object sender, EventArgs e)
        {
            var currentPanel = (ExpandPanel)sender;
            if (currentPanel.IsExpanded == false)
            {
                return;
            }
            foreach(var panel in panels)
            {
                if (panel.Name != currentPanel.Name)
                {
                    panel.IsExpanded = false;
                }
            }
        }
    }
}
