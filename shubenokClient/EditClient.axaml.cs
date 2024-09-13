using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace shubenokClient;

public partial class EditClient : Window
{

    public Bitmap bitmapToBind;
    public string resultPhoto;
    public string filename;
    public string path;
    public int index;
    public string destPath;
    public bool ok;

    public int num;
    public EditClient()
    {
        InitializeComponent();
        maskTwo.IsVisible = true;
        num = 2;
        maskThree.IsVisible = false;
        maskFour.IsVisible = false;
    }
    public EditClient(int ind)
    {
       
        index = ind;
        nameEdit.Text = Helper.User724Context.Clients.Where(x => x.IdClient == index).FirstOrDefault().NameClient;
        surnameEdit.Text = Helper.User724Context.Clients.Where(x => x.IdClient == index).FirstOrDefault().SurnameCl;
        lastnameEdit.Text = Helper.User724Context.Clients.Where(x => x.IdClient == index).FirstOrDefault().OtchestvoCl;
        mailEdit.Text = Helper.User724Context.Clients.Where(x => x.IdClient == index).FirstOrDefault().Mail;
        string phone = Helper.User724Context.Clients.Where(x => x.IdClient == index).FirstOrDefault().Phone;

        switch (phone.Length)
        {
            case 14:
                maskTwo.IsVisible = true;
                maskTwo.Text = phone;
                maskThree.IsVisible= false;
                maskFour.IsVisible= false;
                break;
            case 15:
                maskTwo.IsVisible = false;
                maskThree.IsVisible= true;
                maskThree.Text = phone;
                maskFour.IsVisible= false;
                break;
            case 16:
                maskTwo.IsVisible = false;
                maskThree.IsVisible= false;
                maskFour.IsVisible = true;
                maskFour.Text = phone;
                break;
        }

     //   List<string> tags = new List<string>();
        //  tags = Helper.User724Context.Clients.Where(x => x.IdClient == index).Select(x => x.IdTags).All().ToList(); 
      var  tags = Helper.User724Context.Clients.Where(x => x.IdClient == ind).Include(x => x.IdTags);

      //  if (tags.Contains(1))

    }

        public async void ClientImg_Click(object? sender, RoutedEventArgs args)
    {
        OpenFileDialog _openFileDialog = new OpenFileDialog();
        var result = await _openFileDialog.ShowAsync(this);
        if (result == null) return;
        path = string.Join("", result);
        resultPhoto = result.ToString();
        if (result != null)
        {
            bitmapToBind = new Bitmap(path);
        }
        imageClientEdit.Source = bitmapToBind;
        filename = Path.GetFileName(path);
        destPath = AppDomain.CurrentDomain.BaseDirectory.ToString() + "Assets";
        string destPathFile = Path.Combine(destPath, filename);

    }
    public void OkBtn_Click(object? sender, RoutedEventArgs args)
    {
      //  Helper.User724Context.Clients.Find(x => x.)
    }


}