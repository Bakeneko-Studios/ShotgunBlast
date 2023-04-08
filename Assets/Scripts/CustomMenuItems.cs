#if(UNITY_EDITOR)
using UnityEngine;
using UnityEditor;

public static class CustomMenuItems
{
    /*
        how to add a new menu item:
        - copy paste one of the methods + the header (preferably one without comments)
        - things to change:
            - path in the header (where it will show up in the gameobject menu)
            - the name of the void method
            - put the prefab path in Resources.Load<>()
            - (optional) change the text in Undo.RegisterCreatedObjectUndo()
        - pls add a blank line between each one for my OCD
    */

    [MenuItem("GameObject/Interactables/Shotguns/Shotgun")]
    private static void InstantiateShotgun(MenuCommand menuCommand) {
        // Load the prefab from the Resources folder
        GameObject prefab = Resources.Load<GameObject>("Interactables/shotgunpickup"); //if u changed the prefab name u have to change this
        if (prefab == null) {
            Debug.LogError("Prefab not found.");
            return;
        }

        // Create a new instance of the prefab
        GameObject instance = GameObject.Instantiate(prefab);

        // Register the new instance with the undo system and select it
        Undo.RegisterCreatedObjectUndo(instance, "Instantiate shotgunpickup");
        Selection.activeObject = instance;
    }

    [MenuItem("GameObject/Interactables/Cash Drop")]
    private static void InstantiateCashDop(MenuCommand menuCommand) {
        GameObject prefab = Resources.Load<GameObject>("Interactables/cashdrop");
        if (prefab == null) {
            Debug.LogError("Prefab not found.");
            return;
        }
        GameObject instance = GameObject.Instantiate(prefab);
        Undo.RegisterCreatedObjectUndo(instance, "Instantiate cashdrop");
        Selection.activeObject = instance;
    }

    [MenuItem("GameObject/Interactables/Upgrades/Health Pack")]
    private static void InstantiateHealthPack(MenuCommand menuCommand) {
        GameObject prefab = Resources.Load<GameObject>("Interactables/healthpack");
        if (prefab == null) {
            Debug.LogError("Prefab not found.");
            return;
        }
        GameObject instance = GameObject.Instantiate(prefab);
        Undo.RegisterCreatedObjectUndo(instance, "Instantiate healthpack");
        Selection.activeObject = instance;
    }

    [MenuItem("GameObject/Interactables/Upgrades/Max Health Upgrade")]
    private static void InstantiateMaxHealthUpgrade(MenuCommand menuCommand) {
        GameObject prefab = Resources.Load<GameObject>("Interactables/healthupgrade");
        if (prefab == null) {
            Debug.LogError("Prefab not found.");
            return;
        }
        GameObject instance = GameObject.Instantiate(prefab);
        Undo.RegisterCreatedObjectUndo(instance, "Instantiate healthupgrade");
        Selection.activeObject = instance;
    }

    [MenuItem("GameObject/Interactables/Upgrades/Speed Boost")]
    private static void InstantiateSpeedBoost(MenuCommand menuCommand) {
        GameObject prefab = Resources.Load<GameObject>("Interactables/speedboost");
        if (prefab == null) {
            Debug.LogError("Prefab not found.");
            return;
        }
        GameObject instance = GameObject.Instantiate(prefab);
        Undo.RegisterCreatedObjectUndo(instance, "Instantiate speedboost");
        Selection.activeObject = instance;
    }

    [MenuItem("GameObject/Objects/Yeetpad")]
    private static void InstantiateYeetpad(MenuCommand menuCommand) {
        GameObject prefab = Resources.Load<GameObject>("Terrain/yeetpad");
        if (prefab == null) {
            Debug.LogError("Prefab not found.");
            return;
        }
        GameObject instance = GameObject.Instantiate(prefab);
        Undo.RegisterCreatedObjectUndo(instance, "Instantiate yeetpad");
        Selection.activeObject = instance;
    }

