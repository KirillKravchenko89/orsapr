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
        /// переменная цвета поля без ошибки
        /// </summary>
        private readonly Color _trueColor = Color.White;

        /// <summary>
        /// выделение памяти под объект параметра
        /// </summary>
        private Parameter _parameter = new Parameter();

        public FormParameters()
        {
            InitializeComponent();
        }

        /// <summary>
        /// вывод после ошибки 
        /// </summary>
        /// <param name="exception"></param>
        private void OutputAfterErrorTextBoxWidth(Exception exception)
        {
            TextBoxWidth.BackColor = _errorColor;
            _errorTextBoxWidth = exception.Message;
            toolTipInformation.SetToolTip(TextBoxWidth, _errorTextBoxWidth);
        }

        private void TextBoxWidth_TextChanged(object sender, EventArgs e)
        {
            try
            {            
                _parameter.Width =Convert.ToDouble(TextBoxWidth.Text);
                TextBoxWidth.BackColor = _trueColor;
                toolTipInformation.SetToolTip(TextBoxWidth, string.Empty);
                _errorTextBoxWidth = string.Empty;
                /*OKButtonON();*/
            }
            catch (Exception exception)
            {
                OutputAfterErrorTextBoxWidth(exception);
            }
        }

        /// <summary>
        /// обработчик события закрытия формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormParameters_FormClosing(object sender, FormClosingEventArgs e)
        {
            string messageBoxText = $"Вы действительно хотите закрыть окно?" +
               $"{ Environment.NewLine}Введенные данные будут удалены.";
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
