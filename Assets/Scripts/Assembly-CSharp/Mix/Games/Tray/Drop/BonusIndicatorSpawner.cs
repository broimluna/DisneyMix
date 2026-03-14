using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mix.Games.Tray.Drop
{
	public class BonusIndicatorSpawner : MonoBehaviour
	{
		public BonusIndicator BonusIndicatorPrefab;

		public int BonusIndicatorPoolSize;

		private List<BonusIndicator> bonusIndicatorPool;

		private DropGame game;

		private void Awake()
		{
			game = DropGame.Instance;
			bonusIndicatorPool = new List<BonusIndicator>();
			for (int i = 0; i < BonusIndicatorPoolSize; i++)
			{
				BonusIndicator bonusIndicator = UnityEngine.Object.Instantiate(BonusIndicatorPrefab);
				bonusIndicator.transform.SetParent(base.transform, false);
				bonusIndicator.GameCamera = game.GameController.MixGameCamera;
				bonusIndicator.gameObject.SetActive(false);
				bonusIndicatorPool.Add(bonusIndicator);
			}
		}

		private void OnEnable()
		{
			DropGame dropGame = game;
			dropGame.OnEarnBonus = (Action)Delegate.Combine(dropGame.OnEarnBonus, new Action(OnBonusEarned));
		}

		private void OnDisable()
		{
			DropGame dropGame = game;
			dropGame.OnEarnBonus = (Action)Delegate.Remove(dropGame.OnEarnBonus, new Action(OnBonusEarned));
		}

		public void OnBonusEarned()
		{
			BonusIndicator bonusIndicatorFromPool = GetBonusIndicatorFromPool();
			bonusIndicatorFromPool.Show(game.Player.transform.position);
		}

		public BonusIndicator GetBonusIndicatorFromPool()
		{
			BonusIndicator bonusIndicator = null;
			for (int i = 0; i < bonusIndicatorPool.Count; i++)
			{
				if (!bonusIndicatorPool[i].gameObject.activeSelf)
				{
					bonusIndicator = bonusIndicatorPool[i];
					break;
				}
			}
			if (bonusIndicator == null)
			{
				bonusIndicator = UnityEngine.Object.Instantiate(BonusIndicatorPrefab);
				bonusIndicator.transform.SetParent(base.transform, false);
				bonusIndicatorPool.Add(bonusIndicator);
			}
			return bonusIndicator;
		}
	}
}
