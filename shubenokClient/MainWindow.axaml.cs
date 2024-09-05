using Avalonia.Controls;
using Avalonia.Interactivity;
using Metsys.Bson;
using shubenokClient.Context;
using shubenokClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace shubenokClient
{
    public partial class MainWindow : Window
    {
        public bool nextBack = true;
        public int selectPg;
        public  int indexUp;
        public  int indexDown;
        public static List<Client> clients;
        public static List<Visit> visits;
        public static List<Gender> genders;
        public List<Client> clientsOnPage = new List<Client>();
        public MainWindow()
        {
            InitializeComponent();
            loadServices();
        }
        public void loadServices()
        {
            int currPg = Convert.ToInt32(currpageNumber.Text);
            int pgNum = Convert.ToInt32(allpageNumber.Text);
           
            
            switch (filterCmb.SelectedIndex)
            {
                case 0: 
                    clients = Helper.User724Context.Clients.Where(x => x.IdGender == 1).ToList(); 
                    break;
                case 1:
                    clients  =Helper.User724Context.Clients.Where(x => x.IdGender == 2).ToList();
                    break;
                case 2:
                default:
                    clients = Helper.User724Context.Clients.ToList();
                    break;
            }

            switch (sortCmb.SelectedIndex)
            {
                case 0:
                    clients = clients.OrderBy(a => a.SurnameCl).ToList();
                    break; 
                case 1:
                    List<Visit> visits;
                    List<int> numVisits = new List<int>();
                    foreach (Client client in clients)
                    {
                        visits = Helper.User724Context.Visits.Where(x => x.IdClient == client.IdClient).ToList();
                        numVisits.Add(visits.Count);

                    }
                    for (int i = 0; i < numVisits.Count; i++)
                    {
                        for (int j = 0; j < numVisits.Count - 1; j++)
                        {
                            if (numVisits[j] > numVisits[j+1])
                            {
                                Client temp = clients[j];
                                clients[j] = clients[j+1];
                                clients[j+1] = temp;
                            }
                        }
                    }


                  /*  foreach (int numvisit in numVisits)
                    visits = Helper.User724Context.Visits.OrderByDescending(x => x.TimedateVisit).ToList();
                    List<int> clientsIds = new List<int>();*/
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
                             

                case 2: //количество посещений от б к м
                    break;
                default:
                   // clients = Helper.User724Context.Clients.ToList();
                    break;
            }

            string searchText = searchString.Text ?? "";
            if (!string.IsNullOrEmpty(searchText))
            { 
                clients = clients.Where(x => x.NameClient.Contains(searchText) 
                || x.SurnameCl.Contains(searchText) || x.OtchestvoCl.Contains(searchText)
                || x.Mail.Contains(searchText) || x.Phone.Contains(searchText)).ToList();
            }
            int newindex = 0; ;
            int newindexD = 0;
            if (tenPages.IsChecked == true) //10 клиентов на странице
            {

                if (nextBack == true) //нажата кнопка вперед
                {
                    if (selectPg != 10)
                    {
                        indexUp = 0;
                        selectPg = 10;
                    }
                    else
                    {
                        selectPg = 10;
                        if (allpageNumber.Text != currpageNumber.Text)
                        {
                            indexUp = clients.IndexOf(clientsOnPage[0]) + 10;
                        }
                        else
                        {
                            indexUp = clients.IndexOf(clientsOnPage[0]);
                        }
                    }
                    clientsOnPage.Clear();
                    for (int i = indexUp; i < indexUp + 10; i++)
                    {
                        if (i > clients.Count - 1)
                        {
                            break;
                        }

                        clientsOnPage.Add(clients[i]);

                        if (i == indexUp)
                        {
                            newindex = indexUp + 10;
                        }
                    }
                    indexUp = newindex;
                }
                else //нажата кнопка назад
                {
                    if (selectPg != 10)
                    {
                        indexUp = 0;
                        selectPg = 10;

                    }
                    else
                    {
                        selectPg = 10;
                        if (allpageNumber.Text != currpageNumber.Text)
                        {
                            indexUp = clients.IndexOf(clientsOnPage[0]) + 10;
                        }
                        else
                        {
                            indexUp = clients.IndexOf(clientsOnPage[0]);
                        }
                    }
                    indexDown = clients.IndexOf(clientsOnPage[0]);
                    clientsOnPage.Clear();
                    //если элементов полное количество на стр (10)
                    for (int j = indexDown - 10; j <= indexDown - 1; j++)
                    {
                        if (j < 0)
                        {
                            break;

                        }

                        clientsOnPage.Add(clients[j]);
                        if (j == indexDown)
                        {
                            newindexD = indexDown + 10;
                        }

                    }
                    indexDown = newindexD;
                    nextBack = true;
                }

                pgNum = (int)Math.Ceiling((decimal)(clients.Count / 10));
                if (clients.Count % 10 >= 1)
                {
                    pgNum += 1;

                }
                if (pgNum == 1)
                {
                    nextBtn.IsVisible = false;
                    backBtn.IsVisible = false;

                }

                currPg = (int)Math.Ceiling((decimal)clients.IndexOf(clientsOnPage[0]) / 10) + 1;


                currpageNumber.Text = currPg.ToString();
                allpageNumber.Text = pgNum.ToString();

                if (currPg == pgNum && currPg == 1)
                {
                    nextBtn.IsVisible = false;
                    backBtn.IsVisible = false;
                }
                else if (currPg == pgNum && currPg != 1)
                {
                    nextBtn.IsVisible = false;
                    backBtn.IsVisible = true;

                }
                else if (currPg == 1 && currPg != pgNum)
                {
                    nextBtn.IsVisible = true;
                    backBtn.IsVisible = false;

                }
                else
                {
                    nextBtn.IsVisible = true;
                    backBtn.IsVisible = true;
                }


            }
            else if (fiftyPages.IsChecked == true) //50 клиентов на странице
            {

                if (nextBack == true) //выбрано перейти на след страницу
                {
                    if (selectPg != 50)
                    {
                        indexUp = 0;
                        selectPg = 50;
                    }
                    else
                    {
                        selectPg = 50;
                        if (allpageNumber.Text != currpageNumber.Text)
                        {
                            indexUp = clients.IndexOf(clientsOnPage[0]) + 50;
                        }
                        else
                        {
                            indexUp = clients.IndexOf(clientsOnPage[0]);
                        }

                    }

                    clientsOnPage.Clear();
                    for (int i = indexUp; i < indexUp + 50; i++)
                    {

                        if (i > clients.Count - 1)
                        {
                            break;

                        }

                        clientsOnPage.Add(clients[i]);

                        if (i == indexUp)
                        {
                            newindex = indexUp + 50;
                        }

                    }
                    indexUp = newindex;

                }
                else //выбрано перейти на предыд страницу
                {
                    indexDown = clients.IndexOf(clientsOnPage[0]);
                    clientsOnPage.Clear();
                    //если элементов полное количество на стр (50)
                    for (int j = indexDown - 50; j <= indexDown - 1; j++)
                    {
                        if (j < 0)
                        {
                            break;

                        }

                        clientsOnPage.Add(clients[j]);
                        if (j == indexDown)
                        {
                            newindexD = indexDown + 50;
                        }

                    }
                    indexDown = newindexD;
                    nextBack = true;
                }


                pgNum = (int)Math.Ceiling((decimal)(clients.Count / 50));
                if (clients.Count % 50 >= 1)
                {
                    pgNum += 1;

                }
                if (pgNum == 1)
                {
                    nextBtn.IsVisible = false;
                    backBtn.IsVisible = false;

                }

                currPg = (int)Math.Ceiling((decimal)clients.IndexOf(clientsOnPage[0]) / 50) + 1;

                currpageNumber.Text = currPg.ToString();
                allpageNumber.Text = pgNum.ToString();

                if (currPg == pgNum && currPg == 1)
                {
                    nextBtn.IsVisible = false;
                    backBtn.IsVisible = false;
                }
                else if (currPg == pgNum && currPg != 1)
                {
                    nextBtn.IsVisible = false;
                    backBtn.IsVisible = true;

                }
                else if (currPg == 1 && currPg != pgNum)
                {
                    nextBtn.IsVisible = true;
                    backBtn.IsVisible = false;

                }
                else
                {
                    nextBtn.IsVisible = true;
                    backBtn.IsVisible = true;
                }

            }
            else if (twohundredPages.IsChecked == true)
            {
                if (nextBack == true)
                {
                    if (selectPg != 200)
                    {
                        indexUp = 0;
                        selectPg = 200;
                    }
                    else
                    {
                        selectPg = 200;
                        if (allpageNumber.Text != currpageNumber.Text)
                        {
                            indexUp = clients.IndexOf(clientsOnPage[0]) + 200;
                        }
                        else
                        {
                            indexUp = clients.IndexOf(clientsOnPage[0]);
                        }

                    }
                    clientsOnPage.Clear();

                    for (int i = indexUp; i < indexUp + 200; i++)
                    {
                        if (i > clients.Count - 1)
                        {
                            break;
                        }

                        clientsOnPage.Add(clients[i]);

                        if (i == indexUp)
                        {
                            newindex = indexUp + 200;
                        }

                    }
                    indexUp = newindex;
                }
                else
                {
                    indexDown = clients.IndexOf(clientsOnPage[0]);
                    clientsOnPage.Clear();
                    //если элементов полное количество на стр (50)
                    for (int j = indexDown - 200; j <= indexDown - 1; j++)
                    {
                        if (j < 0)
                        {
                            break;

                        }

                        clientsOnPage.Add(clients[j]);
                        if (j == indexDown)
                        {
                            newindexD = indexDown + 200;
                        }

                    }
                    indexDown = newindexD;
                    nextBack = true;
                }


                pgNum = (int)Math.Ceiling((decimal)(clients.Count / 200));
                if (clients.Count % 200 >= 1)
                {
                    pgNum += 1;

                }
                if (pgNum == 1)
                {
                    nextBtn.IsVisible = false;
                    backBtn.IsVisible = false;

                }

                currPg = (int)Math.Ceiling((decimal)clients.IndexOf(clientsOnPage[0]) / 200) + 1;

                currpageNumber.Text = currPg.ToString();
                allpageNumber.Text = pgNum.ToString();

                if (currPg == pgNum && currPg == 1)
                {
                    nextBtn.IsVisible = false;
                    backBtn.IsVisible = false;
                }
                else if (currPg == pgNum && currPg != 1)
                {
                    nextBtn.IsVisible = false;
                    backBtn.IsVisible = true;

                }
                else if (currPg == 1 && currPg != pgNum)
                {
                    nextBtn.IsVisible = true;
                    backBtn.IsVisible = false;

                }
                else
                {
                    nextBtn.IsVisible = true;
                    backBtn.IsVisible = true;
                }

            }
            else if (allPages.IsChecked == true || (tenPages.IsChecked == false
                && fiftyPages.IsChecked == false && twohundredPages.IsChecked == false
                || allPages.IsChecked == false))
            {
                clientsOnPage = clients;
                currpageNumber.Text = "1";
                allpageNumber.Text = "1";
                nextBtn.IsVisible = false;
                backBtn.IsVisible = false;
            }
           
            clientsListBox.ItemsSource = clientsOnPage.
                   Select(x => new 
                   {
                       x.IdClient,
                       x.NameClient,
                       x.SurnameCl,
                       x.OtchestvoCl,
                       x.Mail,
                       x.Phone,
                       idcl = x.IdClient,
                       x.Birthday,
                       x.Photo,
                       x.IdGenderNavigation.NameGender,
                       x.DateReg,
                       NumbOfVisits = Helper.User724Context.Visits.Where(n=> n.IdClient == x.IdClient).Select(n => n.TimedateVisit).Count().ToString(),
                       LastVisit = TimeSet(x.IdClient)
                       //LastVisit = Helper.User724Context.Visits.Where(x =>x. x.TimedateVisit).Max(x => x.TimedateVisit).ToString(),

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

        public string TimeSet(long id)
        {
            var a = Helper.User724Context.Visits.Where(x => x.IdClient == id).ToList(); ;
            if (a.Count > 0)
            {
                return a.OrderBy(x => x.TimedateVisit).Select(n => n.TimedateVisit).LastOrDefault().ToString();
          
            }
            else
            {
                return "Не посещал";
            }
        }

        public void PrevPage_OnClick(object? sender, RoutedEventArgs args)
        {
            nextBack = false;
            loadServices();
        }
        public void NextPage_OnClick(object? sender, RoutedEventArgs args)
        {
            nextBack = true;
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
           // index = 0;
            loadServices();
        }

        private void fiftyPages_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
           // index = 0;
            loadServices();
        }

        private void twohundredPages_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
           // index = 0;
            loadServices();

        }

        private void allPages_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
           // index = 0;
            loadServices();

        }

        private void GenderCmb_SelectionChanged(object? sender, Avalonia.Controls.SelectionChangedEventArgs e)
        {
            loadServices();

        }

        private void Sorts_SelectionChanged(object? sender, Avalonia.Controls.SelectionChangedEventArgs e)
        {
            loadServices();
        }
    }
}