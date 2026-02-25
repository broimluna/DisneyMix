using UnityEngine;

namespace Mix.Games.Tray.Fireworks
{
	public class FireworkSnowflake : Firework
	{
		public Snowflake Snowflake;

		public int NumSnowflakes;

		[HideInInspector]
		public Snowflake[] mSnowflakes;

		public Sprite[] SnowflakeSprites;

		private void Start()
		{
			mSnowflakes = new Snowflake[NumSnowflakes + 1];
			for (int i = 0; i < NumSnowflakes; i++)
			{
				GameObject gameObject = Object.Instantiate(Snowflake.gameObject);
				gameObject.GetComponent<SpriteRenderer>().sprite = SnowflakeSprites[Random.Range(0, SnowflakeSprites.Length)];
				gameObject.transform.SetParent(base.transform.GetChild(0));
				mSnowflakes[i] = gameObject.GetComponent<Snowflake>();
			}
			mSnowflakes[NumSnowflakes] = Snowflake;
		}

		public Snowflake[] GetSnowflakes()
		{
			return mSnowflakes;
		}

		public override void Explode()
		{
			base.Explode();
			float num = -0.5f;
			Snowflake[] array = mSnowflakes;
			foreach (Snowflake snowflake in array)
			{
				Vector3 onUnitSphere = Random.onUnitSphere;
				onUnitSphere.Set(num, 0.4f + Mathf.Abs(onUnitSphere.y) / 2f, 0f);
				snowflake.transform.localPosition = onUnitSphere;
				snowflake.gameObject.SetActive(true);
				snowflake.Launch(onUnitSphere * 2f);
				num += 0.25f;
			}
		}

		public override void Return()
		{
			base.Return();
			Snowflake[] array = mSnowflakes;
			foreach (Snowflake snowflake in array)
			{
				Vector3 zero = Vector3.zero;
				snowflake.transform.localPosition = zero;
				snowflake.gameObject.SetActive(false);
			}
		}
	}
}
