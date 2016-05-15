﻿using TgcViewer.Utils.TgcSceneLoader;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using TgcViewer.Utils.TgcGeometry;
using TgcViewer.Utils.TgcSkeletalAnimation;
using TgcViewer;
using System.Drawing;
using System.IO;
using System.Collections.Generic;

namespace AlumnoEjemplos.NatusVincere
{
    public class ObjectsFactory
    {
        private TgcMesh arbolMesh;
        private TgcMesh pinoMesh;
        private TgcMesh piedraMesh;
        private TgcMesh hachaMesh;
        private TgcMesh maderaMesh;
        private TgcMesh fogataMesh;
        private List<Crafteable> objectList;
        int objectId = 0;


        TgcSkeletalLoader skeletalLoader = new TgcSkeletalLoader();
        string skeletalPath = GuiController.Instance.ExamplesMediaDir + "SkeletalAnimations\\BasicHuman\\";
        string[] animationsPath;
        Microsoft.DirectX.Direct3D.Device d3dDevice = GuiController.Instance.D3dDevice;
        DirectoryInfo dirAnim;
        FileInfo[] animFiles;
        string[] animationList;

        public ObjectsFactory(List<Crafteable> objectList)
        {
            this.objectList = objectList;

            TgcSceneLoader loader = new TgcSceneLoader();
            Microsoft.DirectX.Direct3D.Device d3dDevice = GuiController.Instance.D3dDevice;

            TgcScene arbolScene = loader.loadSceneFromFile(System.Environment.CurrentDirectory + @"\AlumnoEjemplos\NatusVincere\ArbolSelvatico\ArbolSelvatico-TgcScene.xml");
            this.arbolMesh = arbolScene.Meshes[0];

            TgcScene pinoScene = loader.loadSceneFromFile(System.Environment.CurrentDirectory + @"\AlumnoEjemplos\NatusVincere\Pino\Pino-TgcScene.xml");
            this.pinoMesh = pinoScene.Meshes[0];

            TgcScene piedraScene = loader.loadSceneFromFile(System.Environment.CurrentDirectory + @"\AlumnoEjemplos\NatusVincere\Roca\Roca-TgcScene.xml");
            this.piedraMesh = piedraScene.Meshes[0];

            TgcScene hachaScene = loader.loadSceneFromFile(System.Environment.CurrentDirectory + @"\AlumnoEjemplos\NatusVincere\Hacha\Hacha-TgcScene.xml");
            this.hachaMesh = hachaScene.Meshes[0];

            TgcScene maderaScene = loader.loadSceneFromFile(System.Environment.CurrentDirectory + @"\AlumnoEjemplos\NatusVincere\Madera\Madera-TgcScene.xml");
            this.maderaMesh = maderaScene.Meshes[0];

            TgcScene fogataScene = loader.loadSceneFromFile(System.Environment.CurrentDirectory + @"\AlumnoEjemplos\NatusVincere\Fogata\Fogata-TgcScene.xml");
            this.fogataMesh = maderaScene.Meshes[0];

            dirAnim = new DirectoryInfo(skeletalPath + "Animations\\");
            animFiles = dirAnim.GetFiles("*-TgcSkeletalAnim.xml", SearchOption.TopDirectoryOnly);
            animationList = new string[animFiles.Length];
            animationsPath = new string[animFiles.Length];

            for (int i = 0; i < animFiles.Length; i++)
            {
                string name = animFiles[i].Name.Replace("-TgcSkeletalAnim.xml", "");
                animationList[i] = name;
                animationsPath[i] = animFiles[i].FullName;
            }
        }

        public Human createHuman(Vector3 position, Vector3 scale)
        {
            Inventory inventory = new Inventory(this, new Vector2(20, 20));
            TgcSkeletalMesh humanMesh;
            humanMesh = skeletalLoader.loadMeshAndAnimationsFromFile(skeletalPath + "WomanJeans-TgcSkeletalMesh.xml", skeletalPath, animationsPath);
            humanMesh.buildSkletonMesh();
            return new Human(inventory, humanMesh, position, scale);
        }

        public Arbol createArbol(Vector3 position, Vector3 scale)
        {
            objectId++;
            TgcMesh meshInstance = this.arbolMesh.createMeshInstance("arbol_" + objectId);
            Arbol arbol = new Arbol(meshInstance, position, scale);
            this.objectList.Add(arbol);
            return arbol;
        }

        public Pino createPino(Vector3 position, Vector3 scale)
        {
            objectId++;
            TgcMesh meshInstance = this.pinoMesh.createMeshInstance("pino_" + objectId);
            Pino pino = new Pino(meshInstance, position, scale);
            this.objectList.Add(pino);
            return pino;
        }

        public Piedra createPiedra(Vector3 position, Vector3 scale)
        {
            objectId++;
            TgcMesh meshInstance = this.piedraMesh.createMeshInstance("piedra_" + objectId);
            Piedra piedra = new Piedra(meshInstance, position, scale);
            this.objectList.Add(piedra);
            return piedra;
        }

        public Hacha createHacha(Vector3 position, Vector3 scale)
        {
            objectId++;
            TgcMesh meshInstance = this.hachaMesh.createMeshInstance("hacha_" + objectId);
            Hacha hacha = new Hacha(meshInstance, position, scale);
            this.objectList.Add(hacha);
            return hacha;
        }

        public Madera createMadera(Vector3 position, Vector3 scale)
        {
            objectId++;
            TgcMesh meshInstance = this.maderaMesh.createMeshInstance("madera_" + objectId);
            Madera madera = new Madera(meshInstance, position, scale);
            this.objectList.Add(madera);
            return madera;
        }

        public Fogata createFogata(Vector3 position, Vector3 scale)
        {
            objectId++;
            TgcMesh meshInstance = this.fogataMesh.createMeshInstance("fogata_" + objectId);
            Fogata fogata = new Fogata(meshInstance, position, scale);
            this.objectList.Add(fogata);
            return fogata;
        }

        public void transform(Crafteable crafteable)
        {
            if (crafteable.getType() == 1 && crafteable.getStatus() == 1)
            {
                this.createMadera(crafteable.getPosition(), new Vector3(1, 1, 1));
                crafteable.destroy();
                return;
            }

        }

        public void dispose()
        {
            arbolMesh.dispose();
            piedraMesh.dispose();
            hachaMesh.dispose();
            pinoMesh.dispose();        }
    }
}
