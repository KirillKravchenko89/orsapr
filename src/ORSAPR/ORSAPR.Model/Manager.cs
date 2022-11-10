using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KompasAPI7;
using Kompas6API5;
using Kompas6Constants3D;
using Kompas6Constants;

namespace ORSAPR.Model
{
	public class Manager
	{


		public KompasConnector _kompasApp = new KompasConnector();

        //старая версия
        public Manager(KompasConnector kompasApp)
        {
            if (kompasApp == null)
            {
                return;
            }
            _kompasApp = kompasApp;
        }



        public void/*bool*/ BuildModel()
	    {

          
           









            //для bool с отдельной функцией создания зубила
            /*var chisel = CreateChisel();
            if (chisel == null)
            {
                return false;
            }

            return true;*/
        }

		//для buildmanager старая вариация с загрузкой компаса
            /*private ksEntity CreateChisel()
            {

				return null;
			}*/

        
	}
}
