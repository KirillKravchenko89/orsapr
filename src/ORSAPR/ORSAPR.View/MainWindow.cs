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
        /// переменная ошибки ввода параметра ширины
        /// </summary>
        private string _errorTextBoxWidth = string.Empty;

        /// <summary>
        /// переменная ошибки ввода параметра длины
        /// </summary>
        private string _errorTextBoxLength = string.Empty;

        /// <summary>
        /// переменная ошибки ввода параметра высоты
        /// </summary>
        private string _errorTextBoxHeight = string.Empty;

        /// <summary>
        /// переменная ошибки ввода параметра длины лезвия
        /// </summary>
        private string _errorTextBoxBladeLength = string.Empty;

        /// <summary>
        /// переменная ошибки ввода параметра внутренней длины выреза
        /// </summary>
        private string _errorTextBoxInnerLength = string.Empty;

        /// <summary>
        /// переменная ошибки ввода параметра внутренней ширины выреза
        /// </summary>
        private string _errorTextBoxInnerWidth = string.Empty;

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

        private Manager _Manager;

        private KompasConnector _kompasApp;

        public FormParameters()
        {
            InitializeComponent();
            SetAllInputsEnabledState(false);
        }

        /// <summary>
        /// изменения параметра Enabled у группы типа Textbox
        /// </summary>
        /// <param name="state"></param>
        private void SetAllInputsEnabledState(bool state)
        {
            foreach (Control control in Controls)
            {
                foreach (Control textbox in control.Controls)
                {
                    if (textbox.GetType() == typeof(TextBox))
                    {
                        textbox.Enabled = state;
                    }
                }
            }
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
            textBox.SelectionStart = outS.Length;
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
            if (textBox == TextBoxBladeLength)
            {
                _errorTextBoxBladeLength = errorMessage;
            }
            if (textBox == TextBoxInnerLength)
            {
                _errorTextBoxInnerLength = errorMessage;
            }
            if (textBox == TextBoxInnerWidth)
            {
                _errorTextBoxInnerWidth = errorMessage;
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
            if (e.KeyChar == (char)Keys.Enter)
            {
                SendKeys.Send("{Tab}");
            }
            if (e.KeyChar == (char)Keys.Space)
            {
                Buttonbuild_Click(null, null);                
            }          
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
        private void ReloadChiselData(TextBox textBox, double chiselData)
        {
            if ((chiselData == _chiselData.Width) && (textBox == TextBoxWidth) &&
                TextBoxWidth.Text != string.Empty)
            {              
                _chiselData.Width = Convert.ToDouble(TextBoxWidth.Text);                                   
            }         
            if ((chiselData == _chiselData.Length) && (textBox == TextBoxLength) &&
                TextBoxLength.Text != string.Empty)
            {              
                _chiselData.Length = Convert.ToDouble(TextBoxLength.Text);                             
            }        
            if ((chiselData == _chiselData.Height) && (textBox == TextBoxHeight) &&
                TextBoxHeight.Text != string.Empty)
            {              
                _chiselData.Height = Convert.ToDouble(TextBoxHeight.Text);      
            }
            if ((chiselData == _chiselData.BladeLength) && (textBox == TextBoxBladeLength) &&
               TextBoxBladeLength.Text != string.Empty)
            {
                _chiselData.BladeLength = Convert.ToDouble(TextBoxBladeLength.Text);
            }
            if ((chiselData == _chiselData.InnerLength) && (textBox == TextBoxInnerLength) &&
                TextBoxInnerLength.Text != string.Empty)
            {
                _chiselData.InnerLength = Convert.ToDouble(TextBoxInnerLength.Text);
            }
            if ((chiselData == _chiselData.InnerWidth) && (textBox == TextBoxInnerWidth) &&
                TextBoxInnerWidth.Text != string.Empty)
            {
                _chiselData.InnerWidth = Convert.ToDouble(TextBoxInnerWidth.Text);
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
            IfTextBoxTextChanged(TextBoxLength, errorMessage, _chiselData.Length);
            IfTextBoxTextChanged(TextBoxBladeLength, errorMessage, _chiselData.BladeLength);
            IfTextBoxTextChanged(TextBoxInnerLength, errorMessage, _chiselData.InnerLength);
            IfTextBoxTextChanged(TextBoxInnerWidth, errorMessage, _chiselData.InnerWidth);           
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
        /// обработчик события выхода из текстбокса ширины
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxWidth_Leave(object sender, EventArgs e)
        {
            IfTextBoxLeave(TextBoxWidth, _errorTextBoxWidth);
            CheckAffterInput();
        }

        /// <summary>
        /// обработчик события нажатия на клавишу в текстбоксе длины
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxLenght_KeyPress(object sender, KeyPressEventArgs e)
        {
            IfKeyPress(e, TextBoxLength);          
        }

        /// <summary>
        /// обработчик события изменения текста в текстбоксе длины
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxLenght_TextChanged(object sender, EventArgs e)
        {
            IfTextBoxTextChanged(TextBoxLength, _errorTextBoxLength, _chiselData.Length);
            CheckAffterInput();
        }

        /// <summary>
        /// обработчик события выхода из текстбокса длины
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxLenght_Leave(object sender, EventArgs e)
        {
            IfTextBoxLeave(TextBoxLength, _errorTextBoxLength);
            CheckAffterInput();
        }

        /// <summary>
        /// обработчик события нажатия на клавишу в текстбоксе высоты
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxHeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            IfKeyPress(e, TextBoxHeight);
        }

        /// <summary>
        /// обработчик события изменения текста в текстбоксе высоты
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxHeight_TextChanged(object sender, EventArgs e)
        {
            IfTextBoxTextChanged(TextBoxHeight, _errorTextBoxHeight, _chiselData.Height);
            CheckAffterInput();
        }

        /// <summary>
        /// обработчик события выхода из текстбокса высоты
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxHeight_Leave(object sender, EventArgs e)
        {
            IfTextBoxLeave(TextBoxHeight, _errorTextBoxHeight);
            CheckAffterInput();
        }

        /// <summary>
        /// обработчик события нажатия на клавишу в текстбоксе длины лезвия
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxBladeLength_KeyPress(object sender, KeyPressEventArgs e)
        {
            IfKeyPress(e, TextBoxBladeLength);
        }

        /// <summary>
        /// обработчик события изменения текста в текстбоксе длины лезвия
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxBladeLength_TextChanged(object sender, EventArgs e)
        {
            IfTextBoxTextChanged(TextBoxBladeLength, _errorTextBoxBladeLength,
                _chiselData.BladeLength);
            CheckAffterInput();
        }

        /// <summary>
        /// обработчик события выхода из текстбокса длины лезвия
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxBladeLength_Leave(object sender, EventArgs e)
        {
            IfTextBoxLeave(TextBoxBladeLength, _errorTextBoxBladeLength);
            CheckAffterInput();
        }

        /// <summary>
        /// обработчик события нажатия на клавишу в текстбоксе длины внутреннего выреза 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxInnerLength_KeyPress(object sender, KeyPressEventArgs e)
        {
            IfKeyPress(e, TextBoxInnerLength);
        }

        /// <summary>
        /// обрабочик события изменения текста в текстбоксе длины внутреннего выреза
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxInnerLength_TextChanged(object sender, EventArgs e)
        {
            IfTextBoxTextChanged(TextBoxInnerLength, _errorTextBoxInnerLength,
                _chiselData.InnerLength);
            CheckAffterInput();
        }

        /// <summary>
        /// обрабочик события выхода из текстбокса длины внутреннего выреза
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxInnerLength_Leave(object sender, EventArgs e)
        {
            IfTextBoxLeave(TextBoxInnerLength, _errorTextBoxInnerLength);
            CheckAffterInput();
        }

        /// <summary>
        /// обработчик события нажатия на клавишу в текстбоксе ширины внутреннего выреза
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxInnerWidth_KeyPress(object sender, KeyPressEventArgs e)
        {
            IfKeyPress(e, TextBoxInnerWidth);
        }

        /// <summary>
        /// обработчик события изменения текста в текстбоксе ширины внутреннего выреза 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxInnerWidth_TextChanged(object sender, EventArgs e)
        {
            IfTextBoxTextChanged(TextBoxInnerWidth, _errorTextBoxInnerWidth,
                _chiselData.InnerWidth);
            CheckAffterInput();
        }

        /// <summary>
        /// обработчик события выхода из текстбокса ширины внутреннего выреза
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxInnerWidth_Leave(object sender, EventArgs e)
        {
            IfTextBoxLeave(TextBoxInnerWidth, _errorTextBoxInnerWidth);
            CheckAffterInput();
        }
        /// <summary>
        /// обработчик события нажатия на кнопку "построить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Buttonbuild_Click(object sender, EventArgs e)
        {
            var MessageText = string.Empty;
            var errorText = string.Empty;

            if ((_chiselData.Width != 0 || _chiselData.Length != 0 || _chiselData.Height != 0
                || _chiselData.BladeLength != 0 || _chiselData.InnerLength != 0 
                || _chiselData.InnerWidth != 0) && (_errorTextBoxWidth == ""
                && _errorTextBoxLength == "" && _errorTextBoxHeight == ""
                && _errorTextBoxBladeLength == "" && _errorTextBoxInnerLength == ""
                && _errorTextBoxInnerWidth == ""))
            {
                MessageText += $" Parameters of chisel: {Environment.NewLine}";

                if (_chiselData.Width != 0)
                {
                    MessageText += $" - W - Width: {Convert.ToString(_chiselData.Width)}" +
                        $"{Environment.NewLine}";
                }
                if (_chiselData.Length != 0) 
                {
                    MessageText += $" - L - Length: {Convert.ToString(_chiselData.Length)}" +
                        $"{Environment.NewLine}";
                }
                if (_chiselData.Height != 0) 
                {
                    MessageText += $" - H - Height: {Convert.ToString(_chiselData.Height)}" +
                        $"{Environment.NewLine}";
                }
                if (_chiselData.BladeLength != 0)
                {
                    MessageText += $" - l1 - Blade Leigth:" +
                        $" {Convert.ToString(_chiselData.BladeLength)}{Environment.NewLine}";
                }
                if (_chiselData.InnerLength != 0)
                {
                    MessageText += $" - l2 - Inner Cutout Length:" +
                        $" {Convert.ToString(_chiselData.InnerLength)}{Environment.NewLine}";
                }
                if (_chiselData.InnerWidth != 0)
                {
                    MessageText += $" - w1 - Inner Cutout Width:" +
                        $" {Convert.ToString(_chiselData.InnerWidth)}{Environment.NewLine}";
                }
                if (MessageText != string.Empty) 
                {
                    string caption = "Message!";
                    MessageBoxButtons button = MessageBoxButtons.OK;
                    MessageBoxIcon icon = MessageBoxIcon.Information;
                    MessageBox.Show(MessageText, caption, button, icon,
                    MessageBoxDefaultButton.Button1);
                }
            }
            if (_errorTextBoxWidth != "" || _errorTextBoxLength != ""
                || _errorTextBoxHeight != "" || _errorTextBoxBladeLength != ""
                || _errorTextBoxInnerLength != "" || _errorTextBoxInnerWidth != "")
            {
                errorText += $"Some errors on form: {Environment.NewLine}";

                if (_errorTextBoxWidth != "")
                {
                    errorText += $" - W - Width: {_errorTextBoxWidth}{Environment.NewLine}";
                }
                if (_errorTextBoxLength != "")
                {
                    errorText += $" - L - Length: {_errorTextBoxLength}{Environment.NewLine}";
                }
                if (_errorTextBoxHeight != "")
                {
                    errorText += $" - H - Height: {_errorTextBoxHeight}{Environment.NewLine}";
                }
                if (_errorTextBoxBladeLength != "")
                {
                    errorText += $" - l1 - Blade Length: " +
                        $"{_errorTextBoxBladeLength}{Environment.NewLine}";
                }
                if (_errorTextBoxInnerLength != "")
                {
                    errorText += $" - l2 - Inner Length: " +
                        $"{_errorTextBoxInnerLength}{Environment.NewLine}";
                }
                if (_errorTextBoxInnerWidth != "")
                {
                    errorText += $" - w1 - Inner Width: " +
                        $"{_errorTextBoxInnerWidth}{Environment.NewLine}";
                }

                if (errorText != string.Empty)
                {
                    string caption = "Error!";
                    MessageBoxButtons button = MessageBoxButtons.OK;
                    MessageBoxIcon icon = MessageBoxIcon.Error;
                    MessageBox.Show(errorText, caption, button, icon,
                    MessageBoxDefaultButton.Button1);
                }


/*
                if (_kompasApp == null)
                {
                    MessageBox.Show("Load KOMPAS 3D first.", "Information",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return;
                }


                if (!_kompasApp.CreateDocument3D())
                {
                    return;
                }
               

                Manager _Manager = new Manager(_kompasApp);
                if (_Manager == null)
                {             
                    return;
                }
               
                _Manager.BuildModel();*/
     
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

        private void ButtonLoadCompas_Click(object sender, EventArgs e)
        {

            _kompasApp = new KompasConnector();


            SetAllInputsEnabledState(true);

            ButtonBuild.Enabled = true;

            ButtonLoadCompas.Enabled = false;
            ButtonUnloadCompas.Enabled = true;
            
        }

        private void ButtonUnloadCompas_Click(object sender, EventArgs e)
        {

            _kompasApp.DestructApp();

            SetAllInputsEnabledState(false);

            ButtonBuild.Enabled = false;

            ButtonLoadCompas.Enabled = true;
            ButtonUnloadCompas.Enabled = false;
            
        }      
    }
}
