using System.IO;
using System.Linq;
using UnityEngine;
using UnityEditor;

public static class PrefabUtility
{
	/// <summary>
	/// フルパスを返す
	/// </summary>
	/// <param name="path">Path</param>
	public static string GetFullPath(string path)
	{
		return Path.GetFullPath(path);
	}
	
	/// <summary>
	/// Pathが存在するか調べる
	/// </summary>
	/// <param name="assetPath">path</param>
	public static bool IsExistPath(string assetPath)
	{
		var fullPath = GetFullPath(assetPath);
		var isExist = File.Exists(fullPath) || Directory.Exists(fullPath);
		return isExist;
	}
	
	/// <summary>
	/// Pathがあるか確かめる。あった場合HelpBoxを出し、Trueを返す
	/// </summary>
	public static bool IsExistPathAndHelpBox(string path)
	{
		var isExist = ProjectPathUtility.IsExistPath(path);

		if(isExist == true)
		{
			EditorGUILayout.HelpBox($"{path}は既に存在します。", MessageType.Warning);
			return true;
		}
		return false;
	}

	/// <summary>
	/// 指定されたパスからプレハブを読み込む
	/// </summary>
	/// <param name="path">Path</param>
	public static GameObject[] LoadPrefabsFromPath(string path)
	{
		string[] prefabGuids = AssetDatabase.FindAssets("t:Prefab", new[] { path });
		var prefabs = new GameObject[prefabGuids.Length];

		for (int i = 0; i < prefabGuids.Length; i++)
		{
			string assetPath = AssetDatabase.GUIDToAssetPath(prefabGuids[i]);
			prefabs[i] = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);
		}

		return prefabs;
	}

	/// <summary>
	/// 指定されたパスからプレハブの名前を返す
	/// </summary>
	/// <param name="path">Path</param>
	public static string[] LoadPrefabNamesFromPath(string path)
	{
		return LoadPrefabsFromPath(path).Select(prefab => prefab.name).ToArray();
	}
}
