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
using System.Windows.Shapes;
using WpfApp1aaa;

namespace ML_DPZM3P_WPF
{ 
    public partial class StartWindow : Window
    {
        MainWindow mainWindow;
        public StartWindow()
        { 
            mainWindow = new MainWindow();
            InitializeComponent();
        }
              
        private static StartWindow instance;

        public static StartWindow Instance
        {
            get
            {
                if (instance == null)
                    instance = new StartWindow();
                return instance;
            }

        }

        private void btn_start_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            mainWindow.Show();
            MessageBox.Show("A \"Következő kérdés\" gomb megnyomásával megjelenik az első kérdés!");

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
    }
}
