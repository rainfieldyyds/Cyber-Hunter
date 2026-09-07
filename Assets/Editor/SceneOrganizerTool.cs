#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 场景整理工具（第一阶段）：仅做顶层逻辑分组与容器改名。
/// 不会删除物体、不会改 Prefab、不会修改玩法脚本引用。
/// GameManager 因调用 DontDestroyOnLoad 必须保持为场景根物体，因此不在移动列表内。
/// </summary>
public static class SceneOrganizerTool
{
    private const string SampleScenePath = "Assets/Scenes/SampleScene.unity";

    /// <summary>根物体名 -> 目标分组容器名。</summary>
    private static readonly Dictionary<string, string> GroupMap = new Dictionary<string, string>
    {
        { "avatar", "_Player" },
        { "npc1_0", "_NPC" },
        { "npc1_0 (1)", "_NPC" },
        { "enemys1", "_Enemies" },
        { "enemydieds", "_Enemies" },
        { "enemydieds (1)", "_Enemies" },
        { "enemydieds (2)", "_Enemies" },
        { "boss", "_Boss" },
        { "BossTrigger", "_Boss" },
        { "BossRoom", "_Boss" },
        { "BossDoorCheckPoint", "_Boss" },
        { "kongqiqiang", "_Boss" },
        { "streets1", "_Environment" },
        { "streets2", "_Environment" },
        { "streets3", "_Environment" },
        { "street1", "_Environment" },
        { "background", "_Background" },
        { "DeathZone", "_Triggers" },
        { "suspendtrigger", "_Triggers" },
        { "uitrigger1", "_Triggers" },
        { "uitrigger2", "_Triggers" },
        { "uitrigger3", "_Triggers" },
        { "uitrigger4", "_Triggers" },
        { "uitrigger6", "_Triggers" },
        { "StartCheckpoint", "_Triggers" },
        { "GameStateManager", "_Systems" },
        { "pausemanager", "_UI" },
        { "EventSystem", "_UI" },
        { "uiaudio", "_Audio" },
        { "BgmManager", "_Audio" },
        { "GameBgm", "_Audio" },
        { "SuspendBgm", "_Audio" },
        { "BossBGM", "_Audio" },
        { "Main Camera", "_Cameras" }
    };

    /// <summary>即使出现在 GroupMap 中也必须留在场景根层的物体。</summary>
    private static readonly HashSet<string> KeepAtRoot = new HashSet<string>
    {
        "GameManager"
    };

    /// <summary>容器/纯逻辑物体改名表（这些物体没有脚本，仅供整理层级用）。</summary>
    private static readonly Dictionary<string, string> RenameMap = new Dictionary<string, string>
    {
        { "enemys1", "Enemies_Active" },
        { "enemydieds", "Enemies_Dead" },
        { "enemydieds (1)", "Enemies_Dead_Group1" },
        { "enemydieds (2)", "Enemies_Dead_Group2" }
    };

    [MenuItem("Tools/Scene Organizer/Organize SampleScene (safe, stage 1)")]
    public static void OrganizeSampleSceneFromMenu()
    {
        if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            Debug.Log("Scene Organizer: 已取消（当前场景未保存）。");
            return;
        }

        OrganizeAndLog();
    }

    /// <summary>供批处理调用：Unity.exe -batchmode -quit -executeMethod SceneOrganizerTool.OrganizeSampleSceneBatch</summary>
    public static void OrganizeSampleSceneBatch()
    {
        bool ok = OrganizeAndLog();
        if (!ok)
        {
            EditorApplication.Exit(1);
        }
    }

    private static bool OrganizeAndLog()
    {
        Scene scene = EditorSceneManager.OpenScene(SampleScenePath, OpenSceneMode.Single);
        if (!scene.IsValid())
        {
            Debug.LogError("Scene Organizer: 无法打开 SampleScene。");
            return false;
        }

        GameObject[] roots = scene.GetRootGameObjects();
        int movedCount = 0;
        int renameCount = 0;
        var containers = new Dictionary<string, Transform>();

        foreach (GameObject root in roots)
        {
            if (root == null)
            {
                continue;
            }

            // 记录移动前的原名：改名要在查询分组前完成，避免 Key 失配。
            string name = root.name;
            if (KeepAtRoot.Contains(name))
            {
                continue;
            }

            if (RenameMap.TryGetValue(name, out string newName) && root.name != newName)
            {
                Undo.RegisterCompleteObjectUndo(root, "Rename scene organizer container");
                root.name = newName;
                renameCount++;
            }

            if (GroupMap.TryGetValue(name, out string groupName))
            {
                if (!containers.TryGetValue(groupName, out Transform container))
                {
                    GameObject groupGo = new GameObject(groupName);
                    Undo.RegisterCreatedObjectUndo(groupGo, "Create scene organizer group");
                    container = groupGo.transform;
                    containers[groupName] = container;
                }

                Undo.RegisterCompleteObjectUndo(root.transform, "Reparent scene root");
                root.transform.SetParent(container, true);
                movedCount++;
            }
        }

        // 兜底：若此前已分组，改名目标现在可能是容器内的子物体，递归扫描一遍。
        foreach (GameObject sceneRoot in scene.GetRootGameObjects())
        {
            if (sceneRoot != null)
            {
                renameCount += RenameChildrenIfNeeded(sceneRoot.transform);
            }
        }

        EditorSceneManager.MarkSceneDirty(scene);
        EditorSceneManager.SaveScene(scene);

        Debug.Log($"Scene Organizer: 完成。移动根物体 {movedCount} 个，重命名容器 {renameCount} 个。");
        return true;
    }

    private static int RenameChildrenIfNeeded(Transform parent)
    {
        int count = 0;
        foreach (Transform child in parent)
        {
            if (child == null)
            {
                continue;
            }

            if (RenameMap.TryGetValue(child.name, out string newName) && child.name != newName)
            {
                Undo.RegisterCompleteObjectUndo(child.gameObject, "Rename scene organizer container");
                child.name = newName;
                count++;
            }

            count += RenameChildrenIfNeeded(child);
        }

        return count;
    }
}
#endif
