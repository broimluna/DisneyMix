using DG.Tweening.Core.Enums;
using DG.Tweening.Plugins.Core;

namespace DG.Tweening.Core
{
	public class TweenerCore<T1, T2, TPlugOptions> : Tweener where TPlugOptions : struct
	{
		public T2 startValue;

		public T2 endValue;

		public T2 changeValue;

		public TPlugOptions plugOptions;

		public DOGetter<T1> getter;

		public DOSetter<T1> setter;

		internal ABSTweenPlugin<T1, T2, TPlugOptions> tweenPlugin;

		private const string _TxtCantChangeSequencedValues = "You cannot change the values of a tween contained inside a Sequence";

		internal TweenerCore()
		{
			typeofT1 = typeof(T1);
			typeofT2 = typeof(T2);
			typeofTPlugOptions = typeof(TPlugOptions);
			tweenType = TweenType.Tweener;
			Reset();
		}

		internal override Tweener SetFrom(bool relative)
		{
			tweenPlugin.SetFrom(this, relative);
			hasManuallySetStartValue = true;
			return this;
		}

		internal sealed override void Reset()
		{
			base.Reset();
			if (tweenPlugin != null)
			{
				tweenPlugin.Reset(this);
			}
			plugOptions = new TPlugOptions();
			getter = null;
			setter = null;
			hasManuallySetStartValue = false;
			isFromAllowed = true;
		}

		internal override float UpdateDelay(float elapsed)
		{
			return Tweener.DoUpdateDelay(this, elapsed);
		}

		internal override bool Startup()
		{
			return Tweener.DoStartup(this);
		}

		internal override bool ApplyTween(float prevPosition, int prevCompletedLoops, int newCompletedSteps, bool useInversePosition, UpdateMode updateMode, UpdateNotice updateNotice)
		{
			float elapsed = (useInversePosition ? (duration - position) : position);
			if (DOTween.useSafeMode)
			{
				try
				{
					tweenPlugin.EvaluateAndApply(plugOptions, this, isRelative, getter, setter, elapsed, startValue, changeValue, duration, useInversePosition, updateNotice);
				}
				catch
				{
					return true;
				}
			}
			else
			{
				tweenPlugin.EvaluateAndApply(plugOptions, this, isRelative, getter, setter, elapsed, startValue, changeValue, duration, useInversePosition, updateNotice);
			}
			return false;
		}
	}
}
