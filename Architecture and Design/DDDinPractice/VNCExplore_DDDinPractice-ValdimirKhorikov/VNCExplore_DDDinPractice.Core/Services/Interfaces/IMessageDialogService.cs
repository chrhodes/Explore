namespace VNCExplore_DDDinPractice.Core.Services
{
    public interface IMessageDialogService
    {
        void ShowInfoDialog(string text);
        MessageDialogResult ShowOkCancelDialog(string text, string title);
    }
}
