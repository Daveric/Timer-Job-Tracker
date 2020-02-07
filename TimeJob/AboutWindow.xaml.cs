﻿using System.Windows.Navigation;

namespace TimeJobRecord
{
  public partial class AboutWindow
  {
    public AboutWindow()
    {
      InitializeComponent();
      DataContext = ViewModelToUse.Vm;
    }

    private void Hyperlink_OnRequestNavigate(object sender, RequestNavigateEventArgs e)
    {
      System.Diagnostics.Process.Start(e.Uri.ToString());
    }
  }
}
