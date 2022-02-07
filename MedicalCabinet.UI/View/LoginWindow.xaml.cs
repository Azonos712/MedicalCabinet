using MedicalCabinet.Library;
using MedicalCabinet.Library.Data;
using MedicalCabinet.Library.Model;
using MedicalCabinet.Library.Validator;
using MedicalCabinet.UI.Helpers;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MedicalCabinet.UI.View
{
    public partial class LoginWindow : Window
    {
        private readonly SolidColorBrush _primary = Application.Current.Resources["PrimaryBrush"] as SolidColorBrush;
        public Color PrimaryColor => Color.FromArgb(_primary.Color.A, _primary.Color.R, _primary.Color.G, _primary.Color.B);
        private readonly SolidColorBrush _secondary = Application.Current.Resources["SecondaryBrush"] as SolidColorBrush;
        public Color SecondaryColor => Color.FromArgb(_secondary.Color.A, _secondary.Color.R, _secondary.Color.G, _secondary.Color.B);
        private readonly SolidColorBrush _border = Application.Current.Resources["BorderGray"] as SolidColorBrush;
        public Color BorderColor => Color.FromArgb(_border.Color.A, _border.Color.R, _border.Color.G, _border.Color.B);
        private readonly TimeSpan _duration = TimeSpan.FromSeconds(0.4);

        private bool _isLogIn = true;

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            User newUser = GetUserFromEnteredData();
            bool isNewUserValid = ModelValidator.Validate<User>(new UserValidator(_isLogIn), newUser, BackgroundGrid);
            if (!isNewUserValid)
                return;

            LoginContext loginContext = new(newUser);

            if (_isLogIn)
            {
                Authorization(loginContext);
            }
            else
            {
                Registration(loginContext);
            }
        }

        //TODO переделать в DataContext
        private User GetUserFromEnteredData()
        {
            return new User()
            {
                Email = txtBoxEmail.Text.Trim(),
                Login = txtBoxLogin.Text.Trim(),
                Password = passBox1.Password.Trim(),
                PasswordConfirmation = passBox2.Password.Trim(),
                Doctor = new Doctor()
                {
                    BirthDay = DateTime.Now.Date,//TODO сократить путь к изображению
                    Portrait = ImageUtility.ImageSourceToBytes(ImageUtility.PathToImageSource("pack://application:,,,/Images/Portrait/default.png"))
                }
            };
        }

        private void Authorization(LoginContext context)
        {
            User verifiedUser = context.SignIn();
            if (verifiedUser != null)
                WindowsMaker.ShowWindow(this, new WorkWindow(verifiedUser));
            else
                WindowsMaker.ShowPopUp(new CustomMsgBox("Ошибка", "Ошибка входа.\nДанные не найдены!"), BackgroundGrid);
        }

        private void Registration(LoginContext context)
        {
            if (context.SignUp())
            {
                WindowsMaker.ShowPopUp(new CustomMsgBox("Информация", "Регистрация прошла успешно!"), BackgroundGrid);
                ClearInputs();
                RegistrationTxtBlock_MouseUp(this, EventArgs.Empty);
            }
            else
                WindowsMaker.ShowPopUp(new CustomMsgBox("Ошибка", "Во время регистрации произошла ошибка!"), BackgroundGrid);
        }

        private void ClearInputs() => txtBoxEmail.Text = txtBoxLogin.Text = passBox1.Password = passBox2.Password = string.Empty;

        private void TextChanged1(object s, RoutedEventArgs e) => placeHolder1.ToggleVisibility((s as TextBox).Text.Length);

        private void TextChanged2(object s, RoutedEventArgs e) => placeHolder2.ToggleVisibility((s as TextBox).Text.Length);

        private void TextChanged3(object s, RoutedEventArgs e) => placeHolder3.ToggleVisibility((s as PasswordBox).Password.Length);

        private void TextChanged4(object s, RoutedEventArgs e) => placeHolder4.ToggleVisibility((s as PasswordBox).Password.Length);

        private void RegistrationTxtBlock_MouseUp(object sender, EventArgs e)
        {
            AnimateBlueSquare();
            AnimateLogo();
            ChangeTopTextBlocks();
            AnimateTopTextBox();
            AnimateBotTextBox();
            AnimateTextBlockButtons();
            AnimateBotButton();

            _isLogIn = !_isLogIn;
        }

        private void AnimateBlueSquare()
        {
            LeftBlueGrid.MarginAnimation(new Thickness(_isLogIn ? 640.5 : 0, 0, 0, 0), _duration);
        }

        private void AnimateLogo()
        {
            WhiteLogo.OpacityAnimation(Convert.ToInt32(!_isLogIn), _duration);
            BlueLogo.OpacityAnimation(Convert.ToInt32(_isLogIn), _duration);
            LogoTextBlock.ForegroundColorAnimation(_isLogIn ? PrimaryColor : SecondaryColor, _duration);
        }

        private void ChangeTopTextBlocks()
        {
            TopTxtBlock1.OpacityAnimation(Convert.ToInt32(!_isLogIn), _duration);
            TopTxtBlock2.OpacityAnimation(Convert.ToInt32(_isLogIn), _duration);
        }

        private void AnimateTopTextBox()
        {
            NewLoginGrid.MarginAnimation(new Thickness(10, 10, 10, _isLogIn ? 95 : 10), _duration);
            NewLoginGrid.OpacityAnimation(Convert.ToInt32(_isLogIn), _duration);
        }

        private void AnimateBotTextBox()
        {
            ConfirmPassGrid.MarginAnimation(new Thickness(10, _isLogIn ? 95 : 10, 10, 10), _duration);
            ConfirmPassGrid.OpacityAnimation(Convert.ToInt32(_isLogIn), _duration);
        }

        private void AnimateTextBlockButtons()
        {
            RegistrationTxtBlock.ForegroundColorAnimation(_isLogIn ? SecondaryColor : PrimaryColor, _duration);
            if (_isLogIn)
            {
                RegistrationTxtBlock.Text = "Уже есть аккаунт";
                //ForgetPassTxtBlock.Visibility = Visibility.Collapsed;
            }
            else
            {
                RegistrationTxtBlock.Text = "Регистрация";
                //ForgetPassTxtBlock.Visibility = Visibility.Visible;
            }
        }

        private void AnimateBotButton()
        {
            EnterButton.ForegroundColorAnimation(_isLogIn ? PrimaryColor : SecondaryColor, _duration);
            EnterButton.BackgroundColorAnimation(_isLogIn ? SecondaryColor : PrimaryColor, _duration);
            if (_isLogIn)
            {
                EnterButton.Style = (Style)FindResource("WhiteBlueButtonStyle");
                EnterButton.Content = "Зарегистрироваться";
            }
            else
            {
                EnterButton.Style = (Style)FindResource("BlueWhiteButtonStyle");
                EnterButton.Content = "Войти";
            }
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}