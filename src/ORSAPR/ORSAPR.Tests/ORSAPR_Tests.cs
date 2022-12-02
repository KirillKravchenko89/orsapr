using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using ORSAPR.View;
using ORSAPR.Model;
using Kompas6Constants3D;
using Kompas6API5;

namespace ORSAPR.Tests
{
    [TestClass]
    public class ORSAPR_Tests
    {

        /// <summary>
        /// Тест ввода параметров
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="width"></param>
        /// <param name="length"></param>
        /// <param name="height"></param>
        /// <param name="bladeLength"></param>
        /// <param name="innerLength"></param>
        /// <param name="innerWidth"></param>
        [DataTestMethod]   
        [DataRow(false, 0, 0, 0, 0, 0, 0)]//Нулевые значения
        [DataRow(false, 1098, 100, 8, 480, 10, 5)]//Некорректные значения
        [DataRow(true, 30, 300, 24, 150, 75, 15)]//Максимальные значения
        [DataRow(true, 15, 150, 12, 75, 37.5, 7.5)]//Средние значения
        [DataRow(true, 10, 100, 6, 40, 10, 5)]//Минимальные значения    
        public void TestValidation(bool expected, double width, double length, double height,
            double bladeLength, double innerLength, double innerWidth)
        {
            bool actual = true;
            try
            {
                ChiselData chiselData = new ChiselData();
                chiselData.Width = width;
                chiselData.Length = length;
                chiselData.Height = height;
                chiselData.BladeLength = bladeLength;
                chiselData.InnerLength = innerLength;
                chiselData.InnerWidth = innerWidth;
            }
            catch
            {
                actual = false;
            }
            Assert.AreEqual(expected,actual);
        }

        /// <summary>
        /// тест создания Компас коннектора
        /// </summary>
        /// <param name="width"></param>
        /// <param name="length"></param>
        /// <param name="height"></param>
        /// <param name="bladeLength"></param>
        /// <param name="innerLength"></param>
        /// <param name="innerWidth"></param>
        [DataTestMethod]
        [DataRow( 10, 100, 6, 40, 10, 5)]
        public void TestConstructKompasConnector(double width, double length, double height,
            double bladeLength, double innerLength, double innerWidth)
        {
            ChiselData chiselData = new ChiselData();
            chiselData.Width = width;
            chiselData.Length = length;
            chiselData.Height = height;
            chiselData.BladeLength = bladeLength;
            chiselData.InnerLength = innerLength;
            chiselData.InnerWidth = innerWidth;
            var app = new KompasConnector();        
            Assert.IsTrue(app.CreateDocument3D());
        }


        /// <summary>
        /// тест создания Компас коннектора
        /// </summary>
        /// <param name="width"></param>
        /// <param name="length"></param>
        /// <param name="height"></param>
        /// <param name="bladeLength"></param>
        /// <param name="innerLength"></param>
        /// <param name="innerWidth"></param>
        [DataTestMethod]
        [DataRow(10, 100, 6, 40, 10, 5)]
        public void TestCreateDetailLocksmith(double width, double length, double height,
            double bladeLength, double innerLength, double innerWidth)
        {
            ChiselData chiselData = new ChiselData();
            chiselData.Width = width;
            chiselData.Length = length;
            chiselData.Height = height;
            chiselData.BladeLength = bladeLength;
            chiselData.InnerLength = innerLength;
            chiselData.InnerWidth = innerWidth;
            var app = new KompasConnector();
            app.CreateDocument3D();
            var manager = new Manager(app);
            try
            {
                manager.BuildModelLocksmith(chiselData);
                Assert.IsTrue(true);
            }
            catch (Exception)
            {

                Assert.IsTrue(false);
            }
            app.DestructApp();
        }

        /// <summary>
        /// тест создания Компас коннектора
        /// </summary>
        /// <param name="width"></param>
        /// <param name="length"></param>
        /// <param name="height"></param>
        /// <param name="bladeLength"></param>
        /// <param name="innerLength"></param>
        /// <param name="innerWidth"></param>
        [DataTestMethod]
        [DataRow(10, 100, 6, 40, 10, 5)]
        public void TestCreateDetailPeak(double width, double length, double height,
            double bladeLength, double innerLength, double innerWidth)
        {
            ChiselData chiselData = new ChiselData();
            chiselData.Width = width;
            chiselData.Length = length;
            chiselData.Height = height;
            chiselData.BladeLength = bladeLength;
            chiselData.InnerLength = innerLength;
            chiselData.InnerWidth = innerWidth;
            var app = new KompasConnector();
            app.CreateDocument3D();
            var manager = new Manager(app);
            try
            {
                manager.BuildModelPeak(chiselData);
                Assert.IsTrue(true);
            }
            catch (Exception)
            {

                Assert.IsTrue(false);
            }
            app.DestructApp();
        }

        /// <summary>
        /// Тест удаления компас коннектора
        /// </summary>
        /// <param name="width"></param>
        /// <param name="length"></param>
        /// <param name="height"></param>
        /// <param name="bladeLength"></param>
        /// <param name="innerLength"></param>
        /// <param name="innerWidth"></param>
        [DataTestMethod]
        [DataRow(10, 100, 6, 40, 10, 5)]
        public void TestDestructKompasConnector(double width, double length, double height,
            double bladeLength, double innerLength, double innerWidth)
        {
            try
            {
                ChiselData chiselData = new ChiselData();
                chiselData.Width = width;
                chiselData.Length = length;
                chiselData.Height = height;
                chiselData.BladeLength = bladeLength;
                chiselData.InnerLength = innerLength;
                chiselData.InnerWidth = innerWidth;
                var app = new KompasConnector();
                app.DestructApp();
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.IsTrue(false);
            }
            
        }      
    }
}
