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



        public void BuildModel(ChiselData chiselData)
	    {
         
                var Chisel = _kompasApp.Chisel;
                // получим интерфейс базовой плоскости XOY
                ksEntity planeXOY = (ksEntity)Chisel.GetDefaultEntity((short)Obj3dType.o3d_planeXOY);    // 1-интерфейс на плоскость XOY
                ksEntity iSketch = (ksEntity)Chisel.NewEntity((short)Obj3dType.o3d_sketch);
                if (iSketch != null)
                {
                    // интерфейс свойств эскиза
                    ksSketchDefinition iDefinitionSketch = (ksSketchDefinition)iSketch.GetDefinition();
                    if (iDefinitionSketch != null)
                    {
                        iDefinitionSketch.SetPlane(planeXOY);
                        iDefinitionSketch.angle = 45;           // повернем эскиз на 45 град.
                        iSketch.Create();

                        ksDocument2D iDocument2D = (ksDocument2D)iDefinitionSketch.BeginEdit();

                        iDocument2D.ksLineSeg(-40.0, -25.0, 40.0, -25.0, 1);
                        iDocument2D.ksLineSeg(40.0, -25.0, 40.0, 25.0, 1);
                        iDocument2D.ksLineSeg(40.0, 25.0, -40.0, 25.0, 1);
                        iDocument2D.ksLineSeg(-40.0, 25.0, -40.0, -25.0, 1);

                        iDefinitionSketch.EndEdit();

                        ksEntity entityExtr = (ksEntity)Chisel.NewEntity((short)Obj3dType.o3d_bossExtrusion);
                        if (entityExtr != null)
                        {
                            // интерфейс свойств базовой операции выдавливания
                            ksBossExtrusionDefinition extrusionDef = (ksBossExtrusionDefinition)entityExtr.GetDefinition(); // интерфейс базовой операции выдавливания
                            if (extrusionDef != null)
                            {
                                ksExtrusionParam extrProp = (ksExtrusionParam)extrusionDef.ExtrusionParam(); // интерфейс структуры параметров выдавливания
                                ksThinParam thinProp = (ksThinParam)extrusionDef.ThinParam();      // интерфейс структуры параметров тонкой стенки
                                if (extrProp != null && thinProp != null)
                                {
                                    extrusionDef.SetSketch(iSketch); // эскиз операции выдавливания

                                    extrProp.direction = (short)Direction_Type.dtNormal;      // направление выдавливания (прямое)
                                    extrProp.typeNormal = (short)End_Type.etBlind;      // тип выдавливания (строго на глубину)
                                    extrProp.depthNormal = 100;         // глубина выдавливания
                                    thinProp.thin = true;              // тонкая стенка в два направления
                                    thinProp.normalThickness = 10;      //Толщина стенки в прямом направлении
                                    thinProp.reverseThickness = 10;     //Толщина стенки в обратном направлении
                                    thinProp.thinType = (short)Direction_Type.dtBoth;//Направление формирования тонкой стенки
                                    entityExtr.Create();                // создадим операцию
                                }
                            }
                        }
                    }
                }
                // создадим новый эскиз
                ksEntity entitySketch2 = (ksEntity)Chisel.NewEntity((short)Obj3dType.o3d_sketch);
                if (entitySketch2 != null)
                {
                    // интерфейс свойств эскиза
                    ksSketchDefinition sketchDef2 = (ksSketchDefinition)entitySketch2.GetDefinition();
                    if (sketchDef2 != null)
                    {
                        sketchDef2.SetPlane(planeXOY);  // установим плоскость
                        entitySketch2.Create();         // создадим эскиз

                        // интерфейс редактора эскиза
                        ksDocument2D sketchEdit2 = (ksDocument2D)sketchDef2.BeginEdit();
                        sketchEdit2.ksCircle(0, 0, 100, 1);
                        sketchDef2.EndEdit();   // завершение редактирования эскиза

                        // приклеим выдавливанием
                        ksEntity entityBossExtr = (ksEntity)Chisel.NewEntity((short)Obj3dType.o3d_bossExtrusion);
                        if (entityBossExtr != null)
                        {
                            ksBossExtrusionDefinition bossExtrDef = (ksBossExtrusionDefinition)entityBossExtr.GetDefinition();
                            if (bossExtrDef != null)
                            {
                                ksExtrusionParam extrProp = (ksExtrusionParam)bossExtrDef.ExtrusionParam(); // интерфейс структуры параметров выдавливания
                                ksThinParam thinProp = (ksThinParam)bossExtrDef.ThinParam();      // интерфейс структуры параметров тонкой стенки
                                if (extrProp != null && thinProp != null)
                                {
                                    bossExtrDef.SetSketch(entitySketch2); // эскиз операции выдавливания

                                    extrProp.direction = (short)Direction_Type.dtReverse;      // направление выдавливания (обратное)
                                    extrProp.typeNormal = (short)End_Type.etBlind;      // тип выдавливания (строго на глубину)
                                    extrProp.depthReverse = 50;         // глубина выдавливания
                                    thinProp.thin = false;              // без тонкой стенки
                                    entityBossExtr.Create();                // создадим операцию
                                }
                            }
                        }
                    }
                
            }



        }
    }
}
