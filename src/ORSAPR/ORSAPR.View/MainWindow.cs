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
        private void OutputAfterErrorTextBox(TextBox textBox, string errorMessage, Exception exception)
        {
            textBox.BackColor = _errorColor;
            errorMessage = exception.Message;
            toolTipInformation.SetToolTip(textBox, errorMessage);
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
            textBox.Text = textBox.Text.TrimStart('0');
        }

        /// <summary>
        /// ограничивает ввод символов и знаков, в том
        /// случае если они были использованы ранее
        /// </summary>
        /// <param name="e"></param>
        /// <param name="textBox"></param>
        private void IfKeyPress(KeyPressEventArgs e, TextBox textBox)
        {
            if (Char.IsNumber(e.KeyChar) | ((e.KeyChar == Convert.ToChar(",")) &&
            !textBox.Text.Contains(",")) | e.KeyChar == '\b')
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
        private void IfTextBoxTextChanged(TextBox textBox, string errorMessage)
        {
            try
            {
                PointValidation(textBox);

                if (textBox.Text == string.Empty)
                {
                    textBox.BackColor = _emptyColor;
                }
                else
                {
                    ReloadChiselData();
                    textBox.BackColor = _trueColor;
                    toolTipInformation.SetToolTip(textBox, string.Empty);
                    errorMessage = string.Empty;
                }
            }
            catch (System.FormatException exception)
            {
                OutputAfterErrorTextBox(textBox, errorMessage, exception);
            }
            StartsWithComma(textBox);
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
                OutputAfterErrorTextBox(textBox, errorMessage, exception);
            }
        }

        /// <summary>
        /// функция обновления данных фигуры
        /// </summary>
        private void ReloadChiselData()
        {
            _chiselData.Width = Convert.ToDouble(TextBoxWidth.Text);
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
            IfTextBoxTextChanged(TextBoxWidth, _errorTextBoxWidth);
        }
    
        /// <summary>
        /// обработчик события выхода из текстбокса
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxWidth_Leave(object sender, EventArgs e)
        {
            IfTextBoxLeave(TextBoxWidth, _errorTextBoxWidth);
        }


        





        private void Buttonbuild_Click(object sender, EventArgs e)
        {
            TextBoxBladeLenght.Text = Convert.ToString(_chiselData.Width);
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
