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
    public class Parameter
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
        /// возвращает или задает высоту
        /// </summary>
        public double Height
        {
            get => _height;
            set
            {
                _height = value;
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
                _lenght = value;
            }
        }

        /// <summary>
        /// возвращает или задает ширину
        /// </summary>
        public double Width
        {
            get => _width;
            set
            {
                _width = value;
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
