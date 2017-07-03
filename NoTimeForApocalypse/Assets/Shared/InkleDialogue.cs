using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

[Serializable]
public class InkleDialogue {
    JSONNode data;

    public JSONNode activeNode;

    public InkleDialogue(string jsonFile){
        data = JSON.Parse(jsonFile);
    }

	public string GetText(){
        return activeNode[0].Value;
    }

    public string StartDialogue(){
        string initialName = data["data"]["initial"].Value;
        activeNode = data["data"]["stitches"][initialName]["content"];
        return GetText();
    }

	public string NextDialogue(){
		if(!HasFollowing())
            return null;
        string nextName = activeNode[1]["divert"].Value;
        activeNode = data["data"]["stitches"][nextName]["content"];
        return GetText();
    }

	public bool HasFollowing(){
		return activeNode[1]["divert"].IsString;
	}

	public bool IsQuestion(){
        return activeNode[1]["linkPath"] != null;
    }

	public bool IsEnd(){
		return !(IsQuestion()||(NextDialogue() != null));
	}

	public string[] GetFlags(){
        List<string> flags = new List<string>();
        for (int i = 0; i < activeNode.Count;i++){
			if(activeNode[i]["flagName"] != null && activeNode[i]["flagName"].IsString){
                flags.Add(activeNode[i]["flagName"].Value);
            }
		}
        return flags.ToArray();
    }

	public InkleOption[] getOptions(){
		if(!IsQuestion()){
            Debug.LogError("not a Question: " + activeNode.ToString());
            return null;
        }
        List<InkleOption> options = new List<InkleOption>();
        for (int i = 0; i < activeNode.Count;i++){
			if(activeNode[i]["option"] != null)
           		options.Add(new InkleOption(activeNode[i], data));
        }
        return options.ToArray();
    }
}

public class InkleOption{
    public string text;
    public string[] ifConditions;
    public string[] notIfConditions;
    public JSONNode linkPath;
    public InkleOption(JSONNode node, JSONNode data){
        text = node["option"].Value;

        string linkName = node["linkPath"];
		linkPath = data["data"]["stitches"][linkName]["content"];

        if(node["ifConditions"].IsNull){
            ifConditions = new string[0];
		} else {
            ifConditions = new string[node["ifConditions"].Count];
            for (int i = 0; i < ifConditions.Length;i++){
                ifConditions[i] = node["ifConditions"][i]["ifCondition"].Value;
            }
        }

		if (node["notIfConditions"].IsNull){
            notIfConditions = new string[0];
        }else{
            notIfConditions = new string[node["notIfConditions"].Count];
            for (int i = 0; i < notIfConditions.Length; i++){
                notIfConditions[i] = node["notIfConditions"][i]["notIfCondition"].Value;
            }
        }
    }
}