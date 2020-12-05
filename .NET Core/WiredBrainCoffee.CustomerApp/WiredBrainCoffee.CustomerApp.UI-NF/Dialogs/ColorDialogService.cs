using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

namespace WiredBrainCoffee.CustomerApp.UI.Dialogs
{
  public interface IColorDialogService
  {
    bool ShowDialog(ref Color? wpfColor);
  }

  public class ColorDialogService : IColorDialogService
  {
    public bool ShowDialog(ref Color? wpfColor)
    {
      var wpfMainWindowHandle = new WindowInteropHelper(Application.Current.MainWindow).Handle;
      var win32Parent = new System.Windows.Forms.NativeWindow();
      win32Parent.AssignHandle(wpfMainWindowHandle);

      var colorDialog = new System.Windows.Forms.ColorDialog();
      if (wpfColor != null)
      {
        colorDialog.Color = System.Drawing.Color.FromArgb(
          wpfColor.Value.A, wpfColor.Value.R, wpfColor.Value.G, wpfColor.Value.B);
      }

      var dialogResult = colorDialog.ShowDialog(win32Parent);

      var result = false;
      if (dialogResult == System.Windows.Forms.DialogResult.OK)
      {
        var drawingColor = colorDialog.Color;
        wpfColor = Color.FromArgb(drawingColor.A, drawingColor.R, drawingColor.G, drawingColor.B);
        result = true;
      }

      return result;
    }
  }
}
