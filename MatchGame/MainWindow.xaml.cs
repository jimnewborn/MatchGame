using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
namespace MatchGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int tenthoOfSecondElapsed;
        int mathesFound;
        float timeRecord;
        float previosRecord;
        bool gamePlayed;
        public MainWindow()
        {

            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;
            Best_Score.Visibility = Visibility.Hidden;
            gamePlayed = false;
            SetUpGame();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tenthoOfSecondElapsed++;
            timeTextBlock.Text = (tenthoOfSecondElapsed / 10F).ToString("0.0s");

            if(mathesFound == 8)
            {
                timer.Stop();
                timeRecord = tenthoOfSecondElapsed / 10F;
                if (!gamePlayed)
                {
                    Best_Score.Text = "Best time is" + timeTextBlock.Text;
                    previosRecord = tenthoOfSecondElapsed / 10F;
                }
                else if(timeRecord < previosRecord)
                {
                    Best_Score.Text = "Best time is" + timeTextBlock.Text;
                }
                
                timeTextBlock.Text = timeTextBlock.Text + "- Play again?";
            }
        }

        private void SetUpGame()
        {
            mathesFound = 0; 
            List<string> animalEmoji = new List<string>()
            {
                "🦁","🦁",
                "🐿","🐿",
                "🦒","🦒",
                "🙊","🙊",
                "🦓","🦓",
                "🐏","🐏",
                "🐂","🐂",
                "🐕","🐕",
            };
            Random random = new Random();
            foreach(TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                if (textBlock.Name != "timeTextBlock" && textBlock.Name != "Best_Score")
                {
                    textBlock.Visibility = Visibility.Visible;
                  
                    int index = random.Next(animalEmoji.Count);
                    string nextEmoji = animalEmoji[index];
                    textBlock.Text = nextEmoji;
                    animalEmoji.RemoveAt(index);

                }
                
            }
            timer.Start();
            tenthoOfSecondElapsed = 0;
            mathesFound = 0;
        }

        TextBlock lastTextBlockClicked;
        bool findingMatch = false;
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            if (findingMatch == false)
            {
                textBlock.Visibility = Visibility.Hidden;
                lastTextBlockClicked = textBlock;
                findingMatch = true;

            }
            else if (textBlock.Text == lastTextBlockClicked.Text)
            {
                findingMatch = false;
                textBlock.Visibility = Visibility.Hidden;
                mathesFound++;

            }
            else
            {
                findingMatch = false;
                lastTextBlockClicked.Visibility = Visibility.Visible;
            }
        }

        private void timeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (mathesFound == 8)
            {
                gamePlayed = true;
                Best_Score.Visibility = Visibility.Visible;

                

                SetUpGame(); 
            }
            
        }
    }
}
