//-------------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2017 Tasharen Entertainment Inc
//-------------------------------------------------

using UnityEngine;
using UnityEditor;

/// <summary>
/// Inspector class used to edit UISpriteAnimations.
/// </summary>

[CanEditMultipleObjects]
[CustomEditor(typeof(UISpriteAnimation))]
public class UISpriteAnimationInspector : Editor
{
    /// <summary>
    /// Draw the inspector widget.
    /// </summary>

    public override void OnInspectorGUI()
    {
        GUILayout.Space(3f);
        NGUIEditorTools.DrawSeparator();
        NGUIEditorTools.SetLabelWidth(80f);
        serializedObject.Update();

        UISpriteAnimation anim = target as UISpriteAnimation;

        int fps = EditorGUILayout.IntField("Framerate", anim.framesPerSecond);
        fps = Mathf.Clamp(fps, 0, 60);

        if (anim.framesPerSecond != fps)
        {
            NGUIEditorTools.RegisterUndo("Sprite Animation Change", anim);
            anim.framesPerSecond = fps;
            EditorUtility.SetDirty(anim);
        }

        string namePrefix = EditorGUILayout.TextField("Name Prefix", (anim.namePrefix != null) ? anim.namePrefix : "");

        if (anim.namePrefix != namePrefix)
        {
            NGUIEditorTools.RegisterUndo("Sprite Animation Change", anim);
            anim.namePrefix = namePrefix;
            EditorUtility.SetDirty(anim);
        }

        bool loop = EditorGUILayout.Toggle("Loop", anim.loop);

        if (anim.loop != loop)
        {
            NGUIEditorTools.RegisterUndo("Sprite Animation Change", anim);
            anim.loop = loop;
            EditorUtility.SetDirty(anim);
        }

        bool snap = EditorGUILayout.Toggle("Snap", anim.snap);

        if (anim.snap != snap)
        {
            NGUIEditorTools.RegisterUndo("Sprite Animation Change", anim);
            anim.snap = snap;
            EditorUtility.SetDirty(anim);
        }

        int loopOffset = EditorGUILayout.IntField("LoopOffset", anim.loopOffset);

        if (anim.loopOffset != loopOffset)
        {
            NGUIEditorTools.RegisterUndo("Sprite Animation Change", anim);
            anim.loopOffset = loopOffset;
            EditorUtility.SetDirty(anim);
        }

        int startFrame = EditorGUILayout.IntField("StartFrame", anim.startFrame);

        if (anim.startFrame != startFrame)
        {
            NGUIEditorTools.RegisterUndo("Sprite Animation Change", anim);
            anim.startFrame = startFrame;
            EditorUtility.SetDirty(anim);
        }

        int endFrame = EditorGUILayout.IntField("EndFrame", anim.endFrame);

        if (anim.endFrame != endFrame)
        {
            NGUIEditorTools.RegisterUndo("Sprite Animation Change", anim);
            anim.endFrame = endFrame;
            EditorUtility.SetDirty(anim);
        }


        float delay = EditorGUILayout.FloatField("StartDelay", anim.startDelay);

        if (anim.startDelay != delay)
        {
            NGUIEditorTools.RegisterUndo("Sprite Animation Change", anim);
            anim.startDelay = delay;
            EditorUtility.SetDirty(anim);
        }
        serializedObject.ApplyModifiedProperties();
    }
}