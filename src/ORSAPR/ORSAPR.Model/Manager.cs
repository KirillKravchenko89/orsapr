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

        /// <summary>
        /// функция рассчета параметров эскиза слесарного зубила
        /// </summary>
        /// <param name="chiselData"></param>
        /// <param name="iDocument2D"></param>
        /// <returns></returns>
        private ksDocument2D CalculateTopSketchParamethersLocksmith(ChiselData chiselData,
            ksDocument2D iDocument2D)
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

        /// <summary>
        /// функция расчета параметров для выреза слесарного зубила
        /// </summary>
        /// <param name="chiselData"></param>
        /// <param name="iDocument2D"></param>
        /// <returns></returns>
        private ksDocument2D CalculateInnerCotoutSketchParamethersLocksmith(ChiselData chiselData,
            ksDocument2D iDocument2D)
        {
            double x0, y0, x1, y1;

            x0 = -(chiselData.Width / 2) + (0.5 * (chiselData.Width - chiselData.InnerWidth));
            y0 = -((0.5 * chiselData.Length) - (0.15 * chiselData.Length));

            x1 = 0.5 * chiselData.Width - (0.5 * (chiselData.Width - chiselData.InnerWidth));
            y1 = -(0.5 * chiselData.Length) + (0.15 * chiselData.Length) + chiselData.InnerLength;

            iDocument2D.ksLineSeg(x0, y0, x0, y1, 1);
            iDocument2D.ksLineSeg(x0, y1, x1, y1, 1);
            iDocument2D.ksLineSeg(x1, y1, x1, y0, 1);
            iDocument2D.ksLineSeg(x1, y0, x0, y0, 1);

            return iDocument2D;
        }

        /// <summary>
        /// функция сборки слесарного зубила
        /// </summary>
        /// <param name="chiselData"></param>
        public void BuildModelLocksmith(ChiselData chiselData)
        {
            TopLocksmith(chiselData);
            InnerCutoutLocksmith(chiselData);
            BladeChamferLocksmith(chiselData);
            HandleFilletLocksmith(chiselData);
        }

        /// <summary>
        /// функция сборки пикового зубила
        /// </summary>
        /// <param name="chiselData"></param>
        public void BuildModelPeak(ChiselData chiselData)
        {
            TopPeak(chiselData);
            BladePeak(chiselData);
            BladeChamferPeak(chiselData);
            HandleFilletPeak(chiselData);
        }

        /// <summary>
        /// функция построения основания зубила пики
        /// </summary>
        /// <param name="chiselData"></param>
        private void TopPeak(ChiselData chiselData)
        {
            var Chisel = _kompasApp.Chisel;
            // Эскиз ручки
            ksEntity planeXOZ = (ksEntity)Chisel.GetDefaultEntity((short)Obj3dType.o3d_planeXOZ);
            ksEntity SketchTop = (ksEntity)Chisel.NewEntity((short)Obj3dType.o3d_sketch);
            if (SketchTop != null)
            {
                // интерфейс свойств эскиза
                ksSketchDefinition sketchDef = (ksSketchDefinition)SketchTop.GetDefinition();
                if (sketchDef != null)
                {
                    sketchDef.SetPlane(planeXOZ);
                    SketchTop.Create();

                    ksDocument2D sketchEdit2 = (ksDocument2D)sketchDef.BeginEdit();
                    sketchEdit2.ksCircle(0, 0, 0.5 * chiselData.Height, 1);
                    sketchDef.EndEdit();

                    // приклеим выдавливанием
                    ksEntity entityBossExtr =
                        (ksEntity)Chisel.NewEntity((short)Obj3dType.o3d_bossExtrusion);
                    if (entityBossExtr != null)
                    {
                        ksBossExtrusionDefinition bossExtrDef =
                            (ksBossExtrusionDefinition)entityBossExtr.GetDefinition();
                        if (bossExtrDef != null)
                        {
                            ksExtrusionParam extrProp =
                                (ksExtrusionParam)bossExtrDef.ExtrusionParam(); 

                            if (extrProp != null)
                            {
                                bossExtrDef.SetSketch(SketchTop); // эскиз операции выдавливания

                                extrProp.direction = (short)Direction_Type.dtBoth;
                                extrProp.typeNormal = (short)End_Type.etBlind;
                                extrProp.depthReverse = 0.5 * chiselData.Length;
                                extrProp.depthNormal = (0.5 * chiselData.Length)
                                    - (0.5 * chiselData.BladeLength);
                                entityBossExtr.Create();                // создадим операцию
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// функция построения лезвия зубила пики
        /// </summary>
        /// <param name="chiselData"></param>
        private void BladePeak(ChiselData chiselData)
        {
            var Chisel = _kompasApp.Chisel;
            //эскиз лезвия
            ksEntity SketchBlade = (ksEntity)Chisel.NewEntity((short)Obj3dType.o3d_sketch);
            ksEntity ksEntityPlaneOffset =
                (ksEntity)Chisel.NewEntity((short)Obj3dType.o3d_planeOffset);

            ksEntity ksEntityPlaneXOZ =
                (ksEntity)Chisel.GetDefaultEntity((short)Obj3dType.o3d_planeXOZ);
            ksPlaneOffsetDefinition ksPlaneOffsetDefinition =
                (ksPlaneOffsetDefinition)ksEntityPlaneOffset.GetDefinition();
            ksPlaneOffsetDefinition.SetPlane(ksEntityPlaneXOZ);
            ksPlaneOffsetDefinition.direction = true;
            ksPlaneOffsetDefinition.offset = (0.5 * chiselData.Length) 
                - (0.5 * chiselData.BladeLength);
            ksEntityPlaneOffset.Create();

            if (SketchBlade != null)
            {
                // интерфейс свойств эскиза
                ksSketchDefinition sketchDef2 = (ksSketchDefinition)SketchBlade.GetDefinition();
                if (sketchDef2 != null)
                {
                    sketchDef2.SetPlane(ksEntityPlaneOffset);  // установим плоскость
                    SketchBlade.Create();         // создадим эскиз
                    ksDocument2D iDocument2D = (ksDocument2D)sketchDef2.BeginEdit();
                    //method Calculate
                    double x0 = -0.5 * chiselData.Height;
                    double y0 = -0.5 * chiselData.Width;
                    double x1 = 0.5 * chiselData.Height;
                    double y1 = 0.5 * chiselData.Width;
                    iDocument2D.ksLineSeg(x0, y0, x0, y1, 1);
                    iDocument2D.ksLineSeg(x0, y1, x1, y1, 1);
                    iDocument2D.ksLineSeg(x1, y1, x1, y0, 1);
                    iDocument2D.ksLineSeg(x1, y0, x0, y0, 1);

                    sketchDef2.EndEdit();

                    ksEntity entityExtr =
                        (ksEntity)Chisel.NewEntity((short)Obj3dType.o3d_bossExtrusion);
                    if (entityExtr != null)
                    {
                        // интерфейс свойств базовой операции выдавливания
                        ksBossExtrusionDefinition extrusionDef =
                            (ksBossExtrusionDefinition)entityExtr.GetDefinition();

                        if (extrusionDef != null)
                        {
                            ksExtrusionParam extrProp =
                                (ksExtrusionParam)extrusionDef.ExtrusionParam();

                            if (extrProp != null)
                            {
                                extrusionDef.SetSketch(SketchBlade);

                                extrProp.direction = (short)Direction_Type.dtNormal;
                                extrProp.typeNormal = (short)End_Type.etBlind;
                                extrProp.depthNormal = 0.5 * chiselData.BladeLength;
                                entityExtr.Create();
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// функция построения фасок зубила пики
        /// </summary>
        /// <param name="chiselData"></param>
        private void BladeChamferPeak(ChiselData chiselData)
        {
            var Chisel = _kompasApp.Chisel;
            // эскиз фаски вверх
            ksEntity iChamfer = (ksEntity)Chisel.NewEntity((short)Obj3dType.o3d_chamfer);
            if (iChamfer != null)
            {
                ksChamferDefinition chamferDefinition =
                    (ksChamferDefinition)iChamfer.GetDefinition();
                if (chamferDefinition != null)
                {
                    chamferDefinition.tangent = true;
                    chamferDefinition.SetChamferParam(true, 0.5 * chiselData.Height,
                        0.5 * chiselData.BladeLength);
                    var ar = chamferDefinition.array();

                    EntityCollection iCollection =
                        Chisel.EntityCollection((short)Obj3dType.o3d_edge);
                    iCollection.SelectByPoint(0, (0.5 * chiselData.Length)
                        - (0.5 * chiselData.BladeLength), 0.5 * chiselData.Height);
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
                    chamferDefinition.SetChamferParam(false, 0.5 * chiselData.BladeLength,
                        chiselData.BladeLength / 40);
                    var ar = chamferDefinition.array();

                    EntityCollection iCollection =
                        Chisel.EntityCollection((short)Obj3dType.o3d_edge);
                    iCollection.SelectByPoint(0, 0.5 * chiselData.Length, -0.5 * chiselData.Width);
                    ar.Add(iCollection.Last());
                    iCollection = Chisel.EntityCollection((short)Obj3dType.o3d_edge);
                    iCollection.SelectByPoint(0, 0.5 * chiselData.Length, 0.5 * chiselData.Width);
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
                    chamferDefinition.SetChamferParam(true, 0.5 * chiselData.Height * 0.7662626,
                        chiselData.BladeLength);
                    var ar = chamferDefinition.array();

                    EntityCollection iCollection = 
                        Chisel.EntityCollection((short)Obj3dType.o3d_edge);
                    iCollection.SelectByPoint(0.5 * chiselData.Height, 0.5 * chiselData.Length, 0);
                    ar.Add(iCollection.Last());
                    iCollection = Chisel.EntityCollection((short)Obj3dType.o3d_edge);
                    iCollection.SelectByPoint(-0.5 * chiselData.Height, 0.5 * chiselData.Length, 0);
                    ar.Add(iCollection.Last());

                    iChamfer3.Create();
                }
            }
            ksEntity iChamfer4 = (ksEntity)Chisel.NewEntity((short)Obj3dType.o3d_chamfer);

            if (iChamfer4 != null)
            {
                ksChamferDefinition chamferDefinition =
                    (ksChamferDefinition)iChamfer4.GetDefinition();

                if (chamferDefinition != null)
                {
                    chamferDefinition.tangent = true;
                    chamferDefinition.SetChamferParam(false, chiselData.BladeLength / 40,
                        (0.7 / 3) * 0.5 * chiselData.Height);
                    var ar = chamferDefinition.array();

                    EntityCollection iCollection =
                        Chisel.EntityCollection((short)Obj3dType.o3d_edge);

                    iCollection.SelectByPoint((0.5 * chiselData.Height)
                        - (0.5 * chiselData.Height * 0.7662626), 0.5 * chiselData.Length, 0);
                    ar.Add(iCollection.Last());
                    iCollection = Chisel.EntityCollection((short)Obj3dType.o3d_edge);
                    iCollection.SelectByPoint(-0.5 * chiselData.Height
                        + (0.5 * chiselData.Height * 0.7662626), 0.5 * chiselData.Length, 0);
                    ar.Add(iCollection.Last());

                    iChamfer4.Create();
                }
            }
        }

        /// <summary>
        /// функция построения скругления зубила пики
        /// </summary>
        /// <param name="chiselData"></param>
        private void HandleFilletPeak(ChiselData chiselData)
        {
            var Chisel = _kompasApp.Chisel;
            /// Создание скругления
            ksEntity iFillet = (ksEntity)Chisel.NewEntity((short)Obj3dType.o3d_fillet);
            if (iFillet != null)
            {
                ksFilletDefinition filletDefenition = (ksFilletDefinition)iFillet.GetDefinition();
                if (filletDefenition != null)
                {
                    filletDefenition.radius = 0.5 * chiselData.Height / 3;
                    filletDefenition.tangent = true;
                    var araa = filletDefenition.array();

                    //сзади
                    EntityCollection iCollection =
                        Chisel.EntityCollection((short)Obj3dType.o3d_face);
                    iCollection.SelectByPoint(1, -0.5 * chiselData.Length, 1);
                    araa.Add(iCollection.Last());
                    iFillet.Create();
                }
            }
        }

        /// <summary>
        /// функция построения основания слесарного зубила
        /// </summary>
        /// <param name="chiselData"></param>
        private void TopLocksmith(ChiselData chiselData)
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

                    CalculateTopSketchParamethersLocksmith(chiselData, iDocument2D);

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

        /// <summary>
        /// функция построения внутреннего выреза слесарного зубила
        /// </summary>
        /// <param name="chiselData"></param>
        private void InnerCutoutLocksmith(ChiselData chiselData)
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

                    CalculateInnerCotoutSketchParamethersLocksmith(chiselData, iDocument2D);

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
        
       /// <summary>
       /// функция построения фаски лезвия слесарного зубила
       /// </summary>
       /// <param name="chiselData"></param>
        private void BladeChamferLocksmith(ChiselData chiselData)
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
      
        /// <summary>
        /// сборщик скруглений слесарного зубила
        /// </summary>
        /// <param name="chiselData"></param>
        private void HandleFilletLocksmith(ChiselData chiselData)
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

