using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ORSAPR.Model;

namespace ORSAPR.View
{
    public partial class FormParameters : System.Windows.Forms.Form
    {
        /// <summary>
        /// переменная ошибки ввода параметра высоты
        /// </summary>
        private string _errorTextBoxHeight = string.Empty;

        /// <summary>
        /// переменная ошибки ввода параметра длины
        /// </summary>
        private string _errorTextBoxLength = string.Empty;

        /// <summary>
        /// переменная ошибки ввода параметра ширины
        /// </summary>
        private string _errorTextBoxWidth = string.Empty;

        /// <summary>
        /// переменная ошибки ввода параметра внутренней длины выреза
        /// </summary>
        private string _errorTextBoxInnerLenght = string.Empty;

        /// <summary>
        /// переменная ошибки ввода параметра длины лезвия
        /// </summary>
        private string _errorTextBoxBladeLenght = string.Empty;

        /// <summary>
        /// переменная цвета поля ошибки
        /// </summary>
        private readonly Color _errorColor = Color.LightPink;

        /// <summary>
        /// переменная цвета пустого поля
        /// </summary>
        private readonly Color _emptyColor = Color.White;

        /// <summary>
        /// переменная цвета корректно введенных данных
        /// </summary>
        private readonly Color _trueColor = Color.LightGreen;

        /// <summary>
        /// выделение памяти под объект параметра
        /// </summary>
        private ChiselData _chiselData = new ChiselData();

        public FormParameters()
        {
            InitializeComponent();
        }

        /// <summary>
        /// метод замены точек на запятые, запрет ввода символов кроме double
        /// </summary>
        /// <param name="textBox"></param>
        private void PointValidation(TextBox textBox)
        {
            string str = textBox.Text;
            string tmp = textBox.Text.Trim();
            string outS = string.Empty;
            bool comma = true;
            foreach (char ch in tmp)
            {
                if (Char.IsDigit(ch) || (ch == ',' && comma))
                {
                    outS += ch;
                    if (ch == ',')
                        comma = false;
                }
            }
            textBox.Text = outS;

            if (str.Contains("."))
            {
                str.Replace(".", ",");
                textBox.Clear();
                textBox.AppendText(str.Replace(".", ","));

            }            
        }

        /// <summary>
        /// вывод после ошибки сообщения в тултип 
        /// </summary>
        /// <param name="exception"></param>
        private void OutputAfterErrorTextBox(TextBox textBox,
            Exception exception)
        {
            textBox.BackColor = _errorColor;
            var errorMessage = exception.Message;
            ExceptionController(errorMessage, textBox);
            toolTipInformation.SetToolTip(textBox, errorMessage);
        }
     
        /// <summary>
        /// функция сохранения строки ошибки
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <param name="textBox"></param>
        private void ExceptionController(string errorMessage, TextBox textBox)
        {
            if (textBox == TextBoxWidth)
            {
                _errorTextBoxWidth = errorMessage;
            }
            if (textBox == TextBoxLength)
            {
                _errorTextBoxLength = errorMessage;
            }
            if (textBox == TextBoxHeight) 
            {
                _errorTextBoxHeight = errorMessage;
            }       
        }

        /// <summary>
        /// функция одбработчик ввода запятой первым символом
        /// </summary>
        /// <param name="textBox"></param>
        private void StartsWithComma(TextBox textBox)
        {
            if (Convert.ToString(textBox.Text).StartsWith(","))
            {
                if (textBox.Text.Length != 0)
                {
                    textBox.Text = textBox.Text.Insert(0, "0");
                }
            }
        }

        /// <summary>
        /// функция одбработчик ввода запятой последним символом
        /// </summary>
        /// <param name="textBox"></param>
        private void EndsWithComma(TextBox textBox)
        {
            if (Convert.ToString(textBox.Text).EndsWith(","))
            {
                if (textBox.Text.Length != 0)
                {
                    textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1);
                }
            }
            if (!Convert.ToString(textBox.Text).Equals("0"))
            {
                textBox.Text = textBox.Text.TrimStart('0');
            }           
        }

