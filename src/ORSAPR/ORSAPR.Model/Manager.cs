using Kompas6API5;
using Kompas6Constants3D;
using System;

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


        private ksDocument2D CalculateTopSketchParamethers(ChiselData chiselData, ksDocument2D iDocument2D)
        {
            double x0, y0, x1, y1;

            x0 = -0.5 * chiselData.Width;
            y0 = -0.5 * chiselData.Length;
            x1 = 0.5 * chiselData.Width;
            y1 = 0.5 * chiselData.Length;

            iDocument2D.ksLineSeg(x0, y0, x1, y0, 1);
            iDocument2D.ksLineSeg(x1, y0, x1, y1, 1);
            iDocument2D.ksLineSeg(x1, y1, x0, y1, 1);
            iDocument2D.ksLineSeg(x0, y1, x0, y0, 1);    

            return iDocument2D;
        }

        private ksDocument2D CalculateInnerCotoutSketchParamethers(ChiselData chiselData,
            ksDocument2D iDocument2D)
        {
            double x0, y0, x1, y1;

            x0 = -(chiselData.Width / 2) + (0.5 * (chiselData.Width - chiselData.InnerWidth));
            y0 = -((0.5 * chiselData.Length) - (0.15 * chiselData.Length));

            x1 = 0.5 * chiselData.Width - (0.5 * (chiselData.Width - chiselData.InnerWidth));
            y1= -(0.5 * chiselData.Length) + (0.15 * chiselData.Length)+ chiselData.InnerLength;

            iDocument2D.ksLineSeg(x0, y0, x0, y1, 1);
            iDocument2D.ksLineSeg(x0, y1, x1, y1, 1);
            iDocument2D.ksLineSeg(x1, y1, x1, y0, 1);
            iDocument2D.ksLineSeg(x1, y0, x0, y0, 1);
 
            return iDocument2D;
        }


        public void BuildModel(ChiselData chiselData)
        {
           Top(chiselData);
            InnerCutout(chiselData);

            BladeChamfer(chiselData);

            HandleFillet(chiselData);
        }

        private void Top(ChiselData chiselData)
        {
            var Chisel = _kompasApp.Chisel;
            // получим интерфейс базовой плоскости XOY
            ksEntity planeXOY = (ksEntity)Chisel.GetDefaultEntity((short)Obj3dType.o3d_planeXOY);
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

                    CalculateTopSketchParamethers(chiselData, iDocument2D);

                    iDefinitionSketch.EndEdit();

                    ksEntity entityExtr = (ksEntity)Chisel.
                        NewEntity((short)Obj3dType.o3d_bossExtrusion);
                    if (entityExtr != null)
                    {
                        // интерфейс свойств базовой операции выдавливания
                        // интерфейс базовой операции выдавливания
                        ksBossExtrusionDefinition extrusionDef =
                            (ksBossExtrusionDefinition)entityExtr.GetDefinition();
                        if (extrusionDef != null)
                        {// интерфейс структуры параметров выдавливания
                            ksExtrusionParam extrProp =
                                (ksExtrusionParam)extrusionDef.ExtrusionParam();
                            if (extrProp != null)
                            {
                                extrusionDef.SetSketch(iSketch); // эскиз операции выдавливания
                                // направление выдавливания (прямое)
                                extrProp.direction = (short)Direction_Type.dtMiddlePlane;
                                // тип выдавливания (строго на глубину)
                                extrProp.typeNormal = (short)End_Type.etBlind;
                                // глубина выдавливания    
                                extrProp.depthReverse = chiselData.Height;
                                // создадим операцию
                                entityExtr.Create();                             
                            }                         
                        }
                    }
                }                
            }
        }
        private void InnerCutout(ChiselData chiselData)
        {
            var Chisel = _kompasApp.Chisel;
            // создадим новый эскиз         
            ksEntity entitySketch2 = (ksEntity)Chisel.NewEntity((short)Obj3dType.o3d_sketch);
            ksEntity ksEntityPlaneOffset =
                (ksEntity)Chisel.NewEntity((short)Obj3dType.o3d_planeOffset);

            ksEntity ksEntityPlaneXOY =
                (ksEntity)Chisel.GetDefaultEntity((short)Obj3dType.o3d_planeXOY);
            ksPlaneOffsetDefinition ksPlaneOffsetDefinition =
                (ksPlaneOffsetDefinition)ksEntityPlaneOffset.GetDefinition();
            ksPlaneOffsetDefinition.SetPlane(ksEntityPlaneXOY);
            ksPlaneOffsetDefinition.direction = true;
            ksPlaneOffsetDefinition.offset = 0.5 * chiselData.Height;
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

                    CalculateInnerCotoutSketchParamethers(chiselData, iDocument2D);

                    sketchDef2.EndEdit();

                    ksEntity entityExtr =
                        (ksEntity)Chisel.NewEntity((short)Obj3dType.o3d_cutExtrusion);
                    if (entityExtr != null)
                    {
                        // интерфейс свойств базовой операции выдавливания
                        ksCutExtrusionDefinition extrusionDef =
                            (ksCutExtrusionDefinition)entityExtr.GetDefinition(); 

                        if (extrusionDef != null)
                        {
                            ksExtrusionParam extrProp = 
                                (ksExtrusionParam)extrusionDef.ExtrusionParam();

                            if (extrProp != null)
                            {
                                extrusionDef.SetSketch(entitySketch2); 

                                extrProp.direction = (short)Direction_Type.dtNormal;    
                                extrProp.typeNormal = (short)End_Type.etBlind;    
                                extrProp.depthNormal = chiselData.Height / 6;          
                                entityExtr.Create();                
                            }
                        }
                    }
                }
            }
        }
        
       
        private void BladeChamfer(ChiselData chiselData)
        {
            var Chisel = _kompasApp.Chisel;
            ksEntity iChamfer = (ksEntity)Chisel.NewEntity((short)Obj3dType.o3d_chamfer);
            if (iChamfer != null)
            {
                ksChamferDefinition chamferDefinition =
                    (ksChamferDefinition)iChamfer.GetDefinition();
                if (chamferDefinition != null)
                {
                    chamferDefinition.tangent = true;
                    chamferDefinition.SetChamferParam(true, chiselData.BladeLength,
                        chiselData.BladeLength / 40);
                    var ar = chamferDefinition.array();

                    EntityCollection iCollection = Chisel.
                        EntityCollection((short)Obj3dType.o3d_edge);
                    iCollection.SelectByPoint(-0.5 * chiselData.Width, 0.5 * chiselData.Length, 1);
                    ar.Add(iCollection.Last());

                    iCollection = Chisel.EntityCollection((short)Obj3dType.o3d_edge);
                    iCollection.SelectByPoint(0.5 * chiselData.Width, 0.5 * chiselData.Length, 1);
                    ar.Add(iCollection.Last());

                    iChamfer.Create();
                }
            }

            ksEntity iChamfer2 = (ksEntity)Chisel.NewEntity((short)Obj3dType.o3d_chamfer);
            if (iChamfer2 != null)
            {
                ksChamferDefinition chamferDefinition =
                    (ksChamferDefinition)iChamfer2.GetDefinition();
                if (chamferDefinition != null)
                {
                    chamferDefinition.tangent = true;
                    chamferDefinition.SetChamferParam(false, 0.5 * chiselData.Height * 0.7662626,
                        chiselData.BladeLength);

                    var ar = chamferDefinition.array();

                    EntityCollection iCollection =
                        Chisel.EntityCollection((short)Obj3dType.o3d_edge);
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
                ksChamferDefinition chamferDefinition =
                    (ksChamferDefinition)iChamfer3.GetDefinition();

                if (chamferDefinition != null)
                {
                    chamferDefinition.tangent = true;
                    chamferDefinition.SetChamferParam(false, chiselData.BladeLength / 40,
                        (0.7 / 3) * chiselData.Height / 2);
                    var ar = chamferDefinition.array();

                    EntityCollection iCollection =
                        Chisel.EntityCollection((short)Obj3dType.o3d_edge);

                    iCollection.SelectByPoint(0, 0.5 * chiselData.Length,
                        (0.5 * chiselData.Height) - (0.5 * chiselData.Height * 0.7662626));
                    ar.Add(iCollection.Last());
                    iCollection = Chisel.EntityCollection((short)Obj3dType.o3d_edge);
                    iCollection.SelectByPoint(0, 0.5 * chiselData.Length,
                        -(0.5 * chiselData.Height) + (0.5 * chiselData.Height * 0.7662626));
                    ar.Add(iCollection.Last());

                    iChamfer3.Create();
                }
            }
        }

        private void HandleFillet(ChiselData chiselData)
        {
            var Chisel = _kompasApp.Chisel;
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

                    //сзади
                    EntityCollection iCollection =
                        Chisel.EntityCollection((short)Obj3dType.o3d_face);
                    iCollection.SelectByPoint(1, -chiselData.Length / 2, 1);
                    araa.Add(iCollection.Last());

                    //вырез
                    iCollection = Chisel.EntityCollection((short)Obj3dType.o3d_face);
                    iCollection.SelectByPoint(1, -(0.5 * chiselData.Length) + (0.2 * chiselData.Length),
                        (0.5 * chiselData.Height) - (chiselData.Height / 6));
                    araa.Add(iCollection.Last());

                    iCollection = Chisel.EntityCollection((short)Obj3dType.o3d_face);
                    iCollection.SelectByPoint(
                       -(0.5 * chiselData.Width) + (0.5 * (chiselData.Width - chiselData.InnerWidth)),
                       -(0.5 * chiselData.Length) + (0.17 * chiselData.Length),
                         0.5 * chiselData.Height - 0.8);
                    araa.Add(iCollection.Last());

                    iCollection = Chisel.EntityCollection((short)Obj3dType.o3d_face);
                    iCollection.SelectByPoint(
                        0.5 * chiselData.Width - (0.5 * (chiselData.Width - chiselData.InnerWidth)),
                      -(0.5 * chiselData.Length) + (0.17 * chiselData.Length),
                        0.5 * chiselData.Height - 0.8);
                    araa.Add(iCollection.Last());

                    iCollection = Chisel.EntityCollection((short)Obj3dType.o3d_face);
                    iCollection.SelectByPoint(1,
                        -(0.5 * chiselData.Length) + (0.15 * chiselData.Length) + chiselData.InnerLength,
                        0.5 * chiselData.Height - 0.8);
                    araa.Add(iCollection.Last());

                    iCollection = Chisel.EntityCollection((short)Obj3dType.o3d_face);
                    iCollection.SelectByPoint(1,
                        -((0.5 * chiselData.Length) - (0.15 * chiselData.Length)),
                        0.5 * chiselData.Height - 0.8);
                    araa.Add(iCollection.Last());

                    //ребра
                    iCollection = Chisel.EntityCollection((short)Obj3dType.o3d_edge);
                    iCollection.SelectByPoint(-(chiselData.Width / 2),
                        -0.5 * chiselData.Length + 10, 0.5 * chiselData.Height);
                    araa.Add(iCollection.Last());

                    iCollection = Chisel.EntityCollection((short)Obj3dType.o3d_edge);
                    iCollection.SelectByPoint(chiselData.Width / 2,
                        -0.5 * chiselData.Length + 10, 0.5 * chiselData.Height);
                    araa.Add(iCollection.Last());

                    iCollection = Chisel.EntityCollection((short)Obj3dType.o3d_edge);
                    iCollection.SelectByPoint(-(chiselData.Width / 2),
                        -0.5 * chiselData.Length + 10, -0.5 * chiselData.Height);
                    araa.Add(iCollection.Last());

                    iCollection = Chisel.EntityCollection((short)Obj3dType.o3d_edge);
                    iCollection.SelectByPoint(chiselData.Width / 2,
                        -0.5 * chiselData.Length + 10, -0.5 * chiselData.Height);
                    araa.Add(iCollection.Last());

                    iFillet.Create();
                }
            }
        } 
    }
}

