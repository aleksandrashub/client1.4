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
        public static int index;
        public static List<Client> clients;
        public static List<Visit> visits;
        public static List<Gender> genders;
        public List<Client> clientsOnPage = new List<Client>();
        public MainWindow()
        {
            InitializeComponent();
            loadServices();
           // InitializeWindow();
        }
        public void loadServices()
        {
            clientsOnPage.Clear();

            switch (filterCmb.SelectedIndex)
            {
                case 1: 
                    clients = Helper.User724Context.Clients.Where(x => x.IdGender == 1).ToList(); 
                    break;
                case 2:
                    clients  =Helper.User724Context.Clients.Where(x => x.IdGender == 2).ToList();
                    break;
                case 3:
                case 0:
                default:
                    clients = Helper.User724Context.Clients.ToList();
                    break;
            }

            switch (sortCmb.SelectedIndex)
            {
                case 1:
                    clients = clients.OrderByDescending(x => x.SurnameCl).ToList();
                    break; 
                case 2:

                    List<Visit> visits;
                    visits = Helper.User724Context.Visits.OrderByDescending(x => x.TimedateVisit).ToList();
                    List<int> clientsIds = new List<int>();
                  //  clientsIds = visits.Select(x => x.IdClient).ToList();  
                  /*  foreach (Client client in clients)
                    {
                       List <Visit> visitsFotOne;
                        visitsFotOne = Helper.User724Context.Visits.Where(x => x.IdClient == client.IdClient)
                            .OrderByDescending(x => x.TimedateVisit).ToList();
                        visits.Add(visitsFotOne[visitsFotOne.Count - 1]);

                       
                    }*/
                    



                   
                  
                  //  clients = Helper.User724Context.Clients.Where(x => 
                  //  x.Visits.OrderByDescending(x => x.TimedateVisit).Select(x => x.TimedateVisit).ToList());
                  //  clients = Helper.User724Context.Visits.Where(x => ) // дата последнего посещения от новых к старым
                  //clients.OrderByDescending(x => x.)
                    break;
                case 3:
                                    //количество посещений от б к м

                case 0:
                default:
                    clients = Helper.User724Context.Clients.ToList();
                    break;
            }

            string searchText = searchString.Text ?? "";
            if (!string.IsNullOrEmpty(searchText))
            { 
                clients = clients.Where(x => x.NameClient.Contains(searchText) 
                || x.SurnameCl.Contains(searchText) || x.OtchestvoCl.Contains(searchText)
                || x.Mail.Contains(searchText) || x.Phone.Contains(searchText)).ToList();
            }
            int newindex = 0;
            if (tenPages.IsChecked == true)
            {
                for (int i = index; i < index+10; i++)
                {
                    clientsOnPage.Add(clients[i]);
                    if (i == index)
                    {
                        newindex = index+10;
                    }
                }
            }
            else if (fiftyPages.IsChecked == true)
            {
                for (int i = index; i < index + 50; i++)
                {
                    clientsOnPage.Add(clients[i]);
                    if (i == index)
                    {
                        newindex = index+50;
                    }

                }
            }
            else if (twohundredPages.IsChecked == true)
            {
                for (int i = index; i < index + 200; i++)
                {
                    
                    while (i <= clients.Count - 1)
                    {

                        clientsOnPage.Add(clients[i]);
                        if (i == index)
                        {
                            newindex = index + 200;
                        }

                    }
                   if (i > clients.Count-1)
                        { 
                        break;
                        }
                    
                }
            }
            else if (allPages.IsChecked == true || (tenPages.IsChecked==false 
                && fiftyPages.IsChecked==false && twohundredPages.IsChecked==false 
                || allPages.IsChecked==false))
            {
                clientsOnPage = clients;
               
            }
            index = newindex;
            clientsListBox.ItemsSource = clientsOnPage.
                   Select(x => new
                   {
                       x.IdClient,
                       x.NameClient,
                       x.SurnameCl,
                       x.OtchestvoCl,
                       x.Mail,
                       x.Phone,
                       x.Birthday,
                       x.Photo,
                       x.IdGenderNavigation.NameGender,
                       x.DateReg,
                       LastVisit = Helper.User724Context.Visits.Where(x => x.IdClient == x.IdClient).Max(x => x.TimedateVisit).ToString(),

                   });


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



                            });*/
        }

        public void PrevPage_OnClick(object? sender, RoutedEventArgs args)
        {
            loadServices();
        }
        public void NextPage_OnClick(object? sender, RoutedEventArgs args)
        {
            loadServices();
        }

        private void updateListBox()
        {

            loadServices();
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

        private void tenPages_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            index = 0;
            loadServices();
        }

        private void fiftyPages_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            index = 0;
            loadServices();
        }

        private void twohundredPages_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            index = 0;
            loadServices();

        }

        private void allPages_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            index = 0;
            loadServices();

        }
    }
}