        /// <summary>
        /// ограничивает ввод символов и знаков, в том
        /// случае если они были использованы ранее
        /// </summary>
        /// <param name="e"></param>
        /// <param name="textBox"></param>
        private void IfKeyPress(KeyPressEventArgs e, TextBox textBox)
        {          
            if (Char.IsNumber(e.KeyChar) | (((e.KeyChar == Convert.ToChar(","))
                || (e.KeyChar == Convert.ToChar("."))) && !textBox.Text.Contains(","))
                | e.KeyChar == '\b' | e.KeyChar == (char)3 | e.KeyChar == (char)22
                | e.KeyChar == (char)1 | e.KeyChar == (char)24)
            {              
                return;
            }
            else e.Handled = true;
        }

        /// <summary>
        /// функция изменения текста в текстбоксе
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="errorMessage"></param>
        private void IfTextBoxTextChanged(TextBox textBox, string errorMessage, double chiselData)
        {
            try
            {
                PointValidation(textBox);
                StartsWithComma(textBox);
                ReloadChiselData(textBox, chiselData);
                if (textBox.Text == "")
                {
                    textBox.BackColor = _emptyColor;                                
                }
                else
                {
                    textBox.BackColor = _trueColor;
                }
                ExceptionController(errorMessage, textBox);
                toolTipInformation.SetToolTip(textBox, string.Empty);
                errorMessage = string.Empty;               
            }          
            catch (ArgumentException exception)
            {
                OutputAfterErrorTextBox(textBox, exception);
            }
            catch (Exception exception)
            {
                OutputAfterErrorTextBox(textBox, exception);
            }
        }

        /// <summary>
        /// функция обработчика события выхода из текстбокса
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="errorMessage"></param>
        private void IfTextBoxLeave(TextBox textBox, string errorMessage)
        {
            try
            {
                EndsWithComma(textBox);
            }
            catch (ArgumentException exception)
            {
                OutputAfterErrorTextBox(textBox, exception);
            }
        }

        /// <summary>
        /// функция обновления данных фигуры
        /// </summary>
        private void ReloadChiselData(TextBox textBox, double chuselData)
        {
            if ((chuselData == _chiselData.Width) && (textBox == TextBoxWidth) &&
                TextBoxWidth.Text != string.Empty)
            {              
                _chiselData.Width = Convert.ToDouble(TextBoxWidth.Text);                                   
            }
          
            if ((chuselData == _chiselData.Lenght) && (textBox == TextBoxLength) &&
                TextBoxLength.Text != string.Empty)
            {              
                _chiselData.Lenght = Convert.ToDouble(TextBoxLength.Text);                             
            }
           
            if ((chuselData == _chiselData.Height) && (textBox == TextBoxHeight) &&
                TextBoxHeight.Text != string.Empty)
            {              
                _chiselData.Height = Convert.ToDouble(TextBoxHeight.Text);      
            } 
           
        }
      
        /// <summary>
        /// функция проверки правильности введенных данных во всех полях при изменении одного
        /// </summary>
        private void CheckAffterInput()
        {
           string errorMessage = string.Empty;
            IfTextBoxTextChanged(TextBoxHeight, errorMessage, _chiselData.Height);
            IfTextBoxTextChanged(TextBoxWidth, errorMessage, _chiselData.Width);
            IfTextBoxTextChanged(TextBoxLength, errorMessage, _chiselData.Lenght);
        }       







        /// <summary>
        /// обработчик события нажатия на клавишу в текстбоксе ширины
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxWidth_KeyPress(object sender, KeyPressEventArgs e)
        {
            IfKeyPress(e, TextBoxWidth);
        }

        /// <summary>
        /// обработчик события изменения текста текстбокса ширины
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxWidth_TextChanged(object sender, EventArgs e)
        {
            IfTextBoxTextChanged(TextBoxWidth, _errorTextBoxWidth, _chiselData.Width);
            CheckAffterInput();
        }
    
        /// <summary>
        /// обработчик события выхода из текстбокса
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxWidth_Leave(object sender, EventArgs e)
        {
            IfTextBoxLeave(TextBoxWidth, _errorTextBoxWidth);
            CheckAffterInput();
        }







