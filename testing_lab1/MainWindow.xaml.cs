using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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



        public (string, string) CheckRegistration(string login, string password, string repeatPassword,  out string maskPasswd, out string maskRepeatPasswd)
        {
            string message = "";
            maskPasswd = "";
            foreach (var item in password)
                maskPasswd += Convert.ToString(Convert.ToInt32(item) + 7) + "#";

            maskRepeatPasswd = "";
            foreach (var item in repeatPassword) 
                maskRepeatPasswd += Convert.ToString(Convert.ToInt32(item) + 7) + "#";




            if (login.Length == 0)
            {
                message = "Задан пустой логин";
                Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, {3}!", login, maskPasswd, maskRepeatPasswd, message);
                return ("False", "Задан пустой логин");
            }
            else
            {
                if (login.Contains("+"))
                {
                    if (login.Any(c => char.IsLetter(c)))
                    {
                        message = "Номер телефона не может содержать букв";
                        Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, {3}!", login, maskPasswd, maskRepeatPasswd, message);
                        return ("False", "Номер телефона не может содержать букв");
                    }
                    else if (login.Substring(1).Any(c => char.IsSymbol(c)))
                    {
                        message = "Номер телефона не может содержать символов";
                        Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, {3}!", login, maskPasswd, maskRepeatPasswd, message);
                        return ("False", "Номер телефона не может содержать символов");

                    }
                    else if (login.Any(c => char.IsPunctuation(c)))
                    {
                        message = "Номер телефона не может содержать знаки препинания";
                        Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, {3}!", login, maskPasswd, maskRepeatPasswd, message);
                        return ("False", "Номер телефона не может содержать знаки препинания");
                    }
                    else if (login.Any(c => char.IsWhiteSpace(c)))
                    {
                        message = "Номер телефона не должен содержать пробелов";
                        Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, {3}!", login, maskPasswd, maskRepeatPasswd, message);
                        return ("False", "Номер телефона не должен содержать пробелов");
                    }
                    else if (login.Length > 0 && login.Length - 1 < 11)
                    {
                        message = "Номер должен состоять из 11 цифр. Количество цифр меньше 11";
                        Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, {3}!", login, maskPasswd, maskRepeatPasswd, message);
                        return ("False", "Номер должен состоять из 11 цифр. Количество цифр меньше 11");
                    }

                    else if (login.Length - 1 > 11)
                    {
                        message = "Номер должен состоять из 11 цифр. Количество цифр больше 11";
                        Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, {3}!", login, maskPasswd, maskRepeatPasswd, message);
                        return ("False", "Номер должен состоять из 11 цифр. Количество цифр больше 11");
                    }


                }
                else if (login.Contains("@"))
                {
                    if (!login.Contains("."))
                    {
                        message = "Почта не содержит '.'";
                        Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, {3}!", login, maskPasswd, maskRepeatPasswd, message);
                        return ("False", "Почта не содержит '.'");

                    }
                    else if (login.Last() == '.')
                    {
                        message = "Отсутствует домен почты";
                        Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, {3}!", login, maskPasswd, maskRepeatPasswd, message);
                        return ("False", "Отсутствует домен почты");
                    }
                    else if (login.IndexOf('.') < login.IndexOf('@'))
                    {
                        message = "Неверная последовательность символов почты";
                        Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, {3}!", login, maskPasswd, maskRepeatPasswd, message);
                        return ("False", "Неверная последовательность символов почты");
                    }
                    else if (login.IndexOf('@') + 1 == login.IndexOf('.'))
                    {
                        message = "Отсутствует домен второго уровня";
                        Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, {3}!", login, maskPasswd, maskRepeatPasswd, message);
                        return ("False", "Отсутствует домен второго уровня");
                    }
                    else if (login.Any(c => char.IsWhiteSpace(c)))
                    {
                        message = "Логин не должен содержать пробелов";
                        Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, {3}!", login, maskPasswd, maskRepeatPasswd, message);
                        return ("False", "Логин не должен содержать пробелов");
                    }
                    else if (login.Any(x => char.IsLetter(x) && x >= 1072 && x <= 1103))
                    {
                        message = "Почта должна содержать только латиницу";
                        Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, {3}!", login, maskPasswd, maskRepeatPasswd, message);
                        return ("False", "Почта должна содержать только латиницу");
                    }

                    else if (login.Any(c => char.IsSymbol(c)))
                    {
                        message = "Почта не может содержать символов";
                        Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, {3}!", login, maskPasswd, maskRepeatPasswd, message);
                        return ("False", "Почта не может содержать символов");
                    }
                    else if (login.Any(c => char.IsPunctuation(c) && c != '.' && c != '@'))
                    {
                        message = "Почта не может содержать знаков препинания, только '.' и '@'";
                        Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, {3}!", login, maskPasswd, maskRepeatPasswd, message);
                        return ("False", "Почта не может содержать знаков препинания, только '.' и '@'");
                    }
                    else if (login.Length > 30)
                    {
                        message = "Логин не может содержать более 30 символов";
                        Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, {3}!", login, maskPasswd, maskRepeatPasswd, message);
                        return ("False", "Логин не может содержать более 30 символов");
                    }

                }
                else if (!login.Contains('@') && !login.Contains('+'))
                {
                    if (login.Length < 5 && login.Length != 0)
                    {
                        message = "Длина логина должна быть минимум 5 символов";
                        Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, {3}!", login, maskPasswd, maskRepeatPasswd, message);
                        return ("False", "Длина логина должна быть минимум 5 символов");
                    }

                    else if (login.Any(x => char.IsLetter(x) && (x >= 1072 && x <= 1103) || (x >= 1040 && x <= 1071)))
                    {
                        message = "Логин должен содержать только латиницу";
                        Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, {3}!", login, maskPasswd, maskRepeatPasswd, message);
                        return ("False", "Логин должен содержать только латиницу");
                    }

                    else if (login.Any(c => char.IsSymbol(c)))
                    {
                        message = "Логин не может содержать символов, только '_'";
                        Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, {3}!", login, maskPasswd, maskRepeatPasswd, message);
                        return ("False", "Логин не может содержать символов, только '_'");

                    }
                    else if (login.Any(c => char.IsPunctuation(c) && c != '_'))
                    {
                        message = "Логин не может содержать знаков препинания, только '_'";
                        Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, {3}!", login, maskPasswd, maskRepeatPasswd, message);
                        return ("False", "Логин не может содержать знаков препинания, только '_'");
                    }
                    else if (login.Any(c => char.IsWhiteSpace(c)))
                    {
                        txtLogin.ToolTip = "Вводите логин без пробелов";
                        Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, {3}!", login, maskPasswd, maskRepeatPasswd, message);
                        return ("False", "Вводите логин без пробелов");
                    }
                    else if (login.Length > 30)
                    {
                        txtLogin.ToolTip = "Логин не может содержать более 30 символов";
                        Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, {3}!", login, maskPasswd, maskRepeatPasswd, message);
                        return ("False", "Логин не может содержать более 30 символов");
                    }

                }

                if (password.Length == 0)
                {
                    message = "Задан пустой пароль";
                    txtPassword.BorderBrush = Brushes.Red;
                    Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, {3}!", login, maskPasswd, maskRepeatPasswd, message);
                    return ("False", "Задан пустой пароль");
                }
                else
                {
                    if (password.Length > 0 && password.Length < 7)
                    {
                        message = "Пароль должен иметь минимум 7 символов";
                        Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, {3}!", login, maskPasswd, maskRepeatPasswd, message);
                        return ("False", "Пароль должен иметь минимум 7 символов");
                    }
                    else if (password.Any(x => char.IsLetter(x) && (x >= 97 && x <= 122) || (x >= 65 && x <= 90)))
                    {
                        message = "Пароль должен содержать только кириллицу";
                        Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, {3}!", login, maskPasswd, maskRepeatPasswd, message);
                        return ("False", "Пароль должен содержать только кириллицу");
                    }
                    else if (!password.Any(c => char.IsDigit(c)))
                    {
                        message = "Пароль должен содержать хотя бы одну цифру";
                        Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, {3}!", login, maskPasswd, maskRepeatPasswd, message);
                        return ("False", "Пароль должен содержать хотя бы одну цифру");
                    }
                    else if (!password.Any(c => char.IsSymbol(c)))
                    {
                        message = "Пароль должен содержать хотя бы один спецсимвол";
                        Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, {3}!", login, maskPasswd, maskRepeatPasswd, message);
                        return ("False", "Пароль должен содержать хотя бы один спецсимвол");
                    }
                    else if (!password.Any(x => char.IsLetter(x) && x >= 1072 && x <= 1103))
                    {
                        message = "Пароль должен содержать хотя бы одну букву в нижнем регистре";
                        Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, {3}!", login, maskPasswd, maskRepeatPasswd, message);
                        return ("False", "Пароль должен содержать хотя бы одну букву в нижнем регистре");
                    }
                    else if (!password.Any(x => char.IsLetter(x) && x >= 1040 && x <= 1071))
                    {
                        message = "Пароль должен содержать хотя бы одну букву в верхнем регистре";
                        Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, {3}!", login, maskPasswd, maskRepeatPasswd, message);
                        return ("False", "Пароль должен содержать хотя бы одну букву в верхнем регистре");
                    }
                    else if (password.Length > 30)
                    {
                        message = "Пароль не может содержать более 30 символов";
                        Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, {3}!", login, maskPasswd, maskRepeatPasswd, message);
                        return ("False", "Пароль не может содержать более 30 символов");
                    }
                    else if (repeatPassword != password)
                    {
                        message = "Пароли не совпадают";
                        Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, {3}!", login, maskPasswd, maskRepeatPasswd, message);
                        return ("False", "Пароли не совпадают");
                    }

                    else
                    {

                            if (testingDBEntities.GetContext().Users.Count() == 0)
                            {
                                Users user = new Users();
                                user.loginUser = login;
                                user.passwordUser = password;
                                testingDBEntities.GetContext().Users.Add(user);
                                testingDBEntities.GetContext().SaveChanges();
                                Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, Успешная регистрация!", login, maskPasswd, maskRepeatPasswd);
                                message = "Вы зарегистрированы!";
                                return ("True", "Вы зарегистрированы");
                            }

                            else if (testingDBEntities.GetContext().Users.Where(x => x.loginUser == login).FirstOrDefault() == null)
                            {

                                Users user = new Users();
                                user.loginUser = login;
                                user.passwordUser = password;
                                testingDBEntities.GetContext().Users.Add(user);
                                testingDBEntities.GetContext().SaveChanges();
                                Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, Успешная регистрация!", login, maskPasswd, maskRepeatPasswd);
                                message = "Вы зарегистрированы!";
                                return ("True", "Вы зарегистрированы");
                            }
                            else
                            {
                                Log.Information("Логин: {0}, Пароль: {1}, Повторный пароль: {2}, Пользователь с таким логином уже существует!", login, maskPasswd, maskRepeatPasswd);
                                message = "Пользователь с таким логином уже существует";
                                return ("False", "Пользователь с таким логином уже существует");
                            }

                        

                    }


                }
            }




        }

        private void BtnInput_Click(object sender, RoutedEventArgs e)
        {
            Log.Logger = new LoggerConfiguration().WriteTo.File(@"G:\testing_lab1\logFile.log").CreateLogger();
            string login = txtLogin.Text.Trim();
            string password = txtPassword.Password.Trim();
            string repeatPassword = txtRepeatPassword.Password.Trim();
            string maskPasswd = "";
            string maskRepeatPasswd = "";

                (string result, string message) = CheckRegistration(login, password, repeatPassword, out maskPasswd,out maskRepeatPasswd);

            if (message == "")
            {
                MessageBox.Show(result);
            }
            else
            {
                MessageBox.Show(message);
            }

            txtLogin.Text = "";
            txtPassword.Password = "";
            txtRepeatPassword.Password = "";









        }


    }
}
