using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using shubenokClient.Models;
using System;
using System.Linq;

namespace shubenokClient;

public partial class AddClient : Window
{
    public Bitmap bitmapToBind;
    public string resultPhoto;
    public AddClient()
    {
        InitializeComponent();
    }

    public async void ClientImg_Click(object? sender, RoutedEventArgs args)
    {
        OpenFileDialog _openFileDialog = new OpenFileDialog();
        var result = await _openFileDialog.ShowAsync(this);
        if (result == null) return;
        string path = string.Join("", result);
        resultPhoto = result.ToString();
        if (result != null)
        {
            bitmapToBind = new Bitmap(path);
        }
        imageClientAdd.Source = bitmapToBind;

    }
    public void OkBtn_Click(object? sender, RoutedEventArgs args)
    { 
        Client client = new Client();
        client.NameClient = name.Text;
        client.SurnameCl = surname.Text;
        client.OtchestvoCl = otchestvo.Text;
        client.IdClient = Helper.User724Context.Clients.Count() + 1;
        client.Phone = phone.Text;
        client.Birthday = DateOnly.FromDateTime(Convert.ToDateTime(birthdateDP.SelectedDate));
        client.Mail = mail.Text;
        client.DateReg = DateOnly.FromDateTime(Convert.ToDateTime(DateTime.Now));
        client.Photo = resultPhoto;
        switch (denied.IsChecked)
        { 
        case false:
                break;
                case true:
                ClientTag clientTag = new ClientTag();
                clientTag.IdTag = 2;
                clientTag.IdClient = client.IdClient;
              //  Helper.User724Context.Model.ClientTag = clientTag;
               break;
        
        
        }
        Helper.User724Context.Clients.Add(client);
        Helper.User724Context.SaveChanges();
        
    }
}