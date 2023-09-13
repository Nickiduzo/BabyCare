using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public class BuildInfoWindow : EditorWindow
{
    private bool needToBuild = false;   // Змінна, що вказує, чи необхідно здійснити збірку проекту
    private Vector2 depScrollPos;   // Поточний стан позиції прокрутки для списку залежностей
    private Vector2 includedAssetsScrollPos;    // Поточний стан позиції прокрутки для списку включених ресурсів
    private Vector2 unusedAssetsScrollPos;  // Поточний стан позиції прокрутки для списку невикористаних ресурсів

    // Змінні для відображення списків ресурсів
    private bool unusedAssetsVisible = true;
    private bool usedAssetsVisible = true;
    private bool dependenciesVisible = false;

    // Відображати ресурси відповідно до вибраного об'єкта в Інспекторі
    private bool adaptToSelection = true;
    private Object[] selectionInProjectView;

    // Парсер для аналізу журналу збірки
    private BuildLogParser buildLogParser = new BuildLogParser();

    // Список невикористаних ресурсів
    private List<AssetData> unusedAssets = new List<AssetData>();

    // Заголовки розділів
    private const string UNUSED_ASSETS_TITLE = "UNUSED ASSETS";
    private const string INCLUDED_ASSETS_TITLE = "INCLUDED ASSETS";
    private const string DEPENDENCIES_TITLE = "INCLUDED DEPENDENCIES";

    // Додано пункт меню для відкриття вікна
    [MenuItem("Window/Build Info")]
    static void Init()
    {
        BuildInfoWindow window = GetWindow<BuildInfoWindow>();
        window.Show();
        window.OnSelectionChange();
    }

    // Викликається при зміні вибору ресурсів в проекті
    private void OnSelectionChange()
    {
        selectionInProjectView = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets);
        Repaint();
    }

    // Відображення графічного інтерфейсу редактора
    private void OnGUI()
    {
        if (needToBuild)
        {
            ShowBuildInfoNotFoundMessage();
            return;
        }

        ShowUpdateButton();
        ShowToggleOptions();

        if (!needToBuild)
        {
            ShowAssetSections();
        }
    }

    // Відображення повідомлення про відсутність інформації про збірку
    private void ShowBuildInfoNotFoundMessage()
    {
        GUI.color = Color.red;
        GUILayout.Label("No build info could be found.\nAre you sure you already built the project?", EditorStyles.boldLabel);
        GUI.color = Color.white;
    }

    // Відображення кнопки оновлення інформації про збірку
    private void ShowUpdateButton()
    {
        if (GUILayout.Button("Update Build Info"))
        {
            LoadBuildInfo();
        }
    }

    // Відображення налаштувань відображення списків ресурсів
    private void ShowToggleOptions()
    {
        EditorGUILayout.BeginHorizontal();
        unusedAssetsVisible = GUILayout.Toggle(unusedAssetsVisible, "Unused assets");
        usedAssetsVisible = GUILayout.Toggle(usedAssetsVisible, "Used assets");
        dependenciesVisible = GUILayout.Toggle(dependenciesVisible, "Dependencies");
        adaptToSelection = GUILayout.Toggle(adaptToSelection, "Filter by selection in inspector");
        EditorGUILayout.EndHorizontal();
    }

    // Відображення списків ресурсів
    private void ShowAssetSections()
    {
        EditorGUILayout.BeginHorizontal();

        if (unusedAssetsVisible)
        {
            ShowAssets(unusedAssets, UNUSED_ASSETS_TITLE, ref unusedAssetsScrollPos);
        }

        if (usedAssetsVisible)
        {
            ShowAssets(buildLogParser.includedAssets, INCLUDED_ASSETS_TITLE, ref includedAssetsScrollPos);
        }

        if (dependenciesVisible)
        {
            ShowDependencies();
        }
        EditorGUILayout.EndHorizontal();
    }

    // Відображення списку ресурів
    private void ShowAssets(List<AssetData> assets, string title, ref Vector2 scrollPos)
    {
        EditorGUILayout.BeginVertical();
        GUILayout.Label(title.ToUpper() + " (" + assets.Count + ")");
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

        foreach (var asset in assets)
        {
            if (!adaptToSelection || IsChildOfSelection(asset))
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.ObjectField(asset.obj, typeof(UnityEngine.Object), false);

                // Відображення розміру ресурсу
                if (!string.IsNullOrEmpty(asset.byteSize) || !string.IsNullOrEmpty(asset.perCentSize))
                {
                    GUILayout.Label($"{asset.byteSize} {asset.perCentSize}", EditorStyles.boldLabel);
                }

                EditorGUILayout.EndHorizontal();
            }
        }

        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
    }

    // Відображення списку залежностей
    private void ShowDependencies()
    {
        EditorGUILayout.BeginVertical();
        depScrollPos = EditorGUILayout.BeginScrollView(depScrollPos);
        GUILayout.Label($"{DEPENDENCIES_TITLE} ({buildLogParser.includedDependencies.Count})");

        foreach (var dependency in buildLogParser.includedDependencies)
        {
            EditorGUILayout.TextField(dependency);
        }

        EditorGUILayout.EndVertical();
        EditorGUILayout.EndScrollView();
    }

    // Перевірка, чи є ресурс дочірнім для вибраного об'єкта в Інспекторі
    private bool IsChildOfSelection(AssetData asset)
    {
        if (selectionInProjectView == null || selectionInProjectView.Length == 0)
        {
            return true;
        }

        return selectionInProjectView.Any(o => asset.path.Contains(AssetDatabase.GetAssetPath(o)));
    }

    // Оновлення інформації про збірку
    private void LoadBuildInfo()
    {
        buildLogParser.Update();

        // Перевірка, чи є включені ресурси
        if (buildLogParser.includedAssets.Count == 0)
        {
            needToBuild = true;
        }
        else
        {
            needToBuild = false;
            UpdateUnusedAssets();
        }
    }

    // Отримання шляхів невикористаних ресурсів
    private List<string> GetUnusedAssetPaths(List<AssetData> includedAssets, List<string> allAssetPaths)
    {
        return allAssetPaths
            .Where(path => System.IO.File.Exists(path) && !includedAssets.Any(a => a.path == path))
            .ToList();
    }

    // Створення даних про невикористані ресурси
    private List<AssetData> CreateUnusedAssetData(List<string> unusedPaths)
    {
        return unusedPaths
            .Select(path => new AssetData(path))
            .ToList();
    }

    // Оновлення списку невикористаних ресурсів
    private void UpdateUnusedAssets()
    {
        unusedAssets.Clear();
        var allAssetPaths = AssetDatabase.GetAllAssetPaths();
        var unusedPaths = GetUnusedAssetPaths(buildLogParser.includedAssets, allAssetPaths.ToList());
        unusedAssets = CreateUnusedAssetData(unusedPaths);
    }
}
