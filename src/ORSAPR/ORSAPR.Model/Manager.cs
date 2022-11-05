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


		private KompasConnector _kompasApp;


		public Manager(KompasConnector kompasApp)
		{
			if (kompasApp == null)
			{
				return;
			}
			_kompasApp = kompasApp;
		}

		//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public struct KompasPoint2D
		{
			public double X
			{
				get;
				set;
			}
			public double Y
			{
				get;
				set;
			}

			public KompasPoint2D(double xc, double yc)
			{
				X = xc;
				Y = yc;
			}


			public struct MufflerParameters
			{

				public ksPart Document3DPart;
				public Obj3dType BasePlaneAxis;
				public Direction_Type Direction;
				public KompasPoint2D BasePlanePoint;
				public MufflerParameters(ksPart document3DPart, Obj3dType basePlaneAxis, Direction_Type direction, KompasPoint2D basePlanePoint)
				{
					Document3DPart = document3DPart;
					BasePlaneAxis = basePlaneAxis;
					Direction = direction;
					BasePlanePoint = basePlanePoint;
				}
			}

			public bool BuildModel()
			{


                var chisel = CreateChisel();
                if (chisel == null)
                {
                    return false;
                }

                return true;
            }

            private ksEntity CreateChisel()
            {

				return null;
			}

        }
	}
}
