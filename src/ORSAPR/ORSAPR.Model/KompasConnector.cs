using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KompasAPI7;
using Kompas6API5;
using Kompas6Constants3D;
using Kompas6Constants;
using System.Runtime.InteropServices;

namespace ORSAPR.Model
{
    public class KompasConnector
    {
        public KompasObject KompasObject
        {
            get;
            private set;
        }
        public ksDocument3D Document3D
        {
            get;
            private set;
        }
        public ChiselData Parameters
        {
            get;
            set;
        }

        public ksPart Chisel
        {
            get;
            private set;
        }

        public KompasConnector()
        {
            if (!GetActiveApp())
            {
                if (!CreateNewApp())
                {               
                    return;                   
                }
            }
        }


        public bool CreateDocument3D()
        {
            Document3D = (ksDocument3D)KompasObject.Document3D();

            if (!Document3D.Create(false/*visible*/, false/*build*/))
            {
                return false;
            }

            Chisel = (ksPart)Document3D.GetPart((short)Part_Type.pTop_Part);
            if (Chisel == null)
            {
                return false;
            }
            return true;
        }     

		private bool GetActiveApp()
		{		
			if (KompasObject == null)
			{
				try
				{
					KompasObject = (KompasObject)Marshal.GetActiveObject("KOMPAS.Application.5");
				}
				catch
				{
					return false;
				}
			}

	
			if (KompasObject == null)
			{
				return false;
			}

			KompasObject.Visible = true;
			KompasObject.ActivateControllerAPI();

			return true;
		}


		private bool CreateNewApp()
		{
			Type t = Type.GetTypeFromProgID("KOMPAS.Application.5");
			KompasObject = (KompasObject)Activator.CreateInstance(t);

			if (KompasObject == null)
			{			
				return false;
			}

			KompasObject.Visible = true;
			KompasObject.ActivateControllerAPI();
            
            return true;
		}


		public void DestructApp()
		{
            KompasObject.Quit();
            KompasObject = null;
			Document3D = null;
			Chisel = null;           
        }
	}
}

