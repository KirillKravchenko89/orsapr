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
    /// <summary>
    /// класс Менеджера
    /// </summary>
	public class Manager
    {
        /// <summary>
        /// выделение памяти под объект компас коннектора
        /// </summary>
		public KompasConnector _kompasApp = new KompasConnector();

        /// <summary>
        /// Конструктор с вложенным классом
        /// </summary>
        /// <param name="kompasApp"></param>
        public Manager(KompasConnector kompasApp)
        {
            if (kompasApp == null)
            {
                return;
            }
            _kompasApp = kompasApp;
        }

        /// <summary>
        /// метод создания детали
        /// </summary>
        /// <param name="chiselData"></param>
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
                    iSketch.Create();
                    ksDocument2D iDocument2D = (ksDocument2D)iDefinitionSketch.BeginEdit();

                    iDocument2D.ksLineSeg(-0.5 * chiselData.Width, -0.5 * chiselData.Length, 0.5 * chiselData.Width, -0.5 * chiselData.Length, 1);
                    iDocument2D.ksLineSeg(0.5 * chiselData.Width, -0.5 * chiselData.Length, 0.5 * chiselData.Width, 0.5 * chiselData.Length, 1);
                    iDocument2D.ksLineSeg(0.5 * chiselData.Width, 0.5 * chiselData.Length, -0.5 * chiselData.Width, 0.5 * chiselData.Length, 1);
                    iDocument2D.ksLineSeg(-0.5 * chiselData.Width, 0.5 * chiselData.Length, -0.5 * chiselData.Width, -0.5 * chiselData.Length, 1);
                    iDefinitionSketch.EndEdit();

                    ksEntity entityExtr = (ksEntity)Chisel.NewEntity((short)Obj3dType.o3d_bossExtrusion);
                    if (entityExtr != null)
                    {
                        // интерфейс свойств базовой операции выдавливания
                        ksBossExtrusionDefinition extrusionDef = (ksBossExtrusionDefinition)entityExtr.GetDefinition(); // интерфейс базовой операции выдавливания
                        if (extrusionDef != null)
                        {
                            ksExtrusionParam extrProp = (ksExtrusionParam)extrusionDef.ExtrusionParam(); // интерфейс структуры параметров выдавливания
                            if (extrProp != null)
                            {
                                extrusionDef.SetSketch(iSketch); // эскиз операции выдавливания
                                extrProp.direction = (short)Direction_Type.dtMiddlePlane;      // направление выдавливания (прямое)
                                extrProp.typeNormal = (short)End_Type.etBlind;      // тип выдавливания (строго на глубину)
                                extrProp.depthReverse = chiselData.Height;         // глубина выдавливания        
                                entityExtr.Create();                // создадим операцию
                            }
                        }
                    }             
                }
            }
           
            ksEntity iChamfer = (ksEntity)Chisel.NewEntity((short)Obj3dType.o3d_chamfer);           
            if (iChamfer != null)
            {                      
                    ksChamferDefinition chamferDefinition = (ksChamferDefinition)iChamfer.GetDefinition();               
                if (chamferDefinition != null)
                {                
                    chamferDefinition.tangent = true;
                    chamferDefinition.SetChamferParam(true, chiselData.BladeLength , chiselData.BladeLength/40);
                    var ar = chamferDefinition.array();

                    EntityCollection iCollection = Chisel.EntityCollection((short)Obj3dType.o3d_edge);
                    iCollection.SelectByPoint(-chiselData.Width/2, 0.5 * chiselData.Length , 1);
                     ar.Add(iCollection.Last());
                    
                    iCollection = Chisel.EntityCollection((short)Obj3dType.o3d_edge);
                    iCollection.SelectByPoint(chiselData.Width/2, 0.5 * chiselData.Length, 1);
                    ar.Add(iCollection.Last());

                    iChamfer.Create();              
                }
            }

            ksEntity iChamfer2 = (ksEntity)Chisel.NewEntity((short)Obj3dType.o3d_chamfer);
            if (iChamfer2 != null)
            {
                ksChamferDefinition chamferDefinition = (ksChamferDefinition)iChamfer2.GetDefinition();
                if (chamferDefinition != null)
                {                  
                    chamferDefinition.tangent = true;
                    chamferDefinition.SetChamferParam(false, 0.5 * chiselData.Height * 0.7662626, chiselData.BladeLength);
                    var ar = chamferDefinition.array();

                    EntityCollection iCollection = Chisel.EntityCollection((short)Obj3dType.o3d_edge);
                    iCollection.SelectByPoint(0, 0.5 * chiselData.Length, 0.5 * chiselData.Height);
                    ar.Add(iCollection.Last());

                    iCollection = Chisel.EntityCollection((short)Obj3dType.o3d_edge);
                    iCollection.SelectByPoint(0, 0.5 * chiselData.Length, -0.5 * chiselData.Height);
                    ar.Add(iCollection.Last());

                    iChamfer2.Create();
                }
            }

            ksEntity iChamfer3 = (ksEntity)Chisel.NewEntity((short)Obj3dType.o3d_chamfer);

            if (iChamfer3 != null)
            {               
                ksChamferDefinition chamferDefinition = (ksChamferDefinition)iChamfer3.GetDefinition();

                if (chamferDefinition != null)
                {
                    chamferDefinition.tangent = true;
                    chamferDefinition.SetChamferParam(false, chiselData.BladeLength/40, (0.7/3)*chiselData.Height/2);
                    var ar = chamferDefinition.array();

                    EntityCollection iCollection = Chisel.EntityCollection((short)Obj3dType.o3d_edge);
                    iCollection.SelectByPoint(0, 0.5 * chiselData.Length, (0.5 * chiselData.Height) -(0.5 * chiselData.Height * 0.7662626));
                    ar.Add(iCollection.Last());
                    iCollection = Chisel.EntityCollection((short)Obj3dType.o3d_edge);
                    iCollection.SelectByPoint(0, 0.5 * chiselData.Length, -(0.5 * chiselData.Height) + (0.5 * chiselData.Height * 0.7662626));
                    ar.Add(iCollection.Last());

                    iChamfer3.Create();
                }
            }

            // создадим новый эскиз         
            ksEntity entitySketch2 = (ksEntity)Chisel.NewEntity((short)Obj3dType.o3d_sketch);
            ksEntity ksEntityPlaneOffset = (ksEntity)Chisel.NewEntity((short)Obj3dType.o3d_planeOffset);

            ksEntity ksEntityPlaneXOY = (ksEntity)Chisel.GetDefaultEntity((short)Obj3dType.o3d_planeXOY);
            ksPlaneOffsetDefinition ksPlaneOffsetDefinition = (ksPlaneOffsetDefinition)ksEntityPlaneOffset.GetDefinition();
            ksPlaneOffsetDefinition.SetPlane(ksEntityPlaneXOY);
            ksPlaneOffsetDefinition.direction = true;
            ksPlaneOffsetDefinition.offset = 0.5* chiselData.Height;
            ksEntityPlaneOffset.Create();

            if (entitySketch2 != null)
            {
                // интерфейс свойств эскиза
                ksSketchDefinition sketchDef2 = (ksSketchDefinition)entitySketch2.GetDefinition();
                if (sketchDef2 != null)
                {                  
                    sketchDef2.SetPlane(ksEntityPlaneOffset);  // установим плоскость
                    entitySketch2.Create();         // создадим эскиз
                    ksDocument2D iDocument2D = (ksDocument2D)sketchDef2.BeginEdit();
                 
                    iDocument2D.ksLineSeg(-(chiselData.Width/2) + (0.5*(chiselData.Width - chiselData.InnerWidth)), -((0.5 * chiselData.Length) - (0.15 * chiselData.Length)),
                                          -(chiselData.Width / 2) + (0.5 * (chiselData.Width - chiselData.InnerWidth)), -(0.5 * chiselData.Length) + (0.15 * chiselData.Length) + chiselData.InnerLength, 1);

                    iDocument2D.ksLineSeg(-(chiselData.Width / 2) + (0.5 * (chiselData.Width - chiselData.InnerWidth)), -(0.5 * chiselData.Length) + (0.15 * chiselData.Length) + chiselData.InnerLength,
                                          0.5 *chiselData.Width - (0.5*(chiselData.Width - chiselData.InnerWidth)), -(0.5 * chiselData.Length) + (0.15 * chiselData.Length) + chiselData.InnerLength, 1);

                    iDocument2D.ksLineSeg(0.5 * chiselData.Width - (0.5 * (chiselData.Width - chiselData.InnerWidth)), -(0.5 * chiselData.Length) + (0.15 * chiselData.Length) + chiselData.InnerLength,
                                           0.5 * chiselData.Width - (0.5 *(chiselData.Width - chiselData.InnerWidth)), -((0.5 * chiselData.Length) - (0.15 * chiselData.Length)), 1);
                    iDocument2D.ksLineSeg(0.5 * chiselData.Width - (0.5 *(chiselData.Width - chiselData.InnerWidth)), -((0.5 * chiselData.Length) - (0.15 * chiselData.Length)),
                                         -(chiselData.Width / 2) + 0.5 * (chiselData.Width - chiselData.InnerWidth), -((0.5 * chiselData.Length) - (0.15 * chiselData.Length)), 1);
                    sketchDef2.EndEdit();

                    ksEntity entityExtr = (ksEntity)Chisel.NewEntity((short)Obj3dType.o3d_cutExtrusion);
                    if (entityExtr != null)
                    {
                        // интерфейс свойств базовой операции выдавливания
                        ksCutExtrusionDefinition extrusionDef = (ksCutExtrusionDefinition)entityExtr.GetDefinition(); // интерфейс базовой операции выдавливания

                        if (extrusionDef != null)
                        {
                            ksExtrusionParam extrProp = (ksExtrusionParam)extrusionDef.ExtrusionParam(); // интерфейс структуры параметров выдавливания

                            if (extrProp != null)
                            {
                                extrusionDef.SetSketch(entitySketch2); // эскиз операции выдавливания

                                extrProp.direction = (short)Direction_Type.dtNormal;      // направление выдавливания (прямое)
                                extrProp.typeNormal = (short)End_Type.etBlind;      // тип выдавливания (строго на глубину)
                                extrProp.depthNormal = chiselData.Height / 6;         // глубина выдавливания        
                                entityExtr.Create();                // создадим операцию
                            }
                        }
                    }
                }
            }

            /// Создание скругления
            ksEntity iFillet = (ksEntity)Chisel.NewEntity((short)Obj3dType.o3d_fillet);
            if (iFillet != null)
            {
                ksFilletDefinition filletDefenition = (ksFilletDefinition)iFillet.GetDefinition();
                if (filletDefenition != null)
                {
                    filletDefenition.radius = chiselData.Height / 6;
                    filletDefenition.tangent = true;
                    var araa = filletDefenition.array();

                    //жопа
                    EntityCollection iCollection = Chisel.EntityCollection((short)Obj3dType.o3d_face);
                    iCollection.SelectByPoint(1, -chiselData.Length / 2, 1);
                    araa.Add(iCollection.Last());            

                    //дырка
                    iCollection = Chisel.EntityCollection((short)Obj3dType.o3d_face);
                    iCollection.SelectByPoint(1, -(0.5*chiselData.Length) + (0.2*chiselData.Length), (0.5*chiselData.Height) - (chiselData.Height / 6));
                    araa.Add(iCollection.Last());
                    
                    iCollection = Chisel.EntityCollection((short)Obj3dType.o3d_face);
                    iCollection.SelectByPoint(-(chiselData.Width / 2) + (0.5 * (chiselData.Width - chiselData.InnerWidth)), -(0.5 * chiselData.Length) + (0.17 * chiselData.Length), 0.5 * chiselData.Height - 0.8);
                    araa.Add(iCollection.Last());

                    iCollection = Chisel.EntityCollection((short)Obj3dType.o3d_face);
                    iCollection.SelectByPoint(0.5 * chiselData.Width - (0.5 * (chiselData.Width - chiselData.InnerWidth)), -(0.5 * chiselData.Length) + (0.17 * chiselData.Length), 0.5 * chiselData.Height - 0.8);
                    araa.Add(iCollection.Last());

                    iCollection = Chisel.EntityCollection((short)Obj3dType.o3d_face);
                    iCollection.SelectByPoint(1, -(0.5 * chiselData.Length) + (0.15 * chiselData.Length) + chiselData.InnerLength, 0.5 * chiselData.Height - 0.8);
                    araa.Add(iCollection.Last());

                    iCollection = Chisel.EntityCollection((short)Obj3dType.o3d_face);
                    iCollection.SelectByPoint(1, -((0.5 * chiselData.Length) - (0.15 * chiselData.Length)), 0.5*chiselData.Height - 0.8);
                    araa.Add(iCollection.Last());

                    //ребра
                    iCollection = Chisel.EntityCollection((short)Obj3dType.o3d_edge);
                    iCollection.SelectByPoint(-(chiselData.Width / 2), -0.5*chiselData.Length+10, 0.5 * chiselData.Height);
                    araa.Add(iCollection.Last());

                    iCollection = Chisel.EntityCollection((short)Obj3dType.o3d_edge);
                    iCollection.SelectByPoint(chiselData.Width / 2, -0.5 * chiselData.Length + 10, 0.5 * chiselData.Height);
                    araa.Add(iCollection.Last());

                    iCollection = Chisel.EntityCollection((short)Obj3dType.o3d_edge);
                    iCollection.SelectByPoint(-(chiselData.Width / 2), -0.5 * chiselData.Length + 10, -0.5 * chiselData.Height);
                    araa.Add(iCollection.Last());

                    iCollection = Chisel.EntityCollection((short)Obj3dType.o3d_edge);
                    iCollection.SelectByPoint(chiselData.Width / 2, -0.5 * chiselData.Length + 10, -0.5 * chiselData.Height);
                    araa.Add(iCollection.Last());

                    iFillet.Create();
                }
            }
        }
    }
}

