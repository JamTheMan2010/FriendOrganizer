namespace FriendOrganizer.UI.View.Services
{
    public interface IMessageDialogService
    {
        MessageDialogResult ShouwOkCancelDialog(string text, string title);
    }
}