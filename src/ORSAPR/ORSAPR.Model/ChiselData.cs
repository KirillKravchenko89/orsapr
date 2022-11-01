using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORSAPR.Model
{
    /// <summary>
    /// класс параметров зубила
    /// </summary>
    public class ChiselData
    {
        /// <summary>
        /// поле высоты зубила
        /// </summary>
        private double _height;

        /// <summary>
        /// поле длины зубила
        /// </summary>
        private double _lenght;

        /// <summary>
        /// поле ширины зубила
        /// </summary>
        private double _width;

        /// <summary>
        /// поле длины внутреннего выреза зубила
        /// </summary>
        private double _innerLenght;

        /// <summary>
        /// поле длины лезвия зубила
        /// </summary>
        private double _bladeLenght;

        /// <summary>
        /// возвращает или задает ширину
        /// </summary>
        public double Width
        {
            get => _width;
            set
            {              
                if (value == 0)
                {
                    _width = value;
                    throw new ArgumentException("Invalid chisel width range. Сannot be width = 0!");
                }
                if (value < 10 || value > 30)
                {                 
                    throw new ArgumentException("Invalid chisel width range, please check the" +
                    " entered parameters according to the range: 10mm >= W >= 30mm.");
                }
                _width = value;
            }
        }

        /// <summary>
        /// возвращает или задает длину
        /// </summary>
        public double Lenght
        {
            get => _lenght;
            set
            {               
                if (value == 0)
                {                 
                    throw new ArgumentException("Invalid chisel lenght range." +
                        " Сannot be length = 0!");
                }
                if (value < 100 || value > 300)
                {
                    throw new ArgumentException("Invalid chisel lenght range, please check " +
                        "the entered parameters according to the range: 100mm >= L >= 300mm.");
                }
                else if (value > 100 || value < 300)
                {
                    if(_width !=0 && (_width > 10 || _width < 30))
                    {
                        if (value < 10 * _width || value >= 10 * _width + 10)
                        {
                            throw new ArgumentException($"Invalid chisel lenght range," +
                                $" please check the entered parameters according to the range:" +
                            $" {10 * _width}mm <= L < {10 * _width + 10}mm.");
                        }
                    }                   
                }               
                _lenght = value;
            }
        }

        /// <summary>
        /// возвращает или задает высоту
        /// </summary>
        public double Height
        {
            get => _height;
            set
            {
                if (value == 0)
                {                   
                    throw new ArgumentException("Invalid chisel height range." +
                        " Сannot be height = 0!");
                }
                if (value < 6 || value > 24)
                {
                    throw new ArgumentException("Invalid chisel height range, please check " +
                        "the entered parameters according to the range: 6mm >= H >= 24mm.");
                }
                else if (value > 6 || value < 24)
                {
                    if (_width != 0 && (_width > 10 || _width < 30))
                    {
                        if (value < 0.6 * _width || value > 0.8 * _width)
                        {
                            throw new ArgumentException($"Invalid chisel height range, please check the" +
                                $" entered parameters according to the range:" +
                                $" {0.6 * _width}mm <= H <= {0.8 * _width}mm.");
                        }
                    }
                }
                _height = value;                                          
            }
        }
        

        /// <summary>
        /// возвращает или задает длину внутреннего выреза зубила
        /// </summary>
        public double InnerLenght
        {
            get => _innerLenght;
            set
            {
                _innerLenght = value;
            }
        }

        /// <summary>
        /// возвращает или задает длину лезвия зубила
        /// </summary>
        public double BladeLenght
        {
            get => _bladeLenght;
            set
            {
                _bladeLenght = value;
            }
        }
    }
}
