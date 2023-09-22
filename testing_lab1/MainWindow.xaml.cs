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
using Serilog;
using Serilog.Core;

namespace testing_lab1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Log.Logger = new LoggerConfiguration().WriteTo.File(@"C:\Users\kit\source\repos\testing_lab1\logFile.log").CreateLogger();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMin_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void TextLoginMouseDown(object sender, MouseButtonEventArgs e)
        {
            txtLogin.Focus();
        }
        private void TxtLoginTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtLogin.Text) && txtLogin.Text.Length > 0)
                textLogin.Visibility = Visibility.Collapsed;
            else
                textLogin.Visibility = Visibility.Visible;
        }



        private void TextPasswordMouseDown(object sender, MouseButtonEventArgs e)
        {
            txtPassword.Focus();
        }

        private void TxtPasswordTextChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPassword.Password) && txtPassword.Password.Length > 0)
                textPassword.Visibility = Visibility.Collapsed;
            else
                textPassword.Visibility = Visibility.Visible;
        }


        private void TextRepeatPasswordMouseDown(object sender, MouseButtonEventArgs e)
        {
            txtRepeatPassword.Focus();
        }

        private void TxtRepeatPasswordTextChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtRepeatPassword.Password) && txtRepeatPassword.Password.Length > 0)
                textRepeatPassword.Visibility = Visibility.Collapsed;
            else
                textRepeatPassword.Visibility = Visibility.Visible;
        }


        private void BtnInput_Click(object sender, RoutedEventArgs e)
        {
            string login = txtLogin.Text.Trim();
            string password = txtPassword.Password.Trim();
            string repeatPassword = txtRepeatPassword.Password.Trim();
            string maskPasswd = "";
            string maskRepeatPasswd = "";

            foreach (var item in password)
                maskPasswd += Convert.ToString(Convert.ToInt32(item) + 7) + "#";
    
            foreach (var item in repeatPassword)
                maskRepeatPasswd += Convert.ToString(Convert.ToInt32(item) + 7) + "#";


            if (login.Length == 0)
            {
                txtLogin.ToolTip = "Введите логин";
                txtLogin.BorderBrush = Brushes.Red;
                Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, {3}!", login, maskPasswd, maskRepeatPasswd, txtLogin.ToolTip);
            }
            if (login.Contains("+"))
                {
                if (login.Any(c => char.IsLetter(c)))
                {
                    txtLogin.ToolTip = "Номер телефона не может содержать букв";
                    
                    txtLogin.BorderBrush = Brushes.Red;
                   
                    Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, {3}!", login, maskPasswd, maskRepeatPasswd, txtLogin.ToolTip);
                }
                else if (login.Substring(1).Any(c => char.IsSymbol(c)))
                {
                    txtLogin.ToolTip = "Номер телефона не может содержать символов";
                    txtLogin.BorderBrush = Brushes.Red;
                    Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, {3}!", login, maskPasswd, maskRepeatPasswd, txtLogin.ToolTip);
                }
                else if (login.Any(c => char.IsPunctuation(c)))
                {
                    txtLogin.ToolTip = "Номер телефона не может содержать знаки препинания";
                    txtLogin.BorderBrush = Brushes.Red;
                    Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, {3}!", login, maskPasswd, maskRepeatPasswd, txtLogin.ToolTip);
                }
                else if (login.Any(c => char.IsWhiteSpace(c)))
                {
                    txtLogin.ToolTip = "Вводите номер телефона без пробелов";
                    txtLogin.BorderBrush = Brushes.Red;
                    Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, {3}!", login, maskPasswd, maskRepeatPasswd, txtLogin.ToolTip);
                }
                else if (login.Length > 0 && login.Length - 1 < 11)
                {
                    txtLogin.ToolTip = "Номер должен состоять из 11 цифр. Количество цифр меньше 11";
                    txtLogin.BorderBrush = Brushes.Red;
                    Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, {3}!", login, maskPasswd, maskRepeatPasswd, txtLogin.ToolTip);
                }

                else if (login.Length - 1 > 11)
                {
                    txtLogin.ToolTip = "Номер должен состоять из 11 цифр. Количество цифр больше 11";
                    txtLogin.BorderBrush = Brushes.Red;
                    Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, {3}!", login, maskPasswd, maskRepeatPasswd, txtLogin.ToolTip);
                }

                   
                
                else
                {
                    txtLogin.ToolTip = "";
                    txtLogin.BorderBrush = Brushes.Transparent;
                }

                


            }
            else if (login.Contains("@"))
            {
                if (!login.Contains("."))
                {
                    txtLogin.ToolTip = "Неверный формат почты";
                    txtLogin.BorderBrush = Brushes.Red;
                    Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, {3}!", login, maskPasswd, maskRepeatPasswd, txtLogin.ToolTip);


                }
                else if (login.Last() == '.')
                {
                    txtLogin.ToolTip = "Неверный формат почты";
                    txtLogin.BorderBrush = Brushes.Red;
                    Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, {3}!", login, maskPasswd, maskRepeatPasswd, txtLogin.ToolTip);

                }
                else if (login.IndexOf('.') < login.IndexOf('@'))
                {
                    txtLogin.ToolTip = "Неверный формат почты";
                    txtLogin.BorderBrush = Brushes.Red;
                    Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, {3}!", login, maskPasswd, maskRepeatPasswd, txtLogin.ToolTip);

                }
                else if (login.IndexOf('@') + 1 == login.IndexOf('.'))
                {
                    txtLogin.ToolTip = "Неверный формат почты";
                    txtLogin.BorderBrush = Brushes.Red;
                    Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, {3}!", login, maskPasswd, maskRepeatPasswd, txtLogin.ToolTip);

                }
                else if (login.Any(c => char.IsWhiteSpace(c)))
                {
                    txtLogin.ToolTip = "Неверный формат почты";
                    txtLogin.BorderBrush = Brushes.Red;
                    Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, {3}!", login, maskPasswd, maskRepeatPasswd, txtLogin.ToolTip);

                }
                else if (login.Any(x => char.IsLetter(x) && x >= 1072 && x <= 1103))
                {
                    txtLogin.ToolTip = "Почта должна содержать только латиницу";
                    txtLogin.BorderBrush = Brushes.Red;
                    Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, {3}!", login, maskPasswd, maskRepeatPasswd, txtLogin.ToolTip);

                }

                else if (login.Any(c => char.IsSymbol(c)))
                {
                    txtLogin.ToolTip = "Почта не может содержать символов";
                    txtLogin.BorderBrush = Brushes.Red;
                    Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, {3}!", login, maskPasswd, maskRepeatPasswd, txtLogin.ToolTip);

                }
                else if (login.Any(c => char.IsPunctuation(c) && c != '.' && c != '@'))
                {
                    txtLogin.ToolTip = "Почта не может содержать знаков препинания, только '.' и '@'";
                    Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, {3}!", login, maskPasswd, maskRepeatPasswd, txtLogin.ToolTip);

                    txtLogin.BorderBrush = Brushes.Red;
                }
                else if (login.Length > 30)
                {
                    txtLogin.ToolTip = "Логин не может содержать более 30 символов";
                    txtLogin.BorderBrush = Brushes.Red;
                    Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, {3}!", login, maskPasswd, maskRepeatPasswd, txtLogin.ToolTip);

                }
                else
                {
                    txtLogin.ToolTip = "";
                    txtLogin.BorderBrush = Brushes.Transparent;
                }


            }
            else
            {
                if (login.Length < 5 && login.Length != 0)
                {
                    txtLogin.ToolTip = "Длина логина должна быть минимум 5 символов";
                    txtLogin.BorderBrush = Brushes.Red;
                    Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, {3}!", login, maskPasswd, maskRepeatPasswd, txtLogin.ToolTip);

                }

                else if (login.Any(x => char.IsLetter(x) && (x >= 1072 && x <= 1103) || (x >= 1040 && x <= 1071)))
                {
                    txtLogin.ToolTip = "Логин должен содержать только латиницу";
                    txtLogin.BorderBrush= Brushes.Red;
                    Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, {3}!", login, maskPasswd, maskRepeatPasswd, txtLogin.ToolTip);

                }

                else if (login.Any(c => char.IsSymbol(c)))
                {
                    txtLogin.ToolTip = "Логин не может содержать символов, только '_'";
                    txtLogin.BorderBrush = Brushes.Red;
                    Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, {3}!", login, maskPasswd, maskRepeatPasswd, txtLogin.ToolTip);

                }
                else if (login.Any(c => char.IsPunctuation(c) && c != '_'))
                {
                    txtLogin.ToolTip = "Логин не может содержать знаков препинания, только '_'";
                    txtLogin.BorderBrush = Brushes.Red;
                    Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, {3}!", login, maskPasswd, maskRepeatPasswd, txtLogin.ToolTip);

                }
                else if (login.Any(c => char.IsWhiteSpace(c)))
                {
                    txtLogin.ToolTip = "Вводите логин без пробелов";
                    txtLogin.BorderBrush = Brushes.Red;
                    Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, {3}!", login, maskPasswd, maskRepeatPasswd, txtLogin.ToolTip);

                }
                else if (login.Length > 30)
                {
                    txtLogin.ToolTip = "Логин не может содержать более 30 символов";
                    txtLogin.BorderBrush = Brushes.Red;
                    Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, {3}!", login, maskPasswd, maskRepeatPasswd, txtLogin.ToolTip);

                }
                else
                {
                    txtLogin.ToolTip = "";
                    txtLogin.BorderBrush = Brushes.Transparent;
                }

                
            }




            if (password.Length == 0)
            {
                txtPassword.ToolTip = "Введите пароль";
                txtPassword.BorderBrush = Brushes.Red;
                Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, {3}!", login, maskPasswd, maskRepeatPasswd, txtPassword.ToolTip);
            }
            else if (password.Length > 0 && password.Length < 7)
            {
                txtPassword.ToolTip = "Пароль должен иметь минимум 7 символов";
                txtPassword.BorderBrush = Brushes.Red;
                Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, {3}!", login, maskPasswd, maskRepeatPasswd, txtPassword.ToolTip);
            }
            else if (password.Any(x => char.IsLetter(x) && (x >= 97 && x <= 122) || (x >= 65 && x <= 90)))
            {
                txtPassword.ToolTip = "Пароль должен содержать только кириллицу";
                txtPassword.BorderBrush = Brushes.Red;
                Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, {3}!", login, maskPasswd, maskRepeatPasswd, txtPassword.ToolTip);
            }
            else if (!password.Any(c => char.IsDigit(c)))
            {
                txtPassword.ToolTip = "Пароль должен содержать хотя бы одну цифру";
                txtPassword.BorderBrush = Brushes.Red;
                Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, {3}!", login, maskPasswd, maskRepeatPasswd, txtPassword.ToolTip);
            }
            else if (!password.Any(c => char.IsSymbol(c)))
            {
                txtPassword.ToolTip = "Пароль должен содержать хотя бы один спецсимвол";
                txtPassword.BorderBrush = Brushes.Red;
                Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, {3}!", login, maskPasswd, maskRepeatPasswd, txtPassword.ToolTip);
            }
            else if (!password.Any(x => char.IsLetter(x) && x >= 1072 && x <= 1103))
            {
                txtPassword.ToolTip = "Пароль должен содержать хотя бы одну букву в нижнем регистре";
                txtPassword.BorderBrush = Brushes.Red;
                Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, {3}!", login, maskPasswd, maskRepeatPasswd, txtPassword.ToolTip);
            }
            else if (!password.Any(x => char.IsLetter(x) && x >= 1040 && x <= 1071))
            {
                txtPassword.ToolTip = "Пароль должен содержать хотя бы одну букву в верхнем регистре";
                txtPassword.BorderBrush = Brushes.Red;
                Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, {3}!", login, maskPasswd, maskRepeatPasswd, txtPassword.ToolTip);
            }
            else if (password.Length > 30)
            {
                txtPassword.ToolTip = "Пароль не может содержать более 30 символов";
                txtPassword.BorderBrush = Brushes.Red;
                Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, {3}!", login, maskPasswd, maskRepeatPasswd, txtPassword.ToolTip);
            }
           
               
            else
            {
                txtPassword.ToolTip = "";
                txtPassword.BorderBrush = Brushes.Transparent;
            }

            if(repeatPassword != password )
            {
                txtRepeatPassword.ToolTip = "Пароли не совпадают";
                txtRepeatPassword.BorderBrush = Brushes.Red;
                Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, {3}!", login, maskPasswd, maskRepeatPasswd, txtRepeatPassword.ToolTip);
            }
            if(repeatPassword == password && repeatPassword.Length != 0 && password.Length != 0)
            {
                txtRepeatPassword.ToolTip = "";
                txtRepeatPassword.BorderBrush = Brushes.Transparent;
               

                if (testingDBEntities.GetContext().Users.Where(x => x.loginUser == login).FirstOrDefault() == null)
                {
                   
                    Users user = new Users();
                    user.loginUser = login;
                    user.passwordUser = password;
                    testingDBEntities.GetContext().Users.Add(user);
                    testingDBEntities.GetContext().SaveChanges();
                    Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, Успешная регистрация!", login, maskPasswd, maskRepeatPasswd);
                    MessageBox.Show("Вы зарегистрированы!");
                    txtLogin.Text = "";
                    txtPassword.Password = "";
                    txtRepeatPassword.Password = "";

                }
                else
                {
                    Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, Пользователь с таким логином уже существует!", login, maskPasswd, maskRepeatPasswd);
                    MessageBox.Show("Пользователь с таким логином уже существует");
                    txtLogin.Text = "";
                    txtPassword.Password = "";
                    txtRepeatPassword.Password = "";
                }
                    
            }

        }


    }
}
