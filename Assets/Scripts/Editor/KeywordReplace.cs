using System.Collections;
using UnityEditor;
using UnityEngine;

public class KeywordReplace : UnityEditor.AssetModificationProcessor {

	public static void OnWillCreateAsset(string path) {
		path = path.Replace(".meta", "");
		int index = path.LastIndexOf(".");
		if (index < 0) { return; }
		string fileType = path.Substring(index);
		if (fileType != ".cs" && fileType != ".js" && fileType != ".boo") { return; }
		index = Application.dataPath.LastIndexOf("Assets");
		path = Application.dataPath.Substring(0, index) + path;
		string file = System.IO.File.ReadAllText(path);

		file = file.Replace("#CREATIONDATE#", System.DateTime.Now + "");
		file = file.Replace("#PROJECTNAME#", GetProjectName());
		file = file.Replace("#SMARTDEVELOPERS#", PlayerSettings.companyName);

		System.IO.File.WriteAllText(path, file);
		AssetDatabase.Refresh();
	}

	public static string GetProjectName() {
		string[] s = Application.dataPath.Split('/');
		string projectName = s[s.Length - 2];
		projectName = projectName.Replace(" ", "");
		return projectName;
	}
}