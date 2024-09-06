using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Metsys.Bson;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using shubenokClient.Context;
using shubenokClient.Models;
using System;
using System.Collections.Generic;
using System.IO;
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


            clients = Helper.User724Context.Clients.Include(x => x.ClientTags).ToList();
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
                    clients = clients;
                    break;
            }

            switch (sortCmb.SelectedIndex)
            {
                case 0:
                    clients = clients.OrderBy(a => a.SurnameCl).ToList();
                    break; 
                case 1:
                    visits = Helper.User724Context.Visits.ToList();
                    List<string> lastvisits = new List<string>();
                    clients.Select(x => new
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
                        NumbOfVisits = Helper.User724Context.Visits.Where(n => n.IdClient == x.IdClient).Select(n => n.TimedateVisit).Count().ToString(),
                        LastVisit = TimeSet(x.IdClient),
                        

                    }) ;
                    foreach (Client c in clients)
                    {
                        string lastVisit = TimeSet(c.IdClient);
                        lastvisits.Add(lastVisit);
                    }
                    string temp;
                 /*   for (int i = 0; i < lastvisits.Count-1; i++)
                    {
                        for (int j = i+1; j < lastvisits.Count; j++)
                        {
                            DateTime dt1 = DateTime.Parse(lastvisits[i]);
                            DateTime dt2 = D
                            if (dt1 < )
                            { 
                                temp = lastvisits[i];
                                lastvisits[i] = lastvisits[j];
                                lastvisits[j] = temp
                            
                            }

                        }
                    
                    }


                    clients = clients.OrderBy(x=> x.Las))*/
                    break;
                             

                case 2: //количество посещений от б к м
                    break;
                default:
                    break;
            }

            string searchText = searchString.Text ?? "";
            int count = searchText.Split(' ').Length;
            string[] values = new string[count];

            values = searchText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string s in values)
            {
                if (!string.IsNullOrEmpty(s))
                {
                    clients = clients.Where(x => x.NameClient.Contains(s)
                    || x.SurnameCl.Contains(s) || x.OtchestvoCl.Contains(s)
                    || x.Mail.Contains(s) || x.Phone.Contains(s)).ToList();
                }
                else
                {
                    continue;
                }
            }
            int newindex = 0;
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

                currentoutput.Text = Convert.ToString((currPg-1) * 10 + clientsOnPage.Count);
                alloutput.Text = clients.Count.ToString();
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

                currentoutput.Text = Convert.ToString((currPg - 1) * 50 + clientsOnPage.Count);
                alloutput.Text = clients.Count.ToString();


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

                currentoutput.Text = Convert.ToString((currPg - 1) * 200 + clientsOnPage.Count);
                alloutput.Text = clients.Count.ToString();


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

                currentoutput.Text = clients.Count.ToString();
                alloutput.Text = clients.Count.ToString();


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
                       x.Birthday,
                       x.IdGenderNavigation.NameGender,
                       x.DateReg,
                       PhotoPath = new Bitmap($"Assets/{x.Photo}"), 
                       NumbOfVisits = Helper.User724Context.Visits.Where(n => n.IdClient == x.IdClient).Select(n => n.TimedateVisit).Count().ToString(),
                       LastVisit = TimeSet(x.IdClient),
                      // x.ClientTags
                   });
        }

        public string TimeSet(long id)
        {
            var a = Helper.User724Context.Visits.Where(x => x.IdClient == id).ToList();
            if (a.Count > 0)
            {
                return a.OrderBy(x => x.TimedateVisit).Select(n => n.TimedateVisit).LastOrDefault().ToString();
            }
            else
            {
                return "Ќе посещал";
            }
        }
       /* public List <string> TagsSet(long id)
        {
            List<> tagsCl = new List<string>();
            var a = Helper.User724Context.Clients.Join(Helper.User724Context.ClientTags, x => x.IdClient, xt => xt.IdClient,
                (x, xt) => new { x, xt }).Join(Helper.User724Context.Tags, xtt => xtt.xt.IdTag, t => t.IdTag,
                (xxt, t) => new { xxt, t }).Select
                (m => new
                {

                    idClient = m.xxt.x.IdClient,
                    idTag = m.t.IdTag,

                }).ToList();
                

            
            
        }*/

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

        private void search_KeyUp(object? sender, Avalonia.Input.KeyEventArgs e)
        {
            loadServices();


        }

        private void search_KeyDown(object? sender, Avalonia.Input.KeyEventArgs e)
        {
            loadServices();

        }
    }
}