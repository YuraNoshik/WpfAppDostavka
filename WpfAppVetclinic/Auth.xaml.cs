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
using EasyCaptcha.Wpf;

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


        string capchaLow;
        string capchaUp;

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Captcha.CreateCaptcha(Captcha.LetterOption.Alphanumeric, 6);

            capchaLow = Captcha.CaptchaText;
            capchaUp = Captcha.CaptchaText;

        }
        private void buttonReCaptcha_Click(object sender, RoutedEventArgs e)
        {
            Captcha.CreateCaptcha(Captcha.LetterOption.Alphanumeric, 6);

            capchaLow = Captcha.CaptchaText;
            capchaUp = Captcha.CaptchaText;
        }

        private void buttonExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            using (DostavkaEntities usersEntities = new DostavkaEntities())
            {
                if (textBoxEnterCaptcha.Text == null || textBoxLogin.Text == null || textBoxPassword.Text == null)
                {

                    MessageBox.Show("Одно из полей пустое");

                }
                else
                {
                    try
                    {
                        var customer = usersEntities.Stuffs.Where(x => x.stuffLogin == textBoxLogin.Text && x.stuffPassword == textBoxPassword.Text).First();
                        var roles = usersEntities.Roles.Where(x => x.RoleID == customer.stuffRole).First();


                        if (customer == null || textBoxEnterCaptcha.Text.ToLowerInvariant() != capchaLow.ToLower() 
                            || textBoxEnterCaptcha.Text.ToUpperInvariant() != capchaUp || textBoxLogin.Text != customer.stuffLogin || textBoxPassword.Text != customer.stuffPassword)
                        {
                            MessageBox.Show("Пользователь не найден");
                        }
                        else
                        {
                            MessageBox.Show($"Здравствуйте, {customer.stuffName}, вы вошли в систему, как {roles.RoleName}");

                            switch (roles.RoleID)
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

                    }
                    catch(InvalidOperationException)
                    {
                        MessageBox.Show("Неверно введены данные");
                    }


                }
            }
            textBoxLogin.Text = "";
            textBoxPassword.Text = "";
            textBoxEnterCaptcha.Text = "";
            Captcha.CreateCaptcha(Captcha.LetterOption.Alphanumeric, 6);
        }

    }


}    


