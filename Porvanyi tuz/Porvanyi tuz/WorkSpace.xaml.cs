using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;
using Xamarin.Forms.Xaml;
using static System.Net.Mime.MediaTypeNames;

namespace Porvanyi_tuz
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkSpace : ContentPage
    {
        List<Entry> entryInput = new List<Entry>();
        List<Entry> entryOutput = new List<Entry>();

        List<Entry> newResults = new List<Entry>();

        int[] cardFor2 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 18, 18, 17, 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };
        int[] cardFor3 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 12, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };
        int[] cardFor4 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 9, 9, 8, 7, 6, 5, 4, 3, 2, 1 };
        int[] cardFor5 = { 1, 2, 3, 4, 5, 6, 7, 7, 7, 6, 5, 4, 3, 2, 1 };
        int[] cardFor6 = { 1, 2, 3, 4, 5, 6, 6, 6, 5, 4, 3, 2, 1 };
        int[] cardFor7 = { 1, 2, 3, 4, 5, 5, 5, 4, 3, 2, 1 };
        int[] cardFor8 = { 1, 2, 3, 4, 4, 4, 3, 2, 1 };

        int[] cardArray;

        private int numOfPlayers;
        private string[] names;

        List<int[]> listResult = new List<int[]>();
        private int numOfGame;
        private int[] results;

        public WorkSpace(int numOfPlayers, string[] names)
        {
            this.numOfPlayers = numOfPlayers;
            this.names = names;

            if (numOfPlayers < 2 || numOfPlayers > 8)
                Navigation.PushModalAsync(new MainPage());

            InitializeComponent();

            bOpenTable.IsVisible = false;
            bSaveTable.IsVisible = false;
            gridTable.IsVisible = false;
            lineTable.IsVisible = false;
            labelTable.IsVisible = false;

            CreatePlayers();
        }

        private void CreatePlayers()
        {
            results = new int[numOfPlayers];
            for (int i = 0; i < numOfPlayers; i++)
            {
                if (i % 2 == 1)
                {
                    Xamarin.Forms.Shapes.Rectangle rectangle = new Xamarin.Forms.Shapes.Rectangle();
                    rectangle.Background = Brush.LightBlue;
                    Grid.SetRow(rectangle, i);
                    Grid.SetColumnSpan(rectangle, 3);
                    gridPlayers.Children.Add(rectangle);
                }

                Label label = new Label();
                label.Text = names[i];
                label.TextColor = Color.Black;
                label.FontSize = 17;
                label.VerticalOptions = LayoutOptions.Center;
                label.HorizontalOptions = LayoutOptions.Center;
                gridPlayers.Children.Add(label, 0, i);

                Entry entryIn = new Entry();
                entryIn.Text = string.Empty;
                entryIn.Keyboard = Keyboard.Numeric;
                entryIn.HorizontalTextAlignment = TextAlignment.Center;
                entryIn.WidthRequest = 100;
                entryIn.HorizontalOptions = LayoutOptions.Center;
                gridPlayers.Children.Add(entryIn, 1, i);
                entryInput.Add(entryIn);

                Entry entryOut = new Entry();
                entryOut.Text = string.Empty;
                entryOut.Keyboard = Keyboard.Numeric;
                entryOut.HorizontalTextAlignment = TextAlignment.Center;
                entryOut.WidthRequest = 100;
                entryOut.HorizontalOptions = LayoutOptions.Center;
                gridPlayers.Children.Add(entryOut, 2, i);
                entryOutput.Add(entryOut);
            }

            firstLine.X1 = 0.05 * Xamarin.Forms.Application.Current.MainPage.Width;
            firstLine.X2 = 0.95 * Xamarin.Forms.Application.Current.MainPage.Width;
            firstLine.Y1 = 0;
            firstLine.Y2 = 0;

            lineResult.X1 = 0.15 * Xamarin.Forms.Application.Current.MainPage.Width;
            lineResult.X2 = 0.85 * Xamarin.Forms.Application.Current.MainPage.Width;
            lineResult.Y1 = 0;
            lineResult.Y2 = 0;

            lineTable.X1 = 0.05 * Xamarin.Forms.Application.Current.MainPage.Width;
            lineTable.X2 = 0.95 * Xamarin.Forms.Application.Current.MainPage.Width;
            lineTable.Y1 = 0;
            lineTable.Y2 = 0;

            labelNumGame.Text = "1";
            labelNumCard.Text = "1";

            if (numOfPlayers == 2)
            {
                cardArray = cardFor2;
            }
            else if (numOfPlayers == 3)
            {
                cardArray = cardFor3;
            }
            else if (numOfPlayers == 4)
            {
                cardArray = cardFor4;
            }
            else if (numOfPlayers == 5)
            {
                cardArray = cardFor5;
            }
            else if (numOfPlayers == 6)
            {
                cardArray = cardFor6;
            }
            else if (numOfPlayers == 7)
            {
                cardArray = cardFor7;
            }
            else if (numOfPlayers == 8)
            {
                cardArray = cardFor8;
            }

            numOfGame = 1;
        }

        private void AddRecord(object sender, EventArgs e)
        {
            int[] valueIn = new int[numOfPlayers];
            int[] valueOut = new int[numOfPlayers];

            int resultIn = 0;
            int resultOut = 0;

            for (int i = 0; i < numOfPlayers; i++)
            {
                try
                {
                    valueIn[i] = int.Parse(entryInput[i].Text);
                }
                catch
                {
                    valueIn[i] = 0;
                }

                if (valueIn[i] > cardArray[numOfGame - 1])
                {
                    DisplayAlert("Помилка!", "Неправильна кількість \"Замовлено\"", "ОК");
                    return;
                }

                try
                {
                    valueOut[i] = int.Parse(entryOutput[i].Text);
                }
                catch
                {
                    valueOut[i] = 0;
                }
            }

            foreach (int i in valueIn)
                resultIn += i;

            foreach (int i in valueOut)
                resultOut += i;

            if (resultIn == cardArray[numOfGame - 1])
            {
                DisplayAlert("Помилка!", "Неправильна кількість \"Замовлено\"\n", "ОК");
                return;
            }

            if (resultOut != cardArray[numOfGame - 1])
            {
                DisplayAlert("Помилка!", "Неправильна кількість \"Взято\"\n", "ОК");
                return;
            }

            int[] thisGameResult = new int[numOfPlayers];

            for (int i = 0; i < numOfPlayers; i++)
            {
                if (valueIn[i] == valueOut[i])
                {
                    if (valueOut[i] == 0)
                        thisGameResult[i] = 5;
                    else
                        thisGameResult[i] = valueIn[i] * 10;
                }
                else if (valueIn[i] < valueOut[i])
                {
                    thisGameResult[i] = valueOut[i];
                }
                else
                {
                    thisGameResult[i] = -(valueIn[i] - valueOut[i]) * 10;
                }
                results[i] += thisGameResult[i];
            }

            listResult.Add(thisGameResult);

            Sort();
            

            numOfGame++;

            labelResult.IsVisible = true;
            lineResult.IsVisible = true;
            bOpenTable.IsVisible = true;
            labelNumGame.Text = numOfGame.ToString();
            labelNumCard.Text = cardArray[numOfGame - 1].ToString();

            CleanEntrys();
        }

        private void Sort()
        {
            int[] temporaryResults = new int[numOfPlayers];
            for (int i = 0; i < numOfPlayers; i++)
            {
                temporaryResults[i] = results[i];
            }

            int[] sortedIndex = new int[numOfPlayers];
            for (int i = 0; i < numOfPlayers; i++)
            {
                int maxIndex = 0;
                for (int j = 0; j < numOfPlayers; j++)
                {
                    if (temporaryResults[j] > temporaryResults[maxIndex])
                    {
                        maxIndex = j;
                    }
                }
                sortedIndex[i] = maxIndex;
                temporaryResults[maxIndex] = int.MinValue;
            }

            gridResult.Children.Clear();

            int position = 1;
            for (int i = 0; i < numOfPlayers; i++)
            {
                if (i != 0)
                {
                    if (results[sortedIndex[i]] != results[sortedIndex[i - 1]])
                        position++;
                }

                Label labelPosition = new Label();
                labelPosition.Text = names[i];
                labelPosition.TextColor = Color.Black;
                labelPosition.FontSize = 17;
                labelPosition.VerticalOptions = LayoutOptions.Center;
                labelPosition.HorizontalOptions = LayoutOptions.Center;
                labelPosition.Margin = new Thickness(30, 0, 0, 0);
                labelPosition.Text = position.ToString() + ")";

                Label labelName = new Label();
                labelName.Text = names[i];
                labelName.TextColor = Color.Black;
                labelName.FontSize = 17;
                labelName.VerticalOptions = LayoutOptions.Center;
                labelName.HorizontalOptions = LayoutOptions.Center;
                labelName.Margin = new Thickness(0, 0, 0, 0);
                labelName.Text = names[sortedIndex[i]];

                Label labelValue = new Label();
                labelValue.Text = names[i];
                labelValue.TextColor = Color.Black;
                labelValue.FontSize = 17;
                labelValue.VerticalOptions = LayoutOptions.Center;
                labelValue.HorizontalOptions = LayoutOptions.Center;
                labelValue.Margin = new Thickness(0, 0, 30, 0);
                labelValue.Text = results[sortedIndex[i]].ToString();

                gridResult.Children.Add(labelPosition, 0, i);
                gridResult.Children.Add(labelName, 1, i);
                gridResult.Children.Add(labelValue, 2, i);
            }
        }

        private void CleanEntrys()
        {
            foreach (var i in entryInput)
                i.Text = string.Empty;

            foreach (var i in entryOutput)
                i.Text = string.Empty;
        }

        private void OpenTable(object sender, EventArgs e)
        {
            bOpenTable.IsVisible = false;
            bSaveTable.IsVisible = true;
            gridTable.IsVisible = true;
            lineTable.IsVisible = true;
            labelTable.IsVisible = true;

            gridTable.Children.Clear();
            newResults.Clear();

            for (int i = 0; i < numOfPlayers; i++)
            {
                Label label = new Label();
                label.Text = names[i];
                label.TextColor = Color.Black;
                label.FontSize = 17;
                label.HorizontalOptions = LayoutOptions.Center;

                Entry entry = new Entry();
                entry.Text = results[i].ToString();
                entry.Keyboard = Keyboard.Numeric;
                entry.WidthRequest = (int)(0.9f * Xamarin.Forms.Application.Current.MainPage.Width / (numOfPlayers + 1));
                entry.HorizontalTextAlignment = TextAlignment.Center;
                entry.HorizontalOptions = LayoutOptions.Center;
                newResults.Add(entry);

                gridTable.Children.Add(label, 0, i);
                gridTable.Children.Add(entry, 1, i);

            }

            scrollView.ForceLayout();
        }

        private void SaveTable(object sender, EventArgs e)
        {
            bOpenTable.IsVisible = true;
            bSaveTable.IsVisible = false;
            gridTable.IsVisible = false;
            lineTable.IsVisible = false;
            labelTable.IsVisible= false;

            for(int i = 0; i < numOfPlayers; i++)
            {
                results[i] = int.Parse(newResults[i].Text);
            }

            Sort();
        }
    }
}