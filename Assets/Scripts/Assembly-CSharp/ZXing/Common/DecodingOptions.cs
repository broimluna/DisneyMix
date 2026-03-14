using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace ZXing.Common
{
	[Serializable]
	public class DecodingOptions
	{
		[Serializable]
		private class ChangeNotifyDictionary<TKey, TValue> : IEnumerable, IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>
		{
			private readonly IDictionary<TKey, TValue> values;

			public ICollection<TKey> Keys
			{
				get
				{
					return values.Keys;
				}
			}

			public ICollection<TValue> Values
			{
				get
				{
					return values.Values;
				}
			}

			public TValue this[TKey key]
			{
				get
				{
					return values[key];
				}
				set
				{
					values[key] = value;
					OnValueChanged();
				}
			}

			public int Count
			{
				get
				{
					return values.Count;
				}
			}

			public bool IsReadOnly
			{
				get
				{
					return values.IsReadOnly;
				}
			}

			public event Action<object, EventArgs> ValueChanged;

			public ChangeNotifyDictionary()
			{
				values = new Dictionary<TKey, TValue>();
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return ((IEnumerable)values).GetEnumerator();
			}

			private void OnValueChanged()
			{
				if (this.ValueChanged != null)
				{
					this.ValueChanged(this, EventArgs.Empty);
				}
			}

			public void Add(TKey key, TValue value)
			{
				values.Add(key, value);
				OnValueChanged();
			}

			public bool ContainsKey(TKey key)
			{
				return values.ContainsKey(key);
			}

			public bool Remove(TKey key)
			{
				bool result = values.Remove(key);
				OnValueChanged();
				return result;
			}

			public bool TryGetValue(TKey key, out TValue value)
			{
				return values.TryGetValue(key, out value);
			}

			public void Add(KeyValuePair<TKey, TValue> item)
			{
				values.Add(item);
				OnValueChanged();
			}

			public void Clear()
			{
				values.Clear();
				OnValueChanged();
			}

			public bool Contains(KeyValuePair<TKey, TValue> item)
			{
				return values.Contains(item);
			}

			public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
			{
				values.CopyTo(array, arrayIndex);
			}

			public bool Remove(KeyValuePair<TKey, TValue> item)
			{
				bool result = values.Remove(item);
				OnValueChanged();
				return result;
			}

			public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
			{
				return values.GetEnumerator();
			}
		}

		[Browsable(false)]
		public IDictionary<DecodeHintType, object> Hints { get; private set; }

		public bool TryHarder
		{
			get
			{
				if (Hints.ContainsKey(DecodeHintType.TRY_HARDER))
				{
					return (bool)Hints[DecodeHintType.TRY_HARDER];
				}
				return false;
			}
			set
			{
				if (value)
				{
					Hints[DecodeHintType.TRY_HARDER] = true;
				}
				else if (Hints.ContainsKey(DecodeHintType.TRY_HARDER))
				{
					Hints.Remove(DecodeHintType.TRY_HARDER);
				}
			}
		}

		public bool PureBarcode
		{
			get
			{
				if (Hints.ContainsKey(DecodeHintType.PURE_BARCODE))
				{
					return (bool)Hints[DecodeHintType.PURE_BARCODE];
				}
				return false;
			}
			set
			{
				if (value)
				{
					Hints[DecodeHintType.PURE_BARCODE] = true;
				}
				else if (Hints.ContainsKey(DecodeHintType.PURE_BARCODE))
				{
					Hints.Remove(DecodeHintType.PURE_BARCODE);
				}
			}
		}

		public string CharacterSet
		{
			get
			{
				if (Hints.ContainsKey(DecodeHintType.CHARACTER_SET))
				{
					return (string)Hints[DecodeHintType.CHARACTER_SET];
				}
				return null;
			}
			set
			{
				if (value != null)
				{
					Hints[DecodeHintType.CHARACTER_SET] = value;
				}
				else if (Hints.ContainsKey(DecodeHintType.CHARACTER_SET))
				{
					Hints.Remove(DecodeHintType.CHARACTER_SET);
				}
			}
		}

		public IList<BarcodeFormat> PossibleFormats
		{
			get
			{
				if (Hints.ContainsKey(DecodeHintType.POSSIBLE_FORMATS))
				{
					return (IList<BarcodeFormat>)Hints[DecodeHintType.POSSIBLE_FORMATS];
				}
				return null;
			}
			set
			{
				if (value != null)
				{
					Hints[DecodeHintType.POSSIBLE_FORMATS] = value;
				}
				else if (Hints.ContainsKey(DecodeHintType.POSSIBLE_FORMATS))
				{
					Hints.Remove(DecodeHintType.POSSIBLE_FORMATS);
				}
			}
		}

		public bool UseCode39ExtendedMode
		{
			get
			{
				if (Hints.ContainsKey(DecodeHintType.USE_CODE_39_EXTENDED_MODE))
				{
					return (bool)Hints[DecodeHintType.USE_CODE_39_EXTENDED_MODE];
				}
				return false;
			}
			set
			{
				if (value)
				{
					Hints[DecodeHintType.USE_CODE_39_EXTENDED_MODE] = true;
				}
				else if (Hints.ContainsKey(DecodeHintType.USE_CODE_39_EXTENDED_MODE))
				{
					Hints.Remove(DecodeHintType.USE_CODE_39_EXTENDED_MODE);
				}
			}
		}

		public bool UseCode39RelaxedExtendedMode
		{
			get
			{
				if (Hints.ContainsKey(DecodeHintType.RELAXED_CODE_39_EXTENDED_MODE))
				{
					return (bool)Hints[DecodeHintType.RELAXED_CODE_39_EXTENDED_MODE];
				}
				return false;
			}
			set
			{
				if (value)
				{
					Hints[DecodeHintType.RELAXED_CODE_39_EXTENDED_MODE] = true;
				}
				else if (Hints.ContainsKey(DecodeHintType.RELAXED_CODE_39_EXTENDED_MODE))
				{
					Hints.Remove(DecodeHintType.RELAXED_CODE_39_EXTENDED_MODE);
				}
			}
		}

		public bool ReturnCodabarStartEnd
		{
			get
			{
				if (Hints.ContainsKey(DecodeHintType.RETURN_CODABAR_START_END))
				{
					return (bool)Hints[DecodeHintType.RETURN_CODABAR_START_END];
				}
				return false;
			}
			set
			{
				if (value)
				{
					Hints[DecodeHintType.RETURN_CODABAR_START_END] = true;
				}
				else if (Hints.ContainsKey(DecodeHintType.RETURN_CODABAR_START_END))
				{
					Hints.Remove(DecodeHintType.RETURN_CODABAR_START_END);
				}
			}
		}

		[field: NonSerialized]
		public event Action<object, EventArgs> ValueChanged;

		public DecodingOptions()
		{
			ChangeNotifyDictionary<DecodeHintType, object> changeNotifyDictionary = (ChangeNotifyDictionary<DecodeHintType, object>)(Hints = new ChangeNotifyDictionary<DecodeHintType, object>());
			UseCode39ExtendedMode = true;
			UseCode39RelaxedExtendedMode = true;
			changeNotifyDictionary.ValueChanged += delegate
			{
				if (this.ValueChanged != null)
				{
					this.ValueChanged(this, EventArgs.Empty);
				}
			};
		}
	}
}
