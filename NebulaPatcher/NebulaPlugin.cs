﻿using BepInEx;
using HarmonyLib;
using NebulaModel.Logger;
using NebulaPatcher.Logger;
using NebulaPatcher.MonoBehaviours;
using System;
using UnityEngine;

namespace NebulaPatcher
{
    [BepInPlugin("dsp.nebula-multiplayer", "Nebula - Multiplayer Mod", "0.2.0.0")]
    [BepInDependency("dsp.galactic-scale.2", BepInDependency.DependencyFlags.SoftDependency)] // to load after GS2
    [BepInProcess("DSPGAME.exe")]
    public class NebulaPlugin : BaseUnityPlugin
    {
        Harmony harmony = new Harmony("dsp.nebula-multiplayer");

        void Awake()
        {
            Log.Init(new BepInExLogger(Logger));
            
            NebulaModel.Config.ModInfo = Info;
            NebulaModel.Config.LoadOptions();

            try
            {
                Initialize();
            }
            catch (Exception ex)
            {
                Log.Error("Unhandled exception occurred while initializing Nebula:", ex);
            }
        }

        void Initialize()
        {
            InitPatches();
            AddNebulaBootstrapper();
        }

        private void InitPatches()
        {
            Log.Info("Patching Dyson Sphere Program...");

            try
            {
                var assemblies = AppDomain.CurrentDomain.GetAssemblies();
                foreach (var assembly in assemblies)
                {
                    if (assembly.FullName.StartsWith("NebulaPatcher"))
                    {
                        Log.Info($"Applying patches from assembly: {assembly.FullName}");
                        harmony.PatchAll(assembly);
                    }
                }

                Log.Info("Patching completed successfully");
            }
            catch (Exception ex)
            {
                Log.Error("Unhandled exception occurred while patching the game:", ex);
            }
        }

        void AddNebulaBootstrapper()
        {
            Log.Info("Applying Nebula behaviours..");

            GameObject nebulaRoot = new GameObject();
            nebulaRoot.name = "Nebula Multiplayer Mod";
            nebulaRoot.AddComponent<NebulaBootstrapper>();

            Log.Info("Behaviours applied.");
        }
    }
}
