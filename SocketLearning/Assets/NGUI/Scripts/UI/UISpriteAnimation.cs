using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// NGUI动画播放,prefix前缀填写"idle",会按顺序播放"idle001","idle002","idle003"
/// Modified Author:LiaoXin
/// </summary>

[ExecuteInEditMode]
[RequireComponent(typeof(UISprite))]
[AddComponentMenu("NGUI/UI/Sprite Animation")]
public class UISpriteAnimation : MonoBehaviour
{
	/// <summary>
	/// 当前动画帧的序号
	/// </summary>
	public int frameIndex = 0;
	
	[HideInInspector][SerializeField] protected int mFPS = 12;
	[HideInInspector][SerializeField] protected string mPrefix = "";
	[HideInInspector][SerializeField] protected bool mLoop = true;
	[HideInInspector][SerializeField] protected bool mSnap = false;
	
	protected UISprite mSprite;
	protected float mDelta = 0f;
	protected bool mActive = true;
	protected List<string> mSpriteNames = new List<string>();
	
	[HideInInspector][SerializeField] protected int mStartFrame = 0;
	[HideInInspector][SerializeField] protected int mEndFrame = 0;
	[HideInInspector][SerializeField] protected int mLoopOffset = 0;
	[HideInInspector][SerializeField] protected float mStartDelay = 0;
	protected float start_delay;
	
	
	
	/// <summary>
	/// 每次开始延时多久播放动画，默认不延迟值为0
	/// </summary>
	public float startDelay { get { return mStartDelay; } set { mStartDelay = value; } }
	/// <summary>
	/// 跳帧幅度，默认为0，不跳帧
	/// </summary>
	public int loopOffset { get { return mLoopOffset; } set { mLoopOffset = value; } }
	/// <summary>
	/// 初始帧的序号，默认为0
	/// </summary>
	public int startFrame { get { return mStartFrame; } set { mStartFrame = value; } }
	/// <summary>
	/// 结束帧的序号，默认为这类精灵总数
	/// </summary>
	public int endFrame { get { return mEndFrame; } set { mEndFrame = value; } }
	
	/// <summary>
	/// Number of frames in the animation.
	/// </summary>
	public int frames { get { return mSpriteNames.Count; } }
	
	/// <summary>
	/// Animation framerate.
	/// </summary>
	public int framesPerSecond { get { return mFPS; } set { mFPS = value; } }
	
	/// <summary>
	/// 用于从图集中过滤出需要的
	/// Set the name prefix used to filter sprites from the atlas.
	/// </summary>
	public string namePrefix { get { return mPrefix; } set { if (mPrefix != value) { mPrefix = value; RebuildSpriteList(); } } }
	
	/// <summary>
	/// Set the animation to be looping or not
	/// </summary>
	public bool loop { get { return mLoop; } set { mLoop = value; } }
	
	/// <summary>
	/// 动画时缩放
	/// </summary>
	public bool snap { get { return mSnap; } set { mSnap = value; } }
	
	/// <summary>
	/// Returns is the animation is still playing or not
	/// </summary>
	public bool isPlaying { get { return mActive; } }
	
	/// <summary>
	/// Rebuild the sprite list first thing.
	/// </summary>
	protected virtual void Start () { RebuildSpriteList(); }
	
	/// <summary>
	/// Advance the sprite animation process.
	/// </summary>
	protected virtual void Update ()
	{
		if(start_delay > 0)
		{
			start_delay -= RealTime.deltaTime;
			return;
		}
		if (mActive && mSpriteNames.Count > 1 && Application.isPlaying && mFPS > 0f)
		{
			mDelta += Mathf.Min(1f, RealTime.deltaTime);
			float rate = 1f / mFPS;
			
			if (rate < mDelta)
			{			
				mDelta = (rate > 0f) ? mDelta - rate : 0f;
				
				int _endF = mSpriteNames.Count;
				
				if(endFrame != 0)
				{
					_endF = endFrame+1;
				}
				
				if (++frameIndex >= _endF)
				{
					frameIndex = loopOffset + startFrame;
					mActive = loop;
				}
				
				if (mActive)
				{
					mSprite.spriteName = mSpriteNames[frameIndex];
					if(mSnap) mSprite.MakePixelPerfect();
					
					if(frameIndex == startFrame)
					{
						start_delay = startDelay;
					}
				}
			}
		}
	}
	
	/// <summary>
	/// Rebuild the sprite list after changing the sprite name.
	/// </summary>
	public void RebuildSpriteList ()
	{
		if (mSprite == null) mSprite = GetComponent<UISprite>();
		mSpriteNames.Clear();
		
		if (mSprite != null && mSprite.atlas != null)
		{
			List<UISpriteData> sprites = mSprite.atlas.spriteList;
			
			for (int i = 0, imax = sprites.Count; i < imax; ++i)
			{
				UISpriteData sprite = sprites[i];
				
				if (string.IsNullOrEmpty(mPrefix) || sprite.name.StartsWith(mPrefix))
				{
					mSpriteNames.Add(sprite.name);
				}
			}
			mSpriteNames.Sort();
		}
		
		start_delay = startDelay;
		frameIndex = startFrame;
	}
	
	/// <summary>
	/// Play the animation
	/// </summary>
	public void Play() { mActive = true; }
	
	/// <summary>
	/// Pause the animation
	/// </summary>
	public void Pause() { mActive = false; }
	
	/// <summary>
	/// 重新播放动画
	/// Reset the animation to frame 0 and activate it.
	/// </summary>
	public void Reset()
	{
		mActive = true;
		frameIndex = startFrame;
		
		if (mSprite != null && mSpriteNames.Count > 0)
		{
			mSprite.spriteName = mSpriteNames[frameIndex];
			if(mSnap) mSprite.MakePixelPerfect();
		}
	}
}
