using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Porvanyi_tuz
{
    public partial class MainPage : ContentPage
    {
        private int countPlayers = 0;
        private List<StackLayout> plSt = new List<StackLayout>();
        private List<Entry> plEn = new List<Entry>();
        private List<Button> plBu = new List<Button>();

        public MainPage()
        {
            InitializeComponent();

            AddPlayer();
            AddPlayer();
        }

        private void OpenOnButton(object sender, EventArgs e)
        {
            Button button = sender as Button;

            string[] names = new string[plEn.Count];

            for (int i = 0; i < names.Length; i++)
            {
                names[i] = plEn[i].Text;
            }

            Navigation.PushModalAsync(new WorkSpace(countPlayers, names));
        }

        private void AddPlayer(object sender, EventArgs e)
        {
            AddPlayer();
        }

        private void AddPlayer()
        {
            if (countPlayers == 8) return;
            else countPlayers++;

            StackLayout player = new StackLayout();
            player.HorizontalOptions = LayoutOptions.Center;
            player.Orientation = StackOrientation.Horizontal;
            player.Margin = new Thickness(10, 0, 10, 0);

            Entry playerEntry = new Entry();
            playerEntry.Text = string.Empty;
            playerEntry.WidthRequest = 225;

            Button playerButton = new Button();
            playerButton.Text = "✖";
            playerButton.Clicked += new EventHandler(RemoveName);
            playerButton.FontSize = 15;
            playerButton.TextColor = Color.White;
            playerButton.BackgroundColor = Color.FromHex("#DB1818");
            playerButton.BorderWidth = 1;
            playerButton.BorderColor = Color.FromHex("#E04B41");
            playerButton.CornerRadius = 10;

            player.Children.Add(playerEntry);
            player.Children.Add(playerButton);

            plSt.Add(player);
            plEn.Add(playerEntry);
            plBu.Add(playerButton);

            gridPlayers.Children.Add(player, 0, gridPlayers.Children.Count);
        }

        private void RemoveName(object sender, EventArgs e)
        {
            if (countPlayers == 2) return;
            else countPlayers--;

            Button currentButton = (Button)sender;

            bool isDeleted = false;

            for (int i = 0; i < plBu.Count; i++)
            {

                if (currentButton == plBu[i])
                {
                    gridPlayers.Children.Remove(plSt[i]);

                    plSt.RemoveAt(i);
                    plEn.RemoveAt(i);
                    plBu.RemoveAt(i);

                    /*if (i == plBu.Count - 1) break;

                    isDeleted = true;*/
                }

                /*if (isDeleted)
                {
                    gridPlayers.Children.Remove(plSt[i]);
                    gridPlayers.Children.Add(plSt[i], 0, i);
                }*/
            }

            gridPlayers.Children.Clear();
            for (int i = 0; i < plBu.Count; i++)
            {
                gridPlayers.Children.Add(plSt[i], 0, i);
            }
        }
    }
}