    [MenuItem("GameObject/Objects/Trapdoor")]
    private static void InstantiateTrapdoor(MenuCommand menuCommand) {
        GameObject prefab = Resources.Load<GameObject>("Terrain/trapdoor");
        if (prefab == null) {
            Debug.LogError("Prefab not found.");
            return;
        }
        GameObject instance = GameObject.Instantiate(prefab);
        Undo.RegisterCreatedObjectUndo(instance, "Instantiate trapdoor");
        Selection.activeObject = instance;
    }

    [MenuItem("GameObject/Objects/Spike Trap")]
    private static void InstantiateSpikeTrap(MenuCommand menuCommand) {
        GameObject prefab = Resources.Load<GameObject>("Terrain/spiketrap");
        if (prefab == null) {
            Debug.LogError("Prefab not found.");
            return;
        }
        GameObject instance = GameObject.Instantiate(prefab);
        Undo.RegisterCreatedObjectUndo(instance, "Instantiate spiketrap");
        Selection.activeObject = instance;
    }

    [MenuItem("GameObject/Enemies/Lil Ball")]
    private static void InstantiateLilBall(MenuCommand menuCommand) {
        GameObject prefab = Resources.Load<GameObject>("Enemies/LilBall");
        if (prefab == null) {
            Debug.LogError("Prefab not found.");
            return;
        }
        GameObject instance = GameObject.Instantiate(prefab);
        Undo.RegisterCreatedObjectUndo(instance, "Instantiate LilBall");
        Selection.activeObject = instance;
    }

    [MenuItem("GameObject/Enemies/Chainsaw Psycho")]
    private static void InstantiateChainsawPsycho(MenuCommand menuCommand) {
        GameObject prefab = Resources.Load<GameObject>("Enemies/dashing chainsaw psyco");
        if (prefab == null) {
            Debug.LogError("Prefab not found.");
            return;
        }
        GameObject instance = GameObject.Instantiate(prefab);
        Undo.RegisterCreatedObjectUndo(instance, "Instantiate Dashing Chainsaw Psyco");
        Selection.activeObject = instance;
    }

    [MenuItem("GameObject/Enemies/Doomba")]
    private static void InstantiateDoomba(MenuCommand menuCommand) {
        GameObject prefab = Resources.Load<GameObject>("Enemies/Doomba 1");
        if (prefab == null) {
            Debug.LogError("Prefab not found.");
            return;
        }
        GameObject instance = GameObject.Instantiate(prefab);
        Undo.RegisterCreatedObjectUndo(instance, "Instantiate Doomba");
        Selection.activeObject = instance;
    }

    [MenuItem("GameObject/NPCs/Dummy")]
    private static void InstantiateDummy(MenuCommand menuCommand) {
        GameObject prefab = Resources.Load<GameObject>("NPCs/Dummy");
        if (prefab == null) {
            Debug.LogError("Prefab not found.");
            return;
        }
        GameObject instance = GameObject.Instantiate(prefab);
        Undo.RegisterCreatedObjectUndo(instance, "Instantiate Dummy");
        Selection.activeObject = instance;
    }

    [MenuItem("GameObject/NPCs/Shrek")]
    private static void InstantiateShrek(MenuCommand menuCommand) {
        GameObject prefab = Resources.Load<GameObject>("NPCs/npc Shrek");
        if (prefab == null) {
            Debug.LogError("Prefab not found.");
            return;
        }
        GameObject instance = GameObject.Instantiate(prefab);
        Undo.RegisterCreatedObjectUndo(instance, "Instantiate NPC Shrek");
        Selection.activeObject = instance;
    }

    [MenuItem("GameObject/NPCs/Shield")]
    private static void InstantiateShield(MenuCommand menuCommand) {
        GameObject prefab = Resources.Load<GameObject>("LucasScenePrefabs/Test Shield");
        if (prefab == null) {
            Debug.LogError("Prefab not found.");
            return;
        }
        GameObject instance = GameObject.Instantiate(prefab);
        Undo.RegisterCreatedObjectUndo(instance, "Instantiate Shield");
        Selection.activeObject = instance;
    }

    
}
#endif