﻿using System.Windows;

namespace VNCExplore_DDDinPractice.Core.Services
{
    public class MessageDialogService : IMessageDialogService
    {
        public MessageDialogResult ShowOkCancelDialog(string text, string title)
        {
            var result = MessageBox.Show(text, title, MessageBoxButton.OKCancel);
            return result == MessageBoxResult.OK
              ? MessageDialogResult.OK
              : MessageDialogResult.Cancel;
        }

        public void ShowInfoDialog(string text)
        {
            MessageBox.Show(text, "Info");
        }
    }

    
    public enum MessageDialogResult
    {
        OK,
        Cancel
    }
}
