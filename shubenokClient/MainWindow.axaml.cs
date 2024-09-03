using Avalonia.Controls;
using Avalonia.Interactivity;
using Metsys.Bson;
using shubenokClient.Context;
using shubenokClient.Models;
using System.Collections.Generic;
using System.Linq;

namespace shubenokClient
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            loadServices();
           // InitializeWindow();
        }
        public void loadServices()
        {
            List<Client> clients;

            switch (filterCmb.SelectedIndex)
            {
                case 1: 
                    clients = Helper.User724Context.Clients.Where(x => x.IdGender == 1).ToList(); 
                    break;
                case 2:
                    clients  =Helper.User724Context.Clients.Where(x => x.IdGender == 2).ToList();
                    break;
                case 3:
                    clients = Helper.User724Context.Clients.ToList();
                    break;
                case 0:
                default:
                    clients = Helper.User724Context.Clients.ToList();
                    break;
            }




/*
            clientsListBox.Items = clients.
                Select(x => new
                { 
                    x.IdClient,
                    x.NameClient,
                    x.SurnameCl,
                    x.OtchestvoCl,
                    x.Phone,
                    x.Mail,
                    x.DateReg,
                    x.Birthday,
                    x.IdGender
                
                
                
                })*/
        }
        private void updateListBox()
        {
           

        }
        public void AddClient_OnClick(object? sender, RoutedEventArgs args)
        {
            
        }

        private void EditClientBtn_OnClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {

        }
        private void DeleteClientBtn_OnClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {

        }

        

    }
}