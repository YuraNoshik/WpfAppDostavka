using System;
using System.Collections.Generic;
using System.IO;
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

namespace WpfAppVetclinic
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class Auth : Window
    {
        public Auth()
        {
            InitializeComponent();
            
        }
        
        private void buttonReCaptcha_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            using (DostavkaEntities usersEntities = new DostavkaEntities())
            {
                var customer = usersEntities.Stuffs.Where(x => x.stuffLogin == textBoxLogin.Text && x.stuffPassword == textBoxPassword.Text).First();
                if (customer == null)
                {
                    MessageBox.Show("Пользователь не найден");
                }


                var roles = usersEntities.Roles.Where(x => x.RoleID == customer.stuffRole).First();
                if (roles == null)
                {
                    MessageBox.Show("Роли не существует");
                }

                MessageBox.Show($"Дарова, {customer.stuffName} ты {roles.RoleName}");


                
                switch(roles.RoleID)
                {
                    case 1:
                        new AdminForm().Show();
                        break;
                    case 2:
                        new Katalog().Show();
                        break;
                    case 3:
                        new ManagerForm().Show();
                        break;
                }
                
            }
            
            //Captcha.CreateCaptcha(Captcha.LetterOption.Alphanumeric, 6);
            //var answer = Captcha.CaptchaText;
            //return answer;
        }
        
    }
}