        private void TextBoxHeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            IfKeyPress(e, TextBoxHeight);
        }

        private void TextBoxHeight_TextChanged(object sender, EventArgs e)
        {
            IfTextBoxTextChanged(TextBoxHeight, _errorTextBoxHeight, _chiselData.Height);
            CheckAffterInput();
        }

        private void TextBoxHeight_Leave(object sender, EventArgs e)
        {
            IfTextBoxLeave(TextBoxHeight, _errorTextBoxHeight);
            CheckAffterInput();
        }








        private void TextBoxLenght_KeyPress(object sender, KeyPressEventArgs e)
        {
            IfKeyPress(e, TextBoxLength);
        }

        private void TextBoxLenght_TextChanged(object sender, EventArgs e)
        {
            IfTextBoxTextChanged(TextBoxLength, _errorTextBoxLength, _chiselData.Lenght);
            CheckAffterInput();
        }

        private void TextBoxLenght_Leave(object sender, EventArgs e)
        {
            IfTextBoxLeave(TextBoxLength, _errorTextBoxLength);
            CheckAffterInput();
        }










        private void Buttonbuild_Click(object sender, EventArgs e)
        {
            TextBoxBladeLenght.Text = Convert.ToString(_chiselData.Width);//
            TextBoxInnerLenght.Text = Convert.ToString(_chiselData.Height);//

            var MessageText = string.Empty;
            var errorText = string.Empty;

            if ((_chiselData.Width != 0 || _chiselData.Lenght != 0
               || _chiselData.Height != 0) && (_errorTextBoxWidth == "" && _errorTextBoxLength == ""
                && _errorTextBoxHeight == ""))// общие ошибки 
            {
                MessageText += $" Parameters of chisel: {Environment.NewLine}";

                if (_chiselData.Width != 0)// индивидуальная ошибка фамилии
                {
                    MessageText += $" - Width: {Convert.ToString(_chiselData.Width)}{Environment.NewLine}";
                }
                if (_chiselData.Lenght != 0)  // индивидуальная ошибка имени
                {
                    MessageText += $" - Length: {Convert.ToString(_chiselData.Lenght)}{Environment.NewLine}";
                }
                if (_chiselData.Height != 0)  // индивидуальная ошибка номера
                {
                    MessageText += $" - Height: {Convert.ToString(_chiselData.Height)}{Environment.NewLine}";
                }

                if (MessageText != string.Empty) //Вывод данных при отсутствии ошибок ИЛИ вывод ошибок
                {
                    string caption = "Message!";
                    MessageBoxButtons button = MessageBoxButtons.OK;
                    MessageBoxIcon icon = MessageBoxIcon.Information;
                    MessageBox.Show(MessageText, caption, button, icon,
                    MessageBoxDefaultButton.Button1);
                }
            }
            if (_errorTextBoxWidth != "" || _errorTextBoxLength != ""
                || _errorTextBoxHeight != "")
            {
                errorText += $"Some errors on form: {Environment.NewLine}";

                if (_errorTextBoxWidth != "")// индивидуальная ошибка фамилии
                {
                    errorText += $" - Width: {_errorTextBoxWidth}{Environment.NewLine}";
                }
                if (_errorTextBoxLength != "")  // индивидуальная ошибка имени
                {
                    errorText += $" - Length: {_errorTextBoxLength}{Environment.NewLine}";
                }
                if (_errorTextBoxHeight != "")  // индивидуальная ошибка номера
                {
                    errorText += $" - Height: {_errorTextBoxHeight}{Environment.NewLine}";
                }

                if (errorText != string.Empty)
                {
                    string caption = "Error!";
                    MessageBoxButtons button = MessageBoxButtons.OK;
                    MessageBoxIcon icon = MessageBoxIcon.Error;
                    MessageBox.Show(errorText, caption, button, icon,
                    MessageBoxDefaultButton.Button1);
                }

            }
            

        }







        /// <summary>
        /// обработчик события закрытия формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormParameters_FormClosing(object sender, FormClosingEventArgs e)
        {
            string messageBoxText = $"Do you really want to close the window?" +
               $"{ Environment.NewLine}The entered data will be deleted.";
            string caption = "Exit";
            MessageBoxButtons button = MessageBoxButtons.YesNo;
            MessageBoxIcon icon = MessageBoxIcon.Question;
            DialogResult result = MessageBox.Show(messageBoxText, caption, button, icon,
                MessageBoxDefaultButton.Button1);

            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

      
    }
}
