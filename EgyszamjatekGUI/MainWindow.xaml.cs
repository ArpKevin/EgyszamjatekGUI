using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EgyszamjatekGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public class Versenyzo
    {
        public string Nev { get; set; }
        public List<int> Tippek { get; set; }

        public Versenyzo(string sor)
        {
            var x = sor.Split(" ");
            Nev = x[0];
            Tippek = new List<int>();
            for (int i = 1; i < x.Length; i++)
            {
                Tippek.Add(int.Parse(x[i]));
            }
        }

        public override string ToString()
        {
            return $"{Nev}, {string.Join(";", Tippek)}";
        }
    }
    public partial class MainWindow : Window
    {
        private List<Versenyzo> versenyzok;

        private string file = @"..\..\..\src\egyszamjatek2.txt";
        public MainWindow()
        {
            InitializeComponent();

            versenyzok = new List<Versenyzo>();

            foreach (var item in File.ReadAllLines(file))
            {
                versenyzok.Add(new(item));
            }
        }

        private void txtboxTippek_TextChanged(object sender, TextChangedEventArgs e)
        {
            lblDb.Content = $"{txtboxTippek.Text.ToString().Trim().Split().Count()} db";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string nev = txtboxNev.Text.Trim();
            string tippek = txtboxTippek.Text.Trim();
            if (!string.IsNullOrEmpty(nev) && !string.IsNullOrEmpty(tippek))
            {
                if (versenyzok.Any(v => v.Nev == nev))
                {
                    MessageBox.Show("Van már ilyen nevű játékos!", "Hiba!");
                }
                else
                {
                    if (tippek.Split(" ").Count() != 4)
                    {
                        MessageBox.Show("A tippek száma nem megfelelő!", "Hiba!");
                    }
                    else
                    {
                        using (StreamWriter sw = File.AppendText(file))
                        {
                            sw.WriteLine($"{nev} {string.Join(" ", tippek)}");
                        }

                        MessageBox.Show("Az állomány bővítése sikeres volt!", "Üzenet");

                        txtboxNev.Clear(); txtboxTippek.Clear();

                    }
                }
            }
        }
    }
}