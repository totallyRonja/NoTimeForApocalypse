using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class InkleDialogue {
    JSONNode data;

    JSONNode activeNode;

    public InkleDialogue(string jsonFile){
        data = JSON.Parse(jsonFile);
    }

    public string StartDialogue(){
        string initialName = data["data"]["initial"].Value;
        activeNode = data["data"]["stitches"][initialName]["content"];
        return activeNode[0].Value;
    }

	public bool IsQuestion(){
        return !activeNode[1]["divert"].IsString;
    }

	public InkleOption[] getOptions(){
		if(!IsQuestion()){
            Debug.LogError("not a Question");
            return null;
        }
        InkleOption[] options = new InkleOption[activeNode.Count-2];
        for (int i = 0; i < activeNode.Count-2;i++){
            options[i] = new InkleOption(activeNode[i+1], data);
        }
        return options;
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