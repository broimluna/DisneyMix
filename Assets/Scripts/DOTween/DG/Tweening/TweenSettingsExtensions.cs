using DG.Tweening.Core;
using DG.Tweening.Core.Easing;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DG.Tweening
{
	public static class TweenSettingsExtensions
	{
		public static T SetAutoKill<T>(this T t, bool autoKillOnCompletion) where T : Tween
		{
			if (t == null || !t.active || t.creationLocked)
			{
				return t;
			}
			t.autoKill = autoKillOnCompletion;
			return t;
		}

		public static T SetId<T>(this T t, object id) where T : Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.id = id;
			return t;
		}

		public static T SetTarget<T>(this T t, object target) where T : Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.target = target;
			return t;
		}

		public static T SetLoops<T>(this T t, int loops, LoopType loopType) where T : Tween
		{
			if (t == null || !t.active || t.creationLocked)
			{
				return t;
			}
			if (loops < -1)
			{
				loops = -1;
			}
			else if (loops == 0)
			{
				loops = 1;
			}
			t.loops = loops;
			t.loopType = loopType;
			if (t.tweenType == TweenType.Tweener)
			{
				if (loops > -1)
				{
					t.fullDuration = t.duration * (float)loops;
				}
				else
				{
					t.fullDuration = float.PositiveInfinity;
				}
			}
			return t;
		}

		public static T SetEase<T>(this T t, Ease ease) where T : Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.easeType = ease;
			if (EaseManager.IsFlashEase(ease))
			{
				t.easeOvershootOrAmplitude = (int)t.easeOvershootOrAmplitude;
			}
			t.customEase = null;
			return t;
		}

		public static T SetEase<T>(this T t, AnimationCurve animCurve) where T : Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.easeType = Ease.INTERNAL_Custom;
			t.customEase = new EaseCurve(animCurve).Evaluate;
			return t;
		}

		public static T SetUpdate<T>(this T t, UpdateType updateType, bool isIndependentUpdate) where T : Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			TweenManager.SetUpdateType(t, updateType, isIndependentUpdate);
			return t;
		}

		public static T OnUpdate<T>(this T t, TweenCallback action) where T : Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.onUpdate = action;
			return t;
		}

		public static T OnStepComplete<T>(this T t, TweenCallback action) where T : Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.onStepComplete = action;
			return t;
		}

		public static T OnComplete<T>(this T t, TweenCallback action) where T : Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.onComplete = action;
			return t;
		}

		public static Sequence Append(this Sequence s, Tween t)
		{
			if (s == null || !s.active || s.creationLocked)
			{
				return s;
			}
			if (t == null || !t.active || t.isSequenced)
			{
				return s;
			}
			Sequence.DoInsert(s, t, s.duration);
			return s;
		}

		public static Sequence Join(this Sequence s, Tween t)
		{
			if (s == null || !s.active || s.creationLocked)
			{
				return s;
			}
			if (t == null || !t.active || t.isSequenced)
			{
				return s;
			}
			Sequence.DoInsert(s, t, s.lastTweenInsertTime);
			return s;
		}

		public static Sequence Insert(this Sequence s, float atPosition, Tween t)
		{
			if (s == null || !s.active || s.creationLocked)
			{
				return s;
			}
			if (t == null || !t.active || t.isSequenced)
			{
				return s;
			}
			Sequence.DoInsert(s, t, atPosition);
			return s;
		}

		public static Sequence AppendInterval(this Sequence s, float interval)
		{
			if (s == null || !s.active || s.creationLocked)
			{
				return s;
			}
			Sequence.DoAppendInterval(s, interval);
			return s;
		}

		public static Sequence PrependInterval(this Sequence s, float interval)
		{
			if (s == null || !s.active || s.creationLocked)
			{
				return s;
			}
			Sequence.DoPrependInterval(s, interval);
			return s;
		}

		public static Sequence AppendCallback(this Sequence s, TweenCallback callback)
		{
			if (s == null || !s.active || s.creationLocked)
			{
				return s;
			}
			if (callback == null)
			{
				return s;
			}
			Sequence.DoInsertCallback(s, callback, s.duration);
			return s;
		}

		public static Sequence PrependCallback(this Sequence s, TweenCallback callback)
		{
			if (s == null || !s.active || s.creationLocked)
			{
				return s;
			}
			if (callback == null)
			{
				return s;
			}
			Sequence.DoInsertCallback(s, callback, 0f);
			return s;
		}

		public static Sequence InsertCallback(this Sequence s, float atPosition, TweenCallback callback)
		{
			if (s == null || !s.active || s.creationLocked)
			{
				return s;
			}
			if (callback == null)
			{
				return s;
			}
			Sequence.DoInsertCallback(s, callback, atPosition);
			return s;
		}

		public static T From<T>(this T t) where T : Tweener
		{
			if (t == null || !t.active || t.creationLocked || !t.isFromAllowed)
			{
				return t;
			}
			t.isFrom = true;
			t.SetFrom(false);
			return t;
		}

		public static T SetDelay<T>(this T t, float delay) where T : Tween
		{
			if (t == null || !t.active || t.creationLocked)
			{
				return t;
			}
			if (t.tweenType == TweenType.Sequence)
			{
				(t as Sequence).PrependInterval(delay);
			}
			else
			{
				t.delay = delay;
				t.delayComplete = delay <= 0f;
			}
			return t;
		}

		public static T SetRelative<T>(this T t) where T : Tween
		{
			if (t == null || !t.active || t.creationLocked || t.isFrom || t.isBlendable)
			{
				return t;
			}
			t.isRelative = true;
			return t;
		}

		public static Tweener SetOptions(this TweenerCore<Vector2, Vector2, VectorOptions> t, bool snapping)
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.plugOptions.snapping = snapping;
			return t;
		}

		public static Tweener SetOptions(this TweenerCore<Vector3, Vector3, VectorOptions> t, bool snapping)
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.plugOptions.snapping = snapping;
			return t;
		}

		public static Tweener SetOptions(this TweenerCore<Vector3, Vector3, VectorOptions> t, AxisConstraint axisConstraint, bool snapping = false)
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.plugOptions.axisConstraint = axisConstraint;
			t.plugOptions.snapping = snapping;
			return t;
		}

		public static Tweener SetOptions(this TweenerCore<Color, Color, ColorOptions> t, bool alphaOnly)
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.plugOptions.alphaOnly = alphaOnly;
			return t;
		}

		public static Tweener SetOptions(this TweenerCore<Vector3, Vector3[], Vector3ArrayOptions> t, bool snapping)
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.plugOptions.snapping = snapping;
			return t;
		}
	}
}
