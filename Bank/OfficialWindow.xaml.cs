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
using Bank.Objects;
using Bank.ORM;

namespace Bank
{
    /// <summary>
    /// Interaction logic for OfficialWindow.xaml
    /// </summary>
    public partial class OfficialWindow : Window
    {
        public OfficialWindow()
        {
            InitializeComponent();
            Official official = new Official();
            official.CompanyNumber = "123";
            official = UsersORM.GetOfficialById(official);

            //NameBox.Text = official.Name;
            //SurNameBox.Text = official.SurName;
            //PhoneBox.Text = official.Phone;
            //MailBox.Text = official.Mail;

            // bude tam ověření - jméno a přijmení bude string a budou tam jen písmena
            // email bude nějaká maska ... 
            // třída Validator ... metody, které vrátí true nebo false podle toho, co budu kontrolovat, např. že ve stringu jsou jen písmena a nic jiného
            // a pak (po kliknutí na tlačítko) se bude volat Validator s příslušnými metodami a když to bude všechno true, tak se provede update
            // každý box bude mít svůj validátor a bude se tam ukazovat nějaký label
        }
    }
}
