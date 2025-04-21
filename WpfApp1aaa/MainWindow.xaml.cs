using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Diagnostics;
using ML_DPZM3P_WPF;

namespace WpfApp1aaa
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            beolvas();
            voltKerdesekSorszama.Clear();
            for (int h = 0; h < kerdesek.Count; h++)
            {
                megadottValaszok.Add(string.Empty);
            }
            InitializeComponent();
        }

        private static MainWindow instance;

        public static MainWindow Instance
        {
            get
            {
                if (instance == null)
                    instance = new MainWindow();
                return instance;
            }
        }

        // Változók:
        #region
        private Random rnd = new Random();
        private StreamReader sr = new StreamReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "resources", "forras.txt"));
        private string valasz;
        private int i;
        private int counter = 0;
        private int questionCounter = 0;
        private bool kovGombMegnyomva = false;
        private bool formatumJo;
        #endregion

        // Listák:
        #region 
        private List<string> kerdesek = new List<string>();
        private List<string> a_lehetosegek = new List<string>();
        private List<string> b_lehetosegek = new List<string>();
        private List<string> c_lehetosegek = new List<string>();
        private List<string> helyesValaszok = new List<string>();
        private List<string> megadottValaszok = new List<string>();
        private List<int> voltKerdesekSorszama = new List<int>();
        #endregion 

        private void btn_eredmeny_Click(object sender, RoutedEventArgs e)
        {
            eredmenySzamolasa();
        }

        private void beolvas()
        {
            while (!sr.EndOfStream)
            {
                string sor = sr.ReadLine();
                string[] adatok = sor.Split(';');
                kerdesek.Add(adatok[0]);
                a_lehetosegek.Add(adatok[1]);
                b_lehetosegek.Add(adatok[2]);
                c_lehetosegek.Add(adatok[3]);
                helyesValaszok.Add(adatok[4]);
            }
            sr.Close();
        }

        private void eredmenySzamolasa()
        {
            for (int h = 0; h < megadottValaszok.Count; h++)
            {
                if (megadottValaszok[h] == helyesValaszok[h])
                    counter++;
            }
            MessageBox.Show("A jó válaszok száma: " + counter);
            counter = 0;
        }

        private void btn_koviKerdes_Click(object sender, RoutedEventArgs e)
        {
            if (questionCounter < 10)
            {
                i = rnd.Next(0, kerdesek.Count);

                if (voltKerdesekSorszama.Contains(i))
                    btn_koviKerdes_Click(sender, e);
                else
                {
                    voltKerdesekSorszama.Add(i);

                    lb_kerdes.Content = kerdesek[i];
                    a_lehetoseg.Content = a_lehetosegek[i];
                    b_lehetoseg.Content = b_lehetosegek[i];
                    c_lehetoseg.Content = c_lehetosegek[i];

                    kovGombMegnyomva = true;
                    questionCounter++;
                    lb_kerdesMutato.Content = questionCounter + "/10";
                }
            }
        }

        private void btn_valaszMentese_Click(object sender, RoutedEventArgs e)
        {
            if (kovGombMegnyomva)
            {
                valasz = tb_valasz.Text.ToUpper();
                if (valasz != "A" && valasz != "B" && valasz != "C")
                {
                    formatumJo = false;
                    tb_valasz.Clear();
                    MessageBox.Show("Nem megengedett formátum, csak a betűjelek vihetők be! Próbálja újra!");
                }
                else
                {
                    megadottValaszok[i] = valasz;
                    kovGombMegnyomva = false;
                    formatumJo = true;
                    tb_valasz.Clear();
                    MessageBox.Show("A válasz mentése megtörtént!");
                }
            }

            else if (!kovGombMegnyomva && questionCounter != 10)
            {
                tb_valasz.Clear();
                MessageBox.Show("Lépjen tovább a következő kérdésre!");
            }

            if (questionCounter == 10 && formatumJo)
            {
                MessageBox.Show("Nem maradt több kérdés.");
                MessageBox.Show("Kezdjen új kvízt vagy lépjen ki a programból.");
                eredmenySzamolasa();
            }

        }

        private void btn_kilepes_Click(object sender, RoutedEventArgs e)
        {
            eredmenySzamolasa();
            MessageBox.Show("Az alkalmazás bezárul.");
            Application.Current.Shutdown();
        }

        private void btn_info_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Üdvüzlöm! Ez egy történelem támájú kvíz program.");
            MessageBox.Show("50 kérdésből fog 10-et kapni véletlenszerűen. A bal felső sarokban látható, hogy mennyi kérdés maradt még.");
            MessageBox.Show("Három válaszlehetőség tartozik egy-egy kérdéshez, melyből csak egy lesz helyes!");
            MessageBox.Show("Elég csupán a válasz betűjelét begépelni, ami lehet kis- vagy nagybetű is!");
            MessageBox.Show("FONTOS: csak az a/A, b/B vagy c/C betűket írja be, különben, a válasz helytelen lesz és mindig mentsen!");
            MessageBox.Show("A \"Kilépés\" gombbal tudja leállítani a programot.");
            MessageBox.Show("Az \"Újraindítás\" gombbal tudja újraindítani a programot.");
        }

        private void btn_Ujra_Click(object sender, RoutedEventArgs e)
        {
            string appName = Process.GetCurrentProcess().MainModule.FileName;
            Process.Start(appName);
            Application.Current.Shutdown();
        }
    }
}